using System;
using System.Collections.Generic;
using StructureMap;
using Tarantino.Core.Commons.Services.Web;
using Tarantino.Core.WebManagement.Model;
using Tarantino.Core.WebManagement.Services;
using Tarantino.Core.WebManagement.Services.Repositories;

namespace Tarantino.WebManagement.Handlers
{
	public class Application : HandlerBase
	{
		protected override void OnProcessRequest()
		{
		    IApplicationListingManager manager = ObjectFactory.GetInstance<IApplicationListingManager>();
            manager.HandleRequest();
            //if (_authenticated && _context.Request["id"] != null && _context.Request["a"] != null && _context.Request["value"] != null)
            //{
            //    Guid id = new Guid(_context.Request["id"]);

            //    IApplicationInstanceRepository repository = ObjectFactory.GetInstance<IApplicationInstanceRepository>();
            //    ApplicationInstance instance = repository.GetById(id);

            //    bool boolValue = bool.Parse(_context.Request["value"]);

            //    switch (_context.Request["a"])
            //    {
            //        case "DownForMaintenance":
            //            SetAllMaintenance(instance, boolValue);
            //            break;
            //        case "AvailableForLoadBalance":
            //            instance.AvailableForLoadBalancing = boolValue;
            //            repository.Save(instance);
            //            break;
            //        case "Edit":
            //            break;
            //        case "Delete":
            //            repository.Delete(instance);
            //            break;
            //        case "Cache":
            //            try
            //            {
            //                if (instance.UniqueHostHeader != string.Empty)
            //                {
            //                    IWebDataReader reader = ObjectFactory.GetInstance<IWebDataReader>();
            //                    string cacheKey = ApplicationInstance.CacheKey;
            //                    string url = string.Format("http://{0}/{1}?pattern={2}", "Tarantino.WebManagement.cache.axd", instance.UniqueHostHeader, cacheKey);
            //                    reader.ReadUrl(url);
            //                }
            //            }
            //            catch
            //            {
            //            }

            //            break;
            //    }
				
            //    Reload();
            //}

            //WriteCSS();
            //WriteMenu();

            //Write(ListApplications());
		}

        //private void SetAllMaintenance(ApplicationInstance instance, bool downForMaintenance)
        //{
        //    IApplicationInstanceRepository repository = ObjectFactory.GetInstance<IApplicationInstanceRepository>();
        //    IEnumerable<ApplicationInstance> applicationInstances = repository.GetByHostHeader(instance.UniqueHostHeader);

        //    foreach (ApplicationInstance applicationInstance in applicationInstances)
        //    {
        //        if (applicationInstance.MaintenanceHostHeader == instance.MaintenanceHostHeader)
        //        {
        //            applicationInstance.DownForMaintenance = downForMaintenance;
        //            repository.Save(applicationInstance);
        //        }
        //    }
        //}

        //private string ListApplications()
        //{
        //    System.Text.StringBuilder output = new System.Text.StringBuilder();

        //    IApplicationInstanceRepository repository = ObjectFactory.GetInstance<IApplicationInstanceRepository>();
        //    IList<ApplicationInstance> applications = new List<ApplicationInstance>(repository.GetAll());

        //    WriteCSS();

        //    output.Append("<table>\n");
        //    if (applications.Count > 0)
        //    {
        //        output.AppendFormat("<tr ><th>{0}</th><th>{1}</th><th>{2}</th><th class='center'>{3}</th><th class='center'>{4}</th><th>{5}</th>", "MaintenanceHostHeader", "Maintenance Mode", "MachineName", "Load balanaced", "Version", "Unique Hostname");

        //        if (_authenticated)
        //            output.AppendFormat("<th colspan=3>Action</th>");

        //        output.AppendFormat("</tr>\r");
        //        string lastHost = "";
        //        foreach (ApplicationInstance ai in applications)
        //        {
        //            output.AppendFormat("<tr onMouseOver=\"className='over';\" onMouseOut=\"className='out';\"	 >");

        //            string downForMaintenance = ai.DownForMaintenance ? "Down" : "Online";
        //            string availableForLoadBalancing = ai.AvailableForLoadBalancing ? "Online" : "Offline";
        //            if (lastHost != ai.MaintenanceHostHeader)
        //            {
        //                int appcount = ApplicationCount(applications, ai.MaintenanceHostHeader);

        //                string maintenanceHostHeader = ai.MaintenanceHostHeader != lastHost ? ai.MaintenanceHostHeader : string.Empty;
        //                output.AppendFormat("<td rowspan='{1}' class='out'><a target='_blank' href='http://{0}'>{0}</a></td>", maintenanceHostHeader, appcount);

        //                if (_authenticated)
        //                {
        //                    output.AppendFormat("<td rowspan='{3}' class='{0}'><a class='{0}' href='?id={1}&a=DownForMaintenance&value={2}'>{0}</a></td>", downForMaintenance, ai.Id, !ai.DownForMaintenance, appcount);
        //                }
        //                else
        //                {
        //                    output.AppendFormat("<td rowspan='{1}' class='{0}'>{0}</td>", downForMaintenance, appcount);
        //                }
        //            }

        //            output.AppendFormat("<td>{0}</td>", ai.MachineName);

        //            if (_authenticated)
        //            {
        //                output.AppendFormat("<td class='{0}' ><a class='{0}' href='?id={1}&a=AvailableForLoadBalance&value={2}'>{0}</a></td>", availableForLoadBalancing, ai.Id, !ai.AvailableForLoadBalancing);
        //            }
        //            else
        //            {
        //                output.AppendFormat("<td class='{0}' >{0}</td>", availableForLoadBalancing);
        //            }

        //            output.AppendFormat("<td>{0}</td>", ai.Version);

        //            output.AppendFormat("<td><a target='_blank' href='http://{0}'>{0}</a></td>", ai.UniqueHostHeader);

        //            if (_authenticated)
        //            {
        //                output.AppendFormat("<td>");
        //                if (ai.UniqueHostHeader != null && ai.UniqueHostHeader.Length > 0)
        //                {
        //                    output.AppendFormat("&nbsp;<a title=\"Refresh Cache\" href=\"?a=Cache&id={0}&value=true\">Refresh</a>&nbsp;", ai.Id);
        //                }

        //                output.AppendFormat("</td>");
        //                output.AppendFormat("<td>");
        //                output.AppendFormat("&nbsp;<a href=\"Tarantino.WebManagement.ApplicationEdit.axd?id={0}\">Edit</a>&nbsp;", ai.Id);
        //                output.AppendFormat("</td>");
        //                output.AppendFormat("<td>");
        //                output.AppendFormat("&nbsp;<a href=\"?a=Delete&id={0}&value=true\">Delete</a>&nbsp;</td>", ai.Id);
        //                output.AppendFormat("</td>");
        //            }

        //            output.AppendFormat("</tr>\r");
        //            lastHost = ai.MaintenanceHostHeader;
        //        }
        //    }
        //    else
        //    {
        //        output.Append("<tr><td>There are no applications</td></tr>\n");
        //    }
        //    output.Append("</table>\n");
        //    return output.ToString();
        //}

        //private int ApplicationCount(IEnumerable<ApplicationInstance> apps, string HostName)
        //{
        //    int count = 0;
        //    foreach (ApplicationInstance ai in apps)
        //    {
        //        if (HostName != null && ai.MaintenanceHostHeader != null && ai.MaintenanceHostHeader.Equals(HostName))
        //            count++;
        //    }
        //    return count < 1 ? 1 : count;
        //}
	}
}