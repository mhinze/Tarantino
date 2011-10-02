
using Tarantino.Core.Commons.Services.Repositories;

namespace Tarantino.Core.Commons.Services.Security.Impl
{
	
	public class LoginChecker : ILoginChecker
	{
		private readonly IEncryptionEngine _encryptionEngine;

		public LoginChecker(IEncryptionEngine encryptionEngine)
		{
			_encryptionEngine = encryptionEngine;
		}

		public bool IsValidUser(string emailAddress, string cleartextPassword, ISystemUserRepository repository)
		{
			string encryptedPassword = _encryptionEngine.Encrypt(cleartextPassword);
			bool isValidUser = repository.IsValidLogin(emailAddress, encryptedPassword);

			return isValidUser;
		}
	}
}