using System;
using NUnit.Framework;
using Tarantino.Deployer.Core.Model;

namespace Tarantino.Deployer.IntegrationTests.Infrastructure.DataAccess.Mappings
{
	[TestFixture]
	public class DeploymentMappingTester : DeployerDatabaseTester
	{
		[Test]
		public void Can_persist_deployment()
		{
			var deployment = new Deployment
			{
				Application = "SampleApp1",
				Environment = "Development",
				CertifiedBy = "Certifer",
				DeployedBy = "Deployer",
				DeployedOn = new DateTime(2007, 3, 15),
				CertifiedOn = new DateTime(2007, 4, 15),
				Version = "250",
				Result = DeploymentResult.Failure
			};

			var output = new DeploymentOutput { Output = "Output text", Deployment = deployment };
			deployment.SetOutput(output);

			AssertObjectCanBePersisted(deployment);
		}
	}
}