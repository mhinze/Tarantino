using System.ServiceProcess;

namespace Tarantino.Daemon
{
	internal static class Program
	{
		private static void Main()
		{
			ServiceBase.Run(new ServiceBase[] {new DefaultService()});
		}
	}
}