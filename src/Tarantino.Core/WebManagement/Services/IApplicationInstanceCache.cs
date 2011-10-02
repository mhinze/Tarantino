using System;

using Tarantino.Core.WebManagement.Model;

namespace Tarantino.Core.WebManagement.Services
{
	
	public interface IApplicationInstanceCache
	{
		ApplicationInstance GetCurrent();
		void Set(string key, ApplicationInstance item);
	}
}