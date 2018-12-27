using System;
using System.IO;
using System.Xml.Serialization;
using Serilog;
using Serilog.Events;

namespace PatchworkLauncher
{
	/// <summary>
	/// Requires <paramref name="SettingsManager"/>
	/// </summary>
	public static class PreferencesManager
	{
		#region Static Fields

		private static readonly object ThreadLock = new object();

		#endregion

		#region Constructors and Destructors

		static PreferencesManager()
		{
			SerializerInstance = new XmlSerializer(typeof(XmlPreferences));
			FullPath = Path.GetFullPath(SettingsManager.XmlSettings.Launcher.Files.Preferences);

			Logger = LogManager.CreateLogger("PreferencesManager");
		}

		#endregion

		#region Public Properties

		public static bool IgnoreNoClientWarning
		{
			get
			{
				return Preferences.IgnoreNoClientWarning;
			}
			set
			{
				lock (ThreadLock)
				{
					XmlPreferences preferences = Preferences;
					preferences.IgnoreNoClientWarning = value;
					SerializerInstance.Serialize(preferences, FullPath);
				}
			}
		}

		public static bool OpenLogAfterPatch
		{
			get
			{
				return Preferences.OpenLogAfterPatch;
			}
			set
			{
				lock (ThreadLock)
				{
					XmlPreferences preferences = Preferences;
					preferences.OpenLogAfterPatch = value;
					SerializerInstance.Serialize(preferences, FullPath);
				}
			}
		}

		public static string FullPath { get; }

		public static XmlPreferences Preferences
		{
			get
			{
				return SerializerInstance.Deserialize(FullPath, new XmlPreferences());
			}
			set
			{
				lock (ThreadLock)
				{
					SerializerInstance.Serialize(value, FullPath);
				}
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
			((IDisposable) Logger)?.Dispose();
		}

		public static void Initialize()
		{
			CreatePreferences();
		}

		#endregion

		#region Methods

		private static void CreatePreferences()
		{
			lock (ThreadLock)
			{
				if (File.Exists(FullPath))
				{
					return;
				}

				var preferences = new XmlPreferences();
				preferences.AlwaysPatch = true;
				preferences.DontCopyFiles = true;
				preferences.OpenLogAfterPatch = false;
				preferences.MinimumEventLevel = LogEventLevel.Information;
				Preferences = preferences;

				Logger.Debug("Created preferences.pw.xml because the file did not exist.");
			}
		}

		#endregion
	}
}
