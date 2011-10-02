using System.Collections.Generic;
using Tarantino.Core.Commons.Model.Enumerations;


namespace Tarantino.Core.Commons.Services.ListManagement
{
	
	public interface IEnumerationHelper
	{
		IEnumerable<EnumerationType> GetAll<EnumerationType>() where EnumerationType : Enumeration, new();
		int DetermineAbsoluteDifference(Enumeration firstValue, Enumeration secondValue);
	}
}