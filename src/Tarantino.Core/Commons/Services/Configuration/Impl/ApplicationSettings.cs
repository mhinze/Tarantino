using System.Collections.Generic;


namespace Tarantino.Core.Commons.Services.Configuration.Impl
{
	
	public class ApplicationSettings : IApplicationSettings
	{
		protected readonly IConfigurationReader _configurationReader;

		public ApplicationSettings(IConfigurationReader configurationReader)
		{
			_configurationReader = configurationReader;
		}

		public int GetServiceSleepTime()
		{
			int setting = _configurationReader.GetRequiredIntegerSetting("ServiceSleepTime");
			return setting;
		}

		public string GetSmtpServer()
		{
			string setting = _configurationReader.GetRequiredSetting("SmtpServer");
			return setting;
		}

		public bool GetSmtpAuthenticationNecessary()
		{
			bool setting = _configurationReader.GetRequiredBooleanSetting("SmtpAuthenticationNecessary");
			return setting;
		}

		public IEnumerable<string> GetMappingAssemblies()
		{
			IEnumerable<string> settings = _configurationReader.GetStringArray("MappingAssemblies");
			return settings;
		}

		public bool GetShowSql()
		{
			bool showSql = _configurationReader.GetOptionalBooleanSetting("ShowSql") ?? false;
			return showSql;
		}

		public string GetServiceAgentFactory()
		{
			string factory = _configurationReader.GetRequiredSetting("ServiceAgentFactory");
			return factory;
		}

		public string GetSmtpUsername()
		{
			string setting = _configurationReader.GetRequiredSetting("SmtpUsername");
			return setting;
		}

		public string GetSmtpPassword()
		{
			string setting = _configurationReader.GetRequiredSetting("SmtpPassword");
			return setting;
		}
	}
}