using System;
using Tarantino.Deployer.Core.Model;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Tarantino.Deployer.Core.Services.UI;
using Tarantino.Deployer.Core.Services.UI.Impl;

namespace Tarantino.Deployer.UnitTests.Core.Services.UI
{
	[TestFixture]
	public class DeploymentRowFactoryTester
	{
		[Test]
		public void Constructs_deployment_row()
		{
			var deployment = new Deployment
			                 	{
			                 		Version = "845",
			                 		DeployedOn = new DateTime(2007, 4, 15, 6, 45, 32),
			                 		DeployedBy = "khurwitz",
			                 		Result = DeploymentResult.Failure,
			                 		CertifiedOn = new DateTime(2007, 5, 15, 8, 45, 32),
			                 		CertifiedBy = "jpalermo",
			                 		Output = new DeploymentOutput {Output = "Output..."}
			                 	};

			IDeploymentRowFactory factory = new DeploymentRowFactory();

			string[] row = factory.ConstructRow(deployment);

			Assert.That(row.Length, Is.EqualTo(7));
			Assert.That(row[0], Is.EqualTo("845"));
			Assert.That(row[1], Is.EqualTo("4/15/2007 6:45 AM"));
			Assert.That(row[2], Is.EqualTo("khurwitz"));
			Assert.That(row[3], Is.EqualTo("Failure"));
			Assert.That(row[4], Is.EqualTo("5/15/2007 8:45 AM"));
			Assert.That(row[5], Is.EqualTo("jpalermo"));
		}
	}
}