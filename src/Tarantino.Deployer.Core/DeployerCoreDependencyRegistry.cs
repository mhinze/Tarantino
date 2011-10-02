using StructureMap.Configuration.DSL;

namespace Tarantino.Deployer.Core
{
	public class DeployerCoreDependencyRegistry : Registry
	{
		protected override void configure()
		{
			Scan(x =>
			     	{
			     		x.TheCallingAssembly();
			     		x.LookForRegistries();
			     		x.WithDefaultConventions();
			     	});
		}
	}
}