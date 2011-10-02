using System.IO;


namespace Tarantino.Core.Commons.Services.Environment
{
	
	public interface IFileStreamFactory
	{
		Stream ConstructReadFileStream(string path);
		Stream ConstructWriteFileStream(string path);
	}
}