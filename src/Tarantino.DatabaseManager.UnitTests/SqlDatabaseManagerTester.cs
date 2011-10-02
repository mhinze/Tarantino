using NUnit.Framework;
using Rhino.Mocks;
using Tarantino.Core.DatabaseManager.Model;
using Tarantino.Core.DatabaseManager.Services;
using Tarantino.Core.DatabaseManager.Services.Impl;

namespace Tarantino.UnitTests.Core.DatabaseManager.Services
{
	[TestFixture]
	public class SqlDatabaseManagerTester
	{
		[Test]
		public void Manages_database()
		{
			var settings = new ConnectionSettings("server", "db", true, null, null);
            var scriptDirectory = @"c:\scripts"; 
            var taskAttributes = new TaskAttributes(settings, scriptDirectory);

			var mocks = new MockRepository();
			var taskObserver = mocks.CreateMock<ITaskObserver>();
			var generator = mocks.CreateMock<ILogMessageGenerator>();
			var factory = mocks.CreateMock<IDatabaseActionExecutorFactory>();

			var creator = mocks.CreateMock<IDatabaseActionExecutor>();
			var updater = mocks.CreateMock<IDatabaseActionExecutor>();

			var executors = new IDatabaseActionExecutor[] { creator, updater };

			using (mocks.Record())
			{
				Expect.Call(generator.GetInitialMessage(taskAttributes)).Return("starting...");
				taskObserver.Log("starting...");
				Expect.Call(factory.GetExecutors(RequestedDatabaseAction.Create)).Return(executors);

                creator.Execute(taskAttributes, taskObserver);
                updater.Execute(taskAttributes, taskObserver);
			}

			using (mocks.Playback())
			{
				ISqlDatabaseManager manager = new SqlDatabaseManager(generator, factory);

				manager.Upgrade(taskAttributes, taskObserver);
			}

			mocks.VerifyAll();
		}

       [Test]
        public void Should_create_a_new_instance_without_IoC()
       {
           var manager = new SqlDatabaseManager();

       }
	}
}