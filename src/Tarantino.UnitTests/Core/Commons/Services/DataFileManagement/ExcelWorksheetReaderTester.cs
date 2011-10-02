using System;
using System.Data;
using System.IO;
using Tarantino.Core.Commons.Services.DataFileManagement;
using Tarantino.Core.Commons.Services.DataFileManagement.Impl;
using Tarantino.Core.Commons.Services.Environment;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace Tarantino.UnitTests.Core.Commons.Services.DataFileManagement
{
	[TestFixture]
	public class ExcelWorksheetReaderTester
	{
		[Test]
		public void Can_read_worksheet_from_excel_workbook()
		{
			string excelFile = "MyWorkbook.xls";
			MemoryStream excelFileStream = new MemoryStream();

			DataSet workbook = createWorkbook();

			MockRepository mocks = new MockRepository();
			IFileSystem fileSystem = mocks.CreateMock<IFileSystem>();
			IExcelWorkbookReader workbookReader = mocks.CreateMock<IExcelWorkbookReader>();

			Expect.Call(fileSystem.ReadIntoFileStream(excelFile)).Return(excelFileStream);
			Expect.Call(workbookReader.GetWorkbookData(excelFileStream)).Return(workbook);

			mocks.ReplayAll();

			IExcelWorksheetReader worksheetReader = new ExcelWorksheetReader(fileSystem, workbookReader);
			DataTable myWorksheet = worksheetReader.GetWorksheet(excelFile, "MyWorksheet");

			Assert.That(myWorksheet, Is.Not.Null);
			Assert.That(myWorksheet.Columns.Count, Is.EqualTo(2));
			Assert.That(myWorksheet.Columns[0].ColumnName, Is.EqualTo("First Column"));
			Assert.That(myWorksheet.Columns[1].ColumnName, Is.EqualTo("Second Column"));

			Assert.That(myWorksheet.Rows.Count, Is.EqualTo(2));
			Assert.That(myWorksheet.Rows[0]["First Column"], Is.EqualTo("Row 1 - First Column Value"));
			Assert.That(myWorksheet.Rows[0]["Second Column"], Is.EqualTo("Row 1 - Second Column Value"));
			Assert.That(myWorksheet.Rows[1]["First Column"], Is.EqualTo("Row 2 - First Column Value"));
			Assert.That(myWorksheet.Rows[1]["Second Column"], Is.EqualTo("Row 2 - Second Column Value"));

			mocks.VerifyAll();
		}

		private DataSet createWorkbook()
		{
			DataSet workbook = new DataSet();
			workbook.Tables.Add(new DataTable("MyWorksheet"));
			DataTable myWorksheet = workbook.Tables["MyWorksheet"];

			myWorksheet.Columns.Add();
			myWorksheet.Columns.Add();

			DataRow columnHeaders = myWorksheet.NewRow();
			columnHeaders[0] = "First Column";
			columnHeaders[1] = "Second Column";
			myWorksheet.Rows.Add(columnHeaders);

			DataRow firstRow = myWorksheet.NewRow();
			firstRow[0] = "Row 1 - First Column Value";
			firstRow[1] = "Row 1 - Second Column Value";
			myWorksheet.Rows.Add(firstRow);

			DataRow secondRow = myWorksheet.NewRow();
			secondRow[0] = "Row 2 - First Column Value";
			secondRow[1] = "Row 2 - Second Column Value";
			myWorksheet.Rows.Add(secondRow);
			return workbook;
		}

		[Test]
		public void Does_not_continue_reading_after_a_blank_row_is_found()
		{
			string excelFile = "MyWorkbook.xls";
			MemoryStream excelFileStream = new MemoryStream();

			DataSet workbook = new DataSet();
			workbook.Tables.Add(new DataTable("MyWorksheet"));
			workbook.Tables["MyWorksheet"].Columns.Add();

			DataRow columnHeaders = workbook.Tables["MyWorksheet"].NewRow();
			columnHeaders[0] = "First Column";
			workbook.Tables["MyWorksheet"].Rows.Add(columnHeaders);

			DataRow firstRow = workbook.Tables["MyWorksheet"].NewRow();
			firstRow[0] = "Row 1 - First Column Value";
			workbook.Tables["MyWorksheet"].Rows.Add(firstRow);

			DataRow secondRow = workbook.Tables["MyWorksheet"].NewRow();
			secondRow[0] = "Row 2 - First Column Value";
			workbook.Tables["MyWorksheet"].Rows.Add(secondRow);

			DataRow thirdRow = workbook.Tables["MyWorksheet"].NewRow();
			workbook.Tables["MyWorksheet"].Rows.Add(thirdRow);

			DataRow fourthRow = workbook.Tables["MyWorksheet"].NewRow();
			fourthRow[0] = "Row 4 - First Column Value";
			workbook.Tables["MyWorksheet"].Rows.Add(fourthRow);

			MockRepository mocks = new MockRepository();
			IFileSystem fileSystem = mocks.CreateMock<IFileSystem>();
			IExcelWorkbookReader workbookReader = mocks.CreateMock<IExcelWorkbookReader>();

			Expect.Call(fileSystem.ReadIntoFileStream(excelFile)).Return(excelFileStream);
			Expect.Call(workbookReader.GetWorkbookData(excelFileStream)).Return(workbook);

			mocks.ReplayAll();

			IExcelWorksheetReader worksheetReader = new ExcelWorksheetReader(fileSystem, workbookReader);
			DataTable myWorksheet = worksheetReader.GetWorksheet(excelFile, "MyWorksheet");
			
			Assert.That(myWorksheet, Is.Not.Null);
			Assert.That(myWorksheet.Columns.Count, Is.EqualTo(1));
			Assert.That(myWorksheet.Columns[0].ColumnName, Is.EqualTo("First Column"));

			Assert.That(myWorksheet.Rows.Count, Is.EqualTo(2));
			Assert.That(myWorksheet.Rows[0]["First Column"], Is.EqualTo("Row 1 - First Column Value"));
			Assert.That(myWorksheet.Rows[1]["First Column"], Is.EqualTo("Row 2 - First Column Value"));

			mocks.VerifyAll();
		}

		[Test, ExpectedException(ExceptionType = typeof(ApplicationException), ExpectedMessage = "The workbook is missing worksheet named 'MyOtherWorksheet'")]
		public void Handles_scenario_where_invalid_worksheet_requested()
		{
			string excelFile = "MyWorkbook.xls";
			MemoryStream excelFileStream = new MemoryStream();

			DataSet workbook = createWorkbook();

			MockRepository mocks = new MockRepository();
			IFileSystem fileSystem = mocks.CreateMock<IFileSystem>();
			IExcelWorkbookReader workbookReader = mocks.CreateMock<IExcelWorkbookReader>();

			Expect.Call(fileSystem.ReadIntoFileStream(excelFile)).Return(excelFileStream);
			Expect.Call(workbookReader.GetWorkbookData(excelFileStream)).Return(workbook);

			mocks.ReplayAll();

			IExcelWorksheetReader worksheetReader = new ExcelWorksheetReader(fileSystem, workbookReader);
			worksheetReader.GetWorksheet(excelFile, "MyOtherWorksheet");

			mocks.VerifyAll();
		}

		[Test, ExpectedException(ExceptionType = typeof(ApplicationException), ExpectedMessage = "The worksheet named 'MyWorksheet' has no columns")]
		public void Handles_scenario_where_worksheet_has_no_columns()
		{
			string excelFile = "MyWorkbook.xls";
			MemoryStream excelFileStream = new MemoryStream();

			DataSet workbook = new DataSet();
			workbook.Tables.Add("MyWorksheet");

			MockRepository mocks = new MockRepository();
			IFileSystem fileSystem = mocks.CreateMock<IFileSystem>();
			IExcelWorkbookReader workbookReader = mocks.CreateMock<IExcelWorkbookReader>();

			Expect.Call(fileSystem.ReadIntoFileStream(excelFile)).Return(excelFileStream);
			Expect.Call(workbookReader.GetWorkbookData(excelFileStream)).Return(workbook);

			mocks.ReplayAll();

			IExcelWorksheetReader worksheetReader = new ExcelWorksheetReader(fileSystem, workbookReader);
			worksheetReader.GetWorksheet(excelFile, "MyWorksheet");

			mocks.VerifyAll();
		}

		[Test, ExpectedException(ExceptionType = typeof(ApplicationException), ExpectedMessage = "The worksheet named 'MyWorksheet' has no column header row")]
		public void Handles_scenario_where_worksheet_has_no_column_header_row()
		{
			string excelFile = "MyWorkbook.xls";
			MemoryStream excelFileStream = new MemoryStream();

			DataSet workbook = new DataSet();
			workbook.Tables.Add("MyWorksheet");
			workbook.Tables["MyWorksheet"].Columns.Add("Column 1");

			MockRepository mocks = new MockRepository();
			IFileSystem fileSystem = mocks.CreateMock<IFileSystem>();
			IExcelWorkbookReader workbookReader = mocks.CreateMock<IExcelWorkbookReader>();

			Expect.Call(fileSystem.ReadIntoFileStream(excelFile)).Return(excelFileStream);
			Expect.Call(workbookReader.GetWorkbookData(excelFileStream)).Return(workbook);

			mocks.ReplayAll();

			IExcelWorksheetReader worksheetReader = new ExcelWorksheetReader(fileSystem, workbookReader);
			worksheetReader.GetWorksheet(excelFile, "MyWorksheet");

			mocks.VerifyAll();
		}

		[Test, ExpectedException(ExceptionType = typeof(ApplicationException), ExpectedMessage = "The worksheet named 'MyWorksheet' has no rows")]
		public void Handles_scenario_where_worksheet_has_no_rows()
		{
			string excelFile = "MyWorkbook.xls";
			MemoryStream excelFileStream = new MemoryStream();

			DataSet workbook = new DataSet();
			workbook.Tables.Add("MyWorksheet");
			workbook.Tables["MyWorksheet"].Columns.Add();
			DataRow columnHeaderRow = workbook.Tables["MyWorksheet"].NewRow();
			columnHeaderRow[0] = "First Column";
			workbook.Tables["MyWorksheet"].Rows.Add(columnHeaderRow);

			MockRepository mocks = new MockRepository();
			IFileSystem fileSystem = mocks.CreateMock<IFileSystem>();
			IExcelWorkbookReader workbookReader = mocks.CreateMock<IExcelWorkbookReader>();

			Expect.Call(fileSystem.ReadIntoFileStream(excelFile)).Return(excelFileStream);
			Expect.Call(workbookReader.GetWorkbookData(excelFileStream)).Return(workbook);

			mocks.ReplayAll();

			IExcelWorksheetReader worksheetReader = new ExcelWorksheetReader(fileSystem, workbookReader);
			worksheetReader.GetWorksheet(excelFile, "MyWorksheet");

			mocks.VerifyAll();
		}
	}
}