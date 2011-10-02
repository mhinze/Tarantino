using Tarantino.Deployer.Core.Model;

namespace Tarantino.Deployer.Core.Services
{
	
	public interface IDeploymentFactory
	{
		Deployment CreateDeployment(string application, string environment, string deployedBy, string output, string version, bool failed);
	}
}