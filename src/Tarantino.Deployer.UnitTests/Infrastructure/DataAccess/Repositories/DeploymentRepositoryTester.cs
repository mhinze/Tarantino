using Tarantino.Core.Commons.Services.Repositories;
using Tarantino.Deployer.Core.Model;
using Tarantino.Deployer.Core.Services;
using Tarantino.Core.Commons.Model.Enumerations;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Tarantino.Deployer.Infrastructure.DataAccess.Repositories;

namespace Tarantino.Deployer.UnitTests.Infrastructure.DataAccess.Repositories
{
	[TestFixture]
	public class DeploymentRepositoryTester
	{
		[Test]
		public void Returns_deployments_by_application_and_environment()
		{
			var criteria = new CriterionSet();
			criteria.AddCriterion(new Criterion(Deployment.APPLICATION, "SampleApp1"));
			criteria.AddCriterion(new Criterion(Deployment.ENVIRONMENT, "Environment"));
			criteria.OrderBy = Deployment.DEPLOYED_ON;
			criteria.SortOrder = SortOrder.Descending;

			var foundDeployments = new Deployment[0];

			var mocks = new MockRepository();
			var repository = mocks.CreateMock<IPersistentObjectRepository>();

			using (mocks.Record())
			{
				repository.ConfigurationFile = "deployer.hibernate.cfg.xml";
				Expect.Call(repository.FindAll<Deployment>(criteria)).Return(foundDeployments);
			}

			using (mocks.Playback())
			{
				IDeploymentRepository deploymentRepository = new DeploymentRepository(repository);

				var deployments = deploymentRepository.Find("SampleApp1", "Environment");

				Assert.That(deployments, Is.SameAs(foundDeployments));
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Correctly_saves_deployment()
		{
			var deployment = new Deployment();

			var mocks = new MockRepository();
			var repository = mocks.CreateMock<IPersistentObjectRepository>();

			using (mocks.Record())
			{
				repository.ConfigurationFile = "deployer.hibernate.cfg.xml";
				repository.Save(deployment);
			}

			using (mocks.Playback())
			{
				IDeploymentRepository deploymentRepository = new DeploymentRepository(repository);

				deploymentRepository.Save(deployment);
			}
		}

		[Test]
		public void Returns_uncertified_deployments_by_application_and_environment()
		{
			var criteria = new CriterionSet();
			criteria.AddCriterion(new Criterion(Deployment.APPLICATION, "SampleApp1"));
			criteria.AddCriterion(new Criterion(Deployment.ENVIRONMENT, "Environment"));
			criteria.AddCriterion(new Criterion(Deployment.CERTIFIED_ON, null));
			criteria.AddCriterion(new Criterion(Deployment.RESULT, DeploymentResult.Success));
			criteria.OrderBy = Deployment.DEPLOYED_ON;
			criteria.SortOrder = SortOrder.Descending;

			var foundDeployments = new Deployment[0];

			var mocks = new MockRepository();
			var repository = mocks.CreateMock<IPersistentObjectRepository>();

			using (mocks.Record())
			{
				repository.ConfigurationFile = "deployer.hibernate.cfg.xml";
				Expect.Call(repository.FindAll<Deployment>(criteria)).Return(foundDeployments);
			}

			using (mocks.Playback())
			{
				IDeploymentRepository deploymentRepository = new DeploymentRepository(repository);

				var deployments = deploymentRepository.FindSuccessfulUncertified("SampleApp1", "Environment");

				Assert.That(deployments, Is.SameAs(foundDeployments));
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Returns_certified_deployments_by_application_and_environment()
		{
			var criteria = new CriterionSet();
			criteria.AddCriterion(new Criterion(Deployment.APPLICATION, "SampleApp1"));
			criteria.AddCriterion(new Criterion(Deployment.ENVIRONMENT, "Environment"));
			criteria.AddCriterion(new Criterion(Deployment.CERTIFIED_ON, null, ComparisonOperator.NotEqual));
			criteria.AddCriterion(new Criterion(Deployment.RESULT, DeploymentResult.Success));
			criteria.OrderBy = Deployment.DEPLOYED_ON;
			criteria.SortOrder = SortOrder.Descending;

			var foundDeployments = new Deployment[0];

			var mocks = new MockRepository();
			var repository = mocks.CreateMock<IPersistentObjectRepository>();

			using (mocks.Record())
			{
				repository.ConfigurationFile = "deployer.hibernate.cfg.xml";
				Expect.Call(repository.FindAll<Deployment>(criteria)).Return(foundDeployments);
			}

			using (mocks.Playback())
			{
				IDeploymentRepository deploymentRepository = new DeploymentRepository(repository);

				var deployments = deploymentRepository.FindCertified("SampleApp1", "Environment");

				Assert.That(deployments, Is.SameAs(foundDeployments));
			}

			mocks.VerifyAll();
		}
	}
}