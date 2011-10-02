

namespace Tarantino.Core.Commons.Services.Web
{
	
	public interface IWebDataReader
	{
		string ReadUrl(string url, string parameterName, string parameterValue);
		string ReadUrl(string url);
	}
}