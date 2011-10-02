using System.Configuration;

namespace Tarantino.Core.Commons.Services.Configuration.Impl
{
	public abstract class NamedElement : ConfigurationElement
	{
		[ConfigurationProperty("Name", IsKey = true, IsRequired = true)]
		public string Name
		{
			get { return (string)this["Name"]; }
		}

		public abstract string GetElementName();

		public override string ToString()
		{
			return Name;
		}
	}
}