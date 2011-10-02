using NHibernate;
using NHibernate.Cfg;

namespace Tarantino.Infrastructure.Commons.DataAccess.ORMapper
{
	public interface ISessionBuilder
	{
		ISessionFactory GetSessionFactory();
		ISessionFactory GetSessionFactory(string configurationFile);
		ISession GetSession();
		Configuration GetConfiguration();
		ISession GetSession(string configurationFile);
		IStatelessSession GetStatelessSession();
		IStatelessSession GetStatelessSession(string configurationFile);
		Configuration GetConfiguration(string configurationFile);
		ISession GetExistingWebSession(string configurationFile);
		ISession GetExistingWebSession();
	}
}