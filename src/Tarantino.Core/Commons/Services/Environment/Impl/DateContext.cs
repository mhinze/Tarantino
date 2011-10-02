using System;
using System.Data.SqlTypes;
using Tarantino.Core.Commons.Model.Enumerations;


namespace Tarantino.Core.Commons.Services.Environment.Impl
{
	
	public class DateContext : IDateContext
	{
		private readonly ISystemClock _clock;

		public DateContext(ISystemClock clock)
		{
			_clock = clock;
		}

		public int GetCurrentYear()
		{
			var currentYear = _clock.GetCurrentDateTime().Year;
			return currentYear;
		}

		public DateTime GetCurrentDate()
		{
			DateTime currentDate = _clock.GetCurrentDateTime().Date;
			return currentDate;
		}

		public DateTime GetFirstDayOfCurrentMonth()
		{
			DateTime currentDate = _clock.GetCurrentDateTime();
			DateTime firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
			return firstDayOfMonth;
		}

		public DateTime GetFirstDayOfCurrentYear()
		{
			DateTime currentDate = _clock.GetCurrentDateTime();
			DateTime firstDayOfYear = new DateTime(currentDate.Year, 1, 1);
			return firstDayOfYear;
		}

		public DateTime GetFirstDayOfTimePeriod(TimePeriod timePeriod)
		{
			if (timePeriod == TimePeriod.CurrentDay)
			{
				return GetCurrentDate();
			}
			else if (timePeriod == TimePeriod.CurrentMonth)
			{
				return GetFirstDayOfCurrentMonth();
			}
			else if (timePeriod == TimePeriod.CurrentYear)
			{
				return GetFirstDayOfCurrentYear();
			}
			else
			{
				return SqlDateTime.MinValue.Value;
			}
		}

		public string GetTimePeriodName(TimePeriod timePeriod)
		{
			DateTime date = GetFirstDayOfTimePeriod(timePeriod);

			if (timePeriod == TimePeriod.CurrentDay)
			{
				return "Today";
			}
			else if (timePeriod == TimePeriod.CurrentMonth)
			{
				return string.Format("{0}, {1}", date.ToString("MMMM"), date.Year);
			}
			else if (timePeriod == TimePeriod.CurrentYear)
			{
				return date.Year.ToString();
			}
			else
			{
				return "All Time";
			}
		}
	}
}