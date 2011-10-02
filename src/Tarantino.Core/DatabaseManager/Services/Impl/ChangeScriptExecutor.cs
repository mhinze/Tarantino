using System.IO;

using Tarantino.Core.Commons.Services.Environment;
using Tarantino.Core.DatabaseManager.Model;

namespace Tarantino.Core.DatabaseManager.Services.Impl
{
	
	public class ChangeScriptExecutor : IChangeScriptExecutor
	{
		private readonly IScriptExecutionTracker _executionTracker;
		private readonly IQueryExecutor _executor;
		private readonly IFileSystem _fileSystem;

		public ChangeScriptExecutor(IScriptExecutionTracker executionTracker, IQueryExecutor executor, IFileSystem fileSystem)
		{
			_executionTracker = executionTracker;
			_executor = executor;
			_fileSystem = fileSystem;
		}

		public void Execute(string fullFilename, ConnectionSettings settings, ITaskObserver taskObserver)
		{
			string scriptFilename = getFilename(fullFilename);

			if (_executionTracker.ScriptAlreadyExecuted(settings, scriptFilename))
			{
				taskObserver.Log(string.Format("Skipping (already executed): {0}", scriptFilename));
			}
			else
			{
				taskObserver.Log(string.Format("Executing: {0}", scriptFilename));
				string sql = _fileSystem.ReadTextFile(fullFilename);
				_executor.ExecuteNonQuery(settings, sql, true);
				_executionTracker.MarkScriptAsExecuted(settings, scriptFilename, taskObserver);
			}
		}

		private string getFilename(string fullFilename)
		{
			return Path.GetFileName(fullFilename);
		}
	}
}