using System;
using StructureMap;
using Tarantino.Core.WebManagement.Model;
using Tarantino.Core.WebManagement.Services.Repositories;

namespace Tarantino.WebManagement.Handlers
{
	public class ApplicationEdit : HandlerBase
	{
		protected override void OnProcessRequest()
		{
			if (_authenticated && _context.Request["id"] != null)
			{
				Guid id = new Guid(_context.Request["id"]);

				IApplicationInstanceRepository repository = ObjectFactory.GetInstance<IApplicationInstanceRepository>();
				ApplicationInstance applicationInstance = repository.GetById(id);

				string hostName = _context.Request.Form.Get("hostname");

				if (hostName != null)
				{
					applicationInstance.UniqueHostHeader = hostName;
					repository.Save(applicationInstance);
				}

				Write(ListApplications(applicationInstance));
			}
		}

		string ListApplications(ApplicationInstance applicationInstance)
		{
			System.Text.StringBuilder output = new System.Text.StringBuilder();
			WriteCSS();
			if (_authenticated)
			{
				output.AppendFormat("<form method=post action=''>");
			}
			output.Append("<table >\n");
			output.AppendFormat("<tr><td><a href='Tarantino.WebManagement.Application.axd'>Back</a></td><td></td></tr>");
			output.AppendFormat("<tr><td>Machine Name</td><td>{0}</td></tr>", applicationInstance.MachineName);
			output.AppendFormat("<tr><td>Version</td><td>{0}</td></tr>", applicationInstance.Version);
			output.AppendFormat("<tr><td>Unique Hostname</td><td><input type=\"text\" name=\"hostname\" value=\"{0}\"></td></tr>", applicationInstance.UniqueHostHeader);
			output.AppendFormat("<tr><td>Shared Hostname</td><td>{0}</td></tr>", applicationInstance.MaintenanceHostHeader);
			output.AppendFormat("<tr><td>Load balanaced</td><td>{0}</td></tr>", applicationInstance.AvailableForLoadBalancing ? "Online" : "Offline");
			output.AppendFormat("<tr><td>Maintenance Mode</td><td>{0}</td></tr>", applicationInstance.DownForMaintenance ? "Down" : "Online");
			output.Append("</table>\n");

			if (_authenticated)
			{
				output.AppendFormat("<input type=submit value='Submit' />");
				output.AppendFormat("</form>");
			}

			return output.ToString();
		}
	}
}