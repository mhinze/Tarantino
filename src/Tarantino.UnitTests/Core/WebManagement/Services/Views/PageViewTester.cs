using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Tarantino.Core.Commons.Services.DataFileManagement;
using Tarantino.Core.Commons.Services.Environment;
using Tarantino.Core.WebManagement.Services.Views;
using Tarantino.Core.WebManagement.Services.Views.Impl;

namespace Tarantino.UnitTests.Core.WebManagement.Services.Views
{
	[TestFixture]
	public class PageViewTester
	{
		[Test]
		public void Correctly_renders_page_view()
		{
			string cssText = "css stuff";
	
			MockRepository mocks = new MockRepository();
			IResourceFileLocator fileLocator = mocks.CreateMock<IResourceFileLocator>();
			ITokenReplacer replacer = mocks.CreateMock<ITokenReplacer>();
			IMenuView menuView = mocks.CreateMock<IMenuView>();

			using (mocks.Record())
			{
				string htmlTemplate="||CSS|| ||MENU|| ||BODY||";
				Expect.Call(fileLocator.ReadTextFile("Tarantino.Core", PageView.PageTemplate)).Return(htmlTemplate);
	
				replacer.Text = htmlTemplate;
	
				Expect.Call(fileLocator.ReadTextFile("Tarantino.Core", PageView.StylesheetTemplate)).Return(cssText);

				replacer.Replace("CSS", cssText);

				Expect.Call(menuView.BuildHtml()).Return("menu stuff");
				replacer.Replace("MENU","menu stuff");

				replacer.Replace("BODY","body stuff");

				Expect.Call(replacer.Text).Return("page content");
			}

			using (mocks.Playback())
			{
				IPageView pageView = new PageView(fileLocator, menuView, replacer);
				string html = pageView.BuildHtml("body stuff");

				Assert.That(html,Is.EqualTo("page content"));
			}
		}
	}
}