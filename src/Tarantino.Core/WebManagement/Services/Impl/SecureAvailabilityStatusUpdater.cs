
using Tarantino.Core.Commons.Services.Web;

namespace Tarantino.Core.WebManagement.Services.Impl
{
	
	public class SecureAvailabilityStatusUpdater : ISecureAvailabilityStatusUpdater
	{
		private readonly IAdministratorSecurityChecker _securityChecker;
		private readonly IWebContext _context;
		private readonly IAvailabilityStatusUpdater _statusUpdater;

		public SecureAvailabilityStatusUpdater(IAdministratorSecurityChecker securityChecker, IWebContext context, IAvailabilityStatusUpdater statusUpdater)
		{
			_securityChecker = securityChecker;
			_context = context;
			_statusUpdater = statusUpdater;
		}

		public string SetStatus(bool enabled)
		{
			string errorMessage = string.Empty;

			if (_securityChecker.IsCurrentUserAdministrator())
			{
				_statusUpdater.SetAvailabilityStatus(enabled);
				_context.Redirect(_context.GetCurrentUrl());
			}
			else
			{
				errorMessage = "Only authenticated users can change the load balancing status.\n";
			}

			return errorMessage;
		}
	}
}