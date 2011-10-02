using StructureMap;

namespace Tarantino.Deployer.Core
{
	public class DeployerCoreDependencyRegistrar
	{
		public static void Register()
		{
			ObjectFactory.Initialize(x => x.Scan(s =>
				{
					s.TheCallingAssembly();
					s.LookForRegistries();
				}));
		}
	}
}