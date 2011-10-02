using System;


namespace Tarantino.Core.Commons.Services.Environment.Impl
{
	
	public class TypeActivator : ITypeActivator
	{
		public T ActivateType<T>(string typeDescriptor)
		{
			Type type = Type.GetType(typeDescriptor);
			T instance = (T)Activator.CreateInstance(type);

			return instance;
		}
	}
}