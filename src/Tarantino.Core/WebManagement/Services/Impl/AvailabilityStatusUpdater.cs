using System;

using Tarantino.Core.WebManagement.Model;
using Tarantino.Core.WebManagement.Services.Repositories;

namespace Tarantino.Core.WebManagement.Services.Impl
{
	
	public class AvailabilityStatusUpdater : IAvailabilityStatusUpdater
	{
		private readonly IApplicationInstanceRepository _repository;
		private readonly IApplicationInstanceContext _context;

		public AvailabilityStatusUpdater(IApplicationInstanceRepository repository, IApplicationInstanceContext context)
		{
			_repository = repository;
			_context = context;
		}

		public void SetAvailabilityStatus(bool enabled)
		{
			ApplicationInstance instance = _context.GetCurrent();
			instance.AvailableForLoadBalancing = enabled;
			_repository.Save(instance);
		}
	}
}