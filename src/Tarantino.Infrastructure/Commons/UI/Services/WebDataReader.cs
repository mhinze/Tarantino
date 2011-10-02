using System.Net;
using System.Web;
using StructureMap;
using Tarantino.Core;
using Tarantino.Core.Commons.Services.Web;

namespace Tarantino.Infrastructure.Commons.UI.Services
{
	
	public class WebDataReader : IWebDataReader
	{
		public string ReadUrl(string url, string parameterName, string parameterValue)
		{
			WebClient client = getWebClient();
			string formData = string.Format("{0}={1}", HttpUtility.UrlEncode(parameterName), HttpUtility.UrlEncode(parameterValue));
			string data = client.UploadString(url, formData);
			return data;
		}

		public string ReadUrl(string url)
		{
			WebClient client = getWebClient();
			string data = client.DownloadString(url);
			return data;
		}

		private static WebClient getWebClient()
		{
			WebClient client = new WebClient();
			client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
			return client;
		}
	}
}