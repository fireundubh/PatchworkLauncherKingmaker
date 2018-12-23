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

		public static Process GameProcess { get; set; }

		public static string TxtPathReadme { get; }

		public static MainWindow MainWindow { get; private set; }

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

		public static void Idle()
		{
			if (State.Value != LaunchManagerState.Idle)
			{
				State.Value = LaunchManagerState.Idle;
			}
		}

		/// <exception cref="T:System.ComponentModel.Win32Exception">There was an error in launching the process.</exception>
		public static void LaunchProcess(Client clientType)
		{
			// this is disposed when launching a new process or closing the launcher
			GameProcess = new Process();

			string clientPath = SettingsManager.XmlData.ClientPath;

			if (clientPath.IsNullOrWhitespace() || !File.Exists(clientPath))
			{
				string executableName = AppContextManager.Context.Value.Executable.FullName;

				GameProcess.Configure(executableName, SettingsManager.XmlData.Arguments);
			}
			else
			{
				string arguments = string.Empty;

				switch (clientType)
				{
					case Client.Galaxy:
						arguments = AppContextManager.Context.Value.GogArguments;
						break;
					case Client.Steam:
						arguments = AppContextManager.Context.Value.SteamArguments;
						break;
				}

				GameProcess.Configure(clientPath, arguments);
			}

			GameProcess.Exited += (sender, args) => State.Value = LaunchManagerState.Idle;

			// state is not set correctly when running a client, so we handle state changes based on user intent elsewhere (e.g., click actions)
			if (GameProcess.Start())
			{
				State.Value = LaunchManagerState.GameRunning;
			}
			else
			{
				State.Value = LaunchManagerState.Idle;
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

			Process process;

			switch (Environment.OSVersion.Platform)
			{
				// windows can "run" text files using the default editor
				case PlatformID.Win32NT:
					process = Process.Start(textFilePath);
					break;
				default:
					process = TryOpenReadmeLinux(textFilePath);
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
			MainWindow.Enabled = false;

			await this.PatchAsync(launchType).ConfigureAwait(false);
			this.AskUnlockGUI(true);

			LaunchProcess(MainWindow.ClientType);
		}

		public async Task LaunchTestRunAsync(LaunchType launchType = LaunchType.Test)
		{
			MainWindow.Enabled = false;

			XmlHistory history = await this.PatchAsync(launchType).ConfigureAwait(false);
			this.AskUnlockGUI();

			PatchingHelper.RestorePatchedFiles(AppContextManager.Context.Value, history.Files);
		}

		public async Task<XmlHistory> PatchAsync(LaunchType launchType)
		{
			State.Value = LaunchManagerState.IsPatching;

			var history = new XmlHistory();
			history.Success = false;

			try
			{
				var totalProgress = new ProgressObject();

				using (var logForm = new LogForm(totalProgress))
				{
					logForm.Icon = MainWindow.Icon;

					logForm.Show();

					try
					{
						List<PatchGroup> patches = PatchManager.SaveInstructions(ref history);
						await Task.Run(() => PatchManager.ApplyInstructions(launchType, patches.ToList(), totalProgress)).ConfigureAwait(false);
						history.Success = true;
					}
					catch (PatchingProcessException exception)
					{
						Logger.Show(exception);
					}

					if (!history.Success)
					{
						PatchingHelper.RestorePatchedFiles(AppContextManager.Context.Value, history.Files);
					}

					logForm.Close();
				}
			}
			catch (Exception exception)
			{
				var exceptionMessage = new PatchingExceptionMessage
				{
					Hint = "Patch the game",
					Message = exception.Message
				};

				Logger.Show(exceptionMessage);
			}

			State.Value = LaunchManagerState.Idle;

			return history;
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

		private static Process TryOpenReadmeLinux(string textFilePath)
		{
			bool useDefaultEditor;
			string defaultEditor = GetDefaultEditor(out useDefaultEditor);

			Process process = null;

			try
			{
				if (useDefaultEditor && !string.IsNullOrEmpty(defaultEditor))
				{
					process = Process.Start(defaultEditor, textFilePath);
				}
				else if (!useDefaultEditor)
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

		private void AskUnlockGUI(bool runningGame = false)
		{
			string message;

			if (runningGame)
			{
				if (GameProcess != null)
				{
					string fileName = Path.GetFileName(GameProcess.StartInfo.FileName);
					message = string.Format(Resources.UnlockRunningFormat, Environment.NewLine, Environment.NewLine, fileName, GameProcess.Id);
				}
				else
				{
					message = Resources.UnlockRunningText;
				}
			}
			else
			{
				message = Resources.UnlockRunningText;
			}

			using (var messageBox = new UnlockMessageBox(message))
			{
				DialogResult result = messageBox.ShowDialog(MainWindow);

				if (result == DialogResult.OK)
				{
					MainWindow.Enabled = true;
					MainWindow.ShowOrFocus();
				}
			}
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
