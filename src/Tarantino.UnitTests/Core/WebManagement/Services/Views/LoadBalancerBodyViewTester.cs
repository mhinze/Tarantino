using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Tarantino.Core.Commons.Services.DataFileManagement;
using Tarantino.Core.Commons.Services.Environment;
using Tarantino.Core.WebManagement.Model;
using Tarantino.Core.WebManagement.Services;
using Tarantino.Core.WebManagement.Services.Views;
using Tarantino.Core.WebManagement.Services.Views.Impl;

namespace Tarantino.UnitTests.Core.WebManagement.Services.Views
{
	[TestFixture]
	public class LoadBalancerBodyViewTester
	{
		[Test]
		public void Correctly_returns_html_for_error_non_authenticated_page_with_enabled_instance()
		{
			string errorMessages = "An error has occured";
		  
			string bodyTemplate = "body template";

			ApplicationInstance instance = new ApplicationInstance();
			instance.MachineName = "MyMachine";
			instance.AvailableForLoadBalancing = true;

			MockRepository mocks = new MockRepository();
			IApplicationInstanceContext context = mocks.CreateMock<IApplicationInstanceContext>();
			IResourceFileLocator fileLocator = mocks.CreateMock<IResourceFileLocator>();
			ITokenReplacer replacer = mocks.CreateMock<ITokenReplacer>();
			IAdministratorSecurityChecker securityChecker = mocks.CreateMock<IAdministratorSecurityChecker>();

			using (mocks.Record())
			{
				Expect.Call(securityChecker.IsCurrentUserAdministrator()).Return(false);

				Expect.Call(context.GetCurrent()).Return(instance);
				Expect.Call(fileLocator.ReadTextFile("Tarantino.Core", LoadBalancerBodyView.LoadBalancerBodyTemplate)).Return(bodyTemplate);
				replacer.Text = bodyTemplate;
				replacer.Replace("ERROR_MESSAGE", errorMessages);
				replacer.Replace("CURRENT_STATE", "enabled");
				replacer.Replace("MACHINE", "MyMachine");
				replacer.Replace("CHANGE_STATE_LINK", string.Empty);

				Expect.Call(replacer.Text).Return("formatted HTML");
			}

			using (mocks.Playback())
			{
				ILoadBalancerBodyView view = new LoadBalancerBodyView(context, fileLocator, replacer, securityChecker);
				string html = view.BuildHtml(errorMessages);

				Assert.That(html, Is.EqualTo("formatted HTML"));
			}
		}

		[Test]
		public void Correctly_returns_html_for_error_non_authenticated_page_with_disabled_instance()
		{
			string errorMessage = "An error occured!";
			string bodyTemplate = "body template";

			ApplicationInstance instance = new ApplicationInstance();
			instance.MachineName = "MyMachine";
			instance.AvailableForLoadBalancing = false;

			MockRepository mocks = new MockRepository();
			IApplicationInstanceContext context = mocks.CreateMock<IApplicationInstanceContext>();
			IResourceFileLocator fileLocator = mocks.CreateMock<IResourceFileLocator>();
			ITokenReplacer replacer = mocks.CreateMock<ITokenReplacer>();
			IAdministratorSecurityChecker securityChecker = mocks.CreateMock<IAdministratorSecurityChecker>();

			using (mocks.Record())
			{
				Expect.Call(securityChecker.IsCurrentUserAdministrator()).Return(false);

				Expect.Call(context.GetCurrent()).Return(instance);
				Expect.Call(fileLocator.ReadTextFile("Tarantino.Core", LoadBalancerBodyView.LoadBalancerBodyTemplate)).Return(bodyTemplate);
				replacer.Text = bodyTemplate;
				replacer.Replace("ERROR_MESSAGE",errorMessage);
				replacer.Replace("CURRENT_STATE", "disabled");
				replacer.Replace("MACHINE", "MyMachine");
				replacer.Replace("CHANGE_STATE_LINK", string.Empty);

				Expect.Call(replacer.Text).Return("formatted HTML");
			}

			using (mocks.Playback())
			{
				ILoadBalancerBodyView view = new LoadBalancerBodyView(context, fileLocator, replacer, securityChecker);
				string html = view.BuildHtml(errorMessage);

				Assert.That(html, Is.EqualTo("formatted HTML"));
			}
		}

		[Test]		
		public void Correctly_returns_html_for_error_authenticated_page_with_disabled_instance()
		{
			string errorMessage = "An error has occured!";
			string bodyTemplate = "body template";

			ApplicationInstance instance = new ApplicationInstance();
			instance.MachineName = "MyMachine";
			instance.AvailableForLoadBalancing = false;

			MockRepository mocks = new MockRepository();
			IApplicationInstanceContext context = mocks.CreateMock<IApplicationInstanceContext>();
			IResourceFileLocator fileLocator = mocks.CreateMock<IResourceFileLocator>();
			ITokenReplacer replacer = mocks.CreateMock<ITokenReplacer>();
			IAdministratorSecurityChecker securityChecker = mocks.CreateMock<IAdministratorSecurityChecker>();

			using (mocks.Record())
			{
				Expect.Call(securityChecker.IsCurrentUserAdministrator()).Return(true);

				Expect.Call(context.GetCurrent()).Return(instance);
				Expect.Call(fileLocator.ReadTextFile("Tarantino.Core", LoadBalancerBodyView.LoadBalancerBodyTemplate)).Return(bodyTemplate);
				replacer.Text = bodyTemplate;

				replacer.Replace("ERROR_MESSAGE", errorMessage);
				replacer.Replace("CURRENT_STATE", "disabled");
				replacer.Replace("MACHINE", "MyMachine");
				replacer.Replace("CHANGE_STATE_LINK", "<a href=\"?enabled=True\">enable</a>");

				Expect.Call(replacer.Text).Return("formatted HTML");
			}

			using (mocks.Playback())
			{
				ILoadBalancerBodyView view = new LoadBalancerBodyView(context, fileLocator, replacer, securityChecker);
				string html = view.BuildHtml(errorMessage);

				Assert.That(html, Is.EqualTo("formatted HTML"));
			}
		}

		[Test]
		public void Correctly_returns_html_for_error_authenticated_page_with_enabled_instance()
		{
			string errorMessage = "An error has occured!";
			string bodyTemplate = "body template";

			ApplicationInstance instance = new ApplicationInstance();
			instance.MachineName = "MyMachine";
			instance.AvailableForLoadBalancing = true;

			MockRepository mocks = new MockRepository();
			IApplicationInstanceContext context = mocks.CreateMock<IApplicationInstanceContext>();
			IResourceFileLocator fileLocator = mocks.CreateMock<IResourceFileLocator>();
			ITokenReplacer replacer = mocks.CreateMock<ITokenReplacer>();
			IAdministratorSecurityChecker securityChecker = mocks.CreateMock<IAdministratorSecurityChecker>();

			using (mocks.Record())
			{
				Expect.Call(securityChecker.IsCurrentUserAdministrator()).Return(true);

				Expect.Call(context.GetCurrent()).Return(instance);
				Expect.Call(fileLocator.ReadTextFile("Tarantino.Core", LoadBalancerBodyView.LoadBalancerBodyTemplate)).Return(bodyTemplate);
				replacer.Text = bodyTemplate;
				replacer.Replace("ERROR_MESSAGE", errorMessage);
				replacer.Replace("CURRENT_STATE", "enabled");
				replacer.Replace("MACHINE", "MyMachine");
				replacer.Replace("CHANGE_STATE_LINK", "<a href=\"?enabled=False\">disable</a>");

				Expect.Call(replacer.Text).Return("formatted HTML");
			}

			using (mocks.Playback())
			{
				ILoadBalancerBodyView view = new LoadBalancerBodyView(context, fileLocator, replacer, securityChecker);
				string html = view.BuildHtml(errorMessage);

				Assert.That(html, Is.EqualTo("formatted HTML"));
			}
		}
	}
}