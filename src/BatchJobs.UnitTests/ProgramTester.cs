using System;
using System.Collections.Generic;
using BatchJobs.Console;
using BatchJobs.Core;
using BatchJobs.Core.Logging;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace BatchJobs.UnitTests
{
    [TestFixture]
    public class ProgramTester
    {
        [Test]
        public void Should_create_a_factory()
        {
            var program = new Program();
            program.GetFactoryTypeName = () => typeof (FactoryStub).FullName + "," + GetType().Assembly.FullName;
            IJobAgentFactory factory = program.Factory();
            Assert.IsAssignableFrom(typeof (FactoryStub), factory);
        }

        [Test]
        public void Should_locate_a_factory_and_execute_a_job()
        {
            var agent = new StubJob();
            FactoryStub.JobAgent = agent;
            var program = new Program();
            program.GetFactoryTypeName = () => typeof (FactoryStub).FullName + "," + GetType().Assembly.FullName;
            program.Run(new[] {"foo"});
            Assert.That(FactoryStub.Name, Is.EqualTo("foo"));
            Assert.That(agent.Executed, Is.True);
        }

    	[Test]
    	public void Should_create_a_logging_factory()
    	{
			var program = new Program();
			program.GetFactoryTypeName = () => typeof(LoggingFactoryStub).FullName + "," + GetType().Assembly.FullName;
			IJobAgentFactory factory = program.Factory();
			Assert.AreEqual("BatchJobs.UnitTests.LoggingFactoryStub", factory.GetType().ToString());

    	}

		[Test]
		public void Should_rethrow_and_log_exceptions_from_job()
		{
			//Logger.EnsureInitialized(); //view sample error logging in output by uncommenting
			var agent = new ExceptionThrowingStubJob();
			ExceptionThrowingFactoryStub.JobAgent = agent;
			var program = new Program();
			program.GetFactoryTypeName = () => typeof(ExceptionThrowingFactoryStub).FullName + "," + GetType().Assembly.FullName;

			try
			{
				program.Run(new[] {"foo"});
				Assert.Fail("Should not have reached here-Program should have thrown");
			}catch
			{

			}
		}
    }

	public class ExceptionThrowingStubJob : IJobAgent
	{
		public ExceptionThrowingStubJob()
		{
		}

		public ExceptionThrowingStubJob(ILogger logger)
		{
		}

		public bool Executed { get; set; }


		public void Execute()
		{
			Executed = true;
			throw new Exception("fake error occurred");
		}
	}

	public class StubJob : IJobAgent
	{
		public bool Executed = false;
		public StubJob()
		{
		}


		public StubJob(ILogger logger)
		{
		}

		public void Execute()
		{
			Executed = true;
		}
	}



    public class FactoryStub : IJobAgentFactory
    {
        public FactoryStub()
        {
            if(JobAgent==null)
            {
                JobAgent = new StubJob();
            }
        }

        public static IJobAgent JobAgent { get; set; }

        public static string Name { get; set; }


        public IJobAgent Create(string name)
        {
            System.Console.WriteLine(name);
            Name = name;
            return JobAgent;
        }

        public IEnumerable<string> GetInstanceNames()
        {
            return new string[0];
        }
    }

	public class LoggingFactoryStub : IJobAgentFactory
	{
		protected readonly ILogger _logger;

		public LoggingFactoryStub()
		{
			if (JobAgent == null)
			{
				JobAgent = new StubJob(_logger);
			}
		}

		public static IJobAgent JobAgent { get; set; }

		public static string Name { get; set; }


		public IJobAgent Create(string name)
		{
			System.Console.WriteLine(name);
			Name = name;
			return JobAgent;
		}

		public IEnumerable<string> GetInstanceNames()
		{
			return new string[0];
		}
	}

	public class ExceptionThrowingFactoryStub : IJobAgentFactory
	{
		protected readonly ILogger _logger;
		public static IJobAgent JobAgent { get; set; }
		public static string Name { get; set; }
		public ExceptionThrowingFactoryStub()
		{
			JobAgent = new ExceptionThrowingStubJob(_logger);
		}


		public IJobAgent Create(string name)
		{
			System.Console.WriteLine(name);
			Name = name;
			return JobAgent;
		}

		public IEnumerable<string> GetInstanceNames()
		{
			return new string[0];
		}
	}
}