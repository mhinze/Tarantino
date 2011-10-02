using Tarantino.Core.DatabaseManager.Model;
using Tarantino.DatabaseManager.Core;

namespace Tarantino.Core.DatabaseManager.Services.Impl
{
	public class ExcelImporter : IExcelImporter
	{
		private readonly IResourceFileLocator _fileLocator;
		private readonly IQueryExecutor _queryExecutor;
		private readonly ITokenReplacer _tokenReplacer;

		public ExcelImporter(IResourceFileLocator fileLocator, IQueryExecutor queryExecutor, ITokenReplacer tokenReplacer)
		{
			_fileLocator = fileLocator;
			_queryExecutor = queryExecutor;
			_tokenReplacer = tokenReplacer;
		}

		public void Import(string excelFile, string server, string database, bool integrated, string username, string password,
		                   ITaskObserver taskObserver)
		{
			string message = string.Format("\nImporting Excel File '{0}' to Database '{1}' on Server '{2}'\n", excelFile, database, server);
			taskObserver.Log(message);
			var settings = new ConnectionSettings(server, database, integrated, username, password);

			string sql = _fileLocator.ReadTextFile("Tarantino.Core", "Tarantino.Core.DatabaseManager.SqlFiles.ImportExcel.sql");
			_tokenReplacer.Text = sql;
			_tokenReplacer.Replace("DATABASE", database);
			_tokenReplacer.Replace("EXCEL_FILE", excelFile);
			_queryExecutor.ExecuteNonQuery(settings, _tokenReplacer.Text, false);
		}
	}
}