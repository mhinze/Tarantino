

namespace Tarantino.Core.Commons.Services.Environment.Impl
{
	
	public class SystemEnvironment : ISystemEnvironment
	{
		public string GetMachineName()
		{
			return System.Environment.MachineName;
		}
	}
}