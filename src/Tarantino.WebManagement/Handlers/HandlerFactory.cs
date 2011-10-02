using System;
using System.Web;
using Tarantino.Infrastructure.WebManagement.Http;

namespace Tarantino.WebManagement.Handlers
{
	public class HandlerFactory : IHttpHandlerFactory
	{
		public IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated)
		{
			IHttpHandler handler = null;
			string handlerName;

			url = url.ToLower().Trim();
			handlerName = url.Substring(0, url.LastIndexOf("."));
			handlerName = handlerName.Substring(handlerName.LastIndexOf("/") + 1, handlerName.Length - handlerName.LastIndexOf("/") - 1);
			handlerName = handlerName.Replace("callawaygolf.tx.web.management.", "");
			switch (handlerName)
			{
				case "loadbalancer":
					handler = new LoadBalancer();
					break;

				case "version":
					handler = new Version();
					break;

				case "assemblies":
					handler = new Assemblies();
					break;

				case "cache":
					handler = new Cache();
					break;

				case "application":
					handler = new Application();
					break;

				case "applicationedit":
					handler = new ApplicationEdit();
					break;

				case "disablessl":
					handler = new DisableSSL();
					break;

				default:
					break;
			}
			return handler;
		}



		public void ReleaseHandler(IHttpHandler handler)
		{
			IDisposable d = handler as IDisposable;

			if (d != null)
				d.Dispose();
		}
	}
}
