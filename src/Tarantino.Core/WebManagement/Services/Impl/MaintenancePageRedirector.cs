
using Tarantino.Core.Commons.Services.Configuration;
using Tarantino.Core.Commons.Services.Web;

namespace Tarantino.Core.WebManagement.Services.Impl
{
	
	public class MaintenancePageRedirector : IMaintenancePageRedirector
	{
		private readonly IWebContext _context;
		private readonly IConfigurationReader _configurationReader;
		private readonly IMaintenanceRedirectionChecker _checker;

		public MaintenancePageRedirector(IWebContext context, IConfigurationReader configurationReader, IMaintenanceRedirectionChecker checker)
		{
			_context = context;
			_configurationReader = configurationReader;
			_checker = checker;
		}

		public void RedirectToMaintenancePageIfAppropriate()
		{
			try
			{
				bool redirect = _checker.ShouldBeRedirectedToMaintenancePage();

				if (redirect)
				{
					string maintenancePageUrl = _configurationReader.GetRequiredSetting("TarantinoWebManagementMaintenancePage");
					_context.ServerTransfer(maintenancePageUrl, false);
				}
			}
			catch
			{
			}
		}
	}
}