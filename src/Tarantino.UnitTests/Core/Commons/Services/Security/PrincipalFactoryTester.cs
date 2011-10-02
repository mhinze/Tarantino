using System.Security.Principal;
using NUnit.Framework;
using Rhino.Mocks;
using Tarantino.Core.Commons.Services.Security;
using Tarantino.Core.Commons.Services.Security.Impl;

namespace Tarantino.UnitTests.Core.Commons.Services.Security
{
	[TestFixture]
	public class PrincipalFactoryTester
	{
		[Test]
		public void Constructs_principal_with_roles()
		{
			MockRepository mocks = new MockRepository();
			IIdentity identity = mocks.CreateMock<IIdentity>();

			mocks.ReplayAll();

			using (mocks.Playback())
			{
				IPrincipalFactory factory = new PrincipalFactory();
				IPrincipal principal = factory.CreatePrincipal(identity, "Administrator", "Other Role");

				Assert.That(principal.IsInRole("Administrator"));
				Assert.That(principal.IsInRole("Other Role"));
				Assert.That(!principal.IsInRole("MyRole"));
			}

			mocks.VerifyAll();
		}
	}
}