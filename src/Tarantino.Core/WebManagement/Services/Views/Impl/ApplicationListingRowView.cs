
using Tarantino.Core.Commons.Services.DataFileManagement;
using Tarantino.Core.Commons.Services.Environment;
using Tarantino.Core.WebManagement.Model;

namespace Tarantino.Core.WebManagement.Services.Views.Impl
{
    
    public class ApplicationListingRowView:IApplicationListingRowView
    {
        private readonly IResourceFileLocator fileLocator;
        private readonly ITokenReplacer tokenReplacer;

        public ApplicationListingRowView(IResourceFileLocator fileLocator, ITokenReplacer tokenReplacer)
        {
            this.fileLocator = fileLocator;
            this.tokenReplacer = tokenReplacer;
        }

        public const string RowOneTemplate = "ApplicationListingRowOneFragment.html";

        public string BuildFirstRowHtml(ApplicationInstance applicationInstance, int instanceCount)
        {
            string template = fileLocator.ReadTextFile("Tarantino.Core", ApplicationListingBodyView.Row1Fragment);
            tokenReplacer.Text = template;
            tokenReplacer.Replace("INSTANCE_COUNT", instanceCount.ToString());
            tokenReplacer.Replace("INSTANCE_ID", applicationInstance.Id.ToString());
            tokenReplacer.Replace("MAINTANANCE_HOST_HEADER", applicationInstance.MaintenanceHostHeader);
            tokenReplacer.Replace("MAINTANANCE_STATUS", applicationInstance.DownForMaintenance ? "Offline" : "Online");
            tokenReplacer.Replace("MACHINE_NAME", applicationInstance.MachineName);
            tokenReplacer.Replace("LOAD_BALANCE_STATUS", applicationInstance.AvailableForLoadBalancing ? "Online" : "Offline");
            tokenReplacer.Replace("NEW_LOAD_BALANCE_STATUS", (!applicationInstance.AvailableForLoadBalancing).ToString());
            tokenReplacer.Replace("VERSION", applicationInstance.Version);
            tokenReplacer.Replace("HOST_HEADER", applicationInstance.UniqueHostHeader);
            return tokenReplacer.Text;
        }


        public string BuildMRowHtml(ApplicationInstance applicationInstance)
        {
            string template = fileLocator.ReadTextFile("Tarantino.Core", ApplicationListingBodyView.RowNFragment);
            tokenReplacer.Text = template;
            tokenReplacer.Replace("INSTANCE_ID", applicationInstance.Id.ToString());
            tokenReplacer.Replace("MAINTANANCE_HOST_HEADER", applicationInstance.MaintenanceHostHeader);
            tokenReplacer.Replace("MAINTANANCE_STATUS", applicationInstance.DownForMaintenance ? "Offline" : "Online");
            tokenReplacer.Replace("MACHINE_NAME", applicationInstance.MachineName);
            tokenReplacer.Replace("LOAD_BALANCE_STATUS", applicationInstance.AvailableForLoadBalancing ? "Online" : "Offline");
            tokenReplacer.Replace("NEW_LOAD_BALANCE_STATUS", (!applicationInstance.AvailableForLoadBalancing).ToString());
            tokenReplacer.Replace("VERSION", applicationInstance.Version);
            tokenReplacer.Replace("HOST_HEADER", applicationInstance.UniqueHostHeader);
            return tokenReplacer.Text;
        }

    }
}