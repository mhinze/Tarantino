

namespace Tarantino.Core.Commons.Services.Caching
{
	
	public interface ICacheManager
	{
		void Set<T>(object key, T objectToCache);
		T Get<T>(object key);
		void Clear();
		bool Has(object key);
	}
}