namespace PatchworkLauncher
{
	public interface IInstruction
	{
		bool IsEnabled { get; set; }

		string PatchLocation { get; set; }
	}
}
