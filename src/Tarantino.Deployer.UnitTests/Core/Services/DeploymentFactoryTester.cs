using System;
using Tarantino.Deployer.Core.Model;
using Tarantino.Deployer.Core.Services;
using Tarantino.Deployer.Core.Services.Impl;
using Tarantino.Core.Commons.Services.Environment;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace Tarantino.Deployer.UnitTests.Core.Services
{
	[TestFixture]
	public class DeploymentFactoryTester
	{
		[Test]
		public void Constructs_deployment()
		{
			var mocks = new MockRepository();
			var clock = mocks.CreateMock<ISystemClock>();
			var resultCalculator = mocks.CreateMock<IDeploymentResultCalculator>();

			using (mocks.Record())
			{
				Expect.Call(clock.GetCurrentDateTime()).Return(new DateTime(2007, 4, 15));
			}

			using (mocks.Playback())
			{
				IDeploymentFactory factory = new DeploymentFactory(clock, resultCalculator);
				Deployment deployment = factory.CreateDeployment("A1", "E1", "jsmith", "Output...", "1.0", true);

				Assert.That(deployment.Application, Is.EqualTo("A1"));
				Assert.That(deployment.Environment, Is.EqualTo("E1"));
				Assert.That(deployment.DeployedBy, Is.EqualTo("jsmith"));
				Assert.That(deployment.DeployedOn, Is.EqualTo(new DateTime(2007, 4, 15)));
				Assert.That(deployment.Version, Is.EqualTo("1.0"));
				Assert.That(deployment.Output.Output, Is.EqualTo("Output..."));
				Assert.That(deployment.Result, Is.SameAs(DeploymentResult.Failure));
				Assert.That(deployment.Output.Deployment, Is.SameAs(deployment));
			}

			mocks.VerifyAll();
		}
	}
}