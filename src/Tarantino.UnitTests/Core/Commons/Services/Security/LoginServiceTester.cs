using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Tarantino.Core.Commons.Services.Repositories;
using Tarantino.Core.Commons.Services.Security;
using Tarantino.Core.Commons.Services.Security.Impl;
using Tarantino.Core.Commons.Services.Web;

namespace Tarantino.UnitTests.Core.Commons.Services.Security
{
	[TestFixture]
	public class LoginServiceTester
	{
		[Test]
		public void Correctly_redirects_from_login_page_when_valid_login_is_entered()
		{
			MockRepository mocks = new MockRepository();
			ILoginChecker checker = mocks.CreateMock<ILoginChecker>();
			IAuthenticationService authenticationService = mocks.CreateMock<IAuthenticationService>();
			ISystemUserRepository repository = mocks.CreateMock<ISystemUserRepository>();

			using (mocks.Record())
			{
				Expect.Call(checker.IsValidUser("test@test.com", "pass", repository)).Return(true);
				authenticationService.RedirectFromLoginPage("test@test.com", true);
			}

			using (mocks.Playback())
			{
				ILoginService loginService = new LoginService(checker, authenticationService, null);
				string userFeedback = loginService.Login("test@test.com", "pass", true, repository);

				Assert.That(userFeedback, Is.Null);
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Correctly_provides_feedback_if_login_is_unsuccessful()
		{
			MockRepository mocks = new MockRepository();
			ILoginChecker checker = mocks.CreateMock<ILoginChecker>();
			IAuthenticationService authenticationService = mocks.CreateMock<IAuthenticationService>();
			ISystemUserRepository repository = mocks.CreateMock<ISystemUserRepository>();

			using (mocks.Record())
			{
				Expect.Call(checker.IsValidUser("test@test.com", "pass", repository)).Return(false);
			}

			using (mocks.Playback())
			{
				ILoginService loginService = new LoginService(checker, authenticationService, null);
				string userFeedback = loginService.Login("test@test.com", "pass", false, repository);

				Assert.That(userFeedback, Is.EqualTo("Invalid e-mail address/password: Please try again"));
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Correctly_logs_users_out_and_redirects_them_to_the_login_page()
		{
			MockRepository mocks = new MockRepository();

			IWebContext context = mocks.CreateMock<IWebContext>();
			IAuthenticationService authenticationService = mocks.CreateMock<IAuthenticationService>();

			using (mocks.Record())
			{
				authenticationService.Logout();
				Expect.Call(authenticationService.GetLoginUrl()).Return("Login.aspx");
				context.Redirect("Login.aspx");
			}

			using (mocks.Playback())
			{
				ILoginService loginService = new LoginService(null, authenticationService, context);
				loginService.Logout();
			}

			mocks.VerifyAll();
		}
	}
}