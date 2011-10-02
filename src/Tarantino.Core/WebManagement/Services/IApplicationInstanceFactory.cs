
using Tarantino.Core.WebManagement.Model;

namespace Tarantino.Core.WebManagement.Services
{
	
	public interface IApplicationInstanceFactory
	{
		ApplicationInstance Create();
	}
}