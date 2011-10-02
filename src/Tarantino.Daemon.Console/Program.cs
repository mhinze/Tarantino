using System;
using StructureMap;
using Tarantino.Core;
using Tarantino.Core.Commons.Services.Logging;
using Tarantino.Core.Daemon.Services;

namespace Tarantino.Daemon.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				CoreDependencyRegistrar.Register();
				var serviceRunner = ObjectFactory.GetInstance<IServiceRunner>();
				Logger.Info(serviceRunner, "Tarantino.Daemon MachineConsole starting");
				serviceRunner.Start();
			}
			catch (Exception exc)
			{
				System.Console.WriteLine("MachineConsole failed to run: {0}", exc);
				throw;
			}
		}
	}
}