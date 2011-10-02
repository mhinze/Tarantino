using Tarantino.Core.DatabaseManager.Model;

namespace Tarantino.Core.DatabaseManager.Services.Impl
{
	public class DatabaseUpdater : IDatabaseActionExecutor
	{
		private readonly IScriptFolderExecutor _folderExecutor;

		public DatabaseUpdater(IScriptFolderExecutor folderExecutor)
		{
			_folderExecutor = folderExecutor;
		}

		public void Execute(TaskAttributes taskAttributes, ITaskObserver taskObserver)
		{
            _folderExecutor.ExecuteScriptsInFolder(taskAttributes, "Update", taskObserver);
		}
	}
}