using Tarantino.Core.DatabaseManager.Model;

namespace Tarantino.Core.DatabaseManager.Services
{
	public interface ISqlDatabaseManager
	{
		void Upgrade(TaskAttributes taskAttributes, ITaskObserver taskObserver);
	}
}