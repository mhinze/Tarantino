using System.Collections.Generic;
using System.IO;

using Tarantino.Core.Commons.Services.Environment;
using System.Linq;

namespace Tarantino.Core.DatabaseManager.Services.Impl
{
	
	public class SqlFileLocator : ISqlFileLocator
	{
		private IFileSystem _fileSystem;

		public SqlFileLocator(IFileSystem fileSystem)
		{
			_fileSystem = fileSystem;
		}

		public string[] GetSqlFilenames(string scriptBaseFolder, string scriptFolder)
		{
			List<string> list = new List<string>();

			string folder = Path.Combine(scriptBaseFolder, scriptFolder);
			string[] sqlFiles = _fileSystem.GetAllFilesWithExtensionWithinFolder(folder, "sql");

			foreach (string sqlFilename in sqlFiles)
			{
				list.Add(sqlFilename);
			}
		    return list.OrderBy(x => x).ToArray();
		}
	}
}