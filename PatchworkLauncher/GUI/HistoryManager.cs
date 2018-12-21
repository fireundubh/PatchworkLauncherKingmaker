using System;
using System.IO;
using System.Xml.Serialization;
using Serilog;

namespace PatchworkLauncher
{
	/// <summary>
	/// Requires <paramref name="SettingsManager"/>
	/// </summary>
	public static class HistoryManager
	{
		#region Constructors and Destructors

		static HistoryManager()
		{
			SerializerInstance = new XmlSerializer(typeof(XmlHistory));
			FullPath = Path.GetFullPath(SettingsManager.XmlSettings.Launcher.Files.History);

			Logger = LogManager.CreateLogger("HistoryManager");
		}

		#endregion

		#region Public Properties

		public static string FullPath { get; }

		public static XmlHistory History
		{
			get
			{
				return SerializerInstance.Deserialize(FullPath, new XmlHistory());
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

		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.IOException">The specified file is in use. -or- There is an open handle on the file, and the operating system is Windows XP or earlier. This open handle can result from enumerating directories and files.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. -or- The file is an executable file that is in use. -or- <paramref name="FullPath" /> is a directory. -or- <paramref name="FullPath" /> specified a read-only file.</exception>
		public static void Delete()
		{
			File.Delete(FullPath);

			if (!File.Exists(FullPath))
			{
				Logger.Debug("Deleted: {0}", FullPath);
			}
		}

		public static void Dispose()
		{
			((IDisposable) Logger)?.Dispose();
		}

		public static void Initialize()
		{
			Delete();
		}

		public static void RestorePatchedFiles()
		{
			if (History != null)
			{
				PatchingHelper.RestorePatchedFiles(AppContextManager.Context, History.Files);
			}
		}

		#endregion
	}
}
