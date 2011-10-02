using System;


namespace Tarantino.Core.Commons.Services.Environment.Impl
{
	
	public class SystemClock : ISystemClock
	{
		public DateTime GetCurrentDateTime()
		{
			return DateTime.Now;
		}
	}
}