using System;
using Tarantino.Core.Commons.Services.Environment;


namespace Tarantino.Core.Commons.Services.Environment.Impl
{
	
	public class ApplicationDomain : IApplicationDomain
	{
		public string GetBaseFolder()
		{
			string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
			return baseDirectory;
		}
	}
}