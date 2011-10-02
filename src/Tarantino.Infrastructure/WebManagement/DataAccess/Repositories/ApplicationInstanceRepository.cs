using System;
using System.Collections.Generic;
using Tarantino.Core.Commons.Services.Repositories;
using Tarantino.Core.WebManagement.Model;
using Tarantino.Core.WebManagement.Services.Repositories;

namespace Tarantino.Infrastructure.WebManagement.DataAccess.Repositories
{
	public class ApplicationInstanceRepository : IApplicationInstanceRepository
	{
		private readonly IPersistentObjectRepository _objectRepository;

		public ApplicationInstanceRepository(IPersistentObjectRepository objectRepository)
		{
			_objectRepository = objectRepository;
			_objectRepository.ConfigurationFile = "webmanagement.hibernate.cfg.xml";
		}

		public IEnumerable<ApplicationInstance> GetAll()
		{
			var instances = _objectRepository.GetAll<ApplicationInstance>();
			return instances;
		}

		public ApplicationInstance GetByMaintenanceHostHeaderAndMachineName(string maintenanceHostHeader, string machineName)
		{
			var criteria = new CriterionSet();
			criteria.AddCriterion(new Criterion(ApplicationInstance.MachineNameAttribute, machineName));
			criteria.AddCriterion(new Criterion(ApplicationInstance.MaintenanceHostHeaderAttribute, maintenanceHostHeader));

			var instance = _objectRepository.FindFirst<ApplicationInstance>(criteria);
			return instance;
		}

		public ApplicationInstance GetById(Guid id)
		{
			var instance = _objectRepository.GetById<ApplicationInstance>(id);
			return instance;
		}

		public void Save(ApplicationInstance instance)
		{
			_objectRepository.Save(instance);
		}

		public void Delete(ApplicationInstance instance)
		{
			_objectRepository.Delete(instance);
		}

		public IEnumerable<ApplicationInstance> GetByHostHeader(string uniqueHostHeader)
		{
			var criteria = new CriterionSet();
			criteria.AddCriterion(new Criterion(ApplicationInstance.UniqueHostHeaderAttribute, uniqueHostHeader));
			IEnumerable<ApplicationInstance> instances = _objectRepository.FindAll<ApplicationInstance>(criteria);

			return instances;
		}
	}
}