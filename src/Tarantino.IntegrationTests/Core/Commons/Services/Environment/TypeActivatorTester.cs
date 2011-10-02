using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using StructureMap;
using Tarantino.Core.Commons.Services.Environment;

namespace Tarantino.IntegrationTests.Core.Commons.Services.Environment
{
	[TestFixture]
	public class TypeActivatorTester
	{
		[Test]
		public void Activates_type_from_descriptor()
		{
			ITypeActivator typeActivator = ObjectFactory.GetInstance<ITypeActivator>();

			IResourceFileLocator locator = typeActivator.ActivateType<IResourceFileLocator>(
				"Tarantino.Core.Commons.Services.Environment.Impl.ResourceFileLocator, Tarantino.Core");

			Assert.That(locator, Is.Not.Null);
		}
	}
}