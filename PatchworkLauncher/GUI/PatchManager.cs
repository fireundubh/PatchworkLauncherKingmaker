using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Patchwork.AutoPatching;
using Patchwork.Engine;
using Patchwork.Engine.Utility;
using Patchwork.Utility;
using PatchworkLauncher.Enums;
using PatchworkLauncher.Extensions;
using PatchworkLauncher.FolderBrowserDialogSettings;
using Serilog;

namespace PatchworkLauncher
{
	/// <summary>
	/// Requires <paramref name="SettingsManager"/>
	/// </summary>
	public static class PatchManager
	{
		#region Constructors and Destructors

		static PatchManager()
		{
			FullPath = Path.GetFullPath(SettingsManager.XmlData.ModsPath);
			ManifestCreator = new ManifestCreator();

			Logger = LogManager.CreateLogger("PatchManager");
		}

		#endregion

		#region Properties

		private static IEnumerable<string> Files
		{
			get
			{
				return Directory.GetFiles(FullPath, "*.pw.dll").ToList();
			}
		}

		private static ILogger Logger { get; }

		private static ManifestCreator ManifestCreator { get; }

		private static string FullPath { get; }

		#endregion

		#region Public Methods and Operators

		public static List<PatchGroup> SaveInstructions(ref XmlHistory history)
		{
			List<PatchGroup> patches = GroupXmlInstructions(SettingsManager.XmlData.Instructions);

			// save to history
			history.Files = patches.Select(XmlFileHistory.FromInstrGroup).ToList();
			HistoryManager.History = history;

			return patches;
		}

		/// <exception cref="T:Patchwork.Engine.PatchDeclerationException">The patch did not have a PatchInfo class.</exception>
		public static PatchingManifest TryGetManifest(string assemblyPath)
		{
			PatchingManifest patchingManifest = ManifestCreator.CreateManifest(assemblyPath);

			if (patchingManifest.PatchInfo == null)
			{
				throw new PatchDeclerationException("The patch did not have a PatchInfo class.");
			}

			return patchingManifest;
		}

		private static void SetTaskData(this ProgressObject progress, string taskTitle = "", string taskText = "", int total = -1, bool increment = false)
		{
			if (!string.IsNullOrEmpty(taskTitle))
			{
				progress.TaskTitle.Value = taskTitle;
			}

			if (!string.IsNullOrEmpty(taskText))
			{
				progress.TaskText.Value = taskText;
			}

			if (total > -1)
			{
				progress.Total.Value = total;
			}

			if (increment)
			{
				progress.Current.Value++;
			}
		}

		public static void ApplyInstructions(LaunchType launchType, List<PatchGroup> patchGroups, ProgressObject totalProgress)
		{
			//TODO: Use a different progress tracking system and make the entire patching operation more recoverable and fault-tolerant.
			//TODO: Refactor this method.
			AppInfo appInfo = AppContextManager.Context;

			Assembly myAttributesAssembly = typeof(AppInfo).Assembly;
			string myAttributesAssemblyName = Path.GetFileName(myAttributesAssembly.Location);

			var fileProgress = new ProgressObject();
			var patchProgress = new ProgressObject();

			totalProgress.Child.Value = fileProgress;
			fileProgress.Child.Value = patchProgress;

			totalProgress.SetTaskData("Patching Game", appInfo.AppName, patchGroups.Count);

			foreach (PatchGroup patchGroup in patchGroups)
			{
				int patchCount = patchGroup.Instructions.Count;

				string destinationPath = patchGroup.TargetPath;

				var patcher = new AssemblyPatcher(destinationPath, Logger);
				patcher.EmbedHistory = true;

				string sourcePath = PatchingHelper.GetBackupForModified(destinationPath);
				string backupPath = PatchingHelper.GetBackupForOriginal(destinationPath);

				// note that Path.Combine(FILENAME, "..", OTHER_FILENAME) doesn't work on Mono but does work on .NET.
				string targetDirectory = Path.GetDirectoryName(destinationPath);

				string localAssemblyName = Path.Combine(targetDirectory, myAttributesAssemblyName);

				totalProgress.SetTaskData(string.Format("Patching {0}", appInfo.AppName), Path.GetFileName(destinationPath));

				fileProgress.SetTaskData("Patching File", total: 2 + patchCount, increment: true);
				fileProgress.SetTaskData(taskText: "Applying Patch");

				if (!PatchingHelper.DoesFileMatchPatchList(sourcePath, destinationPath, patchGroup.Instructions) || PreferencesManager.Preferences.AlwaysPatch)
				{
					try
					{
						myAttributesAssembly.TryCopyAttributesAssembly(localAssemblyName);
					}
					catch (Exception exception)
					{
						Logger.Warning(exception, "Failed to read local attributes assembly so it will be overwritten.");
					}

					foreach (PatchInstruction patch in patchGroup.Instructions)
					{
						try
						{
							patcher.TryPatchManifest(patch, patchGroup, patchProgress);
						}
						catch (PatchingProcessException exception)
						{
							Logger.Show(exception);
						}

						fileProgress.SetTaskData(increment: true);
					}

					patchProgress.SetTaskData(string.Empty, string.Empty);

					fileProgress.SetTaskData(taskText: "Writing Assembly", increment: true);

					if (launchType == LaunchType.Test && Environment.OSVersion.Platform == PlatformID.Win32NT)
					{
						fileProgress.SetTaskData(taskText: "Running PEVerify");

						string peOutput = patcher.TryRunPeVerify(appInfo, destinationPath);

						Logger.Information(peOutput);
					}

					try
					{
						patcher.TryBackup(sourcePath, patchGroup);
					}
					catch (PatchingProcessException exception)
					{
						Logger.Show(exception);
					}
				}
				else
				{
					fileProgress.Current.Value += patchCount;
				}

				try
				{
					LaunchManager.TrySwitchFilesSafely(sourcePath, destinationPath, backupPath, patchGroup);
				}
				catch (PatchingProcessException exception)
				{
					Logger.Show(exception);
				}

				AssemblyCache.Default.ClearCache();

				totalProgress.SetTaskData(increment: true);
			}
		}

		public static void Dispose()
		{
			((IDisposable) Logger)?.Dispose();
		}

		public static void Initialize()
		{
			// we need to run setup here so we can combine the registry path with the executable filename
			AppContextManager.Setup();

			// try to get game path from xml settings
			string gamePath = SettingsManager.XmlData.GamePath;

			if (!File.Exists(gamePath))
			{
				// try to get game path from windows registry
				if (Environment.OSVersion.Platform == PlatformID.Win32NT)
				{
					if (string.IsNullOrEmpty(gamePath) || !Directory.Exists(gamePath))
					{
						gamePath = SettingsManager.GetGamePathFromRegistry();
						SettingsManager.XmlData.GamePath = gamePath;
					}
				}

				// try to get game path from user
				if (string.IsNullOrEmpty(gamePath) || !Directory.Exists(gamePath))
				{
					DialogResult result = AppContextManager.AskPath(new GameFolderBrowserDialogSettings
					{
						Description = "Select folder containing game executable"
					});

					if (result == DialogResult.Cancel)
					{
						LaunchManager.Exit();
					}
				}
			}

			AddPatchesFromFolder();
		}

		#endregion

		#region Methods

		private static List<PatchGroup> GroupXmlInstructions(IEnumerable<XmlInstruction> instructions)
		{
			var patchInstructions = new Dictionary<string, List<PatchInstruction>>();

			foreach (XmlInstruction instruction in instructions)
			{
				using (PatchInstruction tmpInstruction = instruction.ToPatchInstruction())
				{
					TryAddPatchInstruction(tmpInstruction, patchInstructions);
				}
			}

			IEnumerable<PatchGroup> groups = patchInstructions.Select(patchInstruction => new PatchGroup
			{
				Instructions = patchInstruction.Value,
				TargetPath = patchInstruction.Key
			});

			return groups.ToList();
		}

		private static void AddPatchesFromFolder()
		{
			List<XmlInstruction> instructions = SettingsManager.XmlData.Instructions;

			foreach (string assemblyPath in Files)
			{
				XmlInstruction collision = instructions.SingleOrDefault(x => x.Location.EqualsIgnoreCase(assemblyPath));

				if (collision != null)
				{
					Logger.Debug("Patch already loaded: {0}", assemblyPath);

					continue;
				}

				Logger.Debug("Loading patch: {0}", assemblyPath);

				TryAddPatch(assemblyPath);
			}
		}

		private static void AddXmlInstruction(string assemblyPath, PatchingManifest patchingManifest, bool enabled = true)
		{
			List<XmlInstruction> instructions = SettingsManager.XmlData.Instructions;

			Logger.Debug("Adding patch instruction: {0}", assemblyPath);

			var xmlInstruction = new XmlInstruction
			{
				IsEnabled = true,
				Name = patchingManifest.PatchInfo.PatchName,
				Location = assemblyPath
			};

			instructions.Add(xmlInstruction);

			SettingsManager.XmlData.Instructions = instructions;
		}

		private static void TryAddPatch(string assemblyPath)
		{
			var manifest = new PatchingManifest();

			try
			{
				manifest = TryGetManifest(assemblyPath);
			}
			catch (PatchDeclerationException patchDeclerationException)
			{
				Logger.Error(patchDeclerationException, "Cannot load patch assembly: {0}", assemblyPath);
				manifest?.Dispose();
				return;
			}

			AddXmlInstruction(assemblyPath, manifest);
		}

		/// <exception cref="T:PatchworkLauncher.PatchingProcessException">Throws an exception when adding an instruction during the patching process</exception>
		private static void TryAddPatchInstruction(PatchInstruction instruction, Dictionary<string, List<PatchInstruction>> instructions)
		{
			string targetFile = string.Empty;

			try
			{
				IPatchInfo patchInfo = instruction.Patch.PatchInfo;
				string canPatch = patchInfo.CanPatch(AppContextManager.Context);

				if (canPatch != null)
				{
					throw new PatchExecutionException(canPatch);
				}

				targetFile = patchInfo.GetTargetFile(AppContextManager.Context).FullName;

				if (!instructions.ContainsKey(targetFile))
				{
					instructions[targetFile] = new List<PatchInstruction>();
				}

				instructions[targetFile].Add(instruction);
			}
			catch (Exception exception)
			{
				throw new PatchingProcessException(exception)
				{
					AssociatedInstruction = instruction,
					TargetFile = targetFile,
					Step = PatchProcessingStep.Grouping
				};
			}
		}

		#endregion
	}
}
