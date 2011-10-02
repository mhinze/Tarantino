using NUnit.Framework;
using Tarantino.Core;

namespace Tarantino.UnitTests
{
	[TestFixture, Explicit]
	public class CoreDependencyRegistrarTester
	{
		[Test]
		public void Test()
		{
			CoreDependencyRegistrar.Register();
		}
	}
}