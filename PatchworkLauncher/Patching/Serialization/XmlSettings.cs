using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using PatchworkLauncher.Comparers;

namespace PatchworkLauncher
{
	[XmlInclude(typeof(XmlInstruction))]
	[XmlRoot("Settings")]
	public class XmlSettings
	{
		[XmlArrayItem("Instruction")]
		public List<XmlInstruction> Instructions { get; set; } = new List<XmlInstruction>();

		public string BaseFolder { get; set; }

		public static XmlSettings FromInstructionSeq(IEnumerable<PatchInstruction> instrSeq)
		{
			return new XmlSettings
			{
				Instructions = instrSeq?.Select(XmlInstruction.FromInstruction).Distinct(new PatchPathEqualityComparer()).ToList() ?? new List<XmlInstruction>()
			};
		}

		public static XmlSettings FromInstructionSeq(IEnumerable<XmlInstruction> instrSeq)
		{
			return new XmlSettings
			{
				Instructions = instrSeq?.Distinct(new PatchPathEqualityComparer()).ToList() ?? new List<XmlInstruction>()
			};
		}
	}
}
