using System.Diagnostics;
using System.Reflection;

namespace Tarantino.Core.Commons.Services.Environment.Impl
{
	public class AssemblyContext : IAssemblyContext
	{
		public Assembly GetExecutingAssembly()
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			return assembly;
		}

		public string GetAssemblyVersion()
		{
			Assembly executingAssembly = GetExecutingAssembly();
			FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(executingAssembly.Location);
			return versionInfo.FileVersion;
		}
	}
}