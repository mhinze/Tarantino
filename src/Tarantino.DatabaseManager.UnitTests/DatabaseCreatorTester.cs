using NUnit.Framework;
using Rhino.Mocks;
using Tarantino.Core.DatabaseManager.Model;
using Tarantino.Core.DatabaseManager.Services;
using Tarantino.Core.DatabaseManager.Services.Impl;

namespace Tarantino.UnitTests.Core.DatabaseManager.Services
{
	[TestFixture]
	public class DatabaseCreatorTester
	{
		[Test]
		public void Creates_database()
		{
			var settings = new ConnectionSettings("server", "db", true, null, null);
            var taskAttributes = new TaskAttributes(settings, "c:\\scripts");

			var mocks = new MockRepository();
			var queryExecutor = mocks.CreateMock<IQueryExecutor>();
			var executor = mocks.CreateMock<IScriptFolderExecutor>();
			var taskObserver = mocks.CreateMock<ITaskObserver>();
			
			using (mocks.Record())
			{
				queryExecutor.ExecuteNonQuery(settings, "create database [db]", false);
				executor.ExecuteScriptsInFolder(taskAttributes, "ExistingSchema", taskObserver);
			}

			using (mocks.Playback())
			{
				IDatabaseActionExecutor creator = new DatabaseCreator(queryExecutor, executor);
				creator.Execute(taskAttributes, taskObserver);
			}

			mocks.VerifyAll();
		}
	}
}