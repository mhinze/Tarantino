using System;
using System.IO;
using StructureMap;
using Tarantino.Core.Commons.Services.Environment;
using Tarantino.Core.Commons.Services.Environment.Impl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace Tarantino.IntegrationTests.Core.Commons.Services.Environment
{
	[TestFixture]
	public class FileSystemTester : IntegrationTester
	{
		[Test]
		public void Correctly_Reports_File_Exists()
		{
			writeSampleFile();

			IFileSystem fileSystem = new FileSystem(null);

			Assert.AreEqual(true, fileSystem.FileExists("test.txt"));
			Assert.AreEqual(false, fileSystem.FileExists("nonExistentFile.txt"));
		}

		[Test, ExpectedException(ExceptionType = typeof(IOException), ExpectedMessage = "Not a locked file")]
		public void Handles_Reading_IO_Exceptions()
		{
			var mocks = new MockRepository();
			var streamFactory = mocks.CreateMock<IFileStreamFactory>();
			
			using (mocks.Record())
			{
				Expect.Call(streamFactory.ConstructReadFileStream(@"MyPath\test.txt")).Throw(new IOException("Not a locked file"));
			}

			using (mocks.Playback())
			{
				IFileSystem fileSystem = new FileSystem(streamFactory);
				fileSystem.ReadIntoFileStream(@"MyPath\test.txt");
			}
		}

		[Test, ExpectedException(ExceptionType = typeof(ApplicationException), ExpectedMessage = "The file you chose cannot be read because it is open in another application.  Please close the file in the other application and try again.")]
		public void Handles_Reading_Locked_Files()
		{
			var mocks = new MockRepository();
			var streamFactory = mocks.CreateMock<IFileStreamFactory>();
			
			using (mocks.Record())
			{
				Expect.Call(streamFactory.ConstructReadFileStream(@"MyPath\test.txt")).Throw(new IOException("it is being used by another process"));
			}

			using (mocks.Playback())
			{
				IFileSystem fileSystem = new FileSystem(streamFactory);
				fileSystem.ReadIntoFileStream(@"MyPath\test.txt");
			}
		}

		[Test]
		public void Correctly_Reads_File_Into_Stream()
		{
			var myFileStream = new MemoryStream();

			var mocks = new MockRepository();
			var streamFactory = mocks.CreateMock<IFileStreamFactory>();
			
			using (mocks.Record())
			{
				Expect.Call(streamFactory.ConstructReadFileStream(@"MyPath\test.txt")).Return(myFileStream);
			}

			using (mocks.Playback())
			{
				IFileSystem fileSystem = new FileSystem(streamFactory);

				using (var stream = fileSystem.ReadIntoFileStream(@"MyPath\test.txt"))
				{
					Assert.That(stream, Is.SameAs(myFileStream));
				}
			}
		}

		[Test]
		public void Correctly_reads_file_into_stream_reader()
		{
			writeSampleFile();
			var fileSystem = ObjectFactory.GetInstance<IFileSystem>();

			using (var reader = fileSystem.ReadFileIntoStreamReader("test.txt"))
			{
				Assert.That(reader, Is.Not.Null);
				Assert.That(reader.ReadToEnd(), Is.EqualTo("testing..."));
			}
		}

		[Test]
		public void Correctly_Reads_Text_File()
		{
			writeSampleFile();
			var fileSystem = ObjectFactory.GetInstance<IFileSystem>();
			var fileContents = fileSystem.ReadTextFile("test.txt");

			Assert.That(fileContents, Is.EqualTo("testing..."));
		}

		[Test]
		public void Correctly_finds_all_files_with_extension()
		{
			writeSampleFile();
			var fileSystem = ObjectFactory.GetInstance<IFileSystem>();
			var files = fileSystem.GetAllFilesWithExtensionWithinFolder(".", "txt");
			
			Assert.That(files.Length, Is.EqualTo(1));
			Assert.That(files[0].Contains(@"\test.txt"));
		}

		[Test]
		public void Correctly_Writes_File()
		{
			deleteTestFile();
			var myFileStream = new FileStream("test.txt", FileMode.Create, FileAccess.Write);
			var fileContents = new byte[] { 1, 3, 5 };

			var mocks = new MockRepository();
			var streamFactory = mocks.CreateMock<IFileStreamFactory>();
			
			using (mocks.Record())
			{
				Expect.Call(streamFactory.ConstructWriteFileStream(@"myPath\test.txt")).Return(myFileStream);
			}

			using (mocks.Playback())
			{
				IFileSystem fileSystem = new FileSystem(streamFactory);
				fileSystem.SaveFile(@"myPath\test.txt", fileContents);

				using (var inputStream = new FileStream("test.txt", FileMode.Open, FileAccess.Read))
				{
					Assert.That(inputStream.Length, Is.EqualTo(3));
				}
			}
		}

		private void writeSampleFile()
		{
			deleteTestFile();

			using (var streamWriter = new StreamWriter("test.txt"))
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