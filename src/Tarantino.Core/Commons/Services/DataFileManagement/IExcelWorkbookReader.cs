using System.Data;
using System.IO;


namespace Tarantino.Core.Commons.Services.DataFileManagement
{
	
	public interface IExcelWorkbookReader
	{
		DataSet GetWorkbookData(Stream file);
	}
}