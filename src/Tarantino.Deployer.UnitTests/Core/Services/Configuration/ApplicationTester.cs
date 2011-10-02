using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Tarantino.Core.Commons.Services.Configuration.Impl;
using Tarantino.Deployer.Core.Services.Configuration.Impl;

namespace Tarantino.Deployer.UnitTests.Core.Services.Configuration
{
	[TestFixture]
	public class ApplicationTester
	{
		[Test]
		public void Should_return_correct_configuration_element_name()
		{
			var application = new Application();
			Assert.That(application.GetElementName(), Is.EqualTo("Application"));
		}
	}
}