using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Forms;
using Patchwork.AutoPatching;
using Patchwork.Engine.Utility;
using Patchwork.Utility;
using Patchwork.Utility.Binding;
using PatchworkLauncher.Enums;
using PatchworkLauncher.Extensions;
using PatchworkLauncher.Properties;
using Serilog;

namespace PatchworkLauncher
{
	public class LaunchManager
	{
		#region Constructors and Destructors

		static LaunchManager()
		{
			Logger = LogManager.CreateLogger("LaunchManager");
			TxtPathReadme = Path.GetFullPath(PathSettings.Default.Readme);
		}

		public LaunchManager()
		{
			State = Bindable.Variable(LaunchManagerState.Idle);

			// TODO: is this needed on windows?
			// the following is needed on linux... the current directory must be the Mono executable, which is bad.
			string assemblyLocation = Assembly.GetExecutingAssembly().Location;
			string assemblyFolder = Path.GetDirectoryName(assemblyLocation);
			Environment.CurrentDirectory = assemblyFolder;

			// set up patch data - must run before setting up app info instance because BaseFolder needs to be set
			PatchManager.Initialize();

			// set up app info instance - should run before setting up main window because AppInfo.ProgramIcon could be set
			AppContextManager.Setup();

			// set up main window
			this.PerformMainWindowSetup();
		}

		#endregion

		#region Public Properties

		public static string TxtPathReadme { get; }

		public guiHome HomeWindow { get; private set; }

		public Image ProgramIcon { get; set; }

		#endregion

		#region Properties

		private static ILogger Logger { get; }

		private static IBindable<LaunchManagerState> State { get; set; }

		private Icon FormIcon { get; set; }

		#endregion

		#region Public Methods and Operators

		public static void ChangeFolder()
		{
			ShowGameDialog(string.Empty);
		}

		public static void Dispose()
		{
			((IDisposable)Logger).Dispose();
		}

		/// <exception cref="T:System.Security.SecurityException">The caller does not have sufficient security permission to perform this function.</exception>
		public static void ExitApplication()
		{
			if (Application.MessageLoop)
			{
				Application.Exit();
			}
			else
			{
				Environment.Exit(0);
			}
		}

		public static string GetGameFolderWarning()
		{
			if (SettingsManager.BaseFolder == null)
			{
				return "(no game folder has been specified)";
			}

			if (!Directory.Exists(SettingsManager.BaseFolder))
			{
				return "(the previous game folder does not exist)";
			}

			return null;
		}

		public static void Idle()
		{
			if (State.Value != LaunchManagerState.Idle)
			{
				State.Value = LaunchManagerState.Idle;
			}
		}

		/// <exception cref="T:System.ComponentModel.Win32Exception">There was an error in launching the process.</exception>
		public static void LaunchProcess()
		{
			var process = new Process();

			AppInfo appContext = AppContextManager.Context;

			string clientPath = PathSettings.Default.Client;

			if (clientPath.IsNullOrWhitespace() || !File.Exists(clientPath))
			{
				process.Create(appContext.Executable.FullName, Settings.Default.Arguments);
			}
			else
			{
				process.Create(clientPath, clientPath.EndsWithIgnoreCase("Steam.exe") ? appContext.SteamArguments : appContext.GogArguments);
			}

			process.Exited += (sender, args) => State.Value = LaunchManagerState.Idle;

			// state is not set correctly when running a client, so we handle state changes based on user intent elsewhere (e.g., click actions)
			if (process.Start())
			{
				State.Value = LaunchManagerState.GameRunning;
			}
			else
			{
				State.Value = LaunchManagerState.Idle;
			}
		}

		public static bool ShowGameDialog(string message)
		{
			using (var dialog = new guiInputGameFolder(message))
			{
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					SettingsManager.BaseFolder = dialog.Folder.Value;
					return true;
				}

				return false;
			}
		}

		public static void TryOpenReadme()
		{
			if (!File.Exists(TxtPathReadme))
			{
				Logger.Error("Cannot open readme file because the file does not exist");
				return;
			}

			string textFilePath = TxtPathReadme.Quote();

			// windows can "run" text files using the default editor
			if (Environment.OSVersion.Platform == PlatformID.Win32NT)
			{
				Process.Start(textFilePath);
				return;
			}

			string defaultEditor = string.Empty;
			var useDefaultEditor = true;

			try
			{
				defaultEditor = Environment.GetEnvironmentVariable("$EDITOR");
			}
			catch (SecurityException securityException)
			{
				Logger.Error(securityException, "Cannot retrieve $EDITOR environment variable due to security error, trying fallback");
				useDefaultEditor = false;
			}

			try
			{
				Process process = useDefaultEditor && !defaultEditor.IsNullOrWhitespace() ? Process.Start(defaultEditor, textFilePath) : TryOpenFileFallback(textFilePath);
			}
			catch (Exception exception)
			{
				Logger.Error(exception, "Cannot open readme file with either default or fallback editor");
			}
		}

		/// <exception cref="T:PatchworkLauncher.PatchingProcessException">Cannot switch files safely during the patching process</exception>
		public static void TrySwitchFilesSafely(string sourcePath, string destinationPath, string backupPath, PatchGroup patchGroup)
		{
			try
			{
				PatchingHelper.SwitchFilesSafely(sourcePath, destinationPath, backupPath);
			}
			catch (Exception exception)
			{
				throw new PatchingProcessException(exception)
				{
					AssociatedInstruction = null,
					AssociatedPatchGroup = patchGroup,
					Step = PatchProcessingStep.PerformingSwitch
				};
			}
		}

		public async Task LaunchModdedAsync(LaunchType launchType = LaunchType.Patch)
		{
			await this.PatchAsync(launchType).ConfigureAwait(false);
			LaunchProcess();
		}

		public async Task<XmlHistory> PatchAsync(LaunchType launchType)
		{
			State.Value = LaunchManagerState.IsPatching;

			var history = new XmlHistory
			{
				Success = false
			};

			try
			{
				var totalProgress = new ProgressObject();

				using (var logForm = new LogForm(totalProgress))
				{
					logForm.Icon = this.HomeWindow.Icon;

					logForm.Show();

					try
					{
						List<PatchGroup> patches = PatchManager.SaveInstructions(ref history);
						await Task.Run(() => PatchManager.ApplyInstructions(launchType, patches.ToList(), totalProgress)).ConfigureAwait(false);
						history.Success = true;
					}
					catch (PatchingProcessException exception)
					{
						exception.ShowMessageBox(Logger);
					}

					if (!history.Success)
					{
						PatchingHelper.RestorePatchedFiles(AppContextManager.Context, history.Files);
					}

					logForm.Close();
				}
			}
			catch (Exception exception)
			{
				var exceptionMessage = new CustomExceptionMessage
				{
					Message = exception.Message,
					Hint = "Patch the game"
				};

				exception.ShowMessageBox(exceptionMessage, Logger);
			}

			State.Value = LaunchManagerState.Idle;

			return history;
		}

		public guiHome StartHomeWindow()
		{
			this.HomeWindow.ShowOrFocus();
			return this.HomeWindow;
		}

		public async Task TestRunAsync(LaunchType launchType = LaunchType.Test)
		{
			XmlHistory history = await this.PatchAsync(launchType).ConfigureAwait(false);
			PatchingHelper.RestorePatchedFiles(AppContextManager.Context, history.Files);
		}

		#endregion

		#region Methods

		private static Process TryOpenFileFallback(string editorArgument)
		{
			string[] editors = { "notepad", "gedit", "kate", "leafpad", "mousepad", "pluma", "gvim", "nano", "emacs", "vim" };
			return editors.Select(editor => Process.Start(editor, editorArgument)).FirstOrDefault(result => result != null);
		}

		private static Image TryOpenIcon(FileSystemInfo iconFile)
		{
			if (iconFile?.Exists != true)
			{
				return null;
			}

			Image image = null;

			try
			{
				image = Image.FromFile(iconFile.FullName);
			}
			catch
			{
				//must not've been an image file. It's not crucial.
			}

			if (image != null)
			{
				return image;
			}

			Icon icon = Icon.ExtractAssociatedIcon(iconFile.FullName);
			image = icon?.ToBitmap();

			return image;
		}

		private void PerformMainWindowSetup()
		{
			this.FormIcon = Icon.FromHandle(Resources.IconSmall.GetHicon());

			this.HomeWindow = new guiHome(this);
			this.HomeWindow.Icon = this.FormIcon;

			this.ProgramIcon = TryOpenIcon(AppContextManager.Context.IconLocation) ?? this.HomeWindow.Icon?.ToBitmap();
		}

		#endregion
	}
}
