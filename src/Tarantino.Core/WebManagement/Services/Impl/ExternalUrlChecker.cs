
using Tarantino.Core.Commons.Services.Configuration;
using Tarantino.Core.Commons.Services.Web;

namespace Tarantino.Core.WebManagement.Services.Impl
{
	
	public class ExternalUrlChecker : IExternalUrlChecker
	{
		private readonly IWebContext _context;
		private readonly IConfigurationReader _configurationReader;

		public ExternalUrlChecker(IWebContext context, IConfigurationReader configurationReader)
		{
			_context = context;
			_configurationReader = configurationReader;
		}

		public bool CurrentUrlIsExternal()
		{
			string applicationHostFromConfigFile = _configurationReader.GetRequiredSetting("TarantinoWebManagementHttpHost");
			string hostFromRequest = _context.GetServerVariable("HTTP_HOST");
			bool isExternalUrl = applicationHostFromConfigFile.ToLower() == hostFromRequest.ToLower();

			return isExternalUrl;
		}
	}
}