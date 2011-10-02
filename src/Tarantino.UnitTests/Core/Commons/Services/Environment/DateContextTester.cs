using System;
using System.Data.SqlTypes;
using Tarantino.Core.Commons.Model.Enumerations;
using Tarantino.Core.Commons.Services.Environment;
using Tarantino.Core.Commons.Services.Environment.Impl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace Tarantino.UnitTests.Core.Commons.Services.Environment
{
	[TestFixture]
	public class DateContextTester
	{
		[Test]
		public void Correctly_determines_current_year()
		{
			MockRepository mocks = new MockRepository();
			ISystemClock clock = mocks.CreateMock<ISystemClock>();

			using (mocks.Record())
			{
				Expect.Call(clock.GetCurrentDateTime()).Return(new DateTime(2007, 4, 15));
			}

			using (mocks.Playback())
			{
				IDateContext context = new DateContext(clock);
				Assert.That(context.GetCurrentYear(), Is.EqualTo(2007));
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Correctly_determines_current_date()
		{
			MockRepository mocks = new MockRepository();
			ISystemClock clock = mocks.CreateMock<ISystemClock>();

			using (mocks.Record())
			{
				Expect.Call(clock.GetCurrentDateTime()).Return(new DateTime(2007, 4, 15, 8, 15, 0));
			}

			using (mocks.Playback())
			{
				IDateContext context = new DateContext(clock);
				Assert.That(context.GetCurrentDate(), Is.EqualTo(new DateTime(2007, 4, 15)));
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Correctly_determines_first_day_of_current_month()
		{
			MockRepository mocks = new MockRepository();
			ISystemClock clock = mocks.CreateMock<ISystemClock>();

			using (mocks.Record())
			{
				Expect.Call(clock.GetCurrentDateTime()).Return(new DateTime(2007, 4, 15, 8, 15, 0));
			}

			using (mocks.Playback())
			{
				IDateContext context = new DateContext(clock);
				Assert.That(context.GetFirstDayOfCurrentMonth(), Is.EqualTo(new DateTime(2007, 4, 1)));
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Correctly_determines_first_day_of_current_year()
		{
			MockRepository mocks = new MockRepository();
			ISystemClock clock = mocks.CreateMock<ISystemClock>();

			using (mocks.Record())
			{
				Expect.Call(clock.GetCurrentDateTime()).Return(new DateTime(2007, 4, 15, 8, 15, 0));
			}

			using (mocks.Playback())
			{
				IDateContext context = new DateContext(clock);
				Assert.That(context.GetFirstDayOfCurrentYear(), Is.EqualTo(new DateTime(2007, 1, 1)));
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Correctly_gets_date_by_time_period()
		{
			MockRepository mocks = new MockRepository();
			ISystemClock clock = mocks.CreateMock<ISystemClock>();

			using (mocks.Record())
			{
				Expect.Call(clock.GetCurrentDateTime()).Return(new DateTime(2007, 4, 15, 8, 15, 0)).Repeat.Times(3);
			}

			using (mocks.Playback())
			{
				IDateContext context = new DateContext(clock);
				Assert.That(context.GetFirstDayOfTimePeriod(TimePeriod.CurrentDay), Is.EqualTo(new DateTime(2007, 4, 15)));
				Assert.That(context.GetFirstDayOfTimePeriod(TimePeriod.CurrentMonth), Is.EqualTo(new DateTime(2007, 4, 1)));
				Assert.That(context.GetFirstDayOfTimePeriod(TimePeriod.CurrentYear), Is.EqualTo(new DateTime(2007, 1, 1)));
				Assert.That(context.GetFirstDayOfTimePeriod(TimePeriod.AllTime), Is.EqualTo(SqlDateTime.MinValue.Value));
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Correctly_gets_date_text_by_time_period()
		{
			MockRepository mocks = new MockRepository();
			ISystemClock clock = mocks.CreateMock<ISystemClock>();

			using (mocks.Record())
			{
				Expect.Call(clock.GetCurrentDateTime()).Return(new DateTime(2007, 4, 15, 8, 15, 0)).Repeat.Times(3);
			}

			using (mocks.Playback())
			{
				IDateContext context = new DateContext(clock);
				Assert.That(context.GetTimePeriodName(TimePeriod.CurrentDay), Is.EqualTo("Today"));
				Assert.That(context.GetTimePeriodName(TimePeriod.CurrentMonth), Is.EqualTo("April, 2007"));
				Assert.That(context.GetTimePeriodName(TimePeriod.CurrentYear), Is.EqualTo("2007"));
				Assert.That(context.GetTimePeriodName(TimePeriod.AllTime), Is.EqualTo("All Time"));
			}

			mocks.VerifyAll();
		}
	}
}