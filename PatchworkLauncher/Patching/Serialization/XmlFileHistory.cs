using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace PatchworkLauncher
{
	[XmlInclude(typeof(XmlPatchHistory))]
	public class XmlFileHistory
	{
		#region Public Properties

		[XmlArray("PatchHistory")]
		[XmlArrayItem("PatchHistoryInstruction")]
		public List<XmlPatchHistory> PatchHistory { get; set; } = new List<XmlPatchHistory>();

		public string TargetPath { get; set; }

		#endregion

		#region Public Methods and Operators

		public static XmlFileHistory FromInstrGroup(PatchGroup group)
		{
			return new XmlFileHistory()
			{
				PatchHistory = group.Instructions.Select(XmlPatchHistory.FromInstruction).ToList(),
				TargetPath = group.TargetPath
			};
		}

		#endregion
	}
}
