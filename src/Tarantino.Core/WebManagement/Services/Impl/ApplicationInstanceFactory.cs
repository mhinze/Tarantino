using Tarantino.Core.Commons.Services.Configuration;
using Tarantino.Core.Commons.Services.Environment;
using Tarantino.Core.WebManagement.Model;

namespace Tarantino.Core.WebManagement.Services.Impl
{
	
	public class ApplicationInstanceFactory : IApplicationInstanceFactory
	{
		private readonly ISystemEnvironment _systemEnvironment;
		private readonly IAssemblyContext _assemblyContext;
		private readonly IConfigurationReader _configurationReader;

		public ApplicationInstanceFactory(ISystemEnvironment systemEnvironment, IAssemblyContext assemblyContext, IConfigurationReader configurationReader)
		{
			_systemEnvironment = systemEnvironment;
			_assemblyContext = assemblyContext;
			_configurationReader = configurationReader;
		}

		public ApplicationInstance Create()
		{
			string hostHeader = _configurationReader.GetRequiredSetting("TarantinoWebManagementHttpHost");

			ApplicationInstance instance = new ApplicationInstance();
			instance.MachineName = _systemEnvironment.GetMachineName();
			instance.AvailableForLoadBalancing = true;
			instance.Version = _assemblyContext.GetAssemblyVersion();
			instance.MaintenanceHostHeader = hostHeader;
			instance.ApplicationDomain = hostHeader;

			return instance;
		}
	}
}