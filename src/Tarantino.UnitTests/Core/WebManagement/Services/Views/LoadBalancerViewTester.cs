using NUnit.Framework;
using Rhino.Mocks;
using Tarantino.Core.Commons.Services.Web;
using Tarantino.Core.WebManagement.Services.Views;
using Tarantino.Core.WebManagement.Services.Views.Impl;

namespace Tarantino.UnitTests.Core.WebManagement.Services.Views
{
	[TestFixture]
	public class LoadBalancerViewTester
	{
		[Test]
		public void Correctly_builds_error_page_html()
		{
			MockRepository mocks = new MockRepository();
			ILoadBalancerBodyView bodyView = mocks.CreateMock<ILoadBalancerBodyView>();
			IPageView pageView = mocks.CreateMock<IPageView>();
			IWebContext webContext = mocks.CreateMock<IWebContext>();

			using (mocks.Record())
			{
				Expect.Call(bodyView.BuildHtml("error message")).Return("some body html");
				Expect.Call(pageView.BuildHtml("some body html")).Return("the page html");
				webContext.WriteToResponse("the page html");
			}

			using (mocks.Playback())
			{
				ILoadBalancerView view = new LoadBalancerView(bodyView, pageView, webContext);
				view.Render("error message");
			}
		}
	}
}