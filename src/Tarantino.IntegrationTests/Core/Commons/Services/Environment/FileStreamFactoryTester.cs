using System.IO;
using Tarantino.Core.Commons.Services.Environment;
using Tarantino.Core.Commons.Services.Environment.Impl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Tarantino.IntegrationTests.Core.Commons.Services.Environment
{
	[TestFixture]
	public class FileStreamFactoryTester
	{
		[Test]
		public void Correctly_Constructs_Read_File_Stream()
		{
			writeSampleFile();

			IFileStreamFactory fileStreamFactory = new FileStreamFactory();

			using (Stream stream = fileStreamFactory.ConstructReadFileStream("test.txt"))
			{
				Assert.That(stream.Length, Is.EqualTo(10));
			}
		}

		[Test]
		public void Correctly_Constructs_Write_File_Stream()
		{
			deleteTestFile();

			IFileStreamFactory fileStreamFactory = new FileStreamFactory();

			using (Stream stream = fileStreamFactory.ConstructWriteFileStream("test.txt"))
			{
				FileStream fileStream = (FileStream) stream;
				fileStream.Write(new byte[] {7, 9, 11}, 0, 3);
			}

			using (Stream stream = fileStreamFactory.ConstructReadFileStream("test.txt"))
			{
				byte[] fileContents = new byte[3];
				Assert.That(stream.Read(fileContents, 0, 3), Is.EqualTo(3));
				Assert.That(fileContents, Is.EqualTo(new byte[] { 7, 9, 11 }));
			}
		}

		private void writeSampleFile()
		{
			deleteTestFile();

			using (StreamWriter streamWriter = new StreamWriter("test.txt"))
			{
				streamWriter.Write("testing...");
			}
		}

		private void deleteTestFile()
		{
			if (File.Exists("test.txt"))
				File.Delete("test.txt");
		}
	}
}