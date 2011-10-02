using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Tarantino.Core.Commons.Services.Web;
using Tarantino.Core.WebManagement.Model;
using Tarantino.Core.WebManagement.Services;
using Tarantino.Core.WebManagement.Services.Impl;

namespace Tarantino.UnitTests.Core.WebManagement.Services
{
	[TestFixture]
	public class LoadBalanceStatusManagerTester
	{
		[Test]
		public void Return_normal_status_200_message_if_instance_should_be_online_and_status_not_being_changed()
		{
			ApplicationInstance instance = new ApplicationInstance();
			instance.AvailableForLoadBalancing = true;

			MockRepository mocks = new MockRepository();
			IApplicationInstanceContext instanceContext = mocks.CreateMock<IApplicationInstanceContext>();
			IWebContext context = mocks.CreateMock<IWebContext>();

			using (mocks.Record())
			{
				Expect.Call(instanceContext.GetCurrent()).Return(instance);
				Expect.Call(context.GetRequestItem(LoadBalanceStatusManager.ENABLED_PARAM)).Return(null);
			}

			using (mocks.Playback())
			{
				ILoadBalanceStatusManager manager = new LoadBalanceStatusManager(instanceContext, context, null);
				string errorMessage = manager.HandleLoadBalanceRequest();

				Assert.That(errorMessage, Is.Empty);
			}
		}

		[Test]
		public void Return_status_400_message_if_instance_should_be_offline_and_status_not_being_changed()
		{
			ApplicationInstance instance = new ApplicationInstance();
			instance.AvailableForLoadBalancing = false;

			MockRepository mocks = new MockRepository();
			IApplicationInstanceContext instanceContext = mocks.CreateMock<IApplicationInstanceContext>();
			IWebContext context = mocks.CreateMock<IWebContext>();

			using (mocks.Record())
			{
				Expect.Call(instanceContext.GetCurrent()).Return(instance);
				Expect.Call(context.GetRequestItem(LoadBalanceStatusManager.ENABLED_PARAM)).Return(null);
				context.SetHttpResponseStatus(400, "This application has been turned off");
			}

			using (mocks.Playback())
			{
				ILoadBalanceStatusManager manager = new LoadBalanceStatusManager(instanceContext, context, null);
				string errorMessage = manager.HandleLoadBalanceRequest();

				Assert.That(errorMessage, Is.Empty);
			}
		}

		[Test]
		public void Should_enable_load_balancing_when_requested()
		{
			ApplicationInstance instance = new ApplicationInstance();
			instance.AvailableForLoadBalancing = false;

			MockRepository mocks = new MockRepository();
			IApplicationInstanceContext instanceContext = mocks.CreateMock<IApplicationInstanceContext>();
			IWebContext context = mocks.CreateMock<IWebContext>();
			ISecureAvailabilityStatusUpdater updater = mocks.CreateMock<ISecureAvailabilityStatusUpdater>();			

			using (mocks.Record())
			{
				Expect.Call(instanceContext.GetCurrent()).Return(instance);
				Expect.Call(context.GetRequestItem(LoadBalanceStatusManager.ENABLED_PARAM)).Return("True");
				Expect.Call(updater.SetStatus(true)).Return("My error message");
			}

			using (mocks.Playback())
			{
				ILoadBalanceStatusManager manager = new LoadBalanceStatusManager(instanceContext, context, updater);
				string errorMessage = manager.HandleLoadBalanceRequest();

				Assert.That(errorMessage, Is.EqualTo("My error message"));
			}
		}

		[Test]
		public void Should_disable_load_balancing_when_requested()
		{
			ApplicationInstance instance = new ApplicationInstance();
			instance.AvailableForLoadBalancing = false;

			MockRepository mocks = new MockRepository();
			IApplicationInstanceContext instanceContext = mocks.CreateMock<IApplicationInstanceContext>();
			IWebContext context = mocks.CreateMock<IWebContext>();
			ISecureAvailabilityStatusUpdater updater = mocks.CreateMock<ISecureAvailabilityStatusUpdater>();			

			using (mocks.Record())
			{
				Expect.Call(instanceContext.GetCurrent()).Return(instance);
				Expect.Call(context.GetRequestItem(LoadBalanceStatusManager.ENABLED_PARAM)).Return("FALSE");
				Expect.Call(updater.SetStatus(false)).Return(string.Empty);
			}

			using (mocks.Playback())
			{
				ILoadBalanceStatusManager manager = new LoadBalanceStatusManager(instanceContext, context, updater);
				string errorMessage = manager.HandleLoadBalanceRequest();

				Assert.That(errorMessage, Is.Empty);
			}
		}

			//    string errorMessage = string.Empty;

			//IApplicationInstanceContext instanceContext = ObjectFactory.GetInstance<IApplicationInstanceContext>();
			//ApplicationInstance currentInstance = instanceContext.GetCurrent();
			//IWebContext context = ObjectFactory.GetInstance<IWebContext>();

			//string enabledParameter = context.GetRequestItem("enabled");

			//if (enabledParameter != null)
			//{
			//  bool enabled = bool.Parse(enabledParameter);
			//  ISecureAvailabilityStatusUpdater updater = ObjectFactory.GetInstance<ISecureAvailabilityStatusUpdater>();
			//  errorMessage = updater.SetStatus(enabled);
			//}
			//else if (!currentInstance.AvailableForLoadBalancing)
			//{
			//  context.SetHttpResponseStatus(400, "This application has been turned off");
			//}

			//return errorMessage;

	}
}