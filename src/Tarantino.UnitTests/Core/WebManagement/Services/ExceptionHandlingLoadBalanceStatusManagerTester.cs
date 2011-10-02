using System;
using NUnit.Framework;
using Rhino.Mocks;
using Tarantino.Core.Commons.Services.Web;
using Tarantino.Core.WebManagement.Services;
using Tarantino.Core.WebManagement.Services.Impl;
using Tarantino.Core.WebManagement.Services.Views;

namespace Tarantino.UnitTests.Core.WebManagement.Services
{
	[TestFixture]
	public class ExceptionHandlingLoadBalanceStatusManagerTester
	{
		[Test]
		public void Correctly_returns_error_message_from_load_balance_manager_when_no_exception_occurs()
		{
			MockRepository mocks = new MockRepository();
			ILoadBalanceStatusManager manager = mocks.CreateMock<ILoadBalanceStatusManager>();
			ILoadBalancerView view = mocks.CreateMock<ILoadBalancerView>();

			using (mocks.Record())
			{
				Expect.Call(manager.HandleLoadBalanceRequest()).Return("My error message");
				view.Render("My error message");
			}

			using (mocks.Playback())
			{
				IExceptionHandlingLoadBalanceStatusManager statusManager = new ExceptionHandlingLoadBalanceStatusManager(manager, view, null);
				statusManager.HandleLoadBalancing();
			}
		}

		[Test]
		public void Correctly_swallows_exception_from_action_and_returns_error_message()
		{
			MockRepository mocks = new MockRepository();
			ILoadBalanceStatusManager manager = mocks.CreateMock<ILoadBalanceStatusManager>();
			ILoadBalancerView view = mocks.CreateMock<ILoadBalancerView>();
			IWebContext context = mocks.CreateMock<IWebContext>();

			Exception exception = mocks.PartialMock<Exception>();

			using (mocks.Record())
			{
				SetupResult.For(exception.ToString()).Return("My exception message");
				Expect.Call(manager.HandleLoadBalanceRequest()).Throw(exception);
				context.WriteToResponse("My exception message");
			}

			using (mocks.Playback())
			{
				IExceptionHandlingLoadBalanceStatusManager statusManager = new ExceptionHandlingLoadBalanceStatusManager(manager, view, context);
				statusManager.HandleLoadBalancing();
			}
		}

		[Test]
		public void Correctly_swallows_exception_from_render_and_returns_error_message()
		{
			MockRepository mocks = new MockRepository();
			ILoadBalanceStatusManager manager = mocks.CreateMock<ILoadBalanceStatusManager>();
			ILoadBalancerView view = mocks.CreateMock<ILoadBalancerView>();
			IWebContext context = mocks.CreateMock<IWebContext>();

			Exception exception = mocks.PartialMock<Exception>();

			using (mocks.Record())
			{
				SetupResult.For(exception.ToString()).Return("My exception message");
				Expect.Call(manager.HandleLoadBalanceRequest()).Return(string.Empty);
				view.Render(string.Empty);
				LastCall.Throw(exception);
				context.WriteToResponse("My exception message");
			}

			using (mocks.Playback())
			{
				IExceptionHandlingLoadBalanceStatusManager statusManager = new ExceptionHandlingLoadBalanceStatusManager(manager, view, context);
				statusManager.HandleLoadBalancing();
			}
		}
	}
}