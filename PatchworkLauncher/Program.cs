using System;
using System.Windows.Forms;

namespace PatchworkLauncher
{
	internal static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			PreferencesManager.Initialize();
			SettingsManager.Initialize();
			HistoryManager.Delete();
			var manager = new LaunchManager();
			manager.StartHomeWindow();
			Application.Run();
		}
	}
}
