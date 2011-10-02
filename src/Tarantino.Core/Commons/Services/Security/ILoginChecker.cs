using Tarantino.Core.Commons.Services.Repositories;

namespace Tarantino.Core.Commons.Services.Security
{
	public interface ILoginChecker
	{
		bool IsValidUser(string emailAddress, string clearTextPassword, ISystemUserRepository repository);
	}
}