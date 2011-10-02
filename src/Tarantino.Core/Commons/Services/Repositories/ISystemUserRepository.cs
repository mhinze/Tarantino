using Tarantino.Core.Commons.Model;

namespace Tarantino.Core.Commons.Services.Repositories
{
	public interface ISystemUserRepository
	{
		bool IsValidLogin(string emailAddress, string password);
		ISystemUser GetByEmailAddress(string emailAddress);
	}
}