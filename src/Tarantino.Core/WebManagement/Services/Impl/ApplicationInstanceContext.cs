
using Tarantino.Core.WebManagement.Model;

namespace Tarantino.Core.WebManagement.Services.Impl
{
	
	public class ApplicationInstanceContext : IApplicationInstanceContext
	{
		private readonly IApplicationInstanceCache _cache;
		private readonly ICurrentApplicationInstanceRetriever _retriever;

		public ApplicationInstanceContext(IApplicationInstanceCache cache, ICurrentApplicationInstanceRetriever retriever)
		{
			_cache = cache;
			_retriever = retriever;
		}

		public ApplicationInstance GetCurrent()
		{
			ApplicationInstance instance = _cache.GetCurrent();

			if (instance == null)
			{
				instance = _retriever.GetApplicationInstance();
				_cache.Set(ApplicationInstance.CacheKey, instance);
			}

			return instance;
		}
	}
}