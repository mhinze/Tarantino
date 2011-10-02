using System;
using System.Collections.Generic;
using Tarantino.Deployer.Core.Model;

namespace Tarantino.Deployer.Core.Services
{
	
	public interface IDeploymentRepository
	{
		IEnumerable<Deployment> Find(string application, string environment);
		IEnumerable<Deployment> FindSuccessfulUncertified(string application, string environment);
		IEnumerable<Deployment> FindCertified(string application, string environment);
		void Save(Deployment deployment);
		Deployment GetById(Guid id);
	}
}