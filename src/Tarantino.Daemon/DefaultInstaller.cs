using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace Tarantino.Daemon
{
	[RunInstaller(true)]
	public class DefaultInstaller : Installer
	{
		private readonly ServiceInstaller serviceInstaller;
		private readonly ServiceProcessInstaller processInstaller;

		public DefaultInstaller()
		{
			// define and create the service installer
			serviceInstaller = new ServiceInstaller
			                   	{
			                   		StartType = ServiceStartMode.Manual,
			                   		ServiceName = DefaultService.SERVICE_NAME,
			                   		DisplayName = DefaultService.SERVICE_NAME,
			                   		Description = DefaultService.SERVICE_DESCRIPTION
			                   	};

			Installers.Add(serviceInstaller);

			// define and create the process installer
			processInstaller = new ServiceProcessInstaller {Account = ServiceAccount.LocalSystem};

			Installers.Add(processInstaller);
		}
	}
}