using System;
using System.IO;
using System.Xml.Serialization;
using PatchworkLauncher.Properties;
using Serilog;
using Serilog.Events;

namespace PatchworkLauncher
{
	public static class PreferencesManager
	{
		#region Constructors and Destructors

		static PreferencesManager()
		{
			SerializerInstance = new XmlSerializer(typeof(XmlPreferences));
			FullPath = Path.GetFullPath(PathSettings.Default.Preferences);

			Logger = LogManager.CreateLogger("PreferencesManager");
		}

		#endregion

		#region Public Properties

		public static string FullPath { get; }

		public static XmlPreferences Preferences
		{
			get
			{
				return SerializerInstance.Deserialize(FullPath, new XmlPreferences());
			}
			set
			{
				SerializerInstance.Serialize(value, FullPath);
			}
		}

		#endregion

		#region Properties

		private static ILogger Logger { get; }

		private static XmlSerializer SerializerInstance { get; }

		#endregion

		#region Public Methods and Operators

		public static void Dispose()
		{
			((IDisposable)Logger).Dispose();
		}

		public static void Initialize()
		{
			CreatePreferences();
		}

		#endregion

		#region Methods

		private static void CreatePreferences()
		{
			if (!File.Exists(FullPath))
			{
				var preferences = new XmlPreferences();
				preferences.AlwaysPatch = true;
				preferences.DontCopyFiles = true;
				preferences.OpenLogAfterPatch = false;
				preferences.MinimumEventLevel = LogEventLevel.Information;
				Preferences = preferences;
			}
		}

		#endregion
	}
}
