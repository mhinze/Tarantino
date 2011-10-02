using Tarantino.Core.DatabaseManager.Model;

namespace Tarantino.Core.DatabaseManager.Services
{
	
	public interface ILogMessageGenerator
	{
		string GetInitialMessage(TaskAttributes taskAttributes);
	}
}