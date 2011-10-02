using System.Security.Principal;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Tarantino.Core.Commons.Model;
using Tarantino.Core.Commons.Services.Repositories;
using Tarantino.Core.Commons.Services.Security;
using Tarantino.Core.Commons.Services.Security.Impl;
using Tarantino.Core.Commons.Services.Web;

namespace Tarantino.UnitTests.Core.Commons.Services.Security
{
	[TestFixture]
	public class SystemUserContextManagerTester
	{
		[Test]
		public void Correctly_sets_current_user_in_web_context()
		{
			MockRepository mocks = new MockRepository();
			IWebContext context = mocks.CreateMock<IWebContext>();
			IIdentity userIdentity = mocks.CreateMock<IIdentity>();
			ISystemUserRepository repository = mocks.CreateMock<ISystemUserRepository>();
			ISystemUser user = mocks.CreateMock<ISystemUser>();

			using (mocks.Record())
			{
				Expect.Call(context.GetUserIdentity()).Return(userIdentity);
				Expect.Call(userIdentity.Name).Return("khurwitz_2000@yahoo.com");
				Expect.Call(repository.GetByEmailAddress("khurwitz_2000@yahoo.com")).Return(user);
				context.SetItem(SystemUserContextManager.CURRENT_USER, user);
			}

			using (mocks.Playback())
			{
				ISystemUserContextManager manager = new SystemUserContextManager(context, repository);
				manager.SetUserContext();
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Does_not_set_current_user_in_context_if_one_is_not_logged_in()
		{
			MockRepository mocks = new MockRepository();
			IWebContext context = mocks.CreateMock<IWebContext>();

			using (mocks.Record())
			{
				Expect.Call(context.GetUserIdentity()).Return(null);
			}

			using (mocks.Playback())
			{
				ISystemUserContextManager manager = new SystemUserContextManager(context, null);
				manager.SetUserContext();
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Correctly_gets_current_user_in_web_context()
		{
			MockRepository mocks = new MockRepository();
			IWebContext context = mocks.CreateMock<IWebContext>();
			ISystemUser user = mocks.CreateMock<ISystemUser>();

			using (mocks.Record())
			{
				Expect.Call(context.GetItem<ISystemUser>(SystemUserContextManager.CURRENT_USER)).Return(user);
			}

			using (mocks.Playback())
			{
				ISystemUserContextManager manager = new SystemUserContextManager(context, null);
				
				Assert.That(manager.GetCurrentUser(), Is.SameAs(user));
			}

			mocks.VerifyAll();
		}
	}
}