using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Tarantino.Core.Commons.Services.Web;
using Tarantino.Core.WebManagement.Services;
using Tarantino.Core.WebManagement.Services.Impl;

namespace Tarantino.UnitTests.Core.WebManagement.Services
{
	[TestFixture]
	public class SecureAvailabilityStatusUpdaterTester
	{
		[Test]
		public void Correctly_returns_error_when_user_is_not_an_administrator()
		{
			MockRepository mocks = new MockRepository();
			IAdministratorSecurityChecker checker = mocks.CreateMock<IAdministratorSecurityChecker>();

			using (mocks.Record())
			{
				Expect.Call(checker.IsCurrentUserAdministrator()).Return(false);
			}

			using (mocks.Playback())
			{
				ISecureAvailabilityStatusUpdater statusUpdater = new SecureAvailabilityStatusUpdater(checker, null, null);
				string errorMessage = statusUpdater.SetStatus(true);

				Assert.That(errorMessage, Is.EqualTo("Only authenticated users can change the load balancing status.\n"));
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Correctly_sets_availability_status_for_administrator()
		{
			MockRepository mocks = new MockRepository();
			IAdministratorSecurityChecker checker = mocks.CreateMock<IAdministratorSecurityChecker>();
			IWebContext context = mocks.CreateMock<IWebContext>();
			IAvailabilityStatusUpdater updater = mocks.CreateMock<IAvailabilityStatusUpdater>();

			using (mocks.Record())
			{
				Expect.Call(checker.IsCurrentUserAdministrator()).Return(true);
				updater.SetAvailabilityStatus(true);
				Expect.Call(context.GetCurrentUrl()).Return("http://mydomain/");
				context.Redirect("http://mydomain/");
			}

			using (mocks.Playback())
			{
				ISecureAvailabilityStatusUpdater statusUpdater = new SecureAvailabilityStatusUpdater(checker, context, updater);
				string errorMessage = statusUpdater.SetStatus(true);

				Assert.That(errorMessage, Is.Empty);
			}
		}
	}
}