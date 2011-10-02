namespace Tarantino.Core.Commons.Services.Configuration
{
	public interface IServiceLocator
	{
		T CreateInstance<T>(string instanceKey);
		T CreateInstance<T>();
		T[] CreateAllInstances<T>();
	}
}