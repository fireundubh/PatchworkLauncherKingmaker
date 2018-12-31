using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using Patchwork;
using PatchworkLauncher.Enums;
using PatchworkLauncher.Extensions;
using PatchworkLauncher.FolderBrowserDialogSettings;
using PatchworkLauncher.Properties;

namespace PatchworkLauncher
{
	public partial class MainWindow : Form
	{
		#region Constructors and Destructors

		public MainWindow(LaunchManager manager)
		{
			this.InitializeComponent();

			this.Manager = manager;
		}

		#endregion

		#region Public Properties

		public Client ClientType { get; set; } = Client.None;

		public LaunchManager Manager { get; }

		public PictureBox ClientIcon
		{
			get
			{
				return this.pbClientIcon;
			}
			set
			{
				this.pbClientIcon = value;
			}
		}

		#endregion

		#region Methods

		private static bool AskDirectLaunchPermission()
		{
			if (PreferencesManager.IgnoreNoClientWarning)
			{
				return false;
			}

			if (!string.IsNullOrEmpty(SettingsManager.XmlData.ClientPath))
			{
				return false;
			}

			DialogResult result = MessageBox.Show(Resources.DirectLaunchWarning, Resources.ConfigurationWarning, MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Warning);

			switch (result)
			{
				case DialogResult.Abort:
					return true;
				case DialogResult.Retry:
					break;
				case DialogResult.Ignore:
					PreferencesManager.IgnoreNoClientWarning = true;
					break;
			}

			return false;
		}

		private static Client SetClientType()
		{
			if (string.IsNullOrEmpty(SettingsManager.XmlData.ClientPath))
			{
				return Client.None;
			}

			if (SettingsManager.XmlData.ClientPath.EndsWithIgnoreCase("Steam.exe"))
			{
				return Client.Steam;
			}

			return Client.Galaxy;
		}

		private static void GoToURL(string url)
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.Win32NT:
					Process.Start(url)?.Dispose();
					break;
				case PlatformID.MacOSX:
					Process.Start("open", url)?.Dispose();
					break;
				case PlatformID.Unix:
					Process.Start("xdg-open", url)?.Dispose();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private static void ResetData()
		{
			LaunchManager.GameProcess = null;
			SettingsManager.InvalidateXmlData();
			HistoryManager.RestorePatchedFiles();
			HistoryManager.Delete();
			LaunchManager.Idle();
		}

		private async Task btnLaunchWithMods_Click(object sender, EventArgs e)
		{
			if (AskDirectLaunchPermission())
			{
				return;
			}

			if (SettingsManager.XmlData.Instructions.Count != 0)
			{
				await this.Manager.LaunchModdedAsync().ConfigureAwait(false);
				return;
			}

			MessageBox.Show(Resources.NoActiveMods, Resources.ConfigurationError, MessageBoxButtons.OK);
		}

		private void btnClearArguments_Click(object sender, EventArgs e)
		{
			this.tbArguments.Clear();
		}

		private void btnClientPath_Click(object sender, EventArgs e)
		{
			DialogResult result = AppContextManager.AskPath(new ClientFolderBrowserDialogSettings { Description = Resources.SelectClientFolder });

			if (result == DialogResult.OK)
			{
				LaunchManager.SetClientIcon();
				this.ShowOrFocus();
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			LaunchManager.Exit();
		}

		private void btnGamePath_Click(object sender, EventArgs e)
		{
			DialogResult result = AppContextManager.AskPath(new GameFolderBrowserDialogSettings
			{
				Description = string.Format(Resources.SelectExecutableFolderFormat, AppContextManager.Context.Value.Executable.Name)
			});

			if (result == DialogResult.OK)
			{
				LaunchManager.SetClientIcon();
				this.ShowOrFocus();
			}
		}

		private void btnHelp_Click(object sender, EventArgs e)
		{
			LaunchManager.TryOpenTextFile(LaunchManager.TxtPathReadme);
		}

		private void btnLaunchNoMods_Click(object sender, EventArgs e)
		{
			LaunchManager.LaunchProcess(this.ClientType);
		}

		private void btnPatreon_Click(object sender, EventArgs e)
		{
			GoToURL("https://www.patreon.com/fireundubh");
		}

		private void btnPayPal_Click(object sender, EventArgs e)
		{
			GoToURL("https://www.nexusmods.com/Core/Libs/Common/Widgets/PayPalPopUp?user=513981");
		}

		private void btnTestRun_Click(object sender, EventArgs e)
		{
			DialogResult result = MessageBox.Show("Do you want to open the log after the test run?", "Preferences", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
			PreferencesManager.OpenLogAfterPatch = result == DialogResult.Yes;

			if (SettingsManager.XmlData.Instructions.Count != 0)
			{
				this.Manager.LaunchTestRunAsync().ConfigureAwait(false);
				return;
			}

			MessageBox.Show(Resources.NoActiveMods, Resources.ConfigurationError, MessageBoxButtons.OK);
		}

		private void guiHome_FormClosed(object sender, FormClosedEventArgs e)
		{
			// delete empty logs
			LogManager.DeleteEmptyFiles();
		}

		private void guiHome_FormClosing(object sender, FormClosingEventArgs e)
		{
			LaunchManager.GameProcess?.Dispose();

			// reset
			SettingsManager.InvalidateXmlData();
			HistoryManager.RestorePatchedFiles();
			HistoryManager.Delete();

			// dispose loggers so we can delete empty logs
			AppContextManager.Dispose();
			SettingsManager.Dispose();
			HistoryManager.Dispose();
			LaunchManager.Dispose();
			PatchManager.Dispose();
			PreferencesManager.Dispose();
		}

		private void guiHome_Load(object sender, EventArgs e)
		{
			this.ControlBox = false;

			this.guiGameIcon.Image = LaunchManager.ProgramIcon;
			this.guiGameIcon.Refresh();
			this.guiPwVersion.Text = string.Format(Resources.PatchworkVersionFormat, PatchworkInfo.Version);
			this.guiGameName.Text = AppContextManager.Context.Value.AppName;
			this.guiGameVersion.Text = string.Format(Resources.AppInfoVersionFormat, AppContextManager.Context.Value.AppVersion);

			this.tbArguments.Text = SettingsManager.XmlData.Arguments;

			// currently used only by LaunchManager.LaunchProcess as argument
			this.ClientType = SetClientType();

			// async click handlers
			this.btnLaunchNoMods.Click += (o, args) =>
			                              {
				                              ResetData();
											  this.btnLaunchNoMods_Click(o, args);
			                              };

			this.btnLaunchWithMods.Click += async (o, args) =>
			                                {
				                                ResetData();
												await this.btnLaunchWithMods_Click(o, args).ConfigureAwait(false);
			                                };

			this.btnTestRun.Click += (o, args) =>
			                         {
				                         ResetData();
				                         this.btnTestRun_Click(o, args);
			                         };
		}

		private void guiHome_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.ReleaseCapture();
			}
		}

		private void lblPatchwork_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process process = Process.Start(PatchworkInfo.PatchworkSite);
			process?.Dispose();
		}

		private void tbArguments_TextChanged(object sender, EventArgs e)
		{
			SettingsManager.XmlData.Arguments = this.tbArguments.Text;
		}

		#endregion
	}
}
