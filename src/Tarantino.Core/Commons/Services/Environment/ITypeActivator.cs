

namespace Tarantino.Core.Commons.Services.Environment
{
	
	public interface ITypeActivator
	{
		T ActivateType<T>(string typeDescriptor);
	}
}