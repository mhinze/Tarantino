using Tarantino.Core.Commons.Services.Environment;
using Tarantino.Core.Commons.Services.Security;
using Tarantino.Deployer.Core.Model;

namespace Tarantino.Deployer.Core.Services.Impl
{
	public class VersionCertifier : IVersionCertifier
	{
		private readonly ISystemClock _clock;
		private readonly ISecurityContext _securityContext;
		private readonly IDeploymentRepository _repository;

		public VersionCertifier(ISystemClock clock, ISecurityContext securityContext, IDeploymentRepository repository)
		{
			_clock = clock;
			_securityContext = securityContext;
			_repository = repository;
		}

		public void Certify(Deployment deployment)
		{
			if (deployment != null)
			{
				deployment.CertifiedBy = _securityContext.GetCurrentUsername();
				deployment.CertifiedOn = _clock.GetCurrentDateTime();

				_repository.Save(deployment);
			}
		}
	}
}