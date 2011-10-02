using Tarantino.Deployer.Core.Services.UI;
using Tarantino.Deployer.Core.Model;

namespace Tarantino.Deployer.Core.Services.UI.Impl
{
	public class DeploymentSelectionValidator : IDeploymentSelectionValidator
	{
		public bool IsValid(string versionNumberText, Deployment selectedDeployment)
		{
			bool isValidDeployment = (versionNumberText != string.Empty) && (selectedDeployment != null);
			return isValidDeployment;
		}
	}
}