namespace PatchworkLauncher
{
	public class XmlConfiguration
	{
		public XmlFiles Files { get; set; } = new XmlFiles();

		public XmlFolders Folders { get; set; } = new XmlFolders();

		public XmlRegistry Registry { get; set; } = new XmlRegistry();
	}
}
