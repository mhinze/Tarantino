using System.Collections.Generic;
using System.Threading;

namespace Tarantino.Core.Commons.Services.Caching.Impl
{
	public static class Cache
	{
		private static ReaderWriterLock _lock = new ReaderWriterLock();

		private static Dictionary<object, object> _dictionary = new Dictionary<object, object>();

		public static void Set<T>(object key, T objectToCache)
		{
			_lock.AcquireWriterLock(Timeout.Infinite);

			try
			{
				_dictionary[key] = objectToCache;
			}
			finally
			{
				_lock.ReleaseWriterLock();
			}
		}

		public static T Get<T>(object key)
		{
			T cachedObject = default(T);

			_lock.AcquireReaderLock(Timeout.Infinite);

			try
			{
				if (_dictionary.ContainsKey(key))
				{
					cachedObject = (T)_dictionary[key];
				}
			}
			finally
			{
				_lock.ReleaseReaderLock();
			}

			return cachedObject;
		}

		public static void Clear()
		{
			_lock.AcquireWriterLock(Timeout.Infinite);

			try
			{
				_dictionary = new Dictionary<object, object>();
			}
			finally
			{
				_lock.ReleaseWriterLock();
			}
		}

		public static bool Has(object key)
		{
			bool hasKey = _dictionary.ContainsKey(key);
			return hasKey;
		}
	}
}