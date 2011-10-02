
using Tarantino.Core.Commons.Services.Repositories;

namespace Tarantino.Core.Commons.Services.Security.Impl
{
	
	public class ForgottenPasswordService : IForgottenPasswordService
	{
		private readonly IForgottenPasswordMailer _mailer;

		public ForgottenPasswordService(IForgottenPasswordMailer mailer)
		{
			_mailer = mailer;
		}

		public string SendEmailTo(string emailAddress, ISystemUserRepository repository)
		{
			if (string.IsNullOrEmpty(emailAddress))
			{
				return "Please enter the e-mail address to send your password to";
			}

			bool emailSent = _mailer.SendForgottenPasswordEmail(emailAddress, repository);

			if (emailSent)
			{
				return string.Format("Your password has been e-mailed to {0}", emailAddress);
			}
			else
			{
				return string.Format("We could not find a user with the e-mail address {0}", emailAddress);
			}
		}
	}
}