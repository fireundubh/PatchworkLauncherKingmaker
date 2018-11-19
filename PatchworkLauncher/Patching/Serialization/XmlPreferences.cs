using System.Xml.Serialization;
using Serilog.Events;

namespace PatchworkLauncher
{
	[XmlRoot("Preferences")]
	public class XmlPreferences
	{
		public bool AlwaysPatch { get; set; } = false;

		public bool DontCopyFiles { get; set; }

		public bool OpenLogAfterPatch { get; set; } = false;

		public LogEventLevel MinimumEventLevel { get; set; } = LogEventLevel.Information;
	}
}
