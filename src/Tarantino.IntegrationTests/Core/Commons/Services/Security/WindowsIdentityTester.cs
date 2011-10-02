using Tarantino.Core.Commons.Services.Security;
using Tarantino.Core.Commons.Services.Security.Impl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Tarantino.IntegrationTests.Core.Commons.Services.Security
{
	[TestFixture]
	public class WindowsIdentityTester
	{
		[Test]
		public void Correctly_gets_full_windows_username()
		{
			IWindowsIdentity windowsIdentity = new WindowsIdentity();

			string username = windowsIdentity.GetCurrentUsername();
			
			Assert.That(username.Length, Is.GreaterThan(0));
			Assert.That(username.Contains(@"\"));
		}
	}
}