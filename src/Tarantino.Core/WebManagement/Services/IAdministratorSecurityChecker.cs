

namespace Tarantino.Core.WebManagement.Services
{
	
	public interface IAdministratorSecurityChecker
	{
		bool IsCurrentUserAdministrator();
	}
}