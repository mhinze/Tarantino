using System.Web;
using StructureMap;
using Tarantino.Core.WebManagement.Services;

namespace Tarantino.Infrastructure.WebManagement.Http
{
	public class LoadBalancer : IHttpHandler
	{
		public void ProcessRequest(HttpContext doNotUse)
		{
			IExceptionHandlingLoadBalanceStatusManager manager = ObjectFactory.GetInstance<IExceptionHandlingLoadBalanceStatusManager>();
			manager.HandleLoadBalancing();
		}

		public bool IsReusable
		{
			get { return false; }
		}
	}
}