using System.IO;
using System.Windows.Forms;
using Patchwork.Engine.Utility;

namespace PatchworkLauncher.FolderBrowserDialogSettings
{
	public class GameFolderBrowserDialogSettings : IFolderBrowserDialogSettings
	{
		#region Public Properties

		public string Caption
		{
			get
			{
				return "Game Path";
			}
		}

		public string Description { get; set; }

		public string ExecutablePath
		{
			get
			{
				return SettingsManager.XmlData.GamePath;
			}
		}

		#endregion

		#region Public Methods and Operators

		public DialogResult ShowDialog()
		{
			string path = this.ExecutablePath.IsNullOrWhitespace() ? string.Format("NO {0} SET", this.Caption.ToUpper()) : this.ExecutablePath;
			return MessageBox.Show(path, this.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		public void Save(string value)
		{
			string executablePath = value;

			if (Directory.Exists(executablePath))
			{
				SettingsManager.XmlData.GamePath = executablePath;
				return;
			}

			SettingsManager.XmlData.GamePath = string.Empty;
		}

		#endregion
	}
}
