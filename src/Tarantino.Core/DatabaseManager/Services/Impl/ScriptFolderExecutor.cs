
using Tarantino.Core.DatabaseManager.Model;

namespace Tarantino.Core.DatabaseManager.Services.Impl
{
	
	public class ScriptFolderExecutor : IScriptFolderExecutor
	{
		private readonly ISchemaInitializer _schemaInitializer;
		private readonly ISqlFileLocator _fileLocator;
		private readonly IChangeScriptExecutor _scriptExecutor;
		private readonly IDatabaseVersioner _versioner;
	    private readonly IFileFilterService _fileFilterService;
		public ScriptFolderExecutor(ISchemaInitializer schemaInitializer, ISqlFileLocator fileLocator, IChangeScriptExecutor scriptExecutor, IDatabaseVersioner versioner, IFileFilterService fileFilterService)
		{
			_schemaInitializer = schemaInitializer;
		    _fileFilterService = fileFilterService;
		    _fileLocator = fileLocator;
			_scriptExecutor = scriptExecutor;
			_versioner = versioner;
		}

		public void ExecuteScriptsInFolder(TaskAttributes taskAttributes, string scriptDirectory, ITaskObserver taskObserver)
		{
            _schemaInitializer.EnsureSchemaCreated(taskAttributes.ConnectionSettings);
            
            var sqlFilenames = _fileLocator.GetSqlFilenames(taskAttributes.ScriptDirectory, scriptDirectory);
            
            var filteredFilenames = _fileFilterService.GetFilteredFilenames(sqlFilenames, taskAttributes.SkipFileNameContaining);
            
			foreach (string sqlFilename in filteredFilenames)
			{
                _scriptExecutor.Execute(sqlFilename, taskAttributes.ConnectionSettings, taskObserver);
			}

            _versioner.VersionDatabase(taskAttributes.ConnectionSettings, taskObserver);
		}
	}
}