using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Tarantino.Core.Commons.Services.Security;
using Tarantino.Core.Commons.Services.Security.Impl;

namespace Tarantino.UnitTests.Core.Commons.Services.Security
{
	[TestFixture]
	public class SHA512HashAlgorithmTester
	{
		[Test]
		public void Should_compute_hash()
		{
			IHashAlgorithm algorithm = new SHA512HashAlgorithm();

			var hash = algorithm.ComputeHash("khurwitz");

			Assert.That(hash, Is.EqualTo("n04q+oz8Qqjmc56ohKhbOFVEFNgmROnFIkzya2r+xBiXQKLBMBZAKCT9UDK+3s3x/JbN2HJ5gTSEEVP+ip7NdQ=="));
		}
	}
}