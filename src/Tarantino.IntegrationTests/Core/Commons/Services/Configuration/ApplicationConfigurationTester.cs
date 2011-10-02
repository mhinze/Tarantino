using System.Collections.Specialized;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Tarantino.Core.Commons.Services.Configuration;
using Tarantino.Core.Commons.Services.Configuration.Impl;

namespace Tarantino.IntegrationTests.Core.Commons.Services.Configuration
{
	[TestFixture]
	public class ApplicationConfigurationTester
	{
		[Test]
		public void Reads_application_setting()
		{
			IApplicationConfiguration settings = new ApplicationConfiguration();

			Assert.That(settings.GetSetting("TestKey"), Is.EqualTo("TestValue"));
		}

		[Test]
		public void Reads_missing_application_setting()
		{
			IApplicationConfiguration settings = new ApplicationConfiguration();

			Assert.That(settings.GetSetting("MissingSetting"), Is.Null);
		}

		[Test]
		public void Reads_configuration_section()
		{
			IApplicationConfiguration configuration = new ApplicationConfiguration();
			var settings = (NameValueCollection)configuration.GetSection("MySettings");

			Assert.That(settings.Keys, Is.EqualTo(new [] { "key1", "key2" }));
			Assert.That(settings[0], Is.EqualTo("value1"));
			Assert.That(settings[1], Is.EqualTo("value2"));
		}
	}
}