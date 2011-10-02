using System.Collections.Generic;
using BatchJobs.Core.Logging;


namespace BatchJobs.Core
{
	public abstract class JobAgentBase<T> : IJobAgent where T : class
    {
        private readonly IStateTransitionFactory _factory;

        public JobAgentBase(IStateTransitionFactory factory)
        {
            _factory = factory;
        }

        public void Execute()
        {
			BatchLoggerFactory.Default().Debug(this, "Getting transitions");
            IEnumerable<IStateTransition<T>> transitions = _factory.GetAll<T>();
			BatchLoggerFactory.Default().Debug(this, "Retrieved transitions");
            T[] batches = GetNextEntites();
			BatchLoggerFactory.Default().Debug(this, string.Format("Found {0} batches", batches.Length));

            foreach (T batch in batches)
            {
                foreach (var transition in transitions)
                {
					BatchLoggerFactory.Default().Debug(this, string.Format("Examining transition {0} in batch {1}", transition, batch));
					if (transition.IsValid(batch))
					{
						BatchLoggerFactory.Default().Debug(this, string.Format("Transition {0} is valid for batch {1}, executing", transition, batch));
						transition.Execute(batch);
					}
                }
            }
        }

        protected abstract T[] GetNextEntites();
    }
}