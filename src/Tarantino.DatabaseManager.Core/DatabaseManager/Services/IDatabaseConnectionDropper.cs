
using Tarantino.Core.DatabaseManager.Model;

namespace Tarantino.Core.DatabaseManager.Services
{
	
	public interface IDatabaseConnectionDropper
	{
		void Drop(ConnectionSettings settings, ITaskObserver taskObserver);
	}
}