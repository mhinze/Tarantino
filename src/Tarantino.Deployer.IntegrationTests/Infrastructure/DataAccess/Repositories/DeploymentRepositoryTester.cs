using System;
using NHibernate;
using Tarantino.Deployer.Core.Model;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using StructureMap;
using Tarantino.Deployer.Core.Services;

namespace Tarantino.Deployer.IntegrationTests.Infrastructure.DataAccess.Repositories
{
	[TestFixture]
	public class DeploymentRepositoryTester : DeployerDatabaseTester
	{
		Deployment _uncertified_Application1_Environment1;
		Deployment _uncertified_Application1_Environment2;
		Deployment _uncertified_Application2_Environment1;
		Deployment _uncertified_Application2_Environment2;

		Deployment _certified_Application1_Environment1;
		Deployment _certified_Application1_Environment2;
		Deployment _certified_Application2_Environment1;
		Deployment _certified_Application2_Environment2;

		Deployment _uncertified_failure;
		Deployment _certified_failure;

		protected override void SetupDatabase()
		{
			_uncertified_Application1_Environment1 = new Deployment();
			_uncertified_Application1_Environment2 = new Deployment();
			_uncertified_Application2_Environment1 = new Deployment();
			_uncertified_Application2_Environment2 = new Deployment();

			_certified_Application1_Environment1 = new Deployment();
			_certified_Application1_Environment2 = new Deployment();
			_certified_Application2_Environment1 = new Deployment();
			_certified_Application2_Environment2 = new Deployment();

			_uncertified_failure = new Deployment();
			_certified_failure = new Deployment();

			_uncertified_Application1_Environment1.Version = "1";
			_uncertified_Application1_Environment2.Version = "2";
			_uncertified_Application2_Environment1.Version = "3";
			_uncertified_Application2_Environment2.Version = "4";

			_certified_Application1_Environment1.Version = "5";
			_certified_Application1_Environment2.Version = "6";
			_certified_Application2_Environment1.Version = "7";
			_certified_Application2_Environment2.Version = "8";

			_uncertified_failure.Version = "9";
			_certified_failure.Version = "10";

			_uncertified_Application1_Environment1.Result = DeploymentResult.Success;
			_uncertified_Application1_Environment2.Result = DeploymentResult.Success;
			_uncertified_Application2_Environment1.Result = DeploymentResult.Success;
			_uncertified_Application2_Environment2.Result = DeploymentResult.Success;

			_certified_Application1_Environment1.Result = DeploymentResult.Success;
			_certified_Application1_Environment2.Result = DeploymentResult.Success;
			_certified_Application2_Environment1.Result = DeploymentResult.Success;
			_certified_Application2_Environment2.Result = DeploymentResult.Success;

			_uncertified_failure.Result = DeploymentResult.Failure;
			_certified_failure.Result = DeploymentResult.Failure;

			_uncertified_Application1_Environment1.Application = "A1";
			_uncertified_Application1_Environment2.Application = "A1";
			_uncertified_Application2_Environment1.Application = "A2";
			_uncertified_Application2_Environment2.Application = "A2";

			_certified_Application1_Environment1.Application = "A1";
			_certified_Application1_Environment2.Application = "A1";
			_certified_Application2_Environment1.Application = "A2";
			_certified_Application2_Environment2.Application = "A2";

			_uncertified_Application1_Environment1.Environment = "E1";
			_uncertified_Application1_Environment2.Environment = "E2";
			_uncertified_Application2_Environment1.Environment = "E1";
			_uncertified_Application2_Environment2.Environment = "E2";

			_certified_Application1_Environment1.Environment = "E1";
			_certified_Application1_Environment2.Environment = "E2";
			_certified_Application2_Environment1.Environment = "E1";
			_certified_Application2_Environment2.Environment = "E2";

			_uncertified_failure.Application = "A1";
			_uncertified_failure.Environment = "E2";

			_certified_failure.Application = "A1";
			_certified_failure.Environment = "E2";

			_certified_Application1_Environment1.CertifiedOn = new DateTime(2007, 4, 15);
			_certified_Application1_Environment2.CertifiedOn = new DateTime(2007, 4, 15);
			_certified_Application2_Environment1.CertifiedOn = new DateTime(2007, 4, 15);
			_certified_Application2_Environment2.CertifiedOn = new DateTime(2007, 4, 15);

			_uncertified_Application1_Environment1.DeployedOn = new DateTime(2007, 4, 15);
			_uncertified_Application1_Environment2.DeployedOn = new DateTime(2007, 4, 16);
			_uncertified_Application2_Environment1.DeployedOn = new DateTime(2007, 4, 17);
			_uncertified_Application2_Environment2.DeployedOn = new DateTime(2007, 4, 18);

			_certified_Application1_Environment1.DeployedOn = new DateTime(2007, 4, 19);
			_certified_Application1_Environment2.DeployedOn = new DateTime(2007, 4, 20);
			_certified_Application2_Environment1.DeployedOn = new DateTime(2007, 4, 21);
			_certified_Application2_Environment2.DeployedOn = new DateTime(2007, 4, 22);

			_uncertified_failure.DeployedOn = new DateTime(2007, 3, 2);
			_certified_failure.DeployedOn = new DateTime(2007, 3, 1);
			_certified_failure.CertifiedOn = new DateTime(2007, 6, 5);

			Save(
				_certified_Application1_Environment1,
				_certified_Application1_Environment2,
				_certified_Application2_Environment1,
				_certified_Application2_Environment2,
				_uncertified_Application1_Environment1,
				_uncertified_Application1_Environment2,
				_uncertified_Application2_Environment1,
				_uncertified_Application2_Environment2,
				_uncertified_failure,
				_certified_failure);
		}

		[Test]
		public void Returns_deployments_by_application_and_environment()
		{
			var deploymentRepository = getRepository();

			var deployments = deploymentRepository.Find("A1", "E2");

			Assert.That(deployments, Is.EqualTo(new[]
			                                    	{
			                                    		_certified_Application1_Environment2,
			                                    		_uncertified_Application1_Environment2,
			                                    		_uncertified_failure,
			                                    		_certified_failure
			                                    	}));
		}

		[Test]
		public void Returns_successful_uncertified_deployments_by_application_and_environment()
		{
			var deploymentRepository = getRepository();

			var deployments = deploymentRepository.FindSuccessfulUncertified("A1", "E2");

			Assert.That(deployments, Is.EquivalentTo(new[]
			                                         	{
			                                         		_uncertified_Application1_Environment2
			                                         	}));
		}

		[Test]
		public void Returns_certified_deployments_by_application_and_environment()
		{
			var deploymentRepository = getRepository();

			var deployments = deploymentRepository.FindCertified("A1", "E2");

			Assert.That(deployments, Is.EquivalentTo(new[]
			                                         	{
			                                         		_certified_Application1_Environment2
			                                         	}));
		}

		[Test]
		public void Saves_new_deployment()
		{
			var deploymentRepository = getRepository();

			var deployment = new Deployment {Application = "MyApplication", DeployedOn = new DateTime(2008, 8, 15)};
			deploymentRepository.Save(deployment);

			GetSession().Dispose();
			using (ISession session = GetSession())
			{
				var reloadedDeployment = session.Load<Deployment>(deployment.Id);
				Assert.That(reloadedDeployment, Is.EqualTo(deployment));
			}
		}

		private static IDeploymentRepository getRepository()
		{
			var repository = ObjectFactory.GetInstance<IDeploymentRepository>();
			return repository;
		}
	}
}