using System.Reflection;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Tarantino.Core.Commons.Services.Environment;
using Tarantino.Core.Commons.Services.Environment.Impl;

namespace Tarantino.UnitTests.Core.Commons.Services.Environment
{
	[TestFixture]
	public class AssemblyContextTester
	{
		[Test]
		public void Correctly_returns_executing_assembly()
		{
			IAssemblyContext context = new AssemblyContext();
			Assembly assembly = context.GetExecutingAssembly();

			Assert.That(assembly, Is.Not.Null);
		}

		[Test]
		public void Correctly_returns_version_info()
		{
			IAssemblyContext context = new AssemblyContext();
			Assert.That(context.GetAssemblyVersion().StartsWith("1."));
		}
	}
}