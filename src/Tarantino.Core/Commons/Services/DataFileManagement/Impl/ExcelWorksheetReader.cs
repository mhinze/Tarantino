using System;
using System.Data;
using System.IO;
using Tarantino.Core.Commons.Services.Environment;


namespace Tarantino.Core.Commons.Services.DataFileManagement.Impl
{
	
	public class ExcelWorksheetReader : IExcelWorksheetReader
	{
		private readonly IFileSystem _fileSystem;
		private readonly IExcelWorkbookReader _excelWorkbookReader;

		public ExcelWorksheetReader(IFileSystem fileSystem, IExcelWorkbookReader excelWorkbookReader)
		{
			_fileSystem = fileSystem;
			_excelWorkbookReader = excelWorkbookReader;
		}

		public DataTable GetWorksheet(string filePath, string worksheetName)
		{
			DataTable table;

			using (Stream fileStream = _fileSystem.ReadIntoFileStream(filePath))
			{
				DataTableCollection workbookData = _excelWorkbookReader.GetWorkbookData(fileStream).Tables;

				if (workbookData.IndexOf(worksheetName) > -1)
				{
					table = workbookData[worksheetName];
					modifyColumnNames(table);
				}
				else
				{
					string message = string.Format("The workbook is missing worksheet named '{0}'", worksheetName);
					throw new ApplicationException(message);
				}
			}

			return table;
		}

		protected void modifyColumnNames(DataTable table)
		{
			if (table.Columns.Count == 0)
				throw new ApplicationException(string.Format("The worksheet named '{0}' has no columns", table.TableName));

			if (table.Rows.Count == 0)
				throw new ApplicationException(string.Format("The worksheet named '{0}' has no column header row", table.TableName));

			for (int i = 0; i < table.Columns.Count; i++)
			{
				table.Columns[i].ColumnName = table.Rows[0][i].ToString();
			}

			table.Rows.RemoveAt(0);

			removeAllRowsAfterFirstEmptyRow(table);

			if (table.Rows.Count == 0)
				throw new ApplicationException(string.Format("The worksheet named '{0}' has no rows", table.TableName));
		}

		private void removeAllRowsAfterFirstEmptyRow(DataTable table)
		{
			int firstEmptyRowIndex = int.MaxValue;

			int numberOfRows = table.Rows.Count;

			for (int rowIndex = 0; rowIndex < numberOfRows; rowIndex++)
			{
				DataRow row = table.Rows[rowIndex];

				object[] rowValues = row.ItemArray;
				bool rowHasAValue = false;

				for (int i = 0; i < rowValues.Length; i++)
				{
					if (!string.IsNullOrEmpty(rowValues[i] as string))
					{
						rowHasAValue = true;
						break;
					}
				}

				if (!rowHasAValue)
				{
					firstEmptyRowIndex = rowIndex;
					break;
				}
			}

			while (table.Rows.Count > firstEmptyRowIndex)
			{
				table.Rows.RemoveAt(firstEmptyRowIndex);
			}
		}
	}
}