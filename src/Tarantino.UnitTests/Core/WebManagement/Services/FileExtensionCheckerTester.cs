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
	public class FileExtensionCheckerTester
	{
		[Test]
		public void Correctly_determines_when_extension_is_in_list_of_extensions()
		{
			MockRepository mocks = new MockRepository();
			IConfigurationReader reader = mocks.CreateMock<IConfigurationReader>();
			IWebContext context = mocks.CreateMock<IWebContext>();

			using (mocks.Record())
			{
				Expect.Call(reader.GetStringArray("TarantinoWebManagementMaintenanceExtensions")).Return(new string[] { ".aspx", ".html" });
				Expect.Call(context.GetCurrentUrl()).Return("/website/mypage.aspx");
			}

			using (mocks.Playback())
			{
				IFileExtensionChecker checker = new FileExtensionChecker(reader, context);
				bool canBeRedirected = checker.CurrentUrlCanBeRedirected();
				
				Assert.That(canBeRedirected);
			}
		}

		[Test]
		public void Correctly_determines_when_mixed_case_extension_is_in_list_of_extensions()
		{
			MockRepository mocks = new MockRepository();
			IConfigurationReader reader = mocks.CreateMock<IConfigurationReader>();
			IWebContext context = mocks.CreateMock<IWebContext>();

			using (mocks.Record())
			{
				Expect.Call(reader.GetStringArray("TarantinoWebManagementMaintenanceExtensions")).Return(new string[] { ".aspx", ".html" });
				Expect.Call(context.GetCurrentUrl()).Return("/website/mypage.Aspx");
			}

			using (mocks.Playback())
			{
				IFileExtensionChecker checker = new FileExtensionChecker(reader, context);
				bool canBeRedirected = checker.CurrentUrlCanBeRedirected();
				
				Assert.That(canBeRedirected);
			}
		}

		[Test]
		public void Correctly_determines_when_extension_is_not_in_list_of_extensions()
		{
			string[] extensions = new string[] { ".aspx", ".html" };

			MockRepository mocks = new MockRepository();
			IConfigurationReader reader = mocks.CreateMock<IConfigurationReader>();
			IWebContext context = mocks.CreateMock<IWebContext>();

			using (mocks.Record())
			{
				Expect.Call(reader.GetStringArray("TarantinoWebManagementMaintenanceExtensions")).Return(extensions);
				Expect.Call(context.GetCurrentUrl()).Return("/website/myimage.gif");
			}

			using (mocks.Playback())
			{
				IFileExtensionChecker checker = new FileExtensionChecker(reader, context);
				bool canBeRedirected = checker.CurrentUrlCanBeRedirected();

				Assert.That(canBeRedirected, Is.False);
			}
		}
	}
}