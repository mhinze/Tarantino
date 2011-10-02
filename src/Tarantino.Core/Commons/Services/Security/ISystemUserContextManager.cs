using Tarantino.Core;
using Tarantino.Core.Commons.Model;

namespace Tarantino.Core.Commons.Services.Security
{
	public interface ISystemUserContextManager
	{
		void SetUserContext();
		ISystemUser GetCurrentUser();
	}
}