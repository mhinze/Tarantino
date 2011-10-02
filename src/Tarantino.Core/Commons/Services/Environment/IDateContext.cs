using System;
using Tarantino.Core.Commons.Model.Enumerations;


namespace Tarantino.Core.Commons.Services.Environment
{
	
	public interface IDateContext
	{
		int GetCurrentYear();
		DateTime GetCurrentDate();
		DateTime GetFirstDayOfCurrentMonth();
		DateTime GetFirstDayOfCurrentYear();
		DateTime GetFirstDayOfTimePeriod(TimePeriod timePeriod);
		string GetTimePeriodName(TimePeriod timePeriod);
	}
}