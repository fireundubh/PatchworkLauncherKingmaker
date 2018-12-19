using System;
using System.IO;
using System.Text;
using PatchworkLauncher.Properties;
using Serilog;

namespace PatchworkLauncher
{
	public static class LogManager
	{
		#region Constructors and Destructors

		static LogManager()
		{
			FileExtension = Settings.Default.LogExtension;
			LogsDirectory = Path.GetFullPath(PathSettings.Default.Logs);
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

		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.IOException">The specified file is in use. -or-There is an open handle on the file, and the operating system is Windows XP or earlier. This open handle can result from enumerating directories and files. For more information, see How to: Enumerate Directories and Files.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Access to <paramref name="fileName" /> is denied.</exception>
		public static void DeleteEmptyFiles()
		{
			string[] files = Directory.GetFiles(LogsDirectory, string.Concat("*.", FileExtension));

			foreach (string file in files)
			{
				var fileInfo = new FileInfo(file);
				if (fileInfo.Length > 0)
				{
					continue;
				}

				File.Delete(file);
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
