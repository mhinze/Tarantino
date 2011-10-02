using System.Data;
using Tarantino.Core.Commons.Model.Enumerations;


namespace Tarantino.Core.Commons.Services.DataFileManagement
{
	
	public interface IDataTableReader
	{
		void Open(DataTable table);

		bool Read();

		string GetString(string columnName);
		int GetInteger(string columnName);
		decimal GetDecimal(string columnName);
		T GetEnumeration<T>(string columnName) where T : Enumeration, new();
		bool GetBoolean(string columnName);
	}
}