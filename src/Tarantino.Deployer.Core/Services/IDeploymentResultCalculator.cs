using Tarantino.Deployer.Core.Model;

namespace Tarantino.Deployer.Core.Services
{
	
	public interface IDeploymentResultCalculator
	{
		DeploymentResult GetResult(string output);
	}
}