using System;
using System.ServiceProcess;
using StructureMap;
using Tarantino.Core;
using Tarantino.Core.Commons.Services.Logging;
using Tarantino.Core.Daemon.Services;

namespace Tarantino.Daemon
{
	public class DefaultService : ServiceBase
	{
		public const string SERVICE_DESCRIPTION = "This service manages the execution of daemon service agents";
		public const string SERVICE_NAME = "Tarantino.Daemon";

		private readonly IServiceRunner _serviceRunner;

		public DefaultService()
		{
			try
			{
				CoreDependencyRegistrar.Register();
				Logger.Info(this, "Tarantino.Daemon Service starting");
				ServiceName = SERVICE_NAME;
				_serviceRunner = ObjectFactory.GetInstance<IServiceRunner>();
				Logger.Info(this, "Service Runner loaded");
			}
			catch (Exception exc)
			{
				Logger.Fatal(this, "Service failed to start", exc);
				throw;
			}
		}

		protected override void OnStart(string[] args)
		{
			try
			{
				Logger.Info(this, "Service Runner executed");
				_serviceRunner.Start();
			}
			catch (Exception exc)
			{
				Logger.Fatal(this, "Service failed to start", exc);
				throw;
			}
		}

		protected override void OnStop()
		{
			try
			{
				Logger.Info(this, "Tarantino.Daemon Service stopping");
				_serviceRunner.Stop();
			}
			catch (Exception exc)
			{
				Logger.Fatal(this, "Service failed to stop", exc);
				throw;
			}
		}
	}
}