using System;
using System.IO;
using Tarantino.Core.Commons.Services.Environment;
using Tarantino.Core.Commons.Services.Environment.Impl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Tarantino.IntegrationTests.Core.Commons.Services.Environment
{
	[TestFixture]
	public class ResourceFileLocatorTester
	{
		private const string _resourceTemplate = "Tarantino.IntegrationTests.Core.Commons.Services.DataFileManagement.Files.{0}";
		private const string _testAssembly = "Tarantino.IntegrationTests";

		[Test]
		public void Correctly_reads_resource_file()
		{
			IResourceFileLocator locator = new ResourceFileLocator();
			string contents = locator.ReadTextFile(_testAssembly, string.Format(_resourceTemplate, "Sample.tab"));

			Assert.IsTrue(contents.Contains("Tarantino"));
		}

		[Test]
		public void Correctly_reports_when_resource_file_does_not_exist()
		{
			IResourceFileLocator locator = new ResourceFileLocator();
			bool exists = locator.FileExists(_testAssembly, string.Format(_resourceTemplate, "BadFile.tab"));

			Assert.That(exists, Is.False);
		}

		[Test, ExpectedException(ExceptionType = typeof(ApplicationException), ExpectedMessage = "Resource file not found: BadResource.txt. Make sure the Build Action for the file is 'Embedded Resource'.")]
		public void Correctly_throws_exception_when_resource_name_not_found()
		{
			IResourceFileLocator locator = new ResourceFileLocator();
			locator.ReadTextFile(_testAssembly, "BadResource.txt");
		}

		[Test]
		public void Correctly_reads_binary_resource_file()
		{
			IResourceFileLocator locator = new ResourceFileLocator();
			byte[] contents = locator.ReadBinaryFile(_testAssembly, string.Format(_resourceTemplate, "Sample.tab"));

			Assert.That(contents.Length, Is.GreaterThan(10));
		}

		[Test, ExpectedException(ExceptionType = typeof(ApplicationException), ExpectedMessage = "Resource file not found: BadResource.txt. Make sure the Build Action for the file is 'Embedded Resource'.")]
		public void Correctly_throws_exception_when_binary_resource_name_not_found()
		{
			IResourceFileLocator locator = new ResourceFileLocator();
			locator.ReadBinaryFile(_testAssembly, "BadResource.txt");
		}

		[Test]
		public void Correctly_reads_binary_resource_file_into_stream()
		{
			IResourceFileLocator locator = new ResourceFileLocator();
			Stream stream = locator.ReadFileAsStream(_testAssembly, string.Format(_resourceTemplate, "Sample.tab"));

			Assert.That(stream.Length, Is.GreaterThan(10));
		}
	}
}