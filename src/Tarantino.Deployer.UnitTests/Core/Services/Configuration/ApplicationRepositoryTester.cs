using Tarantino.Deployer.Core.Services.Configuration;
using Tarantino.Deployer.Core.Services.Configuration.Impl;
using Tarantino.Core.Commons.Services.Configuration;
using Tarantino.Core.Commons.Services.Configuration.Impl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace Tarantino.Deployer.UnitTests.Core.Services.Configuration
{
	[TestFixture]
	public class ApplicationRepositoryTester
	{
		[Test]
		public void Reads_application_collection_from_configuration_file()
		{
			var handler = new DeployerSettingsConfigurationHandler();

			var mocks = new MockRepository();
			var configuration = mocks.CreateMock<IApplicationConfiguration>();

			using (mocks.Record())
			{
				Expect.Call(configuration.GetSection("DeployerSettings")).Return(handler);
			}

			using (mocks.Playback())
			{
				IApplicationRepository repository = new ApplicationRepository(configuration);
				ElementCollection<Application> applications = repository.GetAll();

				Assert.That(applications, Is.SameAs(handler.Applications));
			}

			mocks.VerifyAll();
		}
	}
}