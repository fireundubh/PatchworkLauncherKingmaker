namespace PatchworkLauncher
{
	public interface IInstruction
	{
		bool IsEnabled { get; set; }

		string Location { get; set; }
	}
}
