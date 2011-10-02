using NUnit.Framework;
using Rhino.Mocks;
using Tarantino.Core.Commons.Services.Web;
using Tarantino.Core.WebManagement.Services.Views;
using Tarantino.Core.WebManagement.Services.Views.Impl;

namespace Tarantino.UnitTests.Core.WebManagement.Services.Views
{
    [TestFixture]
    public class ApplicationListingViewTester
    {
        [Test]
        public void Correctly_builds_page_html()
        {
            MockRepository mocks = new MockRepository();
            IApplicationListingBodyView bodyView = mocks.CreateMock<IApplicationListingBodyView>();
            IPageView pageView = mocks.CreateMock<IPageView>();
            IWebContext webContext = mocks.CreateMock<IWebContext>();

            using (mocks.Record())
            {
                Expect.Call(bodyView.BuildHtml()).Return("some body html");
                Expect.Call(pageView.BuildHtml("some body html")).Return("the page html");
                webContext.WriteToResponse("the page html");
            }

            using (mocks.Playback())
            {
                IApplicationListingView view = new ApplicationListingView(bodyView,pageView,webContext);
                view.Render();
            }
        }
    }
}