namespace PatchworkLauncher
{
	public class XmlPatchHistory
	{
		public XmlPatchHistory()
		{
			// ignored
		}

		public XmlPatchHistory(string location)
		{
			this.Location = location;
		}

		public string Location { get; set; } = "";

		public static XmlPatchHistory FromInstruction(PatchInstruction instruction)
		{
			return new XmlPatchHistory(instruction.Location);
		}
	}
}
