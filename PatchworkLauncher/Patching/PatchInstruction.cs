using System;
using Patchwork.AutoPatching;
using Patchwork.Engine;

namespace PatchworkLauncher
{
	public class PatchInstruction : IInstruction, IDisposable
	{
		#region Public Properties

		public AppInfo AppInfo { get; set; }

		public bool IsEnabled { get; set; }

		public PatchingManifest Patch { get; set; }

		public string Location { get; set; }

		public string Name
		{
			get
			{
				return this.Patch.PatchInfo.PatchName;
			}
		}

		public string Requirements
		{
			get
			{
				return this.Patch.PatchInfo.Requirements;
			}
		}

		public string Target
		{
			get
			{
				return this.Patch.PatchInfo.GetTargetFile(this.AppInfo).Name;
			}
		}

		public string Version
		{
			get
			{
				return this.Patch.PatchInfo.PatchVersion;
			}
		}

		#endregion

		#region Public Methods and Operators

		public void Dispose()
		{
			this.Patch?.Dispose();
		}

		#endregion
	}
}
