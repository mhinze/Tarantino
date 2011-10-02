using System;

namespace BatchJobs.Core
{
	public class AbstractFactoryBase<T>
	{
		protected static T DefaultUnconfiguredState()
		{
			throw new Exception(typeof(T).Name + " not configured.");
		}
	}
}