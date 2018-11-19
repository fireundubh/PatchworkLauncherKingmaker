using System;
using System.Drawing;
using System.Windows.Forms;
using PatchworkLauncher.Properties;

namespace PatchworkLauncher
{
	public partial class guiMods : Form
	{
		public guiMods(LaunchManager manager)
		{
			Manager = manager;
			InitializeComponent();
		}

		public LaunchManager Manager { get; private set; }

		private void guiMods_Load(object sender, EventArgs e)
		{
			this.ControlBox = false;

			Icon = Icon.FromHandle(Resources.IconSmall.GetHicon());
			guiInstructionsGridView.AutoGenerateColumns = false;
			guiInstructionsGridView.DataSource = Manager.Instructions;
		}

		private void guiAdd_Click(object sender, EventArgs e)
		{
			Manager.Command_Dialog_AddPatch(this);
		}

		private void guiRemove_Click(object sender, EventArgs e)
		{
			object selected = guiInstructionsGridView?.CurrentRow?.DataBoundItem;
			if (selected == null)
			{
				return;
			}

			Manager.Instructions.Remove((PatchInstruction) selected);
		}

		private void guiClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void guiMoveUp_Click(object sender, EventArgs e)
		{
			int? current = guiInstructionsGridView?.CurrentRow?.Index;
			if (!current.HasValue)
			{
				return;
			}

			int newIndex = Manager.Command_MovePatch(current.Value, -1);
			guiInstructionsGridView.CurrentCell = guiInstructionsGridView.Rows[newIndex].Cells[0];
			guiInstructionsGridView.Refresh();
		}

		private void guiMoveDown_Click(object sender, EventArgs e)
		{
			int? current = guiInstructionsGridView?.CurrentRow?.Index;
			if (!current.HasValue)
			{
				return;
			}

			int newIndex = Manager.Command_MovePatch(current.Value, 1);
			guiInstructionsGridView.CurrentCell = guiInstructionsGridView.Rows[newIndex].Cells[0];
			guiInstructionsGridView.Refresh();
		}

		private void guiMods_MouseDown(object sender, MouseEventArgs e)
		{
			if (Environment.OSVersion.Platform == PlatformID.Unix || Environment.OSVersion.Platform == PlatformID.MacOSX)
			{
				return;
			}

			if (e.Button != MouseButtons.Left)
			{
				return;
			}

			guiHome.ReleaseCapture();
			guiHome.SendMessage(this.Handle, guiHome.WM_NCLBUTTONDOWN, guiHome.HT_CAPTION, 0);
		}
	}
}
