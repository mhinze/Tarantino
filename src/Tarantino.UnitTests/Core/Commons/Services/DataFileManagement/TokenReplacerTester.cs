using System;
using Tarantino.Core.Commons.Services.DataFileManagement;
using Tarantino.Core.Commons.Services.DataFileManagement.Impl;
using NUnit.Framework;

namespace Tarantino.UnitTests.Core.Commons.Services.DataFileManagement
{
	[TestFixture]
	public class TokenReplacerTester
	{
		[Test]
		public void CorrectlyReplacesToken()
		{
			const string script = "X ||Token|| X";

			ITokenReplacer replacer = new TokenReplacer {Text = script};

			replacer.Replace("Token", "TokenValue");
			Assert.AreEqual("X TokenValue X", replacer.Text);
		}

		[Test, ExpectedException(typeof(ApplicationException), ExpectedMessage = "The Text property must be set before tokens may be replaced")]
		public void ThrowsExceptionIfTextNotSetFirst()
		{
			ITokenReplacer replacer = new TokenReplacer();
			replacer.Replace("Token", "TokenValue");
		}
	}
}