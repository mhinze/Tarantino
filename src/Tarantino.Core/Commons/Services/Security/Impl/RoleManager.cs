using System;
using System.Security.Principal;
using Tarantino.Core.Commons.Services.Security;

using Tarantino.Core.Commons.Services.Web;

namespace Tarantino.Core.Commons.Services.Security.Impl
{
	
	public class RoleManager : IRoleManager
	{
		private readonly IWebContext _context;
		private readonly IPrincipalFactory _principalFactory;

		public RoleManager(IWebContext context, IPrincipalFactory principalFactory)
		{
			_context = context;
			_principalFactory = principalFactory;
		}

		public void AssignCurrentUserToRoles(params string[] roles)
		{
			IIdentity userIdentity = _context.GetUserIdentity();
			IPrincipal principal = _principalFactory.CreatePrincipal(userIdentity, roles);
			_context.SetUser(principal);
		}

		public bool IsCurrentUserInRole(string role)
		{
			IPrincipal principal = _context.GetUserPrinciple();
			bool isInRole = principal.IsInRole(role);
			return isInRole;
		}

		public bool IsCurrentUserInAtLeastOneRole(params string[] roles)
		{
			bool inAtLeastOneRole = false;

			foreach (string role in roles)
			{
				if (IsCurrentUserInRole(role))
				{
					inAtLeastOneRole = true;
				}
			}

			return inAtLeastOneRole;
		}
	}
}