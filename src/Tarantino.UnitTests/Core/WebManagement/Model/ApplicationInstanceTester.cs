using NUnit.Framework;
using Tarantino.Core.Commons.Model;
using Tarantino.Core.WebManagement.Model;
using Tarantino.UnitTests.Core.Commons.Model;

namespace Tarantino.UnitTests.Core.WebManagement.Model
{
	[TestFixture]
	public class ApplicationInstanceTester : PersistentObjectTester
	{
		[Test]
		public void Property_accessors_work()
		{
			ApplicationInstance instance = new ApplicationInstance();

			Assert.AreEqual(null, instance.MachineName);
			instance.MachineName = "MachineName";
			Assert.AreEqual("MachineName", instance.MachineName);

			Assert.AreEqual(null, instance.ApplicationDomain);
			instance.ApplicationDomain = "ApplicationDomain";
			Assert.AreEqual("ApplicationDomain", instance.ApplicationDomain);

			Assert.AreEqual(null, instance.UniqueHostHeader);
			instance.UniqueHostHeader = "UniqueHostHeader";
			Assert.AreEqual("UniqueHostHeader", instance.UniqueHostHeader);

			Assert.AreEqual(null, instance.CacheRefreshQueryString);
			instance.CacheRefreshQueryString = "CacheRefreshQueryString";
			Assert.AreEqual("CacheRefreshQueryString", instance.CacheRefreshQueryString);

			Assert.AreEqual(null, instance.MaintenanceHostHeader);
			instance.MaintenanceHostHeader = "MaintenanceHostHeader";
			Assert.AreEqual("MaintenanceHostHeader", instance.MaintenanceHostHeader);

			Assert.AreEqual(false, instance.DownForMaintenance);
			instance.DownForMaintenance = true;
			Assert.AreEqual(true, instance.DownForMaintenance);

			Assert.AreEqual(false, instance.AvailableForLoadBalancing);
			instance.AvailableForLoadBalancing = true;
			Assert.AreEqual(true, instance.AvailableForLoadBalancing);

			Assert.AreEqual(null, instance.Version);
			instance.Version = "Version";
			Assert.AreEqual("Version", instance.Version);
		}

		protected override PersistentObject CreatePersisentObject()
		{
			return new ApplicationInstance();
		}
	}
}