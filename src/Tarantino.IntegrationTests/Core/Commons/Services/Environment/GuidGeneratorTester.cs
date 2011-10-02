using NUnit.Framework;
using StructureMap;
using Tarantino.Core.Commons.Services.Environment;
using Tarantino.Infrastructure;

namespace Tarantino.IntegrationTests.Core.Commons.Services.Environment
{
	[TestFixture]
	public class GuidGeneratorTester : InfrastructureIntegrationTester
	{
		[Test]
		public void Should_generate_new_guid()
		{
            InfrastructureDependencyRegistrar.RegisterInfrastructure();

			var generator = ObjectFactory.GetInstance<IGuidGenerator>();

			generator.CreateGuid();
		}
	}
}