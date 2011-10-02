using System;
using System.Web;
using Tarantino.Core.Commons.Services.Logging;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;

namespace Tarantino.Infrastructure.Commons.DataAccess.ORMapper
{
	public class NHibernateSessionModule : IHttpModule
	{
		public void Init(HttpApplication context)
		{
			context.EndRequest += context_EndRequest;
		}

		private void context_EndRequest(object sender, EventArgs e)
		{
			var builder = new HybridSessionBuilder();

			var session = builder.GetExistingWebSession();
			if (session != null)
			{
				Logger.Debug(this, "Disposing of ISession " + session.GetHashCode());
				session.Dispose();
			}
		}

		public void Dispose()
		{
		}
	}
}