
using Tarantino.Core.Commons.Services.Configuration;
using Tarantino.Core.Commons.Services.Environment;
using Tarantino.Core.WebManagement.Model;
using Tarantino.Core.WebManagement.Services.Repositories;

namespace Tarantino.Core.WebManagement.Services.Impl
{
	
	public class CurrentApplicationInstanceRetriever : ICurrentApplicationInstanceRetriever
	{
		private readonly ISystemEnvironment _environment;
		private readonly IConfigurationReader _configurationReader;
		private readonly IApplicationInstanceRepository _repository;
		private readonly IApplicationInstanceFactory _factory;

		public CurrentApplicationInstanceRetriever(ISystemEnvironment environment, IConfigurationReader configurationReader, IApplicationInstanceRepository repository, IApplicationInstanceFactory factory)
		{
			_environment = environment;
			_configurationReader = configurationReader;
			_repository = repository;
			_factory = factory;
		}

		public ApplicationInstance GetApplicationInstance()
		{
			string machineName = _environment.GetMachineName();
			string applicationDomainName = _configurationReader.GetRequiredSetting("TarantinoWebManagementHttpHost");
			ApplicationInstance instance = _repository.GetByMaintenanceHostHeaderAndMachineName(applicationDomainName, machineName);

			if (instance == null)
			{
				instance = _factory.Create();
				_repository.Save(instance);
			}
			
			return instance;
		}
	}
}