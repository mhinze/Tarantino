using System.Collections.Generic;
using Tarantino.Deployer.Infrastructure;
using Tarantino.IntegrationTests;

namespace Tarantino.Deployer.IntegrationTests.Infrastructure.DataAccess
{
	public abstract class DeployerDatabaseTester : DatabaseTesterBase
	{
		public override void SetUp()
		{
			DeployerInfrastructureDependencyRegistrar.RegisterInfrastructure();
			ClearTables();
			SetupDatabase();
		}

		protected override IEnumerable<string> GetTablesToDelete()
		{
			return new[] {"DeploymentOutput", "Deployment"};
		}

		public override string ConfigurationFile
		{
			get { return "deployer.hibernate.cfg.xml"; }
		}
	}
}