using System;
using System.Threading;
using NUnit.Framework;
using Rhino.Mocks;
using Tarantino.Core.Commons.Services.Configuration;
using Tarantino.Core.Daemon.Services;
using Tarantino.Core.Daemon.Services.Impl;
using Tarantino.Core.Commons.Services.Logging;

namespace Tarantino.UnitTests.Core.Daemon.Services
{
	[TestFixture]
	public class ServiceRunnerTester
	{
		[Test]
		public void Run_service()
		{
			MockRepository mocks = new MockRepository();
			IApplicationSettings settings = mocks.CreateMock<IApplicationSettings>();
			IServiceAgentAggregator aggregator = mocks.CreateMock<IServiceAgentAggregator>();

			IServiceRunner runner = new ServiceRunner(aggregator, settings);

			using (mocks.Record())
			{


				aggregator.ExecuteServiceAgentCycle();
				LastCall.Repeat.Times(2, int.MaxValue);

				Expect.Call(settings.GetServiceSleepTime()).Return(10);
				LastCall.Repeat.Times(2, int.MaxValue);

			}

			using (mocks.Playback())
			{
				runner.Start();
				Thread.Sleep(500);
				runner.Stop();
			}

			mocks.VerifyAll();
		}

		[Test]
		public void When_the_cycle_completes_we_should_get_an_event()
		{
			_startFired = DateTime.MinValue;
			_completedFired = DateTime.MinValue;

			MockRepository mocks = new MockRepository();
			IServiceAgentAggregator aggregator = mocks.CreateMock<IServiceAgentAggregator>();

			ServiceRunner runner = new ServiceRunner(aggregator, null);

			using (mocks.Record())
			{

				aggregator.ExecuteServiceAgentCycle();
				LastCall.On(aggregator).Do(new Action(delegate { Thread.Sleep(50); }));
			}

			using (mocks.Playback())
			{
				runner.CycleStarted += runner_CycleStarted;
				runner.CycleCompleted += runner_CycleCompleted;
				runner.RunOneCycle();

				Assert.IsTrue(_completedFired > _startFired);
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Correctly_handles_when_service_agent_aggregator_throws_exception()
		{
			ApplicationException exception = new ApplicationException();

			MockRepository mocks = new MockRepository();
			IApplicationSettings settings = mocks.CreateMock<IApplicationSettings>();
			IServiceAgentAggregator aggregator = mocks.CreateMock<IServiceAgentAggregator>();

			IServiceRunner runner = new ServiceRunner(aggregator, settings);

			using (mocks.Record())
			{

				aggregator.ExecuteServiceAgentCycle();
				LastCall.Throw(exception);

			}

			using (mocks.Playback())
			{
				runner.Start();
				Thread.Sleep(500);
				runner.Stop();
			}

			mocks.VerifyAll();
		}

		private DateTime _startFired;
		private DateTime _completedFired;

		private void runner_CycleStarted(object sender, EventArgs e)
		{
			_startFired = DateTime.Now;
		}

		private void runner_CycleCompleted(object sender, EventArgs e)
		{
			_completedFired = DateTime.Now;
		}
	}

	public delegate void Action();
}