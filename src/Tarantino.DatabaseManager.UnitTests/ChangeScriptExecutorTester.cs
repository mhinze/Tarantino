using System;
using Tarantino.Core.DatabaseManager.Model;
using Tarantino.Core.DatabaseManager.Services;
using Tarantino.Core.DatabaseManager.Services.Impl;
using NUnit.Framework;
using Rhino.Mocks;
using Tarantino.DatabaseManager.Core;

namespace Tarantino.UnitTests.Core.DatabaseManager.Services
{
	[TestFixture]
	public class ChangeScriptExecutorTester
	{
		[Test]
		public void CorrectlyLogsWarningWhenScriptHasAlreadyBeenExecuted()
		{
			ConnectionSettings settings = getConnectionSettings();
			string scriptFile = @"c:\scripts\Update\01_Test.sql";

			MockRepository mocks = new MockRepository();
			IScriptExecutionTracker executionTracker = mocks.CreateMock<IScriptExecutionTracker>();
			ITaskObserver taskObserver = mocks.CreateMock<ITaskObserver>();

			Expect.Call(executionTracker.ScriptAlreadyExecuted(settings, "01_Test.sql")).Return(true);
			taskObserver.Log("Skipping (already executed): 01_Test.sql");

			mocks.ReplayAll();

			IChangeScriptExecutor executor = new ChangeScriptExecutor(executionTracker, null, null);
			executor.Execute(scriptFile, settings, taskObserver);

			mocks.VerifyAll();
		}

		[Test]
		public void CorrectlyExecutesScriptIfItHasntAlreadyBeenExecuted()
		{
			ConnectionSettings settings = getConnectionSettings();
			string scriptFile = @"c:\scripts\Update\01_Test.sql";
			string fileContents = "file contents...";

			MockRepository mocks = new MockRepository();
			IScriptExecutionTracker executionTracker = mocks.CreateMock<IScriptExecutionTracker>();
			IFileSystem fileSystem = mocks.CreateMock<IFileSystem>();
			IQueryExecutor queryExecutor = mocks.CreateMock<IQueryExecutor>();
			ITaskObserver taskObserver = mocks.CreateMock<ITaskObserver>();

			Expect.Call(executionTracker.ScriptAlreadyExecuted(settings, "01_Test.sql")).Return(false);
			taskObserver.Log("Executing: 01_Test.sql");
			Expect.Call(fileSystem.ReadTextFile(scriptFile)).Return(fileContents);
			queryExecutor.ExecuteNonQuery(settings, fileContents, true);
			executionTracker.MarkScriptAsExecuted(settings, "01_Test.sql", taskObserver);

			mocks.ReplayAll();

			IChangeScriptExecutor executor = new ChangeScriptExecutor(executionTracker, queryExecutor, fileSystem);
			executor.Execute(scriptFile, settings, taskObserver);

			mocks.VerifyAll();
		}

		private ConnectionSettings getConnectionSettings()
		{
			return new ConnectionSettings(String.Empty, String.Empty, false, String.Empty, String.Empty);
		}
	}
}