using System.Collections.Generic;

using Tarantino.Core.DatabaseManager.Services.Impl;
using Tarantino.Core.DatabaseManager.Services.Impl.Tarantino.Core.DatabaseManager.Services.Impl;

namespace Tarantino.Core.DatabaseManager.Services
{
	
	public interface IDatabaseActionResolver
	{
		IEnumerable<DatabaseAction> GetActions(RequestedDatabaseAction requestedDatabaseAction);
	}
}