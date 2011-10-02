using System.Net;
using System.Net.Mail;
using Tarantino.Core.Commons.Services.Configuration;


namespace Tarantino.Core.Commons.Services.Web.Impl
{
	
	public class SmtpMailSender : IMailSender
	{
		private IApplicationSettings _applicationSettings;

		public SmtpMailSender(IApplicationSettings applicationSettings)
		{
			_applicationSettings = applicationSettings;
		}

		public void SendMail(MailMessage mailMessage)
		{
			string smtpServer = _applicationSettings.GetSmtpServer();
			SmtpClient client = new SmtpClient(smtpServer);

			if (_applicationSettings.GetSmtpAuthenticationNecessary())
			{
				client.UseDefaultCredentials = false;
				NetworkCredential SMTPUserInfo = new NetworkCredential(_applicationSettings.GetSmtpUsername(), _applicationSettings.GetSmtpPassword());
				client.Credentials = SMTPUserInfo;
			}

			client.Send(mailMessage);
		}
	}
}