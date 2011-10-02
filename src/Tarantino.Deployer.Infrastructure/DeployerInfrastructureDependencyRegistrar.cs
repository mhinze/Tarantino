using StructureMap;
using Tarantino.Core;
using Tarantino.Deployer.Core;
using Tarantino.Infrastructure;

namespace Tarantino.Deployer.Infrastructure
{
	public class DeployerInfrastructureDependencyRegistrar
	{
		public static void RegisterInfrastructure()
		{
			ObjectFactory.Initialize(x => x.Scan(s =>
			                                     	{
			                                     		s.AssemblyContainingType<InfrastructureDependencyRegistry>();
			                                     		s.AssemblyContainingType<CoreDependencyRegistry>();
			                                     		s.AssemblyContainingType<DeployerInfrastructureDependencyRegistry>();
			                                     		s.AssemblyContainingType<DeployerCoreDependencyRegistry>();
			                                     		s.LookForRegistries();
			                                     		s.WithDefaultConventions();
			                                     	}));
		}
	}
}