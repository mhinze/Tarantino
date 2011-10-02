
using Tarantino.Core.Commons.Services.Web;
using Tarantino.Core.WebManagement.Model;

namespace Tarantino.Core.WebManagement.Services.Impl
{
	
	public class LoadBalanceStatusManager : ILoadBalanceStatusManager
	{
		public const string ENABLED_PARAM = "enabled";
	
		private readonly IApplicationInstanceContext _instanceContext;
		private readonly IWebContext _context;
		private readonly ISecureAvailabilityStatusUpdater _statusUpdater;

		public LoadBalanceStatusManager(IApplicationInstanceContext instanceContext, IWebContext context, ISecureAvailabilityStatusUpdater statusUpdater)
		{
			_instanceContext = instanceContext;
			_context = context;
			_statusUpdater = statusUpdater;
		}

		public string HandleLoadBalanceRequest()
		{
			string errorMessage = string.Empty;

			ApplicationInstance currentInstance = _instanceContext.GetCurrent();

			string enabledParameter = _context.GetRequestItem(ENABLED_PARAM);

			if (enabledParameter != null)
			{
				bool enabled = bool.Parse(enabledParameter);
				errorMessage = _statusUpdater.SetStatus(enabled);
			}
			else if (!currentInstance.AvailableForLoadBalancing)
			{
				_context.SetHttpResponseStatus(400, "This application has been turned off");
			}

			return errorMessage;
		}
	}
}