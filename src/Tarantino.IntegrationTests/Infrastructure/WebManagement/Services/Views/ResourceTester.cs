using NUnit.Framework;
using StructureMap;
using Tarantino.Core;
using Tarantino.Core.Commons.Services.Environment;
using Tarantino.Core.WebManagement.Services.Views.Impl;
using Tarantino.IntegrationTests;

namespace Tarantino.Infrastructure.IntegrationTests.WebManagement.Services.Views
{
	[TestFixture]
	public class ResourceTester : IntegrationTester
	{
		[Test]
		public void Correctly_finds_resource_files()
		{
			var locator = ObjectFactory.GetInstance<IResourceFileLocator>();

			Assert.That(locator.FileExists("Tarantino.Core", LoadBalancerBodyView.LoadBalancerBodyTemplate));
			Assert.That(locator.FileExists("Tarantino.Core", MenuView.MenuTemplate));
			Assert.That(locator.FileExists("Tarantino.Core", PageView.PageTemplate));
			Assert.That(locator.FileExists("Tarantino.Core", PageView.StylesheetTemplate));
			Assert.That(locator.FileExists("Tarantino.Core", ApplicationListingBodyView.BodyTemplate));
			Assert.That(locator.FileExists("Tarantino.Core", ApplicationListingBodyView.RowNFragment));
			Assert.That(locator.FileExists("Tarantino.Core", ApplicationListingBodyView.Row1Fragment));
			Assert.That(locator.FileExists("Tarantino.Core", ApplicationListingBodyView.ReadOnlyBodyTemplate));
			Assert.That(locator.FileExists("Tarantino.Core", ApplicationListingBodyView.ReadOnlyRowNFragment));
			Assert.That(locator.FileExists("Tarantino.Core", ApplicationListingBodyView.ReadOnlyRow1Fragment));
		}
	}
}