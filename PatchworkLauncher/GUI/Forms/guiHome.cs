using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Patchwork;
using Patchwork.Utility.Binding;
using PatchworkLauncher.Properties;

namespace PatchworkLauncher
{
	public partial class guiHome : Form
	{
		public const int WM_NCLBUTTONDOWN = 0xA1;
		public const int HT_CAPTION = 0x2;

		[DllImport("user32.dll", EntryPoint = "SendMessage", CallingConvention = CallingConvention.StdCall)]
		public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

		[DllImport("user32.dll", EntryPoint = "ReleaseCapture", CallingConvention = CallingConvention.StdCall)]
		public static extern bool ReleaseCapture();

		public LaunchManager Manager { get; private set; }

		public guiHome(LaunchManager manager)
		{
			Manager = manager;
			InitializeComponent();
		}

		private void guiHome_Load(object sender, EventArgs e)
		{
			this.ControlBox = false;

			guiGameIcon.Image = Manager.ProgramIcon;
			guiGameIcon.Refresh();
			guiPwVersion.Text = PatchworkInfo.Version + " (modified by fireundubh)";
			guiGameName.Text = Manager.AppInfo.AppName;
			guiGameVersion.Text = "AppInfo v" + Manager.AppInfo.AppVersion;
			IBindable<bool> isEnabled = Manager.State.Convert(x => x == LaunchManagerState.Idle);
			this.Bind(x => x.Enabled).Binding = isEnabled.ToBinding(BindingMode.IntoTarget);
			isEnabled.HasChanged += x =>
			                        {
				                        if (x.Value)
				                        {
					                        Invoke((Action) (() => this.Focus()));
				                        }
			                        };

			Settings.Default.Upgrade();
			Settings.Default.Reload();
			Settings.Default.Save();
		}

		private void btnActiveMods_Click(object sender, EventArgs e)
		{
			Manager.Command_OpenMods();
		}

		private void btnLaunchNoMods_Click(object sender, EventArgs e)
		{
			Manager.Command_Launch();
		}

		private void btnLaunchWithMods_Click(object sender, EventArgs e)
		{
			if (this.Manager.Instructions.Count == 0)
			{
				MessageBox.Show("No active mods configured.", "Configuration Error", MessageBoxButtons.OK);
				return;
			}

			Manager.Command_Launch_Modded();
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
				string[] clients =
				{
					"GalaxyClient.exe",
					"Steam.exe"
				};

				foreach (string path in clients.Select(client => Path.Combine(dialog.SelectedPath, client)).Where(File.Exists))
				{
					Settings.Default.ClientPath = path;
					break;
				}

				Settings.Default.Save();
			}
		}

		// ReSharper disable once MemberCanBeMadeStatic.Local
		private void btnResetClientPath_Click(object sender, EventArgs e)
		{
			Settings.Default.ClientPath = string.Empty;
			Settings.Default.Save();
		}

		private void btnChangeFolder_Click(object sender, EventArgs e)
		{
			Manager.Command_ChangeFolder();
		}

		private void btnTestRun_Click(object sender, EventArgs e)
		{
			if (this.Manager.Instructions.Count == 0)
			{
				MessageBox.Show("No active mods configured.", "Configuration Error", MessageBoxButtons.OK);
				return;
			}

			Manager.Command_TestRun();
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(PatchworkInfo.PatchworkSite);
		}

		private void btnHelp_Click(object sender, EventArgs e)
		{
			Manager.Command_Open_Readme();
		}

		private void btnClearArguments_Click(object sender, EventArgs e)
		{
			this.tbArguments.Clear();
			Settings.Default.Save();
		}

		private void btnClose_Click(object sender, EventArgs e)
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

		private void tbArguments_Leave(object sender, EventArgs e)
		{
			Settings.Default.Arguments = this.tbArguments.Text;
			Settings.Default.Save();
		}

		private void guiHome_MouseDown(object sender, MouseEventArgs e)
		{
			if (Environment.OSVersion.Platform == PlatformID.Unix || Environment.OSVersion.Platform == PlatformID.MacOSX)
			{
				return;
			}

			if (e.Button != MouseButtons.Left)
			{
				return;
			}

			ReleaseCapture();
			SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
		}

		private void btnPatreon_Click(object sender, EventArgs e)
		{
			Process.Start("https://www.patreon.com/fireundubh");
		}

		private void guiHome_KeyUp(object sender, KeyEventArgs e)
		{
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
			}
		}
	}
}
