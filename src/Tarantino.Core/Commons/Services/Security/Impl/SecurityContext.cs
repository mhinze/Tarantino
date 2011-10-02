

namespace Tarantino.Core.Commons.Services.Security.Impl
{
	
	public class SecurityContext : ISecurityContext
	{
		private readonly IWindowsIdentity _windowsIdentity;

		public SecurityContext(IWindowsIdentity windowsIdentity)
		{
			_windowsIdentity = windowsIdentity;
		}

		public string GetCurrentUsername()
		{
			string fullUsername = _windowsIdentity.GetCurrentUsername();
			string[] usernameParts = fullUsername.Split('\\');
			string username = usernameParts[usernameParts.Length - 1];
			return username;
		}
	}
}