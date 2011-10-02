using System;
using System.Web;
using System.Configuration;
using System.Collections.Specialized;

namespace Tarantino.WebManagement.Modules
{
	public class SSL : IHttpModule
	{
		public void Init(HttpApplication context)
		{
			context.PreRequestHandlerExecute += context_PreRequestHandlerExecute;
		}

		public void Dispose()
		{
		}

		private void context_PreRequestHandlerExecute(object sender, EventArgs e)
		{
			var httpContext = HttpContext.Current;

			if (ByPassSSL(httpContext))
				return;

			//Get list of files to be secured
			var sslFiles = (NameValueCollection) ConfigurationManager.GetSection("RequiresSSL/RequiresSSL_Files");

			//Get list of paths to be secured
			var sslPaths = (NameValueCollection) ConfigurationManager.GetSection("RequiresSSL/RequiresSSL_Paths");

			//Simple Screen Writes to let the user know what's going on, what we found
			//WriteEntriesToResponse(sslFiles,"Files",ctx);
			//WriteEntriesToResponse(sslPaths,"Paths",ctx);

			string File = httpContext.Request.Url.PathAndQuery.ToLower();

			//Strip queary string for now
			if (File.IndexOf("?") > -1)
				File = File.Substring(0, File.IndexOf("?"));

			//First check the paths
			bool requiresSSL = IsFileInrequiredCollection(sslPaths, File);

			//Now check the files, only if path doesn't require SSL
			if (requiresSSL == false)
				requiresSSL = IsFileInrequiredCollection(sslFiles, File);

			if (requiresSSL && !httpContext.Request.IsSecureConnection)
				httpContext.Response.Redirect(httpContext.Request.Url.ToString().ToLower().Replace("http:", "https:"));

			if (httpContext.Request.IsSecureConnection && !requiresSSL)
				httpContext.Response.Redirect(httpContext.Request.Url.ToString().ToLower().Replace("https:", "http:"));
		}

		private bool ByPassSSL(HttpContext ctx)
		{
			return (ctx.Request.Cookies["BypassSSL"] != null && ctx.Request.Cookies["BypassSSL"].Value == "true");
		}

		private bool IsFileInrequiredCollection(NameValueCollection nv, string filename)
		{
			bool RequiresSSL = false;
			foreach (string key in nv.Keys)
			{
				if (filename.StartsWith(key.ToLower()) || filename.EndsWith(key.ToLower()))
				{
					RequiresSSL = true;
					break;
				}
			}
			return RequiresSSL;
		}
	}
}