using Tarantino.Core.Commons.Services.Security;

namespace Tarantino.Deployer.Core.Services.Impl
{
	public class DeploymentRecorder : IDeploymentRecorder
	{
		private readonly ISecurityContext _securityContext;
		private readonly IDeploymentFactory _factory;
		private readonly IDeploymentRepository _repository;

		public DeploymentRecorder(ISecurityContext securityContext, IDeploymentFactory factory, IDeploymentRepository repository)
		{
			_securityContext = securityContext;
			_factory = factory;
			_repository = repository;
		}

		public string RecordDeployment(string application, string environment, string output, string version, bool failed)
		{
			var deployedBy = _securityContext.GetCurrentUsername();
			var deployment = _factory.CreateDeployment(application, environment, deployedBy, output, version, failed);
			_repository.Save(deployment);

			return deployment.Version;
		}
	}
}