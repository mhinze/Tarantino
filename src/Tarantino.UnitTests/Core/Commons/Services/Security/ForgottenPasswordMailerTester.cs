using System.Net.Mail;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Tarantino.Core.Commons.Model;
using Tarantino.Core.Commons.Services.Repositories;
using Tarantino.Core.Commons.Services.Security;
using Tarantino.Core.Commons.Services.Security.Impl;
using Tarantino.Core.Commons.Services.Web;

namespace Tarantino.UnitTests.Core.Commons.Services.Security
{
	[TestFixture]
	public class ForgottenPasswordMailerTester
	{
		[Test]
		public void Should_send_forgotten_password_email_when_email_address_exists()
		{
			MailMessage forgottenPasswordEmail = new MailMessage();

			MockRepository mocks = new MockRepository();
			ISystemUser user = mocks.CreateMock<ISystemUser>();
			IForgottenPasswordMailFactory mailFactory = mocks.CreateMock<IForgottenPasswordMailFactory>();
			ISystemUserRepository repository = mocks.CreateMock<ISystemUserRepository>();
			IEncryptionEngine encryptionEngine = mocks.CreateMock<IEncryptionEngine>();
			IMailSender sender = mocks.CreateMock<IMailSender>();

			using (mocks.Record())
			{
				Expect.Call(repository.GetByEmailAddress("test@test.com")).Return(user);
				Expect.Call(user.Password).Return("encryptedPassword");
				Expect.Call(encryptionEngine.Decrypt("encryptedPassword")).Return("clearTextPassword");

				Expect.Call(mailFactory.CreateEmail("test@test.com", "clearTextPassword")).Return(forgottenPasswordEmail);

				sender.SendMail(forgottenPasswordEmail);
			}

			using (mocks.Playback())
			{
				IForgottenPasswordMailer mailer = new ForgottenPasswordMailer(encryptionEngine, mailFactory, sender);
				bool emailWasSent = mailer.SendForgottenPasswordEmail("test@test.com", repository);
				Assert.That(emailWasSent);
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Should_not_send_forgotten_password_email_when_email_address_does_not_exist()
		{
			MockRepository mocks = new MockRepository();
			ISystemUserRepository repository = mocks.CreateMock<ISystemUserRepository>();

			using (mocks.Record())
			{
				Expect.Call(repository.GetByEmailAddress("test@test.com")).Return(null);
			}

			using (mocks.Playback())
			{
				IForgottenPasswordMailer mailer = new ForgottenPasswordMailer(null, null, null);
				bool emailWasSent = mailer.SendForgottenPasswordEmail("test@test.com", repository);
				Assert.That(emailWasSent, Is.False);
			}

			mocks.VerifyAll();
		}
	}
}