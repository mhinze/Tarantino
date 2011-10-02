using NUnit.Framework;

using Tarantino.Core.DatabaseManager.Services.Impl;
using Tarantino.DatabaseManager.Core;

namespace Tarantino.IntegrationTests.Core.DatabaseManager
{
	[TestFixture]
	public class SqlFilesTester
	{
		[Test]
		public void All_sql_files_should_be_included_as_embedded_resources()
		{
            //Tarantino.DatabaseManager.Core.InfrastructureDependencyRegistrar.RegisterInfrastructure();
			string assembly = SqlDatabaseManager.SQL_FILE_ASSEMBLY;
			string template = SqlDatabaseManager.SQL_FILE_TEMPLATE;

			IResourceFileLocator locator = new ResourceFileLocator();

			Assert.That(locator.FileExists(assembly, string.Format(template, "CreateSchema")));
			Assert.That(locator.FileExists(assembly, string.Format(template, "DropConnections")));
			Assert.That(locator.FileExists(assembly, string.Format(template, "VersionDatabase")));
		}
	}
}