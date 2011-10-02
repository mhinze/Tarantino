namespace Tarantino.Core.Commons.Services.Environment.Impl
{
	public class MachineEnvironment : IMachineEnvironment
	{
		public void ExitWithError()
		{
			System.Environment.Exit(-1);
		}
	}
}