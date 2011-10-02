using System.Linq;

namespace Tarantino.Core.DatabaseManager.Services.Impl
{
    public class FileFilterService : IFileFilterService
    {
        public string[] GetFilteredFilenames(string[] allFiles, string excludeFilenameContaining)
        {
            if (string.IsNullOrEmpty(excludeFilenameContaining))
                return allFiles;

            return allFiles.Where(x =>
                                      {
                                          var beginningOfFileName = x.LastIndexOfAny(new[]{'\\','/'});
                                          return !x.Substring(beginningOfFileName) .Contains(excludeFilenameContaining);
                                      }).ToArray();
        }
    }
}