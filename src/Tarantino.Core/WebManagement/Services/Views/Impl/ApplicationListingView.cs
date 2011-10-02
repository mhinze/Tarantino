
using Tarantino.Core.Commons.Services.Web;

namespace Tarantino.Core.WebManagement.Services.Views.Impl
{
    
    public class ApplicationListingView:IApplicationListingView
    {
        private readonly IApplicationListingBodyView bodyView;
        private readonly IPageView pageView;
        private readonly IWebContext webContext;

        public ApplicationListingView(IApplicationListingBodyView bodyView, IPageView pageView, IWebContext webContext)
        {
            this.bodyView = bodyView;
            this.pageView = pageView;
            this.webContext = webContext;
        }

        public void Render()
        {
            string bodyHtml = bodyView.BuildHtml();
            string html = pageView.BuildHtml(bodyHtml);
            webContext.WriteToResponse(html);
            
        }
    }
}