using System;

using Tarantino.Core.Commons.Services.Web;
using Tarantino.Core.WebManagement.Services.Views;

namespace Tarantino.Core.WebManagement.Services.Impl
{
	
	public class ExceptionHandlingLoadBalanceStatusManager : IExceptionHandlingLoadBalanceStatusManager
	{
		private readonly ILoadBalanceStatusManager _manager;
		private readonly ILoadBalancerView _view;
		private readonly IWebContext _context;

		public ExceptionHandlingLoadBalanceStatusManager(ILoadBalanceStatusManager manager, ILoadBalancerView view, IWebContext context)
		{
			_manager = manager;
			_view = view;
			_context = context;
		}

		public void HandleLoadBalancing()
		{
			try
			{
				string errorMessage = _manager.HandleLoadBalanceRequest();
				_view.Render(errorMessage);
			}
			catch (Exception ex)
			{
				_context.WriteToResponse(ex.ToString());
			}
		}
	}
}