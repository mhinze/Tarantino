using System;
using System.Collections.Generic;

using Tarantino.Core.Commons.Services.DataFileManagement;
using Tarantino.Core.Commons.Services.Environment;
using Tarantino.Core.WebManagement.Model;
using Tarantino.Core.WebManagement.Services.Repositories;

namespace Tarantino.Core.WebManagement.Services.Views.Impl
{
    
    public class ApplicationListingBodyView : IApplicationListingBodyView
    {
        private readonly IAdministratorSecurityChecker checker;
        private readonly IApplicationInstanceRepository applicationInstanceRepository;
        private readonly IResourceFileLocator fileLocator;
        private readonly ITokenReplacer replacer;
        private readonly IApplicationListingRowView applicationListingRowView;

        public ApplicationListingBodyView(IAdministratorSecurityChecker securityChecker, IApplicationInstanceRepository applicationInstanceRepository, IResourceFileLocator fileLocator, ITokenReplacer replacer, IApplicationListingRowView applicationListingRowView)
        {
            this.checker = securityChecker;
            this.applicationInstanceRepository = applicationInstanceRepository;
            this.fileLocator = fileLocator;
            this.replacer = replacer;
            this.applicationListingRowView = applicationListingRowView;
        }


        public const string Row1Fragment = "Tarantino.Core.WebManagement.Services.Views.Resources.ApplicationListingRowOneFragment.html";
        public const string RowNFragment = "Tarantino.Core.WebManagement.Services.Views.Resources.ApplicationListingRowNFragment.html";
        public const string BodyTemplate = "Tarantino.Core.WebManagement.Services.Views.Resources.ApplicationListingBodyTemplate.html";
        public const string ReadOnlyRow1Fragment = "Tarantino.Core.WebManagement.Services.Views.Resources.ApplicationListingReadOnlyRowOneFragment.html";
        public const string ReadOnlyRowNFragment = "Tarantino.Core.WebManagement.Services.Views.Resources.ApplicationListingReadOnlyRowNFragment.html";
        public const string ReadOnlyBodyTemplate = "Tarantino.Core.WebManagement.Services.Views.Resources.ApplicationListingReadOnlyBodyTemplate.html";

        public string BuildHtml()
        {
            
            if (checker.IsCurrentUserAdministrator())
            {
                IList<ApplicationInstance> applications =
                    new List<ApplicationInstance>(applicationInstanceRepository.GetAll());

                string bodyTemplate =
                    fileLocator.ReadTextFile("Tarantino.Core", ApplicationListingBodyView.BodyTemplate);
             
                replacer.Text = bodyTemplate;

                ApplicationInstance lastInstance = new ApplicationInstance();
                string rowHTMl = "";
                foreach(ApplicationInstance instance in applications)
                {
                    if(instance.MaintenanceHostHeader!=lastInstance.MaintenanceHostHeader)
                    {
                        int instanceCount= GetInstanceCount(applications,instance.MaintenanceHostHeader);
                        rowHTMl += applicationListingRowView.BuildFirstRowHtml(instance,instanceCount);
                    }
                    else
                    {
                        rowHTMl += applicationListingRowView.BuildMRowHtml(instance);
                    }
                    lastInstance = instance;
                }
                replacer.Replace("ROWS", rowHTMl);
            }
            else
            {
                replacer.Text = "Unauthorized request.";
            }
            return replacer.Text;


        }

        private int GetInstanceCount(IList<ApplicationInstance> applications, string hostHeader)
        {
            int count = 0;
            foreach(ApplicationInstance instance in applications)
            {
                if (instance.MaintenanceHostHeader.Equals(hostHeader))
                    count++;
            }
            return count;
        }
    }
}