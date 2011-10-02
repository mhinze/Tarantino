using Tarantino.Core.DatabaseManager.Services.Impl;

namespace Tarantino.Core.DatabaseManager.Model
{
    public class TaskAttributes
    {
        public TaskAttributes(ConnectionSettings connectionSettings, string scriptDirectory)
        {
            ConnectionSettings = connectionSettings;
            ScriptDirectory = scriptDirectory;
        }

        public ConnectionSettings ConnectionSettings { get; set; }
        public string SkipFileNameContaining { get; set; }
        public string ScriptDirectory { get; set; }
        public RequestedDatabaseAction RequestedDatabaseAction { get; set; }
    }
}