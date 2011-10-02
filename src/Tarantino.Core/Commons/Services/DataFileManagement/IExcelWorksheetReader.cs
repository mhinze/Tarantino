using System.Data;


namespace Tarantino.Core.Commons.Services.DataFileManagement
{
	
	public interface IExcelWorksheetReader
	{
		DataTable GetWorksheet(string filePath, string worksheetName);
	}
}