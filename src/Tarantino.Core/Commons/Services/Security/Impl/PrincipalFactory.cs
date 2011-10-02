using System.Security.Principal;


namespace Tarantino.Core.Commons.Services.Security.Impl
{
	
	public class PrincipalFactory : IPrincipalFactory
	{
		public IPrincipal CreatePrincipal(IIdentity identity, params string[] roles)
		{
			GenericPrincipal principal = new GenericPrincipal(identity, roles);
			return principal;
		}
	}
}