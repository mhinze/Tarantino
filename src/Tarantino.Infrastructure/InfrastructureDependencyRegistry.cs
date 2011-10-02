using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using Tarantino.Core.Commons.Services.Web;
using Tarantino.Core.Commons.Services.Web.Impl;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;

namespace Tarantino.Infrastructure
{
	public class InfrastructureDependencyRegistry : Registry
	{
		protected override void configure()
		{
			Scan(y =>
			     	{
						y.TheCallingAssembly();
						y.WithDefaultConventions();
			     	});

			ForRequestedType<IMailSender>().TheDefaultIsConcreteType<SmtpMailSender>();
			BuildInstancesOf<ISessionBuilder>().TheDefaultIsConcreteType<HybridSessionBuilder>();
		}
	}
}