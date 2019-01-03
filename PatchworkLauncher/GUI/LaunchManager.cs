using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Patchwork.Engine.Utility;
using Patchwork.Utility;
using Patchwork.Utility.Binding;
using PatchworkLauncher.Enums;
using PatchworkLauncher.Extensions;
using PatchworkLauncher.Properties;
using Serilog;

namespace PatchworkLauncher
{
	/// <summary>
	/// Requires <paramref name="SettingsManager"/>
	/// </summary>
	public class LaunchManager
	{
		#region Constructors and Destructors

		static LaunchManager()
		{
			// cannot initialize logger without SettingsManager initializing first
			// because the Logs directory needs to be deserialized
			Logger = LogManager.CreateLogger("LaunchManager");

			TxtPathReadme = Path.GetFullPath(SettingsManager.XmlSettings.Launcher.Files.Readme);
		}

		public LaunchManager()
		{
			this.Initialize();

			State = Bindable.Variable(LaunchManagerState.Idle);

			// TODO: is this needed on windows?
			// the following is needed on linux... the current directory must be the Mono executable, which is bad.
			string assemblyLocation = Assembly.GetExecutingAssembly().Location;
			string assemblyFolder = Path.GetDirectoryName(assemblyLocation);
			Environment.CurrentDirectory = assemblyFolder;

			// also sets up AppContextManager
			PatchManager.Initialize();

			// TODO: could just Thread.Sleep instead of calling it again, but LiteDB migration is on roadmap, so whatever
			AppContextManager.Setup();
		}

		#endregion

		#region Public Properties

		public static Image ProgramIcon { get; private set; }

		public static MainWindow MainWindow { get; private set; }

		public static Process GameProcess { get; set; }

		public static string TxtPathReadme { get; }

		#endregion

		#region Properties

		private static IBindable<LaunchManagerState> State { get; set; }

		private static Icon FormIcon { get; set; }

		private static ILogger Logger { get; set; }

		#endregion

		#region Public Methods and Operators

		public static void Dispose()
		{
			((IDisposable) Logger)?.Dispose();
			FormIcon?.Dispose();
			ProgramIcon?.Dispose();
		}

		/// <exception cref="T:System.Security.SecurityException">The caller does not have sufficient security permission to perform this function.</exception>
		public static void Exit()
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

		/// <exception cref="T:System.ComponentModel.Win32Exception">There was an error in launching the process.</exception>
		public static void LaunchProcess(ClientType clientType)
		{
			// this is disposed when launching a new process or closing the launcher
			GameProcess = new Process();

			string clientPath = SettingsManager.XmlData.ClientPath;

			if (clientPath.IsNullOrWhitespace() || !File.Exists(clientPath))
			{
				GameProcess.Configure(AppContextManager.Context.Value.Executable.FullName, SettingsManager.XmlData.Arguments);
			}
			else
			{
				string arguments = string.Empty;

				// ReSharper disable once SwitchStatementMissingSomeCases
				switch (clientType)
				{
					case ClientType.Galaxy:
						arguments = AppContextManager.Context.Value.GogArguments;
						break;
					case ClientType.Steam:
						arguments = AppContextManager.Context.Value.SteamArguments;
						break;
				}

				GameProcess.Configure(clientPath, arguments);
			}

			GameProcess.Exited += (o, args) => SetState(LaunchManagerState.Idle);

			// state is not set correctly when running a client, so we handle state changes based on user intent elsewhere (e.g., click actions)
			if (GameProcess.Start())
			{
				SetState(LaunchManagerState.GameRunning);
				return;
			}

			SetState(LaunchManagerState.Idle);
		}

		public static void SetClientIcon()
		{
			if (AppContextManager.Context.Value == null)
			{
				Logger.Error("Context.Value was null. Cannot set client icon.");
				return;
			}

			if (string.IsNullOrEmpty(SettingsManager.XmlData.GamePath))
			{
				Logger.Error("XmlData.GamePath was null or empty. Cannot set client icon.");
				return;
			}

			string clientPath = SettingsManager.XmlData.ClientPath;

			string iconPath = string.IsNullOrEmpty(clientPath) ? AppContextManager.Context.Value.Executable.FullName : clientPath;

			if (string.IsNullOrEmpty(iconPath))
			{
				return;
			}

			// we don't really care about the icon, so let's ignore any exceptions
			Image iconImage;

			try
			{
				iconImage = TryOpenIcon(new FileInfo(iconPath));
			}
			catch (Exception exception)
			{
				Logger.Error(exception, "Cannot load client icon.");
				return;
			}

			// MainWindow can be null if LaunchManager.Initialize() is not called first in constructor
			// because the MainWindow property needs to be initialized before SetClientIcon is called
			using (iconImage)
			{
				try
				{
					MainWindow.ClientIcon.Image = new Bitmap(iconImage, 19, 19);
					MainWindow.ClientIcon.Refresh();
				}
				catch (Exception exception)
				{
					Logger.Error(exception, "An exception occurred while setting the client icon image.");
				}
			}
		}

		public static void SetState(LaunchManagerState state)
		{
			if (State.Value != state)
			{
				State.Value = state;
			}
		}

		public static void TryOpenTextFile(string path)
		{
			if (!File.Exists(path))
			{
				Logger.Error("Cannot open readme file because the file does not exist");
				return;
			}

			string textFilePath = path.Quote();

			Process process;

			switch (Environment.OSVersion.Platform)
			{
				// windows can "run" text files using the default editor
				case PlatformID.Win32NT:
					process = Process.Start(textFilePath);
					break;
				default:
					process = TryOpenTextFileLinux(textFilePath);
					break;
			}

			process?.Dispose();
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

		public MainWindow StartHomeWindow()
		{
			MainWindow.ShowOrFocus();
			return MainWindow;
		}

		/// <exception cref="T:System.ComponentModel.Win32Exception">There was an error in launching the process.</exception>
		public async Task LaunchModdedAsync(LaunchType launchType = LaunchType.Patch)
		{
			StartUnlockDialog(true);

			SetState(LaunchManagerState.IsPatching);

			await this.PatchAsync(launchType).ConfigureAwait(false);

			SetState(LaunchManagerState.Idle);

			LaunchProcess(MainWindow.ClientType);
		}

		public async Task LaunchTestRunAsync(LaunchType launchType = LaunchType.Test)
		{
			StartUnlockDialog();

			SetState(LaunchManagerState.IsPatching);

			XmlHistory history = await this.PatchAsync(launchType).ConfigureAwait(false);

			SetState(LaunchManagerState.Idle);

			PatchingHelper.RestorePatchedFiles(AppContextManager.Context.Value, history.Files);

			if (!PreferencesManager.OpenLogAfterPatch)
			{
				return;
			}

			try
			{
				string logPath = LogManager.Logs.First(kvp => kvp.Key == "PatchManager").Value;

				TryOpenTextFile(logPath);
			}
			catch (Exception exception)
			{
				Logger.Error(exception, "Cannot open log file after test run");
			}
		}

		public async Task<XmlHistory> PatchAsync(LaunchType launchType)
		{
			var history = new XmlHistory();
			history.Success = false;

			var totalProgress = new ProgressObject();

			var logForm = new LogForm(totalProgress);

			logForm.InvokeIfRequired(() => logForm.Show());

			try
			{
				List<PatchGroup> patches = PatchManager.SaveInstructions(ref history);

				await Task.Run(() => PatchManager.ApplyInstructions(launchType, patches, totalProgress)).ConfigureAwait(false);

				history.Success = true;
			}
			catch (Exception exception)
			{
				Logger.Error(exception, "Patching failed because instructions cannot be applied.");
			}

			if (!history.Success)
			{
				PatchingHelper.RestorePatchedFiles(AppContextManager.Context.Value, history.Files);
			}

			logForm.InvokeIfRequired(() => CloseForm(logForm));

			return history;
		}

		#endregion

		#region Methods

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
				// ignored
			}

			if (image != null)
			{
				return image;
			}

			using (Icon icon = Icon.ExtractAssociatedIcon(iconFile.FullName))
			{
				if (icon != null)
				{
					image = icon.ToBitmap();
				}
			}

			return image;
		}

		private static Process TryOpenFileFallback(string editorArgument)
		{
			string[] editors = { "notepad", "gedit", "kate", "leafpad", "mousepad", "pluma", "gvim", "nano", "emacs", "vim" };
			return editors.Select(editor => Process.Start(editor, editorArgument)).FirstOrDefault(result => result != null);
		}

		private static Process TryOpenTextFileLinux(string textFilePath)
		{
			bool useDefaultEditor;
			string defaultEditor = GetDefaultEditor(out useDefaultEditor);

			Process process = null;

			try
			{
				if (useDefaultEditor)
				{
					if (!string.IsNullOrEmpty(defaultEditor))
					{
						process = Process.Start(defaultEditor, textFilePath);
					}
				}
				else
				{
					process = TryOpenFileFallback(textFilePath);
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, "Cannot open readme file with either default or fallback editor");
			}

			return process;
		}

		private static string GetDefaultEditor(out bool useDefaultEditor)
		{
			string defaultEditor = string.Empty;

			try
			{
				defaultEditor = Environment.GetEnvironmentVariable("$EDITOR");
				useDefaultEditor = true;
			}
			catch (SecurityException securityException)
			{
				Logger.Error(securityException, "Cannot retrieve $EDITOR environment variable due to security error, trying fallback");
				useDefaultEditor = false;
			}

			return defaultEditor;
		}

		private static void AskUnlockGUI(bool runningGame = false)
		{
			string message = runningGame ? Resources.UnlockRunningText : Resources.UnlockTestRunText;

			using (var messageBox = new UnlockMessageBox(message))
			{
				DialogResult result = MainWindow.InvokeDialogIfRequired(messageBox);

				if (result != DialogResult.OK)
				{
					Logger.Fatal("User abnormally terminated lock. Exiting.");
					Exit();
				}

				MainWindow.InvokeIfRequired(delegate
				                            {
												MainWindow.ResetData();

					                            MainWindow.Enabled = true;
					                            MainWindow.ShowOrFocus();
				                            });
			}
		}

		private static void CloseForm(LogForm logForm)
		{
			logForm?.Close();
			logForm?.Dispose();
		}

		private static void StartUnlockDialog(bool runningGame = false)
		{
			MainWindow.Enabled = false;
			new Thread(o => AskUnlockGUI(runningGame)).Start();
		}

		private void Initialize()
		{
			IntPtr iconHandle = Resources.IconSmall.GetHicon();
			FormIcon = Icon.FromHandle(iconHandle);

			MainWindow = new MainWindow(this);
			MainWindow.Icon = FormIcon;

			ProgramIcon = TryOpenIcon(AppContextManager.Context.Value.IconLocation) ?? MainWindow.Icon?.ToBitmap();
		}

		#endregion
	}
}
