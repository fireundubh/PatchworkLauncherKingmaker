using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Patchwork.AutoPatching;
using Patchwork.Engine.Utility;
using Patchwork.Utility.Binding;
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

			Context = Bindable.Variable(DefaultAppInfo);

			Context.HasChanged += delegate(IBindable<AppInfo> bindable)
			                      {
				                      if (bindable.Value != null && bindable.Value != DefaultAppInfo)
				                      {
										  Logger.Debug("Context changed. Trying to set client icon.");
					                      LaunchManager.SetClientIcon();
				                      }
			                      };
		}

		#endregion

		#region Public Properties

		public static IBindable<AppInfo> Context { get; set; }

		public static string FullPath { get; }

		#endregion

		#region Properties

		private static AppInfo DefaultAppInfo
		{
			get
			{
				var defaultAppInfo = new AppInfo();
				defaultAppInfo.AppName = "No AppInfo.dll";
				defaultAppInfo.AppVersion = "version??";
				return defaultAppInfo;
			}
		}

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
			Logger.Debug("Trying to set up AppInfoFactory given path: {0}", FullPath);

			AppInfoFactory appInfoFactory = GetFactory(FullPath);
			CreateInstance(appInfoFactory);
		}

		#endregion

		#region Methods

		private static AppInfoFactory GetFactory(string filePath)
		{
			return File.Exists(filePath) ? PatchingHelper.LoadAppInfoFactory(filePath) : null;
		}

		private static void CreateInstance(AppInfoFactory appInfoFactory)
		{
			if (appInfoFactory != null && !SettingsManager.XmlData.GamePath.IsNullOrWhitespace())
			{
				var directoryInfo = new DirectoryInfo(SettingsManager.XmlData.GamePath);
				Context.Value = appInfoFactory.CreateInfo(directoryInfo);

				Logger.Debug("Found AppInfo.dll. Using AppInfo instance: {0}", Context.Value.AppName);

				return;
			}

			Logger.Error("Cannot find AppInfo.dll. Using default AppInfo instance.");

			Context.Value = DefaultAppInfo;
		}

		#endregion
	}
}
