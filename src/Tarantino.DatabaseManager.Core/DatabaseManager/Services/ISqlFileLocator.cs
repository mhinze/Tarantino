namespace Tarantino.Core.DatabaseManager.Services
{
    public interface ISqlFileLocator
    {
        string[] GetSqlFilenames(string scriptBaseFolder, string scriptFolder);
    }
}