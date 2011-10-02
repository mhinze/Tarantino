using Tarantino.Core.Commons.Services.Repositories;

namespace Tarantino.Core.Commons.Services.Security
{
	public interface IForgottenPasswordService
	{
		string SendEmailTo(string emailAddress, ISystemUserRepository repository);
	}
}