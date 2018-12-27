using System.Xml.Serialization;
using Serilog.Events;

namespace PatchworkLauncher
{
	[XmlRoot("Preferences")]
	public class XmlPreferences
	{
		#region Public Properties

		public bool AlwaysPatch { get; set; } = true;

		public bool DontCopyFiles { get; set; } = true;

		public bool IgnoreNoClientWarning { get; set; } = false;

		public bool OpenLogAfterPatch { get; set; } = false;

		public LogEventLevel MinimumEventLevel { get; set; } = LogEventLevel.Information;

		#endregion
	}
}
