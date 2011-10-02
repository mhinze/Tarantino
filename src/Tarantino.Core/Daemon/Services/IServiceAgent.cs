namespace Tarantino.Core.Daemon.Services
{
	public interface IServiceAgent
	{
		void Run();

		string AgentName { get; }
	}
}