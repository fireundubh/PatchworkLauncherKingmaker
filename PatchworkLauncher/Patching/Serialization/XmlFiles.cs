using System.IO;
using System.Xml.Serialization;

namespace PatchworkLauncher
{
	public class XmlFiles
	{
		public string AppInfo { get; set; } = Path.GetFullPath(@".\AppInfo.dll");

		public string Client { get; set; } = string.Empty;

		public string History { get; set; } = Path.GetFullPath(@".\history.pw.xml");

		public string Preferences { get; set; } = Path.GetFullPath(@".\preferences.pw.xml");

		public string Readme { get; set; } = Path.GetFullPath(@".\readme.txt");

		public string Settings { get; set; } = Path.GetFullPath(@".\settings.pw.xml");
	}
}
