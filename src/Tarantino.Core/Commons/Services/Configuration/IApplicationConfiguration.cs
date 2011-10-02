namespace Tarantino.Core.Commons.Services.Configuration
{
	public interface IApplicationConfiguration
	{
		string GetSetting(string settingName);
		object GetSection(string sectionName);
	}
}