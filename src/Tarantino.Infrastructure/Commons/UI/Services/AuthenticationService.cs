using System.Web.Security;
using Tarantino.Core.Commons.Services.Security;

namespace Tarantino.Infrastructure.Commons.UI.Services
{
	public class AuthenticationService : IAuthenticationService
	{
		public void RedirectFromLoginPage(string emailAddress, bool rememberMe)
		{
			FormsAuthentication.RedirectFromLoginPage(emailAddress, rememberMe);
		}

		public void SetAuthCookie(string username, bool createPersistentCookie)
		{
			FormsAuthentication.SetAuthCookie(username, createPersistentCookie);
		}

		public void Logout()
		{
			FormsAuthentication.SignOut();
		}

		public string GetLoginUrl()
		{
			return FormsAuthentication.LoginUrl;
		}
	}
}