using Tarantino.Deployer.Core.Model;

namespace Tarantino.Deployer.Core.Services.UI
{
	public interface IDeploymentRowFactory
	{
		string[] ConstructRow(Deployment deployment);
	}
}