using System;
using Tarantino.Core.Commons.Services.Environment;
using Tarantino.Core.Commons.Services.Environment.Impl;
using NUnit.Framework;

namespace Tarantino.IntegrationTests.Core.Commons.Services.Environment
{
	[TestFixture]
	public class SystemClockTester
	{
		[Test]
		public void CorrectlyReturnsCurrentDateTime()
		{
			ISystemClock clock = new SystemClock();

			DateTime time = DateTime.Now;
			DateTime currentDateTime = clock.GetCurrentDateTime();

			Assert.Greater(currentDateTime, time.AddSeconds(-1));
			Assert.Less(currentDateTime, time.AddSeconds(1));
		}
	}
}