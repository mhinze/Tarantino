using System.Net.Mail;


namespace Tarantino.Core.Commons.Services.Web
{
	
	public interface IMailSender
	{
		void SendMail(MailMessage message);
	}
}