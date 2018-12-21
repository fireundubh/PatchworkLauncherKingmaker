using System;
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
		#region Public Properties

		[XmlArrayItem("Instruction")]
		public List<XmlInstruction> Instructions { get; set; } = new List<XmlInstruction>();

		public XmlConfiguration Launcher { get; set; } = new XmlConfiguration();

		public XmlOptions Options { get; set; } = new XmlOptions();

		#endregion

		#region Public Methods and Operators

		[Obsolete]
		public static XmlSettings FromInstructionSeq(IEnumerable<PatchInstruction> instrSeq)
		{
			return new XmlSettings
			{
				Instructions = instrSeq?.Select(XmlInstruction.FromInstruction).Distinct(new PatchLocationEqualityComparer()).ToList() ?? new List<XmlInstruction>()
			};
		}

		[Obsolete]
		public static XmlSettings FromInstructionSeq(IEnumerable<XmlInstruction> instrSeq)
		{
			return new XmlSettings
			{
				Instructions = instrSeq?.Distinct(new PatchLocationEqualityComparer()).ToList() ?? new List<XmlInstruction>()
			};
		}

		#endregion
	}
}
