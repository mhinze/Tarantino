
using Tarantino.Core.WebManagement.Model;

namespace Tarantino.Core.WebManagement.Services.Impl
{
	
	public class MaintenanceRedirectionChecker : IMaintenanceRedirectionChecker
	{
		private readonly IFileExtensionChecker _extensionChecker;
		private readonly IExternalUrlChecker _urlChecker;
		private readonly IApplicationInstanceContext _context;

		public MaintenanceRedirectionChecker(IFileExtensionChecker extensionChecker, IExternalUrlChecker urlChecker, IApplicationInstanceContext 
		context)
		{
			_extensionChecker = extensionChecker;
			_urlChecker = urlChecker;
			_context = context;
		}

		public bool ShouldBeRedirectedToMaintenancePage()
		{
			ApplicationInstance instance = _context.GetCurrent();

			bool downForMaintenance = instance.DownForMaintenance;
			bool isExternalUrl = _urlChecker.CurrentUrlIsExternal();
			bool redirectableUrl = _extensionChecker.CurrentUrlCanBeRedirected();

			return redirectableUrl && downForMaintenance && isExternalUrl;
		}
	}
}