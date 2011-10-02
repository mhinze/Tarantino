using System.Collections.Generic;

namespace BatchJobs.Core
{
    public interface IJobAgentFactory
    {
        IJobAgent Create(string name);
        IEnumerable<string> GetInstanceNames();
    }
}