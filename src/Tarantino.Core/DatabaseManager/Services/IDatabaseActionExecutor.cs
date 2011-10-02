
using Tarantino.Core.DatabaseManager.Model;

namespace Tarantino.Core.DatabaseManager.Services
{
	public interface IDatabaseActionExecutor
	{
		void Execute(TaskAttributes taskAttributes, ITaskObserver taskObserver);
	}
}