using Tarantino.Core.Commons.Services.Repositories;
using Tarantino.Core.Commons.Services.Security;

using Tarantino.Core.Commons.Services.Web;

namespace Tarantino.Core.Commons.Services.Security.Impl
{
	
	public class LoginService : ILoginService
	{
		private readonly ILoginChecker _loginChecker;
		private readonly IAuthenticationService _authenticationService;
		private readonly IWebContext _context;

		public LoginService(ILoginChecker loginChecker, IAuthenticationService authenticationService, IWebContext context)
		{
			_loginChecker = loginChecker;
			_authenticationService = authenticationService;
			_context = context;
		}

		public void Logout()
		{
			_authenticationService.Logout();
			string loginUrl = _authenticationService.GetLoginUrl();
			_context.Redirect(loginUrl);
		}

		public string Login(string emailAddress, string password, bool rememberMe, ISystemUserRepository repository)
		{
			string userFeedback = null;

			bool isValidLogin = _loginChecker.IsValidUser(emailAddress, password, repository);

			if (isValidLogin)
			{
				_authenticationService.RedirectFromLoginPage(emailAddress, rememberMe);
			}
			else
			{
				userFeedback = "Invalid e-mail address/password: Please try again";
			}

			return userFeedback;
		}
	}
}