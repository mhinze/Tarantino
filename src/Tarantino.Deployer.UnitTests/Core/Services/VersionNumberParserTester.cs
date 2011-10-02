using System;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Tarantino.Deployer.Core.Services;
using Tarantino.Deployer.Core.Services.Impl;

namespace Tarantino.Deployer.UnitTests.Core.Services
{
	[TestFixture]
	public class VersionNumberParserTester
	{
		[Test]
		public void Correctly_parses_version_number_using_space_as_delimiter()
		{
			IVersionNumberParser versionNumberParser = new VersionNumberParser();
			int versionNumber = versionNumberParser.Parse("some text Working Version Number: 1134 more text");

			Assert.That(versionNumber, Is.EqualTo(versionNumber));
		}

		[Test]
		public void Correctly_parses_version_number_using_hard_return_as_delimiter()
		{
			IVersionNumberParser versionNumberParser = new VersionNumberParser();
			int versionNumber = versionNumberParser.Parse("some text Working Version Number: 1134\n more text");

			Assert.That(versionNumber, Is.EqualTo(versionNumber));
		}

		[Test, ExpectedException(typeof(ApplicationException), ExpectedMessage = "The term 'Working Version Number:' was not found in the build output.  Could not determine the version number or record deployment occurence!")]
		public void Handles_scenario_where_version_number_not_included_in_build_output()
		{
			IVersionNumberParser versionNumberParser = new VersionNumberParser();
			int versionNumber = versionNumberParser.Parse("some text");

			Assert.That(versionNumber, Is.EqualTo(versionNumber));
		}
	}
}