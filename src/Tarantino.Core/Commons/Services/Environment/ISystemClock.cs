using System;


namespace Tarantino.Core.Commons.Services.Environment
{
	
	public interface ISystemClock
	{
		DateTime GetCurrentDateTime();
	}
}