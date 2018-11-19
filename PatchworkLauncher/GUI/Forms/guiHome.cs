using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
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
	}
}
