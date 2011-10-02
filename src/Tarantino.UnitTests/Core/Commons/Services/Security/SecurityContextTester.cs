using Tarantino.Core.Commons.Services.Security;
using Tarantino.Core.Commons.Services.Security.Impl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace Tarantino.UnitTests.Core.Commons.Services.Security
{
	[TestFixture]
	public class SecurityContextTester
	{
		[Test]
		public void Returns_current_windows_username()
		{
			MockRepository mocks = new MockRepository();
			IWindowsIdentity windowsIdentity = mocks.CreateMock<IWindowsIdentity>();

			using (mocks.Record())
			{
				Expect.Call(windowsIdentity.GetCurrentUsername()).Return(@"MY_LAPTOP\khurwitz");
			}

			using (mocks.Playback())
			{
				ISecurityContext context = new SecurityContext(windowsIdentity);
				string username = context.GetCurrentUsername();
				
				Assert.That(username, Is.EqualTo("khurwitz"));
			}

			mocks.VerifyAll();
		}
	}
}