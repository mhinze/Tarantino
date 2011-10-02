using NUnit.Framework;
using Rhino.Mocks;
using Tarantino.Core.DatabaseManager.Model;
using Tarantino.Core.DatabaseManager.Services;
using Tarantino.Core.DatabaseManager.Services.Impl;

namespace Tarantino.UnitTests.Core.DatabaseManager.Services
{
	[TestFixture]
	public class DatabaseUpdaterTester
	{
		[Test]
		public void Updates_database()
		{
			var settings = new ConnectionSettings("server", "db", true, null, null);
            var taskAttributes = new TaskAttributes(settings, "c:\\scripts");

			var mocks = new MockRepository();
			var executor = mocks.CreateMock<IScriptFolderExecutor>();
			var taskObserver = mocks.CreateMock<ITaskObserver>();

			using (mocks.Record())
			{
				executor.ExecuteScriptsInFolder(taskAttributes, "Update", taskObserver);
			}

			using (mocks.Playback())
			{
				IDatabaseActionExecutor updater = new DatabaseUpdater(executor);
                updater.Execute(taskAttributes, taskObserver);
			}

			mocks.VerifyAll();
		}
	}
}