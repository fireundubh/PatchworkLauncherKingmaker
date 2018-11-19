using System;
using System.IO;
using System.Windows.Forms;
using Patchwork.Engine.Utility;
using Patchwork.Utility.Binding;

namespace PatchworkLauncher
{
	public partial class guiInputGameFolder : Form
	{
		private readonly FolderBrowserDialog _folderBrowser;

		public guiInputGameFolder(string warningText = "")
		{
			InitializeComponent();
			lblWarningText.Text = warningText;

			_folderBrowser = new FolderBrowserDialog {Description = "Select game folder", ShowNewFolderButton = false, SelectedPath = PathHelper.GetAbsolutePath("")};
		}

		private void guiInputGameFolder_Load(object sender, EventArgs e)
		{
			this.ControlBox = false;

			Folder.SetRule("Folder must exist", Directory.Exists);
			Folder.IsValid.Binding = btnApply.Bind(x => x.Enabled).ToBinding(BindingMode.FromTarget);
			Folder.Binding = tbLocation.Bind(x => x.Text, "TextChanged").ToBinding();
		}

		public IBindable<string> Folder { get; } = Bindable.Variable("");

		private void guiOkay_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		private void guiCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			DialogResult result = _folderBrowser.ShowDialog();
			if (result != DialogResult.OK)
			{
				return;
			}

			Folder.Value = _folderBrowser.SelectedPath;
		}

		private void guiInputGameFolder_MouseDown(object sender, MouseEventArgs e)
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

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
