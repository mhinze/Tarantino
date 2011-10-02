using System.Net.Mail;

using Tarantino.Core.Commons.Services.Configuration;

namespace Tarantino.Core.Commons.Services.Security.Impl
{
	
	public class ForgottenPasswordMailFactory : IForgottenPasswordMailFactory
	{
		private readonly IConfigurationReader _configurationReader;

		public ForgottenPasswordMailFactory(IConfigurationReader configurationReader)
		{
			_configurationReader = configurationReader;
		}

		public MailMessage CreateEmail(string recipientEmailAddress, string clearTextPassword)
		{
			MailMessage mailMessage = new MailMessage();

			string from = _configurationReader.GetRequiredSetting("SmtpFromAddress");

			mailMessage.From = new MailAddress(from);
			mailMessage.To.Add(recipientEmailAddress);

			mailMessage.Subject = "Your forgotten password";
			mailMessage.Body = string.Format("Your password is {0}", clearTextPassword);

			return mailMessage;
		}
	}
}