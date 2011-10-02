using System.Collections.Generic;

using Tarantino.Core.Commons.Services.Configuration;
using Tarantino.Core.DatabaseManager.Services.Impl.Tarantino.Core.DatabaseManager.Services.Impl;

namespace Tarantino.Core.DatabaseManager.Services.Impl
{
	
	public class DatabaseActionExecutorFactory : IDatabaseActionExecutorFactory
	{
		private readonly IDatabaseActionResolver _resolver;
		private readonly IServiceLocator _locator;

		public DatabaseActionExecutorFactory(IDatabaseActionResolver resolver, IServiceLocator locator)
		{
			_resolver = resolver;
			_locator = locator;
		}

		public IEnumerable<IDatabaseActionExecutor> GetExecutors(RequestedDatabaseAction requestedDatabaseAction)
		{
			IEnumerable<DatabaseAction> actions = _resolver.GetActions(requestedDatabaseAction);

			foreach (DatabaseAction action in actions)
			{
				IDatabaseActionExecutor instance = _locator.CreateInstance<IDatabaseActionExecutor>(action.ToString());
				yield return instance;
			}
		}
	}
}