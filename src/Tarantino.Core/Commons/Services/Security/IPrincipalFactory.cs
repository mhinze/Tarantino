using System.Security.Principal;

namespace Tarantino.Core.Commons.Services.Security
{
	public interface IPrincipalFactory
	{
		IPrincipal CreatePrincipal(IIdentity identity, params string[] roles);
	}
}