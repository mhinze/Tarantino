using System;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Tarantino.Core.Commons.Services.DataFileManagement;
using Tarantino.Core.Commons.Services.Environment;
using Tarantino.Core.WebManagement.Model;
using Tarantino.Core.WebManagement.Services.Views.Impl;

namespace Tarantino.UnitTests.Core.WebManagement.Services.Views
{
    [TestFixture]
    public class ApplicationListingRowViewTester
    {
        [Test]
        public void View_should_build_html_for_row_n()
        {
            ApplicationInstance instance = new ApplicationInstance();
            instance.MaintenanceHostHeader = "1";
            instance.MachineName = "2";
            instance.Id = Guid.NewGuid();

            MockRepository mocks = new MockRepository();
            IResourceFileLocator fileLocator = mocks.CreateMock<IResourceFileLocator>();
            ITokenReplacer tokenReplacer = mocks.CreateMock<ITokenReplacer>();

            using (mocks.Record())
            {
                Expect.Call(fileLocator.ReadTextFile("Tarantino.Core", ApplicationListingBodyView.RowNFragment)).Return(
                    "template");
                tokenReplacer.Text = "template";
                tokenReplacer.Replace("INSTANCE_ID", instance.Id.ToString());
                tokenReplacer.Replace("MAINTANANCE_HOST_HEADER", instance.MaintenanceHostHeader);
                tokenReplacer.Replace("MAINTANANCE_STATUS", instance.DownForMaintenance ? "Offline" : "Online");
                tokenReplacer.Replace("MACHINE_NAME", instance.MachineName);
                tokenReplacer.Replace("LOAD_BALANCE_STATUS", instance.AvailableForLoadBalancing ? "Online" : "Offline");
                tokenReplacer.Replace("NEW_LOAD_BALANCE_STATUS", (!instance.AvailableForLoadBalancing).ToString());
                tokenReplacer.Replace("VERSION", instance.Version);
                tokenReplacer.Replace("HOST_HEADER", instance.UniqueHostHeader);
                Expect.Call(tokenReplacer.Text).Return("html");
            }

            using (mocks.Playback())
            {
                ApplicationListingRowView view = new ApplicationListingRowView(fileLocator, tokenReplacer);

                string html = view.BuildMRowHtml(instance);

                Assert.That(html, Is.EqualTo("html"));
            }
            mocks.VerifyAll();
        }


        [Test]
        public void View_should_build_html_for_row_one()
        {
            ApplicationInstance instance = new ApplicationInstance();
            instance.MaintenanceHostHeader = "1";
            instance.MachineName = "2";
            instance.Id = Guid.NewGuid();

            MockRepository mocks = new MockRepository();
            IResourceFileLocator fileLocator = mocks.CreateMock<IResourceFileLocator>();
            ITokenReplacer tokenReplacer = mocks.CreateMock<ITokenReplacer>();

            using (mocks.Record())
            {
                Expect.Call(fileLocator.ReadTextFile("Tarantino.Core", ApplicationListingBodyView.Row1Fragment)).Return(
                    "template");
                tokenReplacer.Text = "template";
                tokenReplacer.Replace("INSTANCE_COUNT", "2");
                tokenReplacer.Replace("INSTANCE_ID", instance.Id.ToString());
                tokenReplacer.Replace("MAINTANANCE_HOST_HEADER", instance.MaintenanceHostHeader);
                tokenReplacer.Replace("MAINTANANCE_STATUS", instance.DownForMaintenance ? "Offline" : "Online");
                tokenReplacer.Replace("MACHINE_NAME", instance.MachineName);
                tokenReplacer.Replace("LOAD_BALANCE_STATUS", instance.AvailableForLoadBalancing ? "Online" : "Offline");
                tokenReplacer.Replace("NEW_LOAD_BALANCE_STATUS", (!instance.AvailableForLoadBalancing).ToString());
                tokenReplacer.Replace("VERSION", instance.Version);
                tokenReplacer.Replace("HOST_HEADER", instance.UniqueHostHeader);
                Expect.Call(tokenReplacer.Text).Return("html");
            }

            using (mocks.Playback())
            {
                ApplicationListingRowView view = new ApplicationListingRowView(fileLocator, tokenReplacer);

                string html = view.BuildFirstRowHtml(instance, 2);

                Assert.That(html, Is.EqualTo("html"));
            }
            mocks.VerifyAll();
        }
    }
}