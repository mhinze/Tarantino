using Tarantino.Core.Commons.Services.Repositories;

namespace Tarantino.Core.Commons.Services.Security
{
	public interface ILoginService
	{
		string Login(string emailAddress, string password, bool rememberMe, ISystemUserRepository repository);
		void Logout();
	}
}