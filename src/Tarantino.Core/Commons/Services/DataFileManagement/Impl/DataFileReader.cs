using System;
using System.IO;
using Tarantino.Core.Commons.Model.Enumerations;
using Tarantino.Core.Commons.Services.Environment;

namespace Tarantino.Core.Commons.Services.DataFileManagement.Impl
{
	public class DataFileReader : IDataFileReader
	{
		private readonly IResourceFileLocator _resourceFileLocator;
		private StringReader _dataReader;
		private string[] _columnNames;
		private string[] _currentRowValues;

		public DataFileReader(IResourceFileLocator resourceFileLocator)
		{
			_resourceFileLocator = resourceFileLocator;
		}

		public void Open(string assembly, string resourceFilename, string filePath)
		{
			var resourceTemplate = "{0}.{1}.tab";
			var fullResourceFilename = string.Format(resourceTemplate, filePath, resourceFilename);
			var data = _resourceFileLocator.ReadTextFile(assembly, fullResourceFilename);

			_dataReader = new StringReader(data);

			_columnNames = readRow();
		}

		public bool Read()
		{
			_currentRowValues = readRow();

			return _currentRowValues.Length > 0;
		}

		private string[] readRow()
		{
			string currentDataRow = _dataReader.ReadLine();
			bool canRead = currentDataRow != null;

			string[] rowValues = new string[0];

			if (canRead)
			{
				rowValues = currentDataRow.Split('\t');
			}
			return rowValues;
		}

		public int GetInteger(string columnName)
		{
			int value = int.Parse(GetString(columnName));
			return value;
		}

		public T GetEnumerationByDisplayName<T>(string columnName) where T : Enumeration, new()
		{
			string displayName = GetString(columnName);
			T value = Enumeration.FromDisplayName<T>(displayName);
			return value;
		}

		public T GetEnumerationByValue<T>(string columnName) where T : Enumeration, new()
		{
			string rawValue = GetString(columnName);
			T value = Enumeration.FromValue<T>(int.Parse(rawValue));
			return value;
		}

		public string GetString(string columnName)
		{
			int columnIndex = Array.IndexOf(_columnNames, columnName);

			if (columnIndex == -1)
			{
				throw new ApplicationException(string.Format("Column not found: {0}", columnName));
			}

			string currentRowValue = (columnIndex >= _currentRowValues.Length) ? string.Empty : _currentRowValues[columnIndex];
			return currentRowValue;
		}

		public string[] GetColumnHeaders()
		{
			if (_columnNames == null)
			{
				throw new ApplicationException("The reader must be opened before retrieving column names");
			}

			return _columnNames;
		}

		public void Dispose()
		{
			if (_dataReader != null)
			{
				_dataReader.Close();
			}
		}
	}
}