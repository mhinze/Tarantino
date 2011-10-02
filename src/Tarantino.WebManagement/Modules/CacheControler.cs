using System;
using System.Web;
using System.Configuration;
using System.Collections.Specialized;
using System.Web.Caching;

namespace Tarantino.WebManagement.Modules
{
	public class CacheControler : ModuleBase
	{
		protected static string[] domainNames;

		protected bool domainShouldBeCached;

		protected const string CacheDependencyKey = "OutputCacheDependency";

		protected override void Initialized()
		{
			if (domainNames == null)
			{
				var domainnames = (NameValueCollection)ConfigurationManager.GetSection("CacheControl/DomainNames");
				domainNames = new string[domainnames.Count];
				var index = 0;
				foreach (string domain in domainnames.Keys)
				{
					domainNames[index] = domain;
					index++;
				}
				Array.Sort(domainNames);
			}
			InsertCacheDependencyKey();
		}


		public virtual void Validate(HttpContext context, Object data, ref HttpValidationStatus status)
		{
			if (domainShouldBeCached)
				status = HttpValidationStatus.Valid;
			else
				status = HttpValidationStatus.IgnoreThisRequest;
		}

		private void SetDomainShouldBeCached()
		{
			string domain = _context.Request.ServerVariables["HTTP_HOST"];
			domainShouldBeCached = (domain != null && Array.BinarySearch(domainNames, domain) >= 0);
		}

		private void InsertCacheDependencyKey()
		{
			var httpcontext = HttpContext.Current;

			if (httpcontext.Cache[CacheDependencyKey] == null)
			{
				httpcontext.Cache.Insert(CacheDependencyKey, DateTime.Now, null,
				                         DateTime.MaxValue, TimeSpan.Zero,
				                         CacheItemPriority.NotRemovable,
				                         null);
			}
		}

		protected override void BeginRequest(object sender, EventArgs e)
		{
			SetDomainShouldBeCached();

			InsertCacheDependencyKey();

			//callback to prevent showing cached content on non cached domain names
			_context.Response.Cache.AddValidationCallback(new HttpCacheValidateHandler(Validate), null);

			//Do not cache the output of this request.
			if (_context.Request.HttpMethod == "POST" || !domainShouldBeCached)
			{
				_context.Response.Cache.SetNoServerCaching();
			}

			_context.Response.AddCacheItemDependency(CacheDependencyKey);

		}

		protected override void AcquireRequestState(object sender, EventArgs e)
		{

		}
	}
}