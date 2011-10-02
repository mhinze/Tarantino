using System;
using System.IO;
using System.Security.Principal;
using System.Web;
using Tarantino.Core.Commons.Services.Web;

namespace Tarantino.Infrastructure.Commons.UI.Services
{
	public class WebContext : IWebContext
	{
		public bool UserIsAuthenticated()
		{
			var isAuthenticated = HttpContext.Current.Request.IsAuthenticated;
			return isAuthenticated;
		}

		public string GetRequestItem(string key)
		{
			var value = HttpContext.Current.Request[key];
			return value;
		}

		public IIdentity GetUserIdentity()
		{
			var user = HttpContext.Current.User;
			var identity = user != null ? user.Identity : null;
			return identity;
		}

		public IPrincipal GetUserPrinciple()
		{
			var user = HttpContext.Current.User;
			return user;
		}

		public void SetItem(string key, object item)
		{
			HttpContext.Current.Items[key] = item;
		}

		public T GetItem<T>(string key)
		{
			var item = HttpContext.Current.Items[key];
			return (T)item;
		}

		public void SetSessionItem(string key, object item)
		{
			HttpContext.Current.Session[key] = item;
		}

		public string GetReferrerUrl()
		{
			return HttpContext.Current.Request.UrlReferrer.ToString();
		}

		public string GetPhysicalApplicationPath()
		{
			return HttpContext.Current.Request.PhysicalApplicationPath;
		}

		public void SaveUploadedFileAs(string fileNameWithPath)
		{
			var files = HttpContext.Current.Request.Files;

			if (files.Count == 0)
			{
				throw new ApplicationException("No upload files found");
			}

			files[0].SaveAs(fileNameWithPath);
		}

		public bool HasSessionItem(string key)
		{
			return HttpContext.Current.Session[key] != null;
		}

		public T GetSessionItem<T>(string key)
		{
			var item = HttpContext.Current.Session[key];
			return (T)item;
		}

		public T GetCacheItem<T>(string key)
		{
			var item = HttpContext.Current.Cache[key];
			return (T) item;
		}

		public void SetCacheItem(string key, object item, DateTime expiration, TimeSpan slidingExpiration)
		{
			HttpContext.Current.Cache.Insert(key, item, null, expiration, slidingExpiration);
		}

		public void RewriteUrl(string newUrl)
		{
			HttpContext.Current.RewritePath(newUrl);
		}

		public void Redirect(string url)
		{
			HttpContext.Current.Response.Redirect(url);
		}

		public void SetUser(IPrincipal user)
		{
			HttpContext.Current.User = user;
		}

		public void Abandon()
		{
			HttpContext.Current.Session.Abandon();
		}

		public string GetCurrentUrl()
		{
			var url = HttpContext.Current.Request.Path;
			return url;
		}

		public string GetCurrentFullUrl()
		{
			var url = HttpContext.Current.Request.RawUrl;
			return url;
		}

		public void SetHttpResponseStatus(int code, string description)
		{
			var response = HttpContext.Current.Response;
			response.StatusCode = code;
			response.StatusDescription = description;
		}

		public void WriteToResponse(string message)
		{
			HttpContext.Current.Response.Write(message);
		}

		public void ServerTransfer(string url, bool preserveForm)
		{
			HttpContext.Current.Server.Transfer(url, preserveForm);
		}

		public string GetServerVariable(string variableName)
		{
			return HttpContext.Current.Request.ServerVariables[variableName];
		}

		public string GetBaseDirectory()
		{
			var baseDirectory = Path.Combine(HttpContext.Current.Request.ApplicationPath, "bin");
			return baseDirectory;
		}

		public string GetApplicationPath()
		{
			var path = HttpContext.Current.Request.ApplicationPath;
			return path;
		}

		public void AppendResponseHeader(string name, string value)
		{
			HttpContext.Current.Response.AppendHeader(name, value);
		}

		public void SetCharacterSet(string charSet)
		{
			HttpContext.Current.Response.Charset = charSet;
		}
	}
}