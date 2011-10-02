using System.Configuration;
using Tarantino.Core.Commons.Services.Configuration.Impl;

namespace Tarantino.Deployer.Core.Services.Configuration.Impl
{
	public class DeployerSettingsConfigurationHandler : ConfigurationSection
	{
		[ConfigurationProperty("Applications", IsDefaultCollection = true)]
		public ElementCollection<Application> Applications
		{
			get { return (ElementCollection<Application>)base["Applications"]; }
		}
	}
}