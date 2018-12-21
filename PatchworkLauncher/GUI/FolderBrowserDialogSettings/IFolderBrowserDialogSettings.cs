using System.Windows.Forms;

namespace PatchworkLauncher.FolderBrowserDialogSettings
{
	public interface IFolderBrowserDialogSettings
	{
		#region Public Properties

		string Caption { get; }

		string Description { get; set; }

		string ExecutablePath { get; }

		#endregion

		#region Public Methods and Operators

		DialogResult ShowDialog();

		void Save(string value);

		#endregion
	}
}
