using System;
using NUnit.Framework;
using Rhino.Mocks;
using Tarantino.Core.Commons.Services.Configuration;
using Tarantino.Core.Commons.Services.Web;
using Tarantino.Core.WebManagement.Services;
using Tarantino.Core.WebManagement.Services.Impl;

namespace Tarantino.UnitTests.Core.WebManagement.Services
{
	[TestFixture]
	public class MaintenancePageRedirectorTester
	{
		[Test]
		public void Does_not_redirect_if_its_not_supposed_to()
		{
			MockRepository mocks = new MockRepository();

			IMaintenanceRedirectionChecker checker = mocks.CreateMock<IMaintenanceRedirectionChecker>();

			using (mocks.Record())
			{
				Expect.Call(checker.ShouldBeRedirectedToMaintenancePage()).Return(false);
			}

			using (mocks.Playback())
			{
				IMaintenancePageRedirector redirector = new MaintenancePageRedirector(null, null, checker);
				redirector.RedirectToMaintenancePageIfAppropriate();
			}
		}

		[Test]
		public void Does_redirect_if_appropriate()
		{
			MockRepository mocks = new MockRepository();

			IWebContext context = mocks.CreateMock<IWebContext>();
			IConfigurationReader reader = mocks.CreateMock<IConfigurationReader>();
			IMaintenanceRedirectionChecker checker = mocks.CreateMock<IMaintenanceRedirectionChecker>();

			using (mocks.Record())
			{
				Expect.Call(checker.ShouldBeRedirectedToMaintenancePage()).Return(true);
				Expect.Call(reader.GetRequiredSetting("TarantinoWebManagementMaintenancePage")).Return("DownForMaintenance.aspx");
				context.ServerTransfer("DownForMaintenance.aspx", false);
			}

			using (mocks.Playback())
			{
				IMaintenancePageRedirector redirector = new MaintenancePageRedirector(context, reader, checker);
				redirector.RedirectToMaintenancePageIfAppropriate();
			}
		}

		[Test]
		public void Does_not_redirect_if_an_exception_takes_place()
		{
			MockRepository mocks = new MockRepository();

			IConfigurationReader reader = mocks.CreateMock<IConfigurationReader>();
			IMaintenanceRedirectionChecker checker = mocks.CreateMock<IMaintenanceRedirectionChecker>();

			using (mocks.Record())
			{
				Expect.Call(checker.ShouldBeRedirectedToMaintenancePage()).Return(true);
				Expect.Call(reader.GetRequiredSetting("TarantinoWebManagementMaintenancePage")).Throw(new Exception());
			}

			using (mocks.Playback())
			{
				IMaintenancePageRedirector redirector = new MaintenancePageRedirector(null, reader, checker);
				redirector.RedirectToMaintenancePageIfAppropriate();
			}
		}
	}
}