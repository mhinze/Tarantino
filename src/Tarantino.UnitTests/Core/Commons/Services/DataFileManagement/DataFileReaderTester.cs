using System;
using Tarantino.Core.Commons.Model.Enumerations;
using Tarantino.Core.Commons.Services.DataFileManagement;
using Tarantino.Core.Commons.Services.DataFileManagement.Impl;
using Tarantino.Core.Commons.Services.Environment;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace Tarantino.UnitTests.Core.Commons.Services.DataFileManagement
{
	[TestFixture]
	public class DataFileReaderTester
	{
		private const string _testDataFile = "DataFilePath.Test.tab";

		[Test]
		public void CorrectlyReadsIntegerColumn()
		{
			string fileContents = "Integer Column\r\n80\r\n85\r\n";

			MockRepository mocks = new MockRepository();
			IResourceFileLocator fileLocator = mocks.CreateMock<IResourceFileLocator>();
			Expect.Call(fileLocator.ReadTextFile("MyCompany.MyAssembly", _testDataFile)).Return(fileContents);

			mocks.ReplayAll();

			using (IDataFileReader reader = new DataFileReader(fileLocator))
			{
				reader.Open("MyCompany.MyAssembly", "Test", "DataFilePath");

				bool canRead = reader.Read();
				Assert.That(canRead, Is.EqualTo(true));
				Assert.That(reader.GetInteger("Integer Column"), Is.EqualTo(80));

				canRead = reader.Read();
				Assert.That(canRead, Is.EqualTo(true));
				Assert.That(reader.GetInteger("Integer Column"), Is.EqualTo(85));

				canRead = reader.Read();
				Assert.That(canRead, Is.EqualTo(false));
			}

			mocks.VerifyAll();
		}

		[Test]
		public void CanReadEmptyString()
		{
			string fileContents = "StringColumn1\tStringColumn2\r\nString 1";

			MockRepository mocks = new MockRepository();
			IResourceFileLocator fileLocator = mocks.CreateMock<IResourceFileLocator>();
			Expect.Call(fileLocator.ReadTextFile("MyCompany.MyAssembly", _testDataFile)).Return(fileContents);

			mocks.ReplayAll();

			using (IDataFileReader reader = new DataFileReader(fileLocator))
			{
				reader.Open("MyCompany.MyAssembly", "Test", "DataFilePath");

				bool canRead = reader.Read();
				Assert.That(canRead, Is.EqualTo(true));
				Assert.That(reader.GetString("StringColumn2"), Is.EqualTo(string.Empty));

				canRead = reader.Read();
				Assert.That(canRead, Is.EqualTo(false));
			}

			mocks.VerifyAll();
		}

		[Test]
		public void CorrectlyReadsMultiColumnTable()
		{
			string fileContents = "Column 1\tColumn 2\r\n80\t85";

			MockRepository mocks = new MockRepository();
			IResourceFileLocator fileLocator = mocks.CreateMock<IResourceFileLocator>();
			Expect.Call(fileLocator.ReadTextFile("MyCompany.MyAssembly", _testDataFile)).Return(fileContents);

			mocks.ReplayAll();

			using (IDataFileReader reader = new DataFileReader(fileLocator))
			{
				reader.Open("MyCompany.MyAssembly", "Test", "DataFilePath");

				bool canRead = reader.Read();
				Assert.That(canRead, Is.EqualTo(true));
				Assert.That(reader.GetInteger("Column 1"), Is.EqualTo(80));
				Assert.That(reader.GetInteger("Column 2"), Is.EqualTo(85));

				canRead = reader.Read();
				Assert.That(canRead, Is.EqualTo(false));
			}

			mocks.VerifyAll();
		}

		[Test]
		public void CorrectlyReadsStringColumn()
		{
			string fileContents = "String Column\r\nKevin\r\nHurwitz\r\n";

			MockRepository mocks = new MockRepository();
			IResourceFileLocator fileLocator = mocks.CreateMock<IResourceFileLocator>();
			Expect.Call(fileLocator.ReadTextFile("MyCompany.MyAssembly", _testDataFile)).Return(fileContents);

			mocks.ReplayAll();

			using (IDataFileReader reader = new DataFileReader(fileLocator))
			{
				reader.Open("MyCompany.MyAssembly", "Test", "DataFilePath");

				bool canRead = reader.Read();
				Assert.That(canRead, Is.EqualTo(true));
				Assert.That(reader.GetString("String Column"), Is.EqualTo("Kevin"));

				canRead = reader.Read();
				Assert.That(canRead, Is.EqualTo(true));
				Assert.That(reader.GetString("String Column"), Is.EqualTo("Hurwitz"));

				canRead = reader.Read();
				Assert.That(canRead, Is.EqualTo(false));
			}

			mocks.VerifyAll();
		}

		[Test]
		public void CorrectlyReadsEnumeratedTextFileColumnByDisplayName()
		{
			string fileContents = "Persistence Mode\r\nLive\r\nArchive\r\n";

			MockRepository mocks = new MockRepository();
			IResourceFileLocator fileLocator = mocks.CreateMock<IResourceFileLocator>();
			Expect.Call(fileLocator.ReadTextFile("MyCompany.MyAssembly", _testDataFile)).Return(fileContents);

			mocks.ReplayAll();

			using (IDataFileReader reader = new DataFileReader(fileLocator))
			{
				reader.Open("MyCompany.MyAssembly", "Test", "DataFilePath");

				bool canRead = reader.Read();
				Assert.That(canRead, Is.EqualTo(true));
				Assert.That(reader.GetEnumerationByDisplayName<PersistenceMode>("Persistence Mode"), Is.EqualTo(PersistenceMode.Live));

				canRead = reader.Read();
				Assert.That(canRead, Is.EqualTo(true));
				Assert.That(reader.GetEnumerationByDisplayName<PersistenceMode>("Persistence Mode"), Is.EqualTo(PersistenceMode.Archive));

				canRead = reader.Read();
				Assert.That(canRead, Is.EqualTo(false));
			}

			mocks.VerifyAll();
		}

		[Test]
		public void CorrectlyReadsEnumeratedTextFileColumnByValue()
		{
			string fileContents = "Persistence Mode\r\n1\r\n2\r\n";

			MockRepository mocks = new MockRepository();
			IResourceFileLocator fileLocator = mocks.CreateMock<IResourceFileLocator>();
			Expect.Call(fileLocator.ReadTextFile("MyCompany.MyAssembly", _testDataFile)).Return(fileContents);

			mocks.ReplayAll();

			using (IDataFileReader reader = new DataFileReader(fileLocator))
			{
				reader.Open("MyCompany.MyAssembly", "Test", "DataFilePath");

				bool canRead = reader.Read();
				Assert.That(canRead, Is.EqualTo(true));
				Assert.That(reader.GetEnumerationByValue<PersistenceMode>("Persistence Mode"), Is.EqualTo(PersistenceMode.Live));

				canRead = reader.Read();
				Assert.That(canRead, Is.EqualTo(true));
				Assert.That(reader.GetEnumerationByValue<PersistenceMode>("Persistence Mode"), Is.EqualTo(PersistenceMode.Archive));

				canRead = reader.Read();
				Assert.That(canRead, Is.EqualTo(false));
			}

			mocks.VerifyAll();
		}

		[Test]
		public void CorrectlyReadsColumnHeaders()
		{
			string fileContents = "First Column\tSecond Column\r\n";

			MockRepository mocks = new MockRepository();
			IResourceFileLocator fileLocator = mocks.CreateMock<IResourceFileLocator>();
			Expect.Call(fileLocator.ReadTextFile("MyCompany.MyAssembly", _testDataFile)).Return(fileContents);

			mocks.ReplayAll();

			using (IDataFileReader reader = new DataFileReader(fileLocator))
			{
				reader.Open("MyCompany.MyAssembly", "Test", "DataFilePath");
				string[] columnHeaders = reader.GetColumnHeaders();

				Assert.That(columnHeaders, Is.EqualTo(new string[] { "First Column", "Second Column" }));
			}

			mocks.VerifyAll();
		}

		[Test, ExpectedException(ExceptionType = typeof(ApplicationException), ExpectedMessage = "The reader must be opened before retrieving column names")]
		public void ThrowsExceptionIfColumnsAreRetrievedFromClosedReader()
		{
			IDataFileReader reader = new DataFileReader(null);
			reader.GetColumnHeaders();
		}

		[Test, ExpectedException(ExceptionType = typeof(ApplicationException), ExpectedMessage = "Column not found: My Bad Column Name")]
		public void CorrectlyThrowsExceptionIfUnknownColumnSpecified()
		{
			string fileContents = "My Good Column Name\r\nMy Data\r\n";

			MockRepository mocks = new MockRepository();
			IResourceFileLocator fileLocator = mocks.CreateMock<IResourceFileLocator>();
			Expect.Call(fileLocator.ReadTextFile("MyCompany.MyAssembly", _testDataFile)).Return(fileContents);

			mocks.ReplayAll();

			using (IDataFileReader reader = new DataFileReader(fileLocator))
			{
				reader.Open("MyCompany.MyAssembly", "Test", "DataFilePath");

				reader.Read();
				reader.GetInteger("My Bad Column Name");
			}
		}
	}
}