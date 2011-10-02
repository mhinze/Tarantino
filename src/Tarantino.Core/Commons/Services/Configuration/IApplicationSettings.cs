using System.Collections.Generic;


namespace Tarantino.Core.Commons.Services.Configuration
{
	
	public interface IApplicationSettings
	{
		int GetServiceSleepTime();

		string GetSmtpServer();
		string GetSmtpUsername();
		string GetSmtpPassword();
		bool GetSmtpAuthenticationNecessary();
		IEnumerable<string> GetMappingAssemblies();
		bool GetShowSql();
		string GetServiceAgentFactory();
	}
}