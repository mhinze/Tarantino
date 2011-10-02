using Tarantino.Deployer.Core.Model;
using Tarantino.Deployer.Core.Services;
using Tarantino.Deployer.Core.Services.Impl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Tarantino.Deployer.UnitTests.Core.Services
{
	[TestFixture]
	public class DeploymentResultCalculatorTester
	{
		[Test]
		public void Correctly_determines_deployment_is_a_success_when_build_success_is_present()
		{
			IDeploymentResultCalculator calculator = new DeploymentResultCalculator();
			DeploymentResult result = calculator.GetResult("some text BUILD SUCCEEDED some more text");
			
			Assert.That(result, Is.SameAs(DeploymentResult.Success));
		}

		[Test]
		public void Correctly_determines_deployment_is_a_success_when_build_success_is_not_present()
		{
			IDeploymentResultCalculator calculator = new DeploymentResultCalculator();
			DeploymentResult result = calculator.GetResult("some text BUILD SUCCEEDED some more text");
			
			Assert.That(result, Is.SameAs(DeploymentResult.Success));
		}

		[Test]
		public void Correctly_determines_deployment_is_a_failure()
		{
			IDeploymentResultCalculator calculator = new DeploymentResultCalculator();
			DeploymentResult result = calculator.GetResult("some text RuntimeException some more text");
			
			Assert.That(result, Is.SameAs(DeploymentResult.Failure));
		}
	}
}