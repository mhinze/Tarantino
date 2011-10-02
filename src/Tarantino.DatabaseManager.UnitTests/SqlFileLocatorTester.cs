using NUnit.Framework;
using Rhino.Mocks;
using Tarantino.Core.DatabaseManager.Services;
using Tarantino.Core.DatabaseManager.Services.Impl;
using Tarantino.DatabaseManager.Core;

namespace Tarantino.UnitTests.Core.DatabaseManager.Services
{
	[TestFixture]
	public class SqlFileLocatorTester
	{
		[Test]
		public void Correctly_locates_sql_scripts_and_return_in_asc_order()
		{
			string scriptFolder = @"c:\scripts";

			string[] updateSqlFiles = new string[] { "02_Update.sql", "01_Update.sql" };

			MockRepository mocks = new MockRepository();
			IFileSystem fileSystem = mocks.CreateMock<IFileSystem>();

			using (mocks.Record())
			{
				Expect.Call(fileSystem.GetAllFilesWithExtensionWithinFolder(@"c:\scripts\Update", "sql")).Return(updateSqlFiles);
			}

			using (mocks.Playback())
			{
				ISqlFileLocator fileLocator = new SqlFileLocator(fileSystem);
				string[] sqlFilenames = fileLocator.GetSqlFilenames(scriptFolder, "Update");

				Assert.AreEqual(2, sqlFilenames.Length);
				Assert.AreEqual("01_Update.sql", sqlFilenames[0]);
				Assert.AreEqual("02_Update.sql", sqlFilenames[1]);
			}

			mocks.VerifyAll();
		}
	}
}