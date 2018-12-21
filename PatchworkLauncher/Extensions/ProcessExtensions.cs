using System.Diagnostics;
using System.IO;

namespace PatchworkLauncher.Extensions
{
	public static class ProcessExtensions
	{
		#region Public Methods and Operators

		public static void Configure(this Process process, string fileName, string arguments)
		{
			process.StartInfo.FileName = fileName;
			process.StartInfo.WorkingDirectory = Path.GetDirectoryName(fileName);
			process.EnableRaisingEvents = true;
			process.SetArguments(arguments);
		}

		#endregion

		#region Methods

		private static void SetArguments(this Process process, string appInfoArguments)
		{
			process.StartInfo.Arguments = string.Format("{0} {1}", appInfoArguments, SettingsManager.XmlData.Arguments).Trim();
		}

		#endregion
	}
}
