using System.Data;
using Tarantino.Core.Commons.Model.Enumerations;


namespace Tarantino.Core.Commons.Services.DataFileManagement.Impl
{
	
	public class DataTableReader : IDataTableReader
	{
		private DataTable _table;
		private int _currentRowIndex = -1;

		public void Open(DataTable table)
		{
			_table = table;
		}

		public bool Read()
		{
			var numberOfRows = _table.Rows.Count;
			_currentRowIndex++;
			var tableHasEnoughRows = numberOfRows > _currentRowIndex;

			var canRead = tableHasEnoughRows && currentRowHasValues();

			return canRead;
		}

		private bool currentRowHasValues()
		{
			var hasValues = false;
			foreach (DataColumn column in _table.Columns)
			{
				var currentColumnValue = _table.Rows[_currentRowIndex][column.ColumnName].ToString();
				
				if (!string.IsNullOrEmpty(currentColumnValue))
				{
					hasValues = true;
				}
			}
			return hasValues;
		}

		public int GetInteger(string columnName)
		{
			var value = int.Parse(GetString(columnName));
			return value;
		}

		public decimal GetDecimal(string columnName)
		{
			var value = decimal.Parse(GetString(columnName));
			return value;
		}

		public T GetEnumeration<T>(string columnName) where T : Enumeration, new()
		{
			var displayName = GetString(columnName);
			var value = Enumeration.FromDisplayName<T>(displayName);

			return value;
		}

		public bool GetBoolean(string columnName)
		{
			bool value = bool.Parse(GetString(columnName));
			return value;
		}

		public string GetString(string columnName)
		{
			var currentRowValue = _table.Rows[_currentRowIndex][columnName] as string;
			
			if (currentRowValue == string.Empty)
			{
				currentRowValue = null;
			}

			return currentRowValue;
		}
	}
}