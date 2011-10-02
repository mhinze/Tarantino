using System.Text;
using Tarantino.Deployer.Core.Model;
using Environment=Tarantino.Deployer.Core.Services.Configuration.Impl.Environment;

namespace Tarantino.Deployer.Core.Services.UI.Impl
{
	public class LabelTextGenerator : ILabelTextGenerator
	{
		private readonly IDeploymentSelectionValidator _validator;

		public LabelTextGenerator(IDeploymentSelectionValidator validator)
		{
			_validator = validator;
		}

		public string GetDeploymentText(Environment environment, string versionNumberText, Deployment deployment)
		{
			return getText(environment, versionNumberText, deployment, Action.Deploy);
		}

		public string GetCertificationText(string versionNumberText, Deployment deployment)
		{
			return getText(null, versionNumberText, deployment, Action.Certify);
		}

		private string getText(Environment environment, string versionNumber, Deployment deployment, Action action)
		{
			var text = new StringBuilder();

			if (_validator.IsValid(versionNumber, deployment))
			{
				var isDeployment = action == Action.Deploy;
				var username = isDeployment ? deployment.DeployedBy : deployment.CertifiedBy;
				var date = isDeployment ? deployment.DeployedOn : deployment.CertifiedOn.GetValueOrDefault();

				if (environment != null)
				{
					text.AppendFormat("{0} on ", environment.Predecessor);
				}

				text.AppendFormat("{0} by {1}", date.ToString("g"), username);
			}

			return text.ToString();
		}

		enum Action
		{
			Deploy,
			Certify
		}
	}
}