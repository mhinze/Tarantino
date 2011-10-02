using System.Collections.Generic;
using Tarantino.Core.Commons.Model.Enumerations;


namespace Tarantino.Core.Commons.Services.ListManagement.Impl
{
	
	public class EnumerationHelper : IEnumerationHelper
	{
		public IEnumerable<EnumerationType> GetAll<EnumerationType>() where EnumerationType : Enumeration, new()
		{
			return Enumeration.GetAll<EnumerationType>();
		}

		public int DetermineAbsoluteDifference(Enumeration firstValue, Enumeration secondValue)
		{
			return Enumeration.AbsoluteDifference(firstValue, secondValue);
		}
	}
}