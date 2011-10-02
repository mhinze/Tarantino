using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Tarantino.Core.WebManagement.Model;
using Tarantino.Core.WebManagement.Services;
using Tarantino.Core.WebManagement.Services.Impl;

namespace Tarantino.UnitTests.Core.WebManagement.Services
{
	[TestFixture]
	public class MaintenanceRedirectionCheckerTester
	{
		[Test]
		public void Does_not_redirect_to_maintenance_page_if_non_page_extension_or_site_not_down_for_maintenance_or_url_is_internal_url()
		{
			performTest(false, false, false, false);
			performTest(false, false, true, false);
			performTest(false, true, false, false);
			performTest(false, true, true, false);
			performTest(true, false, false, false);
			performTest(true, false, true, false);
			performTest(true, true, false, false);
		}

		[Test]
		public void Does_redirect_to_maintenance_page_if_page_extension_and_site_is_down_for_maintenance_and_url_is_external_url()
		{
			performTest(true, true, true, true);
		}

		private void performTest(bool isRedirectableExtension, bool isExternalUrl, bool isDownForMaintenance, bool shouldBeRedirected)
		{
			ApplicationInstance instance = new ApplicationInstance();
			instance.DownForMaintenance = isDownForMaintenance;

			MockRepository mocks = new MockRepository();
			IFileExtensionChecker extensionChecker = mocks.CreateMock<IFileExtensionChecker>();
			IExternalUrlChecker urlChecker = mocks.CreateMock<IExternalUrlChecker>();
			IApplicationInstanceContext context = mocks.CreateMock<IApplicationInstanceContext>();

			using (mocks.Record())
			{
				Expect.Call(extensionChecker.CurrentUrlCanBeRedirected()).Return(isRedirectableExtension);
				Expect.Call(urlChecker.CurrentUrlIsExternal()).Return(isExternalUrl);
				Expect.Call(context.GetCurrent()).Return(instance);
			}

			using (mocks.Playback())
			{
				IMaintenanceRedirectionChecker checker = new MaintenanceRedirectionChecker(extensionChecker, urlChecker, context);
				Assert.That(checker.ShouldBeRedirectedToMaintenancePage(), Is.EqualTo(shouldBeRedirected));
			}
		}
	}
}