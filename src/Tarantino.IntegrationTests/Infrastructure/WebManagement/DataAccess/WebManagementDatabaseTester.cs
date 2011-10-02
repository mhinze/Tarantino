using System.Collections.Generic;
using Tarantino.IntegrationTests;

namespace Tarantino.Infrastructure.IntegrationTests.WebManagement.DataAccess
{
	public abstract class WebManagementDatabaseTester : DatabaseTesterBase
	{
		protected override IEnumerable<string> GetTablesToDelete()
		{
			return new[]{"ApplicationInstance"};
		}

		public override string ConfigurationFile
		{
			get { return "webmanagement.hibernate.cfg.xml"; }
		}
	}
}