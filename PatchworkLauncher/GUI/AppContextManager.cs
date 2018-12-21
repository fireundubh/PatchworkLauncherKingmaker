using System;
using System.IO;
using System.Windows.Forms;
using Patchwork.AutoPatching;
using Patchwork.Engine.Utility;
using PatchworkLauncher.FolderBrowserDialogSettings;
using Serilog;

namespace PatchworkLauncher
{
	public static class AppContextManager
	{
		#region Constructors and Destructors

		static AppContextManager()
		{
			FullPath = Path.GetFullPath(SettingsManager.XmlSettings.Launcher.Files.AppInfo);
			Logger = LogManager.CreateLogger("AppContextManager");
		}

		#endregion

		#region Public Properties

		public static AppInfo Context { get; set; }

		public static string FullPath { get; }

		#endregion

		#region Properties

		private static ILogger Logger { get; }

		#endregion

		#region Public Methods and Operators

		public static DialogResult AskPath(IFolderBrowserDialogSettings settings)
		{
			DialogResult result;

			using (var dialog = new FolderBrowserDialog { Description = settings.Description, ShowNewFolderButton = false })
			{
				result = dialog.ShowDialog();

				if (result == DialogResult.OK)
				{
					settings.Save(dialog.SelectedPath);
				}
			}

			return result;
		}

		public static void Dispose()
		{
			((IDisposable) Logger)?.Dispose();
		}

		public static void Setup()
		{
			AppInfoFactory appInfoFactory = GetFactory();
			CreateInstance(appInfoFactory);
		}

		#endregion

		#region Methods

		private static AppInfoFactory GetFactory()
		{
			return File.Exists(FullPath) ? PatchingHelper.LoadAppInfoFactory(FullPath) : null;
		}

		private static void CreateInstance(AppInfoFactory appInfoFactory)
		{
			if (appInfoFactory != null && !SettingsManager.XmlData.GamePath.IsNullOrWhitespace())
			{
				var directoryInfo = new DirectoryInfo(SettingsManager.XmlData.GamePath);
				Context = appInfoFactory.CreateInfo(directoryInfo);
				return;
			}

			Logger.Error("Cannot find AppInfo.dll. Using default AppInfo instance.");

			Context = new AppInfo();
			Context.AppName = "No AppInfo.dll";
			Context.AppVersion = "version??";
		}

		#endregion
	}
}
