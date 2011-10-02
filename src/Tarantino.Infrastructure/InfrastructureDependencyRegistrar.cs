using StructureMap;
using Tarantino.Core;

namespace Tarantino.Infrastructure
{
	public class InfrastructureDependencyRegistrar
	{
		public static void RegisterInfrastructure()
		{
			ObjectFactory.Initialize(x => x.Scan(s =>
			                                     	{
			                                     		s.AssemblyContainingType<InfrastructureDependencyRegistry>();
			                                     		s.AssemblyContainingType<CoreDependencyRegistry>();
														s.LookForRegistries();
														s.WithDefaultConventions();
			                                     	}));
		}
	}
}