using System;

using Tarantino.Core.Commons.Services.Environment;
using Tarantino.Core.Commons.Services.Web;
using Tarantino.Core.WebManagement.Model;

namespace Tarantino.Core.WebManagement.Services.Impl
{
	
	public class ApplicationInstanceCache : IApplicationInstanceCache
	{
		private readonly IWebContext _context;
		private readonly ISystemClock _clock;

		public ApplicationInstanceCache(IWebContext context, ISystemClock clock)
		{
			_context = context;
			_clock = clock;
		}

		public ApplicationInstance GetCurrent()
		{
			ApplicationInstance instance = _context.GetItem<ApplicationInstance>(ApplicationInstance.CacheKey);

			if (instance == null)
			{
				instance = _context.GetCacheItem<ApplicationInstance>(ApplicationInstance.CacheKey);
			}

			return instance;
		}

		public void Set(string key, ApplicationInstance item)
		{
			DateTime expiration = _clock.GetCurrentDateTime().AddMinutes(1);

			_context.SetItem(key, item);
			_context.SetCacheItem(key, item, expiration, TimeSpan.Zero);
		}
	}
}