using System;
using Tarantino.Core.Commons.Model;

namespace Tarantino.Deployer.Core.Model
{
	public class Deployment : PersistentObject
	{
		public const string APPLICATION = "Application";
		public const string ENVIRONMENT = "Environment";
		public const string DEPLOYED_ON = "DeployedOn";
		public const string CERTIFIED_ON = "CertifiedOn";
		public const string RESULT = "Result";

		public virtual string Application { get; set; }
		public virtual string Environment { get; set; }
		public virtual string Version { get; set; }
		public virtual DateTime DeployedOn { get; set; }
		public virtual DateTime? CertifiedOn { get; set; }
		public virtual string DeployedBy { get; set; }
		public virtual string CertifiedBy { get; set; }
		public virtual DeploymentOutput Output { get; set; }
		public virtual DeploymentResult Result { get; set; }

		public override string ToString()
		{
			return Version.ToString();
		}

		public virtual void SetOutput(DeploymentOutput output)
		{
			Output = output;
			output.Deployment = this;
		}
	}
}