using System.Diagnostics;
using System.IO;
using PatchworkLauncher.Properties;

namespace PatchworkLauncher.Extensions
{
	public static class ProcessExtensions
	{
		public static Process Create(this Process process, string fileName, string arguments)
		{
			process.StartInfo.FileName = fileName;
			process.StartInfo.WorkingDirectory = Path.GetDirectoryName(fileName);
			process.EnableRaisingEvents = true;
			process.SetArguments(arguments);
			return process;
		}

		public static void SetArguments(this Process process, string appInfoArguments)
		{
			process.StartInfo.Arguments = string.Format("{0} {1}", appInfoArguments, Settings.Default.Arguments).Trim();
		}
	}
}
