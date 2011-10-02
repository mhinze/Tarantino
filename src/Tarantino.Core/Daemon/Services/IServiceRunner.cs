namespace Tarantino.Core.Daemon.Services
{
	public interface IServiceRunner
	{
		void Start();
		void Stop();
		void RunOneCycle();
	}
}