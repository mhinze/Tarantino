namespace Tarantino.Core.DatabaseManager.Services
{
	public interface IExcelImporter
	{
		void Import(string excelFile, string server, string database, bool integrated, string username, string password, ITaskObserver taskObserver);
	}
}