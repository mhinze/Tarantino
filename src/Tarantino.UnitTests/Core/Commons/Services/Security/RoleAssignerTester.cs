using System.Security.Principal;
using NUnit.Framework;
using Rhino.Mocks;
using Tarantino.Core.Commons.Services.Security;
using Tarantino.Core.Commons.Services.Security.Impl;
using Tarantino.Core.Commons.Services.Web;

namespace h2u.UI.UnitTests.Services.Security
{
	[TestFixture]
	public class RoleAssignerTester
	{
		[Test]
		public void Correctly_assigns_current_user_to_role()
		{
			MockRepository mocks = new MockRepository();

			IPrincipal principal = mocks.CreateMock<IPrincipal>();
			IIdentity identity = mocks.CreateMock<IIdentity>();
			IWebContext context = mocks.CreateMock<IWebContext>();
			IPrincipalFactory principalFactory = mocks.CreateMock<IPrincipalFactory>();

			using (mocks.Record())
			{
				Expect.Call(context.GetUserIdentity()).Return(identity);
				Expect.Call(principalFactory.CreatePrincipal(identity, "Administrator", "Other Role")).Return(principal);
				context.SetUser(principal);
			}

			mocks.ReplayAll();

			using (mocks.Playback())
			{
				IRoleAssigner assigner = new RoleAssigner(context, principalFactory);
				assigner.AssignCurrentUserToRoles("Administrator", "Other Role");
			}

			mocks.VerifyAll();
		}
	}
}