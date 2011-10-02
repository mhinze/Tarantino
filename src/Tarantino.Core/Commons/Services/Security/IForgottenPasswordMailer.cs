using Tarantino.Core;
using Tarantino.Core.Commons.Services.Repositories;

namespace Tarantino.Core.Commons.Services.Security
{
	public interface IForgottenPasswordMailer
	{
		bool SendForgottenPasswordEmail(string recipientEmailAddress, ISystemUserRepository repository);
	}
}