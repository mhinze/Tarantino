using Tarantino.Core.Commons.Services.Web;
using NUnit.Framework;
using Tarantino.Infrastructure.Commons.UI.Services;

namespace Tarantino.IntegrationTests.Core.Commons.Services.Web
{
	[TestFixture, Explicit]
	public class WebDataReaderTester
	{
		[Test]
		public void Can_post_to_form()
		{
			IWebDataReader reader = new WebDataReader();

			string webData = reader.ReadUrl("http://www.interlacken.com/webdbdev/ch05/formpost.asp", "box1", "Hello World");

			Assert.That(webData.Contains("HELLO WORLD"));
		}

		[Test]
		public void Can_read_url()
		{
			IWebDataReader reader = new WebDataReader();

			string webData = reader.ReadUrl("http://www.google.com/");

			Assert.That(webData.Contains("Google"));
		}
	}
}