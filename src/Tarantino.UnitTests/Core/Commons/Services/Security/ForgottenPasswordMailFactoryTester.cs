using System.Net.Mail;
using Tarantino.Core.Commons.Services.Configuration;
using Tarantino.Core.Commons.Services.Security;
using Tarantino.Core.Commons.Services.Security.Impl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace Tarantino.UnitTests.Core.Commons.Services.Security
{
	[TestFixture]
	public class ForgottenPasswordMailFactoryTester
	{
		[Test]
		public void Should_construct_forgotten_password_email()
		{
			MockRepository mocks = new MockRepository();
			IConfigurationReader reader = mocks.CreateMock<IConfigurationReader>();

			using (mocks.Record())
			{
				Expect.Call(reader.GetRequiredSetting("SmtpFromAddress")).Return("no-reply@mail.com");
			}

			using (mocks.Playback())
			{
				IForgottenPasswordMailFactory factory = new ForgottenPasswordMailFactory(reader);
				MailMessage message = factory.CreateEmail("test@test.com", "clearTextPassword");

				Assert.That(message.Subject, Is.EqualTo("Your forgotten password"));
				Assert.That(message.Body, Is.EqualTo("Your password is clearTextPassword"));
				Assert.That(message.From.Address, Is.EqualTo("no-reply@mail.com"));
				Assert.That(message.To.Count, Is.EqualTo(1));
				Assert.That(message.To[0].Address, Is.EqualTo("test@test.com"));
			}

			mocks.VerifyAll();
		
		}
	}
}