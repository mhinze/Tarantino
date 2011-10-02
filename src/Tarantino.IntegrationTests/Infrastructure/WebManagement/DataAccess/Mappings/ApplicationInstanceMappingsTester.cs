using NUnit.Framework;
using Tarantino.Core.WebManagement.Model;

namespace Tarantino.Infrastructure.IntegrationTests.WebManagement.DataAccess.Mappings
{
	[TestFixture]
	public class ApplicationInstanceMappingTester : WebManagementDatabaseTester
	{
		[Test]
		public void Can_persist_application_instance()
		{
			var instance = new ApplicationInstance
			               	{
			               		AvailableForLoadBalancing = true,
			               		ApplicationDomain = "Domain...",
			               		CacheRefreshQueryString = "QueryString",
			               		DownForMaintenance = true,
			               		MachineName = "My Machine",
			               		MaintenanceHostHeader = "HostHeader",
			               		UniqueHostHeader = "Unique Host Header",
			               		Version = "Version"
			               	};

			AssertObjectCanBePersisted(instance);
		}
	}
}