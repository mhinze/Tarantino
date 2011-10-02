
using Tarantino.Core.Commons.Services.Web;

namespace Tarantino.Core.WebManagement.Services.Views.Impl
{
	
	public class LoadBalancerView : ILoadBalancerView
	{
		private readonly ILoadBalancerBodyView _bodyView;
		private readonly IPageView _pageView;
		private readonly IWebContext _context;

		public LoadBalancerView(ILoadBalancerBodyView bodyView, IPageView pageView, IWebContext context)
		{
			_bodyView = bodyView;
			_pageView = pageView;
			_context = context;
		}

		public void Render(string errorMessage)
		{
			string bodyHtml = _bodyView.BuildHtml(errorMessage);
			string html = _pageView.BuildHtml(bodyHtml);
			_context.WriteToResponse(html);
		}
	}
}