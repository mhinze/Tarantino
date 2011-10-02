using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Tarantino.Core.WebManagement.Model;
using Tarantino.Core.WebManagement.Services;
using Tarantino.Core.WebManagement.Services.Impl;
using Tarantino.Core.WebManagement.Services.Repositories;

namespace Tarantino.UnitTests.Core.WebManagement.Services
{
	[TestFixture]
	public class AvailabilityStatusUpdaterTester
	{
		[Test]
		public void Correctly_enables_application_instance()
		{
			ApplicationInstance instance = new ApplicationInstance();

			MockRepository mocks = new MockRepository();
			IApplicationInstanceRepository repository = mocks.CreateMock<IApplicationInstanceRepository>();
			IApplicationInstanceContext context = mocks.CreateMock<IApplicationInstanceContext>();

			using (mocks.Record())
			{
				Expect.Call(context.GetCurrent()).Return(instance);
				repository.Save(instance);
			}

			using (mocks.Playback())
			{
				IAvailabilityStatusUpdater updater = new AvailabilityStatusUpdater(repository, context);
				updater.SetAvailabilityStatus(true);
				
				Assert.That(instance.AvailableForLoadBalancing, Is.True);
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Correctly_disables_application_instance()
		{
			ApplicationInstance instance = new ApplicationInstance();

			MockRepository mocks = new MockRepository();
			IApplicationInstanceRepository repository = mocks.CreateMock<IApplicationInstanceRepository>();
			IApplicationInstanceContext context = mocks.CreateMock<IApplicationInstanceContext>();

			using (mocks.Record())
			{
				Expect.Call(context.GetCurrent()).Return(instance);
				repository.Save(instance);
			}

			using (mocks.Playback())
			{
				IAvailabilityStatusUpdater updater = new AvailabilityStatusUpdater(repository, context);
				updater.SetAvailabilityStatus(false);
				
				Assert.That(instance.AvailableForLoadBalancing, Is.False);
			}

			mocks.VerifyAll();
		}
	}
}