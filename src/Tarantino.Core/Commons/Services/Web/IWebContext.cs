using System;
using System.Security.Principal;

namespace Tarantino.Core.Commons.Services.Web
{
	public interface IWebContext
	{
		bool UserIsAuthenticated();
		IIdentity GetUserIdentity();
		void SetUser(IPrincipal user);
		void Redirect(string url);
		void SetItem(string key, object item);
		T GetItem<T>(string key);
		void RewriteUrl(string newUrl);
		string GetCurrentUrl();
		T GetCacheItem<T>(string key);
		void SetCacheItem(string key, object item, DateTime expiration, TimeSpan slidingExpiration);
		IPrincipal GetUserPrinciple();
		string GetRequestItem(string key);
		void SetHttpResponseStatus(int code, string description);
		void WriteToResponse(string message);
		void ServerTransfer(string url, bool preserveForm);
		string GetServerVariable(string variableName);
		bool HasSessionItem(string key);
		void SetSessionItem(string key, object item);
		T GetSessionItem<T>(string key);
		string GetBaseDirectory();
		string GetPhysicalApplicationPath();
		void SaveUploadedFileAs(string fileNameWithPath);
		string GetReferrerUrl();
		void AppendResponseHeader(string name, string value);
		void SetCharacterSet(string charSet);
		string GetCurrentFullUrl();
		string GetApplicationPath();
		void Abandon();
	}
}