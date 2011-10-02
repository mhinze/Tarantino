using System.Collections.Generic;
using Tarantino.Core.DatabaseManager.Model;

namespace Tarantino.Core.DatabaseManager.Services.Impl
{
	public class SqlDatabaseManager : ISqlDatabaseManager
	{
		public const string SQL_FILE_ASSEMBLY = "Tarantino.Core";
		public const string SQL_FILE_TEMPLATE = "Tarantino.Core.DatabaseManager.SqlFiles.{0}.sql";

		private readonly IDatabaseActionExecutorFactory _actionExecutorFactory;
		private readonly ILogMessageGenerator _logMessageGenerator;

		public SqlDatabaseManager(ILogMessageGenerator logMessageGenerator,
		                          IDatabaseActionExecutorFactory actionExecutorFactory)
		{
			_logMessageGenerator = logMessageGenerator;
			_actionExecutorFactory = actionExecutorFactory;
		}

	    public void Upgrade(TaskAttributes taskAttributes, ITaskObserver taskObserver)
		{
            string initializationMessage = _logMessageGenerator.GetInitialMessage(taskAttributes);
			taskObserver.Log(initializationMessage);

            IEnumerable<IDatabaseActionExecutor> executors = _actionExecutorFactory.GetExecutors(taskAttributes.RequestedDatabaseAction);

			foreach (IDatabaseActionExecutor executor in executors)
			{
                executor.Execute(taskAttributes, taskObserver);
			}
		}
	}
}