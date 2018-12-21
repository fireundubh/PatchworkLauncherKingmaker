using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Serilog;

namespace PatchworkLauncher
{
	/// <summary>
	/// Requires <paramref name="SettingsManager"/>
	/// </summary>
	public static class LogManager
	{
		#region Constructors and Destructors

		static LogManager()
		{
			FileExtension = ".log";
			LogsDirectory = Path.GetFullPath(SettingsManager.XmlData.LogsPath);
		}

		#endregion

		#region Properties

		private static string FileExtension { get; }

		private static string LogsDirectory { get; }

		#endregion

		#region Public Methods and Operators

		public static ILogger CreateLogger(string className)
		{
			string logPath = BuildLogPath(className);

			string logTemplate = string.Format("{{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz}} [{0}] [{{Level:u3}}] {{Message:lj}}{{NewLine}}{{Exception}}", className);

			var logConfig = new LoggerConfiguration();
			logConfig.WriteTo.File(logPath, outputTemplate: logTemplate, encoding: Encoding.UTF8);
			logConfig.MinimumLevel.Is(PreferencesManager.Preferences.MinimumEventLevel);

			return logConfig.CreateLogger();
		}

		public static void DeleteEmptyFiles()
		{
			string searchPattern = string.Concat("*", FileExtension);

			IEnumerable<FileInfo> files = Directory.GetFiles(LogsDirectory, searchPattern).Select(x => new FileInfo(x));

			foreach (FileInfo fileInfo in files.Where(f => f.Length == 0))
			{
				File.Delete(fileInfo.FullName);
			}
		}

		#endregion

		#region Methods

		private static string BuildLogPath(string className)
		{
			string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
			string logFileName = string.Concat(className, " - ", timestamp, FileExtension);
			return Path.Combine(LogsDirectory, logFileName);
		}

		#endregion
	}
}
