using System;
using System.Collections.Generic;
using Tarantino.Core.DatabaseManager.Services.Impl.Tarantino.Core.DatabaseManager.Services.Impl;
using Tarantino.DatabaseManager.Core;

namespace Tarantino.Core.DatabaseManager.Services.Impl
{
	
	public class DatabaseActionExecutorFactory : IDatabaseActionExecutorFactory
	{
		private readonly IDatabaseActionResolver _resolver;
        private readonly IDataBaseActionLocator _locator;

		public DatabaseActionExecutorFactory(IDatabaseActionResolver resolver, IDataBaseActionLocator locator)
		{
			_resolver = resolver;
			_locator = locator;
		}

	    public DatabaseActionExecutorFactory():this(new DatabaseActionResolver(),new DataBaseActionLocator())
	    {
	        
	    }

	    public IEnumerable<IDatabaseActionExecutor> GetExecutors(RequestedDatabaseAction requestedDatabaseAction)
		{
			IEnumerable<DatabaseAction> actions = _resolver.GetActions(requestedDatabaseAction);

			foreach (DatabaseAction action in actions)
			{
				IDatabaseActionExecutor instance = _locator.CreateInstance(action);
				yield return instance;
			}
		}
	}
}