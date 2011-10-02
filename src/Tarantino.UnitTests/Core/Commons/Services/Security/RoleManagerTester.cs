using System.Security.Principal;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Tarantino.Core.Commons.Services.Security;
using Tarantino.Core.Commons.Services.Security.Impl;
using Tarantino.Core.Commons.Services.Web;

namespace Tarantino.UnitTests.Core.Commons.Services.Security
{
	[TestFixture]
	public class RoleManagerTester
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
				IRoleManager manager = new RoleManager(context, principalFactory);
				manager.AssignCurrentUserToRoles("Administrator", "Other Role");
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Correctly_determines_if_user_is_in_role()
		{
			MockRepository mocks = new MockRepository();

			IPrincipal principal = mocks.CreateMock<IPrincipal>();
			IWebContext context = mocks.CreateMock<IWebContext>();

			using (mocks.Record())
			{
				Expect.Call(context.GetUserPrinciple()).Return(principal); ;
				Expect.Call(principal.IsInRole("Administrator")).Return(true);
			}

			mocks.ReplayAll();

			using (mocks.Playback())
			{
				IRoleManager manager = new RoleManager(context, null);
				Assert.That(manager.IsCurrentUserInRole("Administrator"));
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Correctly_determines_if_user_is_not_in_role()
		{
			MockRepository mocks = new MockRepository();

			IPrincipal principal = mocks.CreateMock<IPrincipal>();
			IWebContext context = mocks.CreateMock<IWebContext>();

			using (mocks.Record())
			{
				Expect.Call(context.GetUserPrinciple()).Return(principal); ;
				Expect.Call(principal.IsInRole("Administrator")).Return(false);
			}

			mocks.ReplayAll();

			using (mocks.Playback())
			{
				IRoleManager manager = new RoleManager(context, null);
				Assert.That(manager.IsCurrentUserInRole("Administrator"), Is.False);
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Correctly_determines_if_user_is_in_list_of_roles()
		{
			MockRepository mocks = new MockRepository();

			IPrincipal principal = mocks.CreateMock<IPrincipal>();
			IWebContext context = mocks.CreateMock<IWebContext>();

			using (mocks.Record())
			{
				Expect.Call(context.GetUserPrinciple()).Return(principal).Repeat.Any();
				Expect.Call(principal.IsInRole("Administrator")).Return(false);
				Expect.Call(principal.IsInRole("Other")).Return(true);
			}

			mocks.ReplayAll();

			using (mocks.Playback())
			{
				IRoleManager manager = new RoleManager(context, null);
				Assert.That(manager.IsCurrentUserInAtLeastOneRole("Administrator", "Other"));
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Correctly_determines_if_user_is_not_in_list_of_roles()
		{
			MockRepository mocks = new MockRepository();

			IPrincipal principal = mocks.CreateMock<IPrincipal>();
			IWebContext context = mocks.CreateMock<IWebContext>();

			using (mocks.Record())
			{
				Expect.Call(context.GetUserPrinciple()).Return(principal).Repeat.Any();
				Expect.Call(principal.IsInRole("Administrator")).Return(false);
				Expect.Call(principal.IsInRole("Other")).Return(false);
			}

			mocks.ReplayAll();

			using (mocks.Playback())
			{
				IRoleManager manager = new RoleManager(context, null);
				Assert.That(manager.IsCurrentUserInAtLeastOneRole("Administrator", "Other"), Is.False);
			}

			mocks.VerifyAll();
		}
	}
}