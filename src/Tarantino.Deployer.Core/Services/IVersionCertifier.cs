using Tarantino.Deployer.Core.Model;

namespace Tarantino.Deployer.Core.Services
{
	public interface IVersionCertifier
	{
		void Certify(Deployment deployment);
	}
}