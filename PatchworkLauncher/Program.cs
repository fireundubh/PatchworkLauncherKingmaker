using System;
using System.Windows.Forms;

namespace PatchworkLauncher
{
	internal static class Program
	{
		#region Methods

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			SettingsManager.Initialize();
			PreferencesManager.Initialize();
			HistoryManager.Delete();

			var manager = new LaunchManager();
			manager.StartHomeWindow();

			Application.Run();
		}

		#endregion
	}
}
