using System.Net.Mail;

namespace Tarantino.Core.Commons.Services.Security
{
	public interface IForgottenPasswordMailFactory
	{
		MailMessage CreateEmail(string recipientEmailAddress, string clearTextPassword);
	}
}