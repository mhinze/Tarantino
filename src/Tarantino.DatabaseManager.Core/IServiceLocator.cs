namespace Tarantino.DatabaseManager.Core
{
    public interface IServiceLocator
    {
        T CreateInstance<T>(string instanceKey) ;
        T CreateInstance<T>();
        T[] CreateAllInstances<T>();
    }
}