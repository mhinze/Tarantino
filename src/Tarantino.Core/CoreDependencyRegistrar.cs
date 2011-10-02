using StructureMap;

namespace Tarantino.Core
{
	public class CoreDependencyRegistrar
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