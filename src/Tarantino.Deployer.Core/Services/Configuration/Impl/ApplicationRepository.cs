using Tarantino.Core.Commons.Services.Configuration;
using Tarantino.Core.Commons.Services.Configuration.Impl;

namespace Tarantino.Deployer.Core.Services.Configuration.Impl
{
	public class ApplicationRepository : IApplicationRepository
	{
		private readonly IApplicationConfiguration _configuration;

		public ApplicationRepository(IApplicationConfiguration configuration)
		{
			_configuration = configuration;
		}

		public ElementCollection<Application> GetAll()
		{
			object sectionObject = _configuration.GetSection("DeployerSettings");

			var handler = (DeployerSettingsConfigurationHandler) sectionObject;

			return handler.Applications;
		}

		public Application GetByName(string applicationName)
		{
			foreach (Application application in GetAll())
			{
				if (application.Name == applicationName)
				{
					return application;
				}
			}

			return null;
		}
	}
}