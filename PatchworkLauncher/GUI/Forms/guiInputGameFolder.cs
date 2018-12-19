using System;
using System.IO;
using System.Windows.Forms;
using Patchwork.Engine.Utility;
using Patchwork.Utility.Binding;
using PatchworkLauncher.Extensions;

namespace PatchworkLauncher
{
	public partial class guiInputGameFolder : Form
	{
		private readonly FolderBrowserDialog _folderBrowser;

		public guiInputGameFolder(string warningText = "")
		{
			this.InitializeComponent();
			this.lblWarningText.Text = warningText;

			this._folderBrowser = new FolderBrowserDialog {Description = "Select game folder", ShowNewFolderButton = false, SelectedPath = PathHelper.GetAbsolutePath("")};
		}

		private void guiInputGameFolder_Load(object sender, EventArgs e)
		{
			this.ControlBox = false;

			this.Folder.SetRule("Folder must exist", Directory.Exists);
			this.Folder.IsValid.Binding = GuiBindings.Bind(this.btnApply, x => x.Enabled).ToBinding(BindingMode.FromTarget);
			this.Folder.Binding = this.tbLocation.Bind(x => x.Text, "TextChanged").ToBinding();
		}

		public IBindable<string> Folder { get; } = Bindable.Variable("");

		private void guiOkay_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void guiCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			DialogResult result = this._folderBrowser.ShowDialog();
			if (result != DialogResult.OK)
			{
				return;
			}

			this.Folder.Value = this._folderBrowser.SelectedPath;
		}

		private void guiInputGameFolder_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.ReleaseCapture();
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
