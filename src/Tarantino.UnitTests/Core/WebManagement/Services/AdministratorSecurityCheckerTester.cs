using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Tarantino.Core.Commons.Services.Configuration;
using Tarantino.Core.Commons.Services.Security;
using Tarantino.Core.Commons.Services.Web;
using Tarantino.Core.WebManagement.Services;
using Tarantino.Core.WebManagement.Services.Impl;

namespace Tarantino.UnitTests.Core.WebManagement.Services
{
	[TestFixture]
	public class AdministratorSecurityCheckerTester
	{
		[Test]
		public void Does_not_validate_non_authenticated_administrator()
		{
			string[] roles = new string[] { @"BUILTIN\Administrators", "Administrators" };

			MockRepository mocks = new MockRepository();
			IWebContext context = mocks.CreateMock<IWebContext>();
			IRoleManager roleManager = mocks.CreateMock<IRoleManager>();
			IConfigurationReader configurationReader = mocks.CreateMock<IConfigurationReader>();

			using (mocks.Record())
			{
				Expect.Call(context.UserIsAuthenticated()).Return(false);
				Expect.Call(configurationReader.GetStringArray("TarantinoWebManagementRoles")).Return(roles);
				Expect.Call(roleManager.IsCurrentUserInAtLeastOneRole(roles)).Return(true);
			}

			using (mocks.Playback())
			{
				IAdministratorSecurityChecker checker = new AdministratorSecurityChecker(context, roleManager, configurationReader);
				Assert.That(checker.IsCurrentUserAdministrator(), Is.False);
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Does_not_validate_authenticated_non_administrator()
		{
			string[] roles = new string[] { @"BUILTIN\Administrators", "Administrators" };

			MockRepository mocks = new MockRepository();
			IWebContext context = mocks.CreateMock<IWebContext>();
			IRoleManager roleManager = mocks.CreateMock<IRoleManager>();
			IConfigurationReader configurationReader = mocks.CreateMock<IConfigurationReader>();

			using (mocks.Record())
			{
				Expect.Call(context.UserIsAuthenticated()).Return(true);
				Expect.Call(configurationReader.GetStringArray("TarantinoWebManagementRoles")).Return(roles);
				Expect.Call(roleManager.IsCurrentUserInAtLeastOneRole(roles)).Return(false);
			}

			using (mocks.Playback())
			{
				IAdministratorSecurityChecker checker = new AdministratorSecurityChecker(context, roleManager, configurationReader);
				Assert.That(checker.IsCurrentUserAdministrator(), Is.False);
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Validates_authenticated_administrator()
		{
			string[] roles = new string[] { @"BUILTIN\Administrators", "Administrators" };

			MockRepository mocks = new MockRepository();
			IWebContext context = mocks.CreateMock<IWebContext>();
			IRoleManager roleManager = mocks.CreateMock<IRoleManager>();
			IConfigurationReader configurationReader = mocks.CreateMock<IConfigurationReader>();

			using (mocks.Record())
			{
				Expect.Call(context.UserIsAuthenticated()).Return(true);
				Expect.Call(configurationReader.GetStringArray("TarantinoWebManagementRoles")).Return(roles);
				Expect.Call(roleManager.IsCurrentUserInAtLeastOneRole(roles)).Return(true);
			}

			using (mocks.Playback())
			{
				IAdministratorSecurityChecker checker = new AdministratorSecurityChecker(context, roleManager, configurationReader);
				Assert.That(checker.IsCurrentUserAdministrator(), Is.True);
			}

			mocks.VerifyAll();
		}
	}
}