using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Tarantino.Core.Commons.Services.Configuration;
using Tarantino.Core.Commons.Services.Web;
using Tarantino.Core.WebManagement.Services;
using Tarantino.Core.WebManagement.Services.Impl;

namespace Tarantino.UnitTests.Core.WebManagement.Services
{
	[TestFixture]
	public class ExternalUrlCheckerTester
	{
		[Test]
		public void Determines_that_url_is_external_when_application_domain_names_match_except_for_casing()
		{
			MockRepository mocks = new MockRepository();
			IWebContext context = mocks.CreateMock<IWebContext>();
			IConfigurationReader configurationReader = mocks.CreateMock<IConfigurationReader>();

			using (mocks.Record())
			{
				Expect.Call(context.GetServerVariable("HTTP_HOST")).Return("www.myapp.com");
				Expect.Call(configurationReader.GetRequiredSetting("TarantinoWebManagementHttpHost")).Return("www.MyApp.com");
			}

			using (mocks.Playback())
			{
				IExternalUrlChecker urlChecker = new ExternalUrlChecker(context, configurationReader);
				Assert.That(urlChecker.CurrentUrlIsExternal());
			}
		}

		[Test]
		public void Determines_that_url_is_internal_when_application_domain_names_match_except_for_casing()
		{
			MockRepository mocks = new MockRepository();
			IWebContext context = mocks.CreateMock<IWebContext>();
			IConfigurationReader configurationReader = mocks.CreateMock<IConfigurationReader>();

			using (mocks.Record())
			{
				Expect.Call(context.GetServerVariable("HTTP_HOST")).Return("www1.myapp.com");
				Expect.Call(configurationReader.GetRequiredSetting("TarantinoWebManagementHttpHost")).Return("www.MyApp.com");
			}

			using (mocks.Playback())
			{
				IExternalUrlChecker urlChecker = new ExternalUrlChecker(context, configurationReader);
				Assert.That(urlChecker.CurrentUrlIsExternal(), Is.False);
			}
		}
	}
}