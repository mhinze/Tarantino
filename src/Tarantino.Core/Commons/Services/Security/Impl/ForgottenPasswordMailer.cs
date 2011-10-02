using System.Net.Mail;

using Tarantino.Core.Commons.Model;
using Tarantino.Core.Commons.Services.Repositories;
using Tarantino.Core.Commons.Services.Web;

namespace Tarantino.Core.Commons.Services.Security.Impl
{
	
	public class ForgottenPasswordMailer : IForgottenPasswordMailer
	{
		private readonly IEncryptionEngine _encryptionEngine;
		private readonly IForgottenPasswordMailFactory _mailFactory;
		private readonly IMailSender _mailSender;

		public ForgottenPasswordMailer(IEncryptionEngine encryptionEngine, IForgottenPasswordMailFactory mailFactory, IMailSender mailSender)
		{
			_encryptionEngine = encryptionEngine;
			_mailFactory = mailFactory;
			_mailSender = mailSender;
		}

		public bool SendForgottenPasswordEmail(string recipientEmailAddress, ISystemUserRepository repository)
		{
			ISystemUser matchingUser = repository.GetByEmailAddress(recipientEmailAddress);

			bool sendEmail = (matchingUser != null);

			if (sendEmail)
			{
				string clearTextPassword = _encryptionEngine.Decrypt(matchingUser.Password);
				MailMessage mail = _mailFactory.CreateEmail(recipientEmailAddress, clearTextPassword);
				_mailSender.SendMail(mail);
			}

			return sendEmail;
		}
	}
}