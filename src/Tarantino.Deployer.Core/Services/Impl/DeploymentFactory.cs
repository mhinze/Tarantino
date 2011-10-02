using Tarantino.Core.Commons.Services.Environment;
using Tarantino.Deployer.Core.Model;


namespace Tarantino.Deployer.Core.Services.Impl
{
	
	public class DeploymentFactory : IDeploymentFactory
	{
		private readonly ISystemClock _clock;
		private readonly IDeploymentResultCalculator _resultCalculator;

		public DeploymentFactory(ISystemClock clock, IDeploymentResultCalculator resultCalculator)
		{
			_clock = clock;
			_resultCalculator = resultCalculator;
		}

		public Deployment CreateDeployment(string application, string environment, string deployedBy, string output, string version, bool failed)
		{
			var deployment = new Deployment
			                 	{
			                 		Application = application,
			                 		Environment = environment,
			                 		Version = version,
			                 		DeployedBy = deployedBy,
			                 		DeployedOn = _clock.GetCurrentDateTime(),
			                 		Result = (failed || (_resultCalculator.GetResult(output) == DeploymentResult.Failure)) ? DeploymentResult.Failure : DeploymentResult.Success
			                 	};

			deployment.SetOutput(new DeploymentOutput {Output = output});

			return deployment;
		}
	}
}