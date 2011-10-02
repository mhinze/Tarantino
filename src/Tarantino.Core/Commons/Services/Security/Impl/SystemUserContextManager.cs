using System.Security.Principal;

using Tarantino.Core.Commons.Model;
using Tarantino.Core.Commons.Services.Repositories;
using Tarantino.Core.Commons.Services.Web;

namespace Tarantino.Core.Commons.Services.Security.Impl
{
	
	public class SystemUserContextManager : ISystemUserContextManager
	{
		public const string CURRENT_USER = "CurrentUser";

		private readonly IWebContext _webContext;
		private readonly ISystemUserRepository _repository;

		public SystemUserContextManager(IWebContext webContext, ISystemUserRepository repository)
		{
			_webContext = webContext;
			_repository = repository;
		}

		public void SetUserContext()
		{
			IIdentity userIdentity = _webContext.GetUserIdentity();

			if (userIdentity != null)
			{
				ISystemUser systemUser = _repository.GetByEmailAddress(userIdentity.Name);
				_webContext.SetItem(CURRENT_USER, systemUser);
			}
		}

		public ISystemUser GetCurrentUser()
		{
			ISystemUser user = _webContext.GetItem<ISystemUser>(CURRENT_USER);
			return user;
		}
	}
}