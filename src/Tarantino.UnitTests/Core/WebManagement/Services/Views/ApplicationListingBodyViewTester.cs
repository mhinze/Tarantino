using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Tarantino.Core.Commons.Services.DataFileManagement;
using Tarantino.Core.Commons.Services.Environment;
using Tarantino.Core.WebManagement.Model;
using Tarantino.Core.WebManagement.Services;
using Tarantino.Core.WebManagement.Services.Repositories;
using Tarantino.Core.WebManagement.Services.Views;
using Tarantino.Core.WebManagement.Services.Views.Impl;

namespace Tarantino.UnitTests.Core.WebManagement.Services.Views
{
    [TestFixture]
    public class ApplicationListingBodyViewTester
    {
        [Test]
        public void Builder_Should_return_a_friendly_not_suthorized_message_for_non_administrators()
        {

            MockRepository mocks = new MockRepository();
            ITokenReplacer replacer = mocks.CreateMock<ITokenReplacer>();
            IAdministratorSecurityChecker securityChecker = mocks.CreateMock<IAdministratorSecurityChecker>();
            
            using (mocks.Record())
            {
                Expect.Call(securityChecker.IsCurrentUserAdministrator()).Return(false);
                replacer.Text = "Unauthorized request.";
                Expect.Call(replacer.Text).Return("formatted HTML");
            }

            using (mocks.Playback())
            {
                ApplicationListingBodyView view = new ApplicationListingBodyView(securityChecker ,null,null,replacer,null);
                string html = view.BuildHtml();
                Assert.That(html,Is.EqualTo("formatted HTML"));
            }

            mocks.VerifyAll();
        }
        [Test]
        public void Correctly_returns_html_for_page_for_an_administrator()
        {
            List<ApplicationInstance> applicationList = new List<ApplicationInstance>();
            //application group 1
            applicationList.Add(new ApplicationInstance());
            applicationList[0].MaintenanceHostHeader = "1";
            applicationList.Add(new ApplicationInstance());
            applicationList[1].MaintenanceHostHeader = "1";

            //application group 2
            applicationList.Add(new ApplicationInstance());
            applicationList[2].MaintenanceHostHeader = "2";
            applicationList.Add(new ApplicationInstance());
            applicationList[3].MaintenanceHostHeader = "2";

            MockRepository mocks = new MockRepository();
            IApplicationInstanceRepository repository = mocks.CreateMock<IApplicationInstanceRepository>();
            IResourceFileLocator fileLocator = mocks.CreateMock<IResourceFileLocator>();
            ITokenReplacer replacer = mocks.CreateMock<ITokenReplacer>();
            IAdministratorSecurityChecker securityChecker = mocks.CreateMock<IAdministratorSecurityChecker>();
            IApplicationListingRowView applicationListingRowView = mocks.CreateMock<IApplicationListingRowView>(); 
            
            using (mocks.Record())
            {
                Expect.Call(securityChecker.IsCurrentUserAdministrator()).Return(true);

                Expect.Call(repository.GetAll()).Return(applicationList);
                Expect.Call(fileLocator.ReadTextFile("Tarantino.Core", ApplicationListingBodyView.BodyTemplate)).Return("body template");
                replacer.Text = "body template";
                Expect.Call(applicationListingRowView.BuildFirstRowHtml(applicationList[0],2)).Return("1");
                Expect.Call(applicationListingRowView.BuildMRowHtml(applicationList[1])).Return("2");
                Expect.Call(applicationListingRowView.BuildFirstRowHtml(applicationList[2],2)).Return("3");
                Expect.Call(applicationListingRowView.BuildMRowHtml(applicationList[3])).Return("4");
                replacer.Replace("ROWS", "1234");
                Expect.Call(replacer.Text).Return("formatted HTML");
            }

            using (mocks.Playback())
            {
                IApplicationListingBodyView view = new ApplicationListingBodyView(securityChecker ,repository,fileLocator,replacer,applicationListingRowView);
                string html = view.BuildHtml();
                Assert.That(html,Is.EqualTo("formatted HTML"));
            }

            mocks.VerifyAll();
        }
    }
}