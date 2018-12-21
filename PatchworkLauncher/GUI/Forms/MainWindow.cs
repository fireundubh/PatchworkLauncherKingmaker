using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Patchwork;
using PatchworkLauncher.Enums;
using PatchworkLauncher.Extensions;
using PatchworkLauncher.FolderBrowserDialogSettings;

namespace PatchworkLauncher
{
	public partial class MainWindow : Form
	{
		#region Constructors and Destructors

		public MainWindow(LaunchManager manager)
		{
			this.Manager = manager;
			this.InitializeComponent();
		}

		#endregion

		#region Public Properties

		public Client ClientType { get; set; } = Client.None;

		public LaunchManager Manager { get; }

		#endregion

		#region Methods

		private static Icon ExtractIcon()
		{
			string clientPath = SettingsManager.XmlData.ClientPath;

			string iconPath = string.IsNullOrEmpty(clientPath) ? AppContextManager.Context.Executable.FullName : clientPath;

			return Icon.ExtractAssociatedIcon(iconPath);
		}

		private static void ResetData()
		{
			LaunchManager.GameProcess?.Dispose();
			SettingsManager.InvalidateXmlData();
			HistoryManager.RestorePatchedFiles();
			HistoryManager.Delete();
			LaunchManager.Idle();
		}

		private async Task btnLaunchWithMods_Click(object sender, EventArgs e)
		{
			if (SettingsManager.XmlData.Instructions.Count != 0)
			{
				await this.Manager.LaunchModdedAsync().ConfigureAwait(false);
				return;
			}

			MessageBox.Show("No active mods configured.", "Configuration Error", MessageBoxButtons.OK);
		}

		private async Task btnTestRun_Click(object sender, EventArgs e)
		{
			if (SettingsManager.XmlData.Instructions.Count != 0)
			{
				await this.Manager.LaunchTestRunAsync().ConfigureAwait(false);
				return;
			}

			MessageBox.Show("No active mods configured.", "Configuration Error", MessageBoxButtons.OK);
		}

		private void btnClearArguments_Click(object sender, EventArgs e)
		{
			this.tbArguments.Clear();
		}

		private void btnClientPath_Click(object sender, EventArgs e)
		{
			DialogResult result = AppContextManager.AskPath(new ClientFolderBrowserDialogSettings { Description = "Select folder containing GalaxyClient.exe or Steam.exe" });

			if (result == DialogResult.OK)
			{
				this.SetClientIcon();
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
				Description = string.Format("Select folder containing {0}", AppContextManager.Context.Executable.Name)
			});

			if (result == DialogResult.OK)
			{
				this.ShowOrFocus();
			}
		}

		private void btnHelp_Click(object sender, EventArgs e)
		{
			LaunchManager.TryOpenReadme();
		}

		private void btnLaunchNoMods_Click(object sender, EventArgs e)
		{
			LaunchManager.LaunchProcess(this.ClientType);
		}

		private void btnPatreon_Click(object sender, EventArgs e)
		{
			Process.Start("https://www.patreon.com/fireundubh")?.Dispose();
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
			this.guiPwVersion.Text = PatchworkInfo.Version + " (#playwithfire)";
			this.guiGameName.Text = AppContextManager.Context.AppName;
			this.guiGameVersion.Text = "AppInfo v" + AppContextManager.Context.AppVersion;

			this.tbArguments.Text = SettingsManager.XmlData.Arguments;

			this.ClientType = string.IsNullOrEmpty(SettingsManager.XmlData.ClientPath) ? Client.None : SettingsManager.XmlData.ClientPath.EndsWithIgnoreCase("Steam.exe") ? Client.Steam : Client.Galaxy;

			this.SetClientIcon();

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

			this.btnTestRun.Click += async (o, args) =>
			                         {
				                         ResetData();
										 await this.btnTestRun_Click(o, args).ConfigureAwait(false);
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

		private void SetClientIcon()
		{
			Icon icon = ExtractIcon();

			if (icon != null)
			{
				Bitmap bitmap = Bitmap.FromHicon(icon.Handle);

				this.pbClientIcon.Image = new Bitmap(bitmap, new Size(19, 19));
			}

			this.pbClientIcon.Refresh();
		}

		private void tbArguments_TextChanged(object sender, EventArgs e)
		{
			SettingsManager.XmlData.Arguments = this.tbArguments.Text;
		}

		#endregion
	}
}
