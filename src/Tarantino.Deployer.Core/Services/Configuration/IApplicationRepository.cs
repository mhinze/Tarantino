using Tarantino.Core.Commons.Services.Configuration.Impl;
using Tarantino.Deployer.Core.Services.Configuration.Impl;


namespace Tarantino.Deployer.Core.Services.Configuration
{
	
	public interface IApplicationRepository
	{
		ElementCollection<Application> GetAll();
		Application GetByName(string applicationName);
	}
}