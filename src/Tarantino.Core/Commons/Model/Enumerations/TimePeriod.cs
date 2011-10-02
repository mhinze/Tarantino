namespace Tarantino.Core.Commons.Model.Enumerations
{
	public class TimePeriod : Enumeration
	{
		public static readonly TimePeriod CurrentDay = new TimePeriod(1, "Today");
		public static readonly TimePeriod CurrentMonth = new TimePeriod(2, "This Month");
		public static readonly TimePeriod CurrentYear = new TimePeriod(3, "This Year");
		public static readonly TimePeriod AllTime = new TimePeriod(4, "All Time");

		public TimePeriod()
		{
		}

		public TimePeriod(int value, string displayName)
			: base(value, displayName)
		{
		}
	}
}