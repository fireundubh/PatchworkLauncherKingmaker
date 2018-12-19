namespace PatchworkLauncher.Extensions
{
	public static class PatchExtensions
	{
		#region Public Methods and Operators

		/// <exception cref="T:Patchwork.Engine.PatchDeclerationException">The patch did not have a PatchInfo class.</exception>
		public static PatchInstruction ToPatchInstruction(this XmlInstruction instruction)
		{
			return new PatchInstruction
			{
				AppInfo = AppContextManager.Context,
				IsEnabled = true,
				Patch = PatchManager.TryGetManifest(instruction.PatchLocation),
				PatchLocation = instruction.PatchLocation
			};
		}

		public static XmlInstruction ToXmlInstruction(this PatchInstruction instruction)
		{
			return new XmlInstruction
			{
				IsEnabled = instruction.IsEnabled,
				Name = instruction.Patch.PatchInfo.PatchName,
				PatchLocation = instruction.PatchLocation
			};
		}

		#endregion
	}
}
