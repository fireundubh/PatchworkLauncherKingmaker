using System.Collections.Generic;
using System.Xml.Serialization;

namespace PatchworkLauncher
{
	[XmlInclude(typeof(XmlFileHistory))]
	public class XmlHistory
	{
		#region Public Properties

		public bool Success { get; set; }

		[XmlArray("Files")]
		[XmlArrayItem("File")]
		public List<XmlFileHistory> Files { get; set; } = new List<XmlFileHistory>();

		#endregion
	}
}
