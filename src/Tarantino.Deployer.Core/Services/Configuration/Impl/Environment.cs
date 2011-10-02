using System.Configuration;
using Tarantino.Core.Commons.Services.Configuration.Impl;

namespace Tarantino.Deployer.Core.Services.Configuration.Impl
{
	public sealed class Environment : NamedElement
	{
		[ConfigurationProperty("Predecessor", IsRequired = false)]
		public string Predecessor
		{
			get { return (string) this["Predecessor"]; }
			set { this["Predecessor"] = value; }
		}

		public override string GetElementName()
		{
			return "Environment";
		}
	}
}