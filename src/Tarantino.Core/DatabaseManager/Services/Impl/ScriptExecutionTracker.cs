using System;
using Tarantino.Core.DatabaseManager.Model;

namespace Tarantino.Core.DatabaseManager.Services.Impl
{
	
	public class ScriptExecutionTracker : IScriptExecutionTracker
	{
		private string[] _appliedScripts;
		private readonly IQueryExecutor _executor;

		public ScriptExecutionTracker(IQueryExecutor executor)
		{
			_executor = executor;
		}

		public void MarkScriptAsExecuted(ConnectionSettings settings, string scriptFilename, ITaskObserver task)
		{
			string insertTemplate = 
				"insert into usd_AppliedDatabaseScript (ScriptFile, DateApplied) values ('{0}', getdate())";

			string sql = string.Format(insertTemplate, scriptFilename);
			_executor.ExecuteNonQuery(settings, sql, true);
		}

		public bool ScriptAlreadyExecuted(ConnectionSettings settings, string scriptFilename)
		{
			if (_appliedScripts == null)
			{
				_appliedScripts =
					_executor.ReadFirstColumnAsStringArray(settings, "select ScriptFile from usd_AppliedDatabaseScript");
			}

			bool alreadyExecuted = Array.IndexOf(_appliedScripts, scriptFilename) >= 0;

			return alreadyExecuted;
		}
	}
}