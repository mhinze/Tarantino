namespace Tarantino.Core.Daemon.Services
{
	public interface IServiceAgentFactory
	{
		IServiceAgent[] GetServiceAgents();
	}
}