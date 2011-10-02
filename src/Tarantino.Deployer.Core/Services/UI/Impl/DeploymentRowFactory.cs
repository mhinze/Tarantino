using Tarantino.Deployer.Core.Model;

namespace Tarantino.Deployer.Core.Services.UI.Impl
{
	public class DeploymentRowFactory : IDeploymentRowFactory
	{
		public string[] ConstructRow(Deployment deployment)
		{
			string version = deployment.Version;
			string deployedOn = deployment.DeployedOn.ToString("g");
			string deployedBy = deployment.DeployedBy;
			string result = deployment.Result.DisplayName;
			string certifiedOn = deployment.CertifiedOn != null ? deployment.CertifiedOn.Value.ToString("g") : string.Empty;
			string certifiedBy = deployment.CertifiedBy;
			string deploymentId = deployment.Id.ToString();

			return new[] {version, deployedOn, deployedBy, result, certifiedOn, certifiedBy, deploymentId};
		}
	}
}