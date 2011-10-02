using StructureMap.Configuration.DSL;

namespace Tarantino.Deployer.Infrastructure
{
	public class DeployerInfrastructureDependencyRegistry : Registry
	{
		protected override void configure()
		{
			Scan(y =>
			     	{
						y.TheCallingAssembly();
						y.WithDefaultConventions();
			     	});
		}
	}
}