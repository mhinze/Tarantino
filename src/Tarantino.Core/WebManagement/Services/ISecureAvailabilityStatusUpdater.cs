

namespace Tarantino.Core.WebManagement.Services
{
	
	public interface ISecureAvailabilityStatusUpdater
	{
		string SetStatus(bool enabled);
	}
}