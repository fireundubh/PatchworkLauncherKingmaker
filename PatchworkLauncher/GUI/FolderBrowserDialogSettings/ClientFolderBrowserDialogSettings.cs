using System.IO;
using System.Linq;
using System.Windows.Forms;
using Patchwork.Engine.Utility;

namespace PatchworkLauncher.FolderBrowserDialogSettings
{
	public class ClientFolderBrowserDialogSettings : IFolderBrowserDialogSettings
	{
		#region Public Properties

		public string Caption
		{
			get
			{
				return "Client Path";
			}
		}

		public string Description { get; set; } = string.Empty;

		public string ExecutablePath
		{
			get
			{
				return SettingsManager.XmlData.ClientPath;
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
			string[] clients =
			{
				"GalaxyClient.exe",
				"Steam.exe"
			};

			foreach (string clientPath in clients.Select(client => Path.Combine(value, client)).Where(File.Exists))
			{
				SettingsManager.XmlData.ClientPath = clientPath;
				return;
			}

			SettingsManager.XmlData.ClientPath = string.Empty;
		}

		#endregion
	}
}
