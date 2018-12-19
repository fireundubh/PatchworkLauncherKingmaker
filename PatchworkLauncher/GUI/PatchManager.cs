using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Patchwork.AutoPatching;
using Patchwork.Engine;
using Patchwork.Engine.Utility;
using Patchwork.Utility;
using PatchworkLauncher.Enums;
using PatchworkLauncher.Extensions;
using PatchworkLauncher.Properties;
using Serilog;

namespace PatchworkLauncher
{
	public static class PatchManager
	{
		#region Constructors and Destructors

		static PatchManager()
		{
			FullPath = Path.GetFullPath(PathSettings.Default.Mods);
			ManifestCreator = new ManifestCreator();

			Logger = LogManager.CreateLogger("PatchManager");
		}

		#endregion

		#region Properties

		private static List<string> Files
		{
			get
			{
				return Directory.GetFiles(FullPath, "*.pw.dll").ToList();
			}
		}

		private static string FullPath { get; }

		private static ILogger Logger { get; }

		private static ManifestCreator ManifestCreator { get; }

		#endregion

		#region Public Methods and Operators

		public static void ApplyInstructions(LaunchType launchType, List<PatchGroup> patchGroups, ProgressObject totalProgress)
		{
			//TODO: Use a different progress tracking system and make the entire patching operation more recoverable and fault-tolerant.
			//TODO: Refactor this method.
			AppInfo appInfo = AppContextManager.Context;

			var fileProgress = new ProgressObject();
			totalProgress.Child.Value = fileProgress;

			var patchProgress = new ProgressObject();
			fileProgress.Child.Value = patchProgress;

			Assembly myAttributesAssembly = typeof(AppInfo).Assembly;
			string myAttributesAssemblyName = Path.GetFileName(myAttributesAssembly.Location);

			totalProgress.TaskTitle.Value = "Patching Game";
			totalProgress.TaskText.Value = appInfo.AppName;
			totalProgress.Total.Value = patchGroups.Count;

			foreach (PatchGroup patchGroup in patchGroups)
			{
				int patchCount = patchGroup.Instructions.Count;

				string destinationPath = patchGroup.TargetPath;

				totalProgress.TaskTitle.Value = string.Format("Patching {0}", appInfo.AppName);
				totalProgress.TaskText.Value = Path.GetFileName(destinationPath);

				// Note that Path.Combine(FILENAME, "..", OTHER_FILENAME) doesn't work on Mono but does work on .NET.
				string targetDirectory = Path.GetDirectoryName(destinationPath);

				string localAssemblyName = Path.Combine(targetDirectory, myAttributesAssemblyName);

				fileProgress.TaskTitle.Value = "Patching File";
				fileProgress.Total.Value = 2 + patchCount;
				fileProgress.Current.Value++;

				string sourcePath = PatchingHelper.GetBackupForModified(destinationPath);
				string backupPath = PatchingHelper.GetBackupForOriginal(destinationPath);

				fileProgress.TaskText.Value = "Applying Patch";

				if (!PatchingHelper.DoesFileMatchPatchList(sourcePath, destinationPath, patchGroup.Instructions) || PreferencesManager.Preferences.AlwaysPatch)
				{
					myAttributesAssembly.TryToCopyAttributesAssembly(localAssemblyName, Logger);

					var patcher = new AssemblyPatcher(destinationPath, Logger)
					{
						EmbedHistory = true
					};

					foreach (PatchInstruction patch in patchGroup.Instructions)
					{
						try
						{
							patcher.TryPatchManifest(patch, patchGroup, patchProgress);
						}
						catch (PatchingProcessException exception)
						{
							exception.ShowMessageBox(Logger);
						}

						fileProgress.Current.Value++;
					}

					patchProgress.TaskText.Value = string.Empty;
					patchProgress.TaskTitle.Value = string.Empty;

					fileProgress.Current.Value++;
					fileProgress.TaskText.Value = "Writing Assembly";

					if (launchType == LaunchType.Test && Environment.OSVersion.Platform == PlatformID.Win32NT)
					{
						fileProgress.TaskText.Value = "Running PEVerify";
						patcher.TryRunPeVerify(appInfo, destinationPath, Logger);
					}

					try
					{
						patcher.TryBackup(sourcePath, patchGroup);
					}
					catch (PatchingProcessException exception)
					{
						exception.ShowMessageBox(Logger);
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
					exception.ShowMessageBox(Logger);
				}

				AssemblyCache.Default.ClearCache();

				totalProgress.Current.Value++;
			}
		}

		public static void Dispose()
		{
			((IDisposable)Logger).Dispose();
		}

		public static void Initialize()
		{
			string gamePath = SettingsManager.SetGamePathFromRegistry();

			if (gamePath.IsNullOrWhitespace())
			{
				SettingsManager.SetGamePathFallback();
			}

			AddPatchesFromFolder();
		}

		public static List<PatchGroup> SaveInstructions(ref XmlHistory history)
		{
			List<PatchGroup> patches = GroupPatches(SettingsManager.Instructions).ToList();

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

		#endregion

		#region Methods

		private static void AddPatchesFromFolder()
		{
			List<XmlInstruction> instructions = SettingsManager.Instructions;

			foreach (string assemblyPath in Files)
			{
				XmlInstruction collision = instructions.SingleOrDefault(x => x.PatchLocation.EqualsIgnoreCase(assemblyPath));

				if (collision != null)
				{
					Logger.Error("Patch already loaded: {0}", assemblyPath);
					continue;
				}

				Logger.Information("Loading patch: {0}", assemblyPath);

				TryAddPatch(assemblyPath);
			}
		}

		private static void AddPatchInstruction(string assemblyPath, PatchingManifest patchingManifest, bool enabled = true)
		{
			List<XmlInstruction> instructions = SettingsManager.Instructions;

			var patchInstruction = new PatchInstruction
			{
				IsEnabled = enabled,
				Patch = patchingManifest,
				PatchLocation = assemblyPath,
				AppInfo = AppContextManager.Context
			};

			Logger.Information("Adding patch instruction: {0}", patchInstruction.PatchLocation);

			var xmlInstruction = new XmlInstruction
			{
				IsEnabled = true,
				Name = patchInstruction.Name,
				PatchLocation = patchInstruction.PatchLocation
			};

			instructions.Add(xmlInstruction);
			SettingsManager.Instructions = instructions;
		}

		private static IEnumerable<PatchGroup> GroupPatches<T>(IEnumerable<T> instructions) where T : IInstruction
		{
			var patchInstructions = new Dictionary<string, List<PatchInstruction>>();

			foreach (T instruction in instructions)
			{
				PatchInstruction patchInstruction = (instruction as XmlInstruction)?.ToPatchInstruction() ?? instruction as PatchInstruction;
				TryAddPatchInstruction(patchInstruction, patchInstructions);
			}

			IEnumerable<PatchGroup> groups = patchInstructions.Select(patchInstruction => new PatchGroup
			{
				Instructions = patchInstruction.Value,
				TargetPath = patchInstruction.Key
			});

			return groups.ToList();
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
				manifest.Dispose();
				return;
			}

			AddPatchInstruction(assemblyPath, manifest);
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
