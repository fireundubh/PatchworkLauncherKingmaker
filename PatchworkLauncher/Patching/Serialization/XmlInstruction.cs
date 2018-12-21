using System;
using System.Xml.Serialization;

namespace PatchworkLauncher
{
	public class XmlInstruction : IInstruction
	{
		#region Public Properties

		[XmlAttribute]
		public bool IsEnabled { get; set; }

		public string Name { get; set; }

		public string Location { get; set; }

		#endregion

		#region Public Methods and Operators

		public static XmlInstruction FromInstruction(PatchInstruction instruction)
		{
			return new XmlInstruction
			{
				IsEnabled = instruction.IsEnabled,
				Name = instruction.Patch.PatchInfo.PatchName,
				Location = instruction.Location
			};
		}

		#endregion
	}
}
