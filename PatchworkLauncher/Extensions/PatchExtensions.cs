using Patchwork.AutoPatching;
using Patchwork.Engine;

namespace PatchworkLauncher.Extensions
{
	public static class PatchExtensions
	{
		#region Public Methods and Operators

		public static PatchInstruction Configure(this PatchInstruction patchInstruction, AppInfo context, PatchingManifest manifest, string location, bool enabled = true)
		{
			patchInstruction.IsEnabled = enabled;
			patchInstruction.Patch = manifest;
			patchInstruction.Location = location;
			patchInstruction.AppInfo = context;
			return patchInstruction;
		}

		/// <exception cref="T:Patchwork.Engine.PatchDeclerationException">The patch did not have a PatchInfo class.</exception>
		public static PatchInstruction ToPatchInstruction(this XmlInstruction instruction)
		{
			return new PatchInstruction
			{
				AppInfo = AppContextManager.Context.Value,
				IsEnabled = true,
				Patch = PatchManager.TryGetManifest(instruction.Location),
				Location = instruction.Location
			};
		}

		public static XmlInstruction ToXmlInstruction(this PatchInstruction instruction)
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
