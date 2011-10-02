
using Tarantino.Core.WebManagement.Model;

namespace Tarantino.Core.WebManagement.Services
{
	
	public interface IApplicationInstanceContext
	{
		ApplicationInstance GetCurrent();
	}
}