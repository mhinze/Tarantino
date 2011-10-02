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
	public class MenuViewTester
	{
		[Test]
		public void Correctly_build_menu_view()
		{
			MockRepository mocks = new MockRepository();
			IResourceFileLocator locator = mocks.CreateMock<IResourceFileLocator>();
			ITokenReplacer replacer = mocks.CreateMock<ITokenReplacer>();

			using (mocks.Record())
			{
				string htmlTemplate = "||APPLICATION_URL|| ||CACHE_URL|| ||ASSEMBLY_URL|| ||LOADBALANCER_URL|| ||DISABLE_URL||";
				Expect.Call(locator.ReadTextFile("Tarantino.Core", MenuView.MenuTemplate)).Return(htmlTemplate);
				replacer.Text = htmlTemplate;

				replacer.Replace("APPLICATION_URL", "Tarantino.WebManagement.Application.axd");
				replacer.Replace("CACHE_URL", "Tarantino.WebManagement.Cache.axd");
				replacer.Replace("ASSEMBLY_URL", "Tarantino.WebManagement.Assemblies.axd");
				replacer.Replace("LOADBALANCER_URL", "Tarantino.WebManagement.LoadBalancer.axd");
				replacer.Replace("DISABLE_URL", "Tarantino.WebManagement.DisableSSL.axd");

				Expect.Call(replacer.Text).Return("fully formatted html");
			}

			using (mocks.Playback())
			{
				IMenuView menuView = new MenuView(locator, replacer);
				string html = menuView.BuildHtml();

				Assert.That(html, Is.EqualTo("fully formatted html"));
			}
		}
	}
}