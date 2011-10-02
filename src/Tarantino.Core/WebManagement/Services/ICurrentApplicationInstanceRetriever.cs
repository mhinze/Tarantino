
using Tarantino.Core.WebManagement.Model;

namespace Tarantino.Core.WebManagement.Services
{
	
	public interface ICurrentApplicationInstanceRetriever
	{
		ApplicationInstance GetApplicationInstance();
	}
}