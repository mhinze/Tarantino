using NUnit.Framework;
using Tarantino.Infrastructure;

namespace Tarantino.IntegrationTests
{
	public class InfrastructureIntegrationTester
	{
		[SetUp]
		public void Setup()
		{
			InfrastructureDependencyRegistrar.RegisterInfrastructure();
           // DatabaseManager.Core.InfrastructureDependencyRegistrar.RegisterInfrastructure();
		}
	}
}