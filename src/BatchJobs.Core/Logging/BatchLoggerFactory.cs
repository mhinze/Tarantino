using System;

namespace BatchJobs.Core.Logging
{
	public class BatchLoggerFactory : AbstractFactoryBase<ILogger>
	{
		public static Func<ILogger> Default = DefaultUnconfiguredState;
	}
}