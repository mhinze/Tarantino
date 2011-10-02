using System;
using Tarantino.Core.Commons.Services.Configuration;
using Tarantino.Core.Commons.Services.Environment;

using Tarantino.Core.Commons.Services.Logging;

namespace Tarantino.Core.Daemon.Services.Impl
{
	
	public class ServiceAgentAggregator : IServiceAgentAggregator
	{
		private readonly IApplicationSettings _settings;
		private readonly ITypeActivator _activator;

		public ServiceAgentAggregator(IApplicationSettings settings, ITypeActivator activator)
		{
			_settings = settings;
			_activator = activator;
		}

		public void ExecuteServiceAgentCycle()
		{
			string factoryType = _settings.GetServiceAgentFactory();
			IServiceAgentFactory factory = _activator.ActivateType<IServiceAgentFactory>(factoryType);

			foreach (IServiceAgent agent in factory.GetServiceAgents())
			{
				try
				{
					Logger.Debug(this, string.Format("Executing agent: {0}", agent.AgentName));
					agent.Run();
                    Logger.Debug(this, string.Format("Agent execution completed: {0}", agent.AgentName));
				}
				catch (Exception ex)
				{
                    Logger.Error(this, ex.Message, ex);
				}
			}
		}
	}
}