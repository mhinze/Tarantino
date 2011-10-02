namespace Tarantino.Core.DatabaseManager.Services
{
    public interface IFileFilterService
    {
        string[] GetFilteredFilenames(string[] allFiles, string excludeFilenameContaining);
    }
}