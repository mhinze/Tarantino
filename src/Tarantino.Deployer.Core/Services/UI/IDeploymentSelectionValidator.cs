using Tarantino.Deployer.Core.Model;

namespace Tarantino.Deployer.Core.Services.UI
{
	public interface IDeploymentSelectionValidator
	{
		bool IsValid(string versionNumberText, Deployment selectedDeployment);
	}
}