using StructureMap.Configuration.DSL;
using Tarantino.Core.Commons.Services.Security;
using Tarantino.Core.Commons.Services.Security.Impl;

namespace Tarantino.Core
{
	public class CoreDependencyRegistry : Registry
	{
		protected override void configure()
		{
			Scan(x =>
			     	{
			     		x.TheCallingAssembly();
						x.LookForRegistries();
			     		x.WithDefaultConventions();
			     	});

			BuildInstancesOf<IEncryptionEngine>().TheDefaultIsConcreteType<AesEncryptionEngine>();
			BuildInstancesOf<IHashAlgorithm>().TheDefaultIsConcreteType<SHA512HashAlgorithm>();
		}
	}
}