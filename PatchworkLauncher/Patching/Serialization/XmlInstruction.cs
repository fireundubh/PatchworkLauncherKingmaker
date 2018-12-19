using System.Xml.Serialization;

namespace PatchworkLauncher
{
	public class XmlInstruction : IInstruction
	{
		public string Name { get; set; }

		public string PatchLocation { get; set; }

		[XmlAttribute]
		public bool IsEnabled { get; set; }

		public static XmlInstruction FromInstruction(PatchInstruction instr)
		{
			return new XmlInstruction
			{
				IsEnabled = instr.IsEnabled,
				Name = instr.Patch.PatchInfo.PatchName,
				PatchLocation = instr.PatchLocation
			};
		}
	}
}
