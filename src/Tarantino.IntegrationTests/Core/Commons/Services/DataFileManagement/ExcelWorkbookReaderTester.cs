using System.Data;
using System.IO;
using Tarantino.Core.Commons.Services.DataFileManagement;
using Tarantino.Core.Commons.Services.DataFileManagement.Impl;
using Tarantino.Core.Commons.Services.Environment;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using StructureMap;
using Tarantino.Infrastructure;

namespace Tarantino.IntegrationTests.Core.Commons.Services.DataFileManagement
{
	[TestFixture]
	public class ExcelWorkbookReaderTester
	{
		private const string _resourceTemplate = "Tarantino.IntegrationTests.Core.Commons.Services.DataFileManagement.Files.{0}";
		private const string _testAssembly = "Tarantino.IntegrationTests";

		[Test]
		public void Correctly_Reads_Excel_File()
		{
            InfrastructureDependencyRegistrar.RegisterInfrastructure();
   
			IResourceFileLocator fileLocator = ObjectFactory.GetInstance<IResourceFileLocator>();

			byte[] excelFileBytes = fileLocator.ReadBinaryFile(_testAssembly, string.Format(_resourceTemplate, "Sample.xls"));

			IExcelWorkbookReader reader = new ExcelWorkbookReader();

			DataSet workbook = reader.GetWorkbookData(new MemoryStream(excelFileBytes));

			Assert.That(workbook, Is.Not.Null);

			Assert.That(workbook.Tables.Count, Is.EqualTo(1));
			Assert.That(workbook.Tables["Sample Data"], Is.Not.Null);
			Assert.That(workbook.Tables["Sample Data"].Columns.Count, Is.EqualTo(3));
			Assert.That(workbook.Tables["Sample Data"].Rows.Count, Is.EqualTo(6));
		}
	}
}