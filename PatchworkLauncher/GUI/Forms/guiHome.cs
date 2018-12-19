using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Patchwork;
using Patchwork.Engine.Utility;
using PatchworkLauncher.Enums;
using PatchworkLauncher.Extensions;
using PatchworkLauncher.Properties;

namespace PatchworkLauncher
{
	public partial class guiHome : Form
	{
		#region Constructors and Destructors

		public guiHome(LaunchManager manager)
		{
			this.Manager = manager;
			this.InitializeComponent();
		}

		#endregion

		#region Public Properties

		public static Client Client { get; set; } = Client.None;

		public LaunchManager Manager { get; }

		#endregion

		#region Methods

		private static void ResetData()
		{
			SettingsManager.InvalidateInstructions();
			HistoryManager.RestorePatchedFiles();
			HistoryManager.Delete();
			LaunchManager.Idle();
		}

		private static void SaveClientPathToSettings(string clientPath)
		{
			string[] clients =
			{
				"GalaxyClient.exe",
				"Steam.exe"
			};

			foreach (string path in clients.Select(client => Path.Combine(clientPath, client)).Where(File.Exists))
			{
				PathSettings.Default.Client = path;
				break;
			}

			PathSettings.Default.Save();
		}

		private static void ShowClientPathMessageBox()
		{
			string clientPath = PathSettings.Default.Client.IsNullOrWhitespace() ? "NO CLIENT PATH SET" : PathSettings.Default.Client;
			MessageBox.Show(clientPath, "Client Path", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly, false);
		}

		// ReSharper disable once MemberCanBeMadeStatic.Local
		private void btnChangeClientPath_Click(object sender, EventArgs e)
		{
			var dialog = new FolderBrowserDialog
			{
				Description = "Select folder containing Steam.exe or GalaxyClient.exe",
				ShowNewFolderButton = false,
			};

			// ReSharper disable once InvertIf
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				SaveClientPathToSettings(dialog.SelectedPath);
			}

			ShowClientPathMessageBox();
		}

		private void btnChangeFolder_Click(object sender, EventArgs e)
		{
			LaunchManager.ChangeFolder();
		}

		private void btnClearArguments_Click(object sender, EventArgs e)
		{
			this.tbArguments.Clear();
			Settings.Default.Save();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			LaunchManager.ExitApplication();
		}

		private void btnHelp_Click(object sender, EventArgs e)
		{
			LaunchManager.TryOpenReadme();
		}

		private void btnLaunchNoMods_Click(object sender, EventArgs e)
		{
			LaunchManager.LaunchProcess();
		}

		private async Task btnLaunchWithMods_Click(object sender, EventArgs e)
		{
			if (SettingsManager.Instructions.Count != 0)
			{
				await this.Manager.LaunchModdedAsync().ConfigureAwait(false);
				return;
			}

			MessageBox.Show("No active mods configured.", "Configuration Error", MessageBoxButtons.OK);
		}

		private void btnPatreon_Click(object sender, EventArgs e)
		{
			Process.Start("https://www.patreon.com/fireundubh");
		}

		// ReSharper disable once MemberCanBeMadeStatic.Local
		private void btnResetClientPath_Click(object sender, EventArgs e)
		{
			PathSettings.Default.Client = string.Empty;
			PathSettings.Default.Save();
		}

		private async Task btnTestRun_Click(object sender, EventArgs e)
		{
			if (SettingsManager.Instructions.Count != 0)
			{
				await this.Manager.TestRunAsync().ConfigureAwait(false);
				return;
			}

			MessageBox.Show("No active mods configured.", "Configuration Error", MessageBoxButtons.OK);
		}

		private void guiHome_FormClosing(object sender, FormClosingEventArgs e)
		{
			SettingsManager.InvalidateBaseFolder();
			SettingsManager.InvalidateInstructions();
			HistoryManager.RestorePatchedFiles();
			HistoryManager.Delete();

			// dispose loggers so we can delete empty logs
			AppContextManager.Dispose();
			SettingsManager.Dispose();
			HistoryManager.Dispose();
			LaunchManager.Dispose();
			PatchManager.Dispose();
			PreferencesManager.Dispose();

			// delete empty logs
			LogManager.DeleteEmptyFiles();
		}

		private void guiHome_KeyUp(object sender, KeyEventArgs e)
		{
			// clear existing click events
			if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2 || e.KeyCode == Keys.F3)
			{
				this.btnSetGamePath.ClearClickEvents(this.btnChangeFolder_Click, this.btnChangeClientPath_Click, this.btnResetClientPath_Click);
			}

			switch (e.KeyCode)
			{
				case Keys.F1:
					this.btnSetGamePath.Text = "SET GAME PATH";
					this.btnSetGamePath.Click += this.btnChangeFolder_Click;
					break;
				case Keys.F2:
					this.btnSetGamePath.Text = "SET CLIENT PATH";
					this.btnSetGamePath.Click += this.btnChangeClientPath_Click;
					break;
				case Keys.F3:
					this.btnSetGamePath.Text = "RESET CLIENT PATH";
					this.btnSetGamePath.Click += this.btnResetClientPath_Click;
					break;
				case Keys.F4:
					ShowClientPathMessageBox();
					break;
			}
		}

		private void guiHome_Load(object sender, EventArgs e)
		{
			this.ControlBox = false;

			this.guiGameIcon.Image = this.Manager.ProgramIcon;
			this.guiGameIcon.Refresh();
			this.guiPwVersion.Text = PatchworkInfo.Version + " (#playwithfire)";
			this.guiGameName.Text = AppContextManager.Context.AppName;
			this.guiGameVersion.Text = "AppInfo v" + AppContextManager.Context.AppVersion;

			Settings.Default.Upgrade();
			Settings.Default.Reload();
			Settings.Default.Save();

			string clientPath = PathSettings.Default.Client;

			Client = clientPath.IsNullOrWhitespace() ? Client.None : clientPath.EndsWithIgnoreCase("Steam.exe") ? Client.Steam : Client.Galaxy;

			// async click handlers
			this.btnLaunchNoMods.Click += (o, args) =>
			                              {
				                              ResetData();
											  this.btnLaunchNoMods_Click(o, args);
			                              };

			this.btnLaunchWithMods.Click += async (o, args) =>
			                                {
				                                ResetData();
												await this.btnLaunchWithMods_Click(o, args);
			                                };

			this.btnTestRun.Click += async (o, args) =>
			                         {
				                         ResetData();
										 await this.btnTestRun_Click(o, args);
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
			Process.Start(PatchworkInfo.PatchworkSite);
		}

		private void tbArguments_Leave(object sender, EventArgs e)
		{
			Settings.Default.Arguments = this.tbArguments.Text;
			Settings.Default.Save();
		}

		#endregion
	}
}
