using System.Collections.Generic;

using Tarantino.Core.DatabaseManager.Services.Impl.Tarantino.Core.DatabaseManager.Services.Impl;

namespace Tarantino.Core.DatabaseManager.Services.Impl
{
	
	public class DatabaseActionResolver : IDatabaseActionResolver
	{
		public IEnumerable<DatabaseAction> GetActions(RequestedDatabaseAction requestedDatabaseAction)
		{
			if (requestedDatabaseAction == RequestedDatabaseAction.Create)
			{
				return new DatabaseAction[] {DatabaseAction.Create, DatabaseAction.Update};
			}
			else if (requestedDatabaseAction == RequestedDatabaseAction.Drop)
			{
				return new DatabaseAction[] {DatabaseAction.Drop};
			}
			else if (requestedDatabaseAction == RequestedDatabaseAction.Rebuild)
			{
				return new DatabaseAction[] { DatabaseAction.Drop, DatabaseAction.Create, DatabaseAction.Update };
			}
			else
			{
				return new DatabaseAction[] { DatabaseAction.Update };
			}
		}
	}
}