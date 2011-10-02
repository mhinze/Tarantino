using NUnit.Framework.SyntaxHelpers;
using Tarantino.Deployer.Core.Model;
using Tarantino.Deployer.Core.Services;
using Tarantino.Deployer.Core.Services.Impl;
using Tarantino.Core.Commons.Services.Security;
using NUnit.Framework;
using Rhino.Mocks;

namespace Tarantino.Deployer.UnitTests.Core.Services
{
	[TestFixture]
	public class DeploymentRecorderTester
	{
		[Test]
		public void Records_deployment()
		{
			var deployment = new Deployment{ Version = "1.0"};

			var mocks = new MockRepository();
			var factory = mocks.CreateMock<IDeploymentFactory>();
			var repository = mocks.CreateMock<IDeploymentRepository>();
			var context = mocks.CreateMock<ISecurityContext>();

			using (mocks.Record())
			{
				Expect.Call(context.GetCurrentUsername()).Return("jsmith");
				Expect.Call(factory.CreateDeployment("application", "environment", "jsmith", "Output...", "1.0", false)).Return(deployment);
				repository.Save(deployment);
			}

			using (mocks.Playback())
			{
				IDeploymentRecorder recorder = new DeploymentRecorder(context, factory, repository);
				var version = recorder.RecordDeployment("application", "environment", "Output...", "1.0", false);

				Assert.That(version, Is.EqualTo("1.0"));
			}

			mocks.VerifyAll();
		}
	}
}