using System.Collections.Generic;

namespace BatchJobs.Core
{
    public interface IStateTransitionFactory
    {
        IEnumerable<IStateTransition<T>> GetAll<T>();
    }
}