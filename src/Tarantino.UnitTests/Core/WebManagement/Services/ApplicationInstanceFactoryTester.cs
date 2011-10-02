using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Tarantino.Core.Commons.Services.Configuration;
using Tarantino.Core.Commons.Services.Environment;
using Tarantino.Core.WebManagement.Model;
using Tarantino.Core.WebManagement.Services;
using Tarantino.Core.WebManagement.Services.Impl;

namespace Tarantino.UnitTests.Core.WebManagement.Services
{
	[TestFixture]
	public class ApplicationInstanceFactoryTester
	{
		[Test]
		public void Creates_new_application_instance()
		{
			MockRepository mocks = new MockRepository();
			ISystemEnvironment systemEnvironment = mocks.CreateMock<ISystemEnvironment>();
			IAssemblyContext context = mocks.CreateMock<IAssemblyContext>();
			IConfigurationReader configurationReader = mocks.CreateMock<IConfigurationReader>();

			using (mocks.Record())
			{
				Expect.Call(context.GetAssemblyVersion()).Return("1.0");
				Expect.Call(systemEnvironment.GetMachineName()).Return("MyMachine");
				Expect.Call(configurationReader.GetRequiredSetting("TarantinoWebManagementHttpHost")).Return("www.myapp.com");
			}

			using (mocks.Playback())
			{
				IApplicationInstanceFactory factory = new ApplicationInstanceFactory(systemEnvironment, context, configurationReader);
				ApplicationInstance instance = factory.Create();

				Assert.That(instance.AvailableForLoadBalancing, Is.True);
				Assert.That(instance.MachineName, Is.EqualTo("MyMachine"));
				Assert.That(instance.Version, Is.EqualTo("1.0"));
				Assert.That(instance.MaintenanceHostHeader, Is.EqualTo("www.myapp.com"));
				Assert.That(instance.ApplicationDomain, Is.EqualTo("www.myapp.com"));
			}

			mocks.VerifyAll();
		}
	}
}