using System;
using Tarantino.Infrastructure;

namespace Tarantino.WebManagement.Modules
{
	public abstract class ModuleBase : System.Web.IHttpModule
	{
		protected System.Web.HttpApplication _context;

		public void Init(System.Web.HttpApplication context)
		{
			InfrastructureDependencyRegistrar.RegisterInfrastructure();

			_context = context;
			_context.PreRequestHandlerExecute += PreRequestHandlerExecute;
			_context.BeginRequest += BeginRequest;
			_context.AuthenticateRequest += AuthenticateRequest;
			_context.AcquireRequestState += AcquireRequestState;
			Initialized();
		}

		protected virtual void Initialized() { }

		public void Dispose()
		{
		}

		protected virtual void AuthenticateRequest(object sender, EventArgs e) { }

		protected abstract void BeginRequest(object sender, EventArgs e);

		protected abstract void AcquireRequestState(object sender, EventArgs e);

		protected virtual void PreRequestHandlerExecute(object sender, EventArgs e) { }
	}
}