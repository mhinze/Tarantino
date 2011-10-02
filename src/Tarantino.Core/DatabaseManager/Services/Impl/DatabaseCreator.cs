using Tarantino.Core.DatabaseManager.Model;

namespace Tarantino.Core.DatabaseManager.Services.Impl
{
	public class DatabaseCreator : IDatabaseActionExecutor
	{
		private readonly IQueryExecutor _queryExecutor;
		private readonly IScriptFolderExecutor _folderExecutor;

		public DatabaseCreator(IQueryExecutor queryExecutor, IScriptFolderExecutor folderExecutor)
		{
			_queryExecutor = queryExecutor;
			_folderExecutor = folderExecutor;
		}

		public void Execute(TaskAttributes taskAttributes, ITaskObserver taskObserver)
		{
            string sql = string.Format("create database {0}", taskAttributes.ConnectionSettings.Database);
            _queryExecutor.ExecuteNonQuery(taskAttributes.ConnectionSettings, sql, false);

            _folderExecutor.ExecuteScriptsInFolder(taskAttributes, "ExistingSchema", taskObserver);
		}
	}
}