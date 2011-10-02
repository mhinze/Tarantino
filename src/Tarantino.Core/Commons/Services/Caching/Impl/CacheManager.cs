using Tarantino.Core.Commons.Services.Caching.Impl;


namespace Tarantino.Core.Commons.Services.Caching.Impl
{
	
	public class CacheManager : ICacheManager
	{
		public void Set<T>(object key, T objectToCache)
		{
			Cache.Set(key, objectToCache);
		}

		public T Get<T>(object key)
		{
			return Cache.Get<T>(key);
		}

		public void Clear()
		{
			Cache.Clear();
		}

		public bool Has(object key)
		{
			return Cache.Has(key);
		}
	}
}