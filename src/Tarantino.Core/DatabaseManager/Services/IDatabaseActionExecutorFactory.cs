using System.Collections.Generic;

using Tarantino.Core.DatabaseManager.Services.Impl;

namespace Tarantino.Core.DatabaseManager.Services
{
	
	public interface IDatabaseActionExecutorFactory
	{
		IEnumerable<IDatabaseActionExecutor> GetExecutors(RequestedDatabaseAction requestedDatabaseAction);
	}
}