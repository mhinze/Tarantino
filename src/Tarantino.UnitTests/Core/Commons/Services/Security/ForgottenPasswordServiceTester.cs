using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Tarantino.Core.Commons.Services.Repositories;
using Tarantino.Core.Commons.Services.Security;
using Tarantino.Core.Commons.Services.Security.Impl;

namespace Tarantino.UnitTests.Core.Commons.Services.Security
{
	[TestFixture]
	public class ForgottenPasswordServiceTester
	{
		[Test]
		public void Correctly_attempts_to_send_email_and_provide_feedback_when_email_can_be_sent()
		{
			MockRepository mocks = new MockRepository();
			IForgottenPasswordMailer mailer = mocks.CreateMock<IForgottenPasswordMailer>();
			ISystemUserRepository repository = mocks.CreateMock<ISystemUserRepository>();

			using (mocks.Record())
			{
				Expect.Call(mailer.SendForgottenPasswordEmail("test@test.com", repository)).Return(true);
			}

			using (mocks.Playback())
			{
				IForgottenPasswordService service = new ForgottenPasswordService(mailer);
				string userFeedback = service.SendEmailTo("test@test.com", repository);

				Assert.That(userFeedback, Is.EqualTo("Your password has been e-mailed to test@test.com"));
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Correctly_attempts_to_send_email_and_provide_feedback_when_email_cannot_be_sent()
		{
			MockRepository mocks = new MockRepository();
			IForgottenPasswordMailer mailer = mocks.CreateMock<IForgottenPasswordMailer>();
			ISystemUserRepository repository = mocks.CreateMock<ISystemUserRepository>();

			using (mocks.Record())
			{
				Expect.Call(mailer.SendForgottenPasswordEmail("test@test.com", repository)).Return(false);
			}

			using (mocks.Playback())
			{
				IForgottenPasswordService service = new ForgottenPasswordService(mailer);
				string userFeedback = service.SendEmailTo("test@test.com", repository);

				Assert.That(userFeedback, Is.EqualTo("We could not find a user with the e-mail address test@test.com"));
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Correctly_provides_feedback_to_the_user_when_email_address_is_not_specified()
		{
			IForgottenPasswordService service = new ForgottenPasswordService(null);
			string userFeedback = service.SendEmailTo(string.Empty, null);

			Assert.That(userFeedback, Is.EqualTo("Please enter the e-mail address to send your password to"));
		}
	}
}