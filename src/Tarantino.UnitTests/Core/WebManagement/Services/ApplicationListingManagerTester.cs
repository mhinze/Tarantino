using NUnit.Framework;
using Rhino.Mocks;
using Tarantino.Core.WebManagement.Services;
using Tarantino.Core.WebManagement.Services.Impl;
using Tarantino.Core.WebManagement.Services.Views;

namespace Tarantino.UnitTests.Core.WebManagement.Services
{
    [TestFixture]
    public class ApplicationListingManagerTester
    {
        [Test]
        public void Application_Listing_should_render_a_view_of_existing_applications_and_instances()
        {
            MockRepository mocks = new MockRepository();
            IApplicationListingView view = mocks.CreateMock<IApplicationListingView>();

            using (mocks.Record())
            {
                view.Render();
            }

            using (mocks.Playback())
            {
                IApplicationListingManager manager = new ApplicationListingManager(view);
                manager.HandleRequest();
            }

            mocks.VerifyAll();
        }
    }
}