using NUnit.Framework;
using Tarantino.Core;

namespace Tarantino.IntegrationTests
{
	public class IntegrationTester
	{
		[SetUp]
		public void Setup()
		{
			CoreDependencyRegistrar.Register();
		}
	}
}