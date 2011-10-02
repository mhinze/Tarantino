using System.Configuration;

namespace Tarantino.Core.Commons.Services.Configuration.Impl
{
	public class ElementCollection<T> : ConfigurationElementCollection where T : NamedElement, new()
	{
		protected override ConfigurationElement CreateNewElement()
		{
			return new T();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((T)element).Name;
		}

		public override ConfigurationElementCollectionType CollectionType
		{
			get { return ConfigurationElementCollectionType.BasicMap; }
		}

		public T this[int index]
		{
			get { return (T)BaseGet(index); }
		}

		protected override string ElementName
		{
			get { return new T().GetElementName(); }
		}
	}
}