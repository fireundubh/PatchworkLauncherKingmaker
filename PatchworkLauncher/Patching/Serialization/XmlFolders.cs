using System.IO;

namespace PatchworkLauncher
{
	public class XmlFolders
	{
		public string Game { get; set; } = string.Empty;

		public string Logs { get; set; } = Path.GetFullPath(@".\Logs");

		public string Mods { get; set; } = Path.GetFullPath(@".\Mods");
	}
}
