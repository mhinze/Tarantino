using Tarantino.Core.Commons.Services.Configuration;
using Tarantino.Core.Commons.Services.Configuration.Impl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace Tarantino.UnitTests.Core.Commons.Services.Configuration
{
	[TestFixture]
	public class ApplicationSettingsTester
	{
		[Test]
		public void Should_read_smtp_server()
		{
			var mocks = new MockRepository();
			var reader = mocks.CreateMock<IConfigurationReader>();

			using (mocks.Record())
			{
				Expect.Call(reader.GetRequiredSetting("SmtpServer")).Return("localhost");
			}

			using (mocks.Playback())
			{
				IApplicationSettings settings = new ApplicationSettings(reader);
				Assert.That(settings.GetSmtpServer(), Is.EqualTo("localhost"));
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Should_read_service_agent_factory()
		{
			var mocks = new MockRepository();
			var reader = mocks.CreateMock<IConfigurationReader>();

			using (mocks.Record())
			{
				Expect.Call(reader.GetRequiredSetting("ServiceAgentFactory")).Return("my type");
			}

			using (mocks.Playback())
			{
				IApplicationSettings settings = new ApplicationSettings(reader);
				Assert.That(settings.GetServiceAgentFactory(), Is.EqualTo("my type"));
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Should_read_smtp_username()
		{
			var mocks = new MockRepository();
			var reader = mocks.CreateMock<IConfigurationReader>();

			using (mocks.Record())
			{
				Expect.Call(reader.GetRequiredSetting("SmtpUsername")).Return("khurwitz");
			}

			using (mocks.Playback())
			{
				IApplicationSettings settings = new ApplicationSettings(reader);
				Assert.That(settings.GetSmtpUsername(), Is.EqualTo("khurwitz"));
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Should_read_smtp_password()
		{
			MockRepository mocks = new MockRepository();
			IConfigurationReader reader = mocks.CreateMock<IConfigurationReader>();

			using (mocks.Record())
			{
				Expect.Call(reader.GetRequiredSetting("SmtpPassword")).Return("mypass");
			}

			using (mocks.Playback())
			{
				IApplicationSettings settings = new ApplicationSettings(reader);
				Assert.That(settings.GetSmtpPassword(), Is.EqualTo("mypass"));}
		}

		[Test]
		public void Should_read_smtp_authentication_necessary()
		{
			MockRepository mocks = new MockRepository();
			IConfigurationReader reader = mocks.CreateMock<IConfigurationReader>();

			using (mocks.Record())
			{
				Expect.Call(reader.GetRequiredBooleanSetting("SmtpAuthenticationNecessary")).Return(true);
			}

			using (mocks.Playback())
			{
				IApplicationSettings settings = new ApplicationSettings(reader);
				Assert.That(settings.GetSmtpAuthenticationNecessary(), Is.EqualTo(true));}
		}

		[Test]
		public void Should_read_service_sleep_time()
		{
			MockRepository mocks = new MockRepository();
			IConfigurationReader reader = mocks.CreateMock<IConfigurationReader>();

			using (mocks.Record())
			{
				Expect.Call(reader.GetRequiredIntegerSetting("ServiceSleepTime")).Return(7);
			}

			using (mocks.Playback())
			{
				IApplicationSettings settings = new ApplicationSettings(reader);
				Assert.That(settings.GetServiceSleepTime(), Is.EqualTo(7));}
		}

		[Test]
		public void Should_read_show_sql()
		{
			MockRepository mocks = new MockRepository();
			IConfigurationReader reader = mocks.CreateMock<IConfigurationReader>();

			using (mocks.Record())
			{
				Expect.Call(reader.GetOptionalBooleanSetting("ShowSql")).Return(true);
			}

			using (mocks.Playback())
			{
				IApplicationSettings settings = new ApplicationSettings(reader);
				Assert.That(settings.GetShowSql(), Is.EqualTo(true));}
		}

		[Test]
		public void Should_read_mapping_assemblies()
		{
			string[] mappingAssemblies = new string[0];

			MockRepository mocks = new MockRepository();
			IConfigurationReader reader = mocks.CreateMock<IConfigurationReader>();

			using (mocks.Record())
			{
				Expect.Call(reader.GetStringArray("MappingAssemblies")).Return(mappingAssemblies);
			}

			using (mocks.Playback())
			{
				IApplicationSettings settings = new ApplicationSettings(reader);
				Assert.That(settings.GetMappingAssemblies(), Is.SameAs(mappingAssemblies));
			}
		}
	}
}