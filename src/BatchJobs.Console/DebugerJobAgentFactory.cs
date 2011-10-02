using System.Collections.Generic;
using BatchJobs.Core;

namespace BatchJobs.Console
{
	public class DebugerJobAgentFactory : IJobAgentFactory
	{
		public IJobAgent Create(string name)
		{
			System.Console.WriteLine(name);
			return new DebugerJobAgent();
		}

		public IEnumerable<string> GetInstanceNames()
		{
			return new string[] { "Foo", "Bar" };
		}
	}
}