using BatchJobs.Core;

namespace BatchJobs.Console
{
	public class DebugerJobAgent : IJobAgent
	{
		public void Execute()
		{
			System.Console.WriteLine("Executing");
		}
	}
}