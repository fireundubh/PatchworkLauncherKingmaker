using System;
using System.IO;
using Patchwork.AutoPatching;
using Patchwork.Engine.Utility;
using PatchworkLauncher.Properties;
using Serilog;

namespace PatchworkLauncher
{
	public static class AppContextManager
	{
		#region Constructors and Destructors

		static AppContextManager()
		{
			FullPath = Path.GetFullPath(PathSettings.Default.AppInfo);
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

		public static void Dispose()
		{
			((IDisposable)Logger).Dispose();
		}

		public static void Setup()
		{
			AppInfoFactory appInfoFactory = GetFactory();
			CreateInstance(appInfoFactory);
		}

		#endregion

		#region Methods

		private static void CreateInstance(AppInfoFactory appInfoFactory)
		{
			if (appInfoFactory != null && !SettingsManager.BaseFolder.IsNullOrWhitespace())
			{
				var directoryInfo = new DirectoryInfo(SettingsManager.BaseFolder);
				Context = appInfoFactory.CreateInfo(directoryInfo);
				return;
			}

			Logger.Error("Cannot find AppInfo.dll. Using default AppInfo instance.");

			Context = new AppInfo();
			Context.AppName = "No AppInfo.dll";
			Context.AppVersion = "version??";
		}

		private static AppInfoFactory GetFactory()
		{
			return File.Exists(FullPath) ? PatchingHelper.LoadAppInfoFactory(FullPath) : null;
		}

		#endregion
	}
}
