using System;
using System.Web;
using StructureMap;
using Tarantino.Core.WebManagement.Services;

namespace Tarantino.Infrastructure.WebManagement.Http
{
	public class MaintenanceMode : IHttpModule
	{
		public void Init(HttpApplication context)
		{
			context.AuthenticateRequest += new EventHandler(authenticateRequest);
		}

		private void authenticateRequest(object sender, EventArgs e)
		{
			IMaintenancePageRedirector redirector = ObjectFactory.GetInstance<IMaintenancePageRedirector>();
			redirector.RedirectToMaintenancePageIfAppropriate();
		}

		public void Dispose()
		{
		}
	}
}