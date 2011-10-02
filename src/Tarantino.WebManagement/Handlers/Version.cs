using Tarantino.Core.WebManagement.Model;

namespace Tarantino.WebManagement.Handlers
{
	public class Version : HandlerBase
	{
		protected override void OnProcessRequest()
		{
			ApplicationInstance applicationInstance = CurrentContext.CurrentApplicationInstance;
			Write(string.Format("{0} {1}", applicationInstance, applicationInstance.Version));
		}
	}
}