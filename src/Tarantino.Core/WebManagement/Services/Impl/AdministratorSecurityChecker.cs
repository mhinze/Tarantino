using System.Collections.Generic;

using Tarantino.Core.Commons.Services.Configuration;
using Tarantino.Core.Commons.Services.Security;
using Tarantino.Core.Commons.Services.Web;

namespace Tarantino.Core.WebManagement.Services.Impl
{
	
	public class AdministratorSecurityChecker : IAdministratorSecurityChecker
	{
		private readonly IWebContext _context;
		private readonly IRoleManager _manager;
		private readonly IConfigurationReader _configurationReader;

		public AdministratorSecurityChecker(IWebContext context, IRoleManager manager, IConfigurationReader configurationReader)
		{
			_context = context;
			_manager = manager;
			_configurationReader = configurationReader;
		}

		public bool IsCurrentUserAdministrator()
		{
			IEnumerable<string> roles = _configurationReader.GetStringArray("TarantinoWebManagementRoles");
			string[] roleArray = new List<string>(roles).ToArray();
			bool isAdministrator = _manager.IsCurrentUserInAtLeastOneRole(roleArray);
			bool isAuthenticatedAdministrator = _context.UserIsAuthenticated() && isAdministrator;

			return isAuthenticatedAdministrator;
		}
	}
}