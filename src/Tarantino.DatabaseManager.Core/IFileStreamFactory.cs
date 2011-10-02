using System.IO;


namespace Tarantino.DatabaseManager.Core
{
    public interface IFileStreamFactory
    {
        Stream ConstructReadFileStream(string path);
        Stream ConstructWriteFileStream(string path);
    }
}