using System;

namespace Tarantino.Core.Commons.Services.Environment.Impl
{
	public class GuidGenerator : IGuidGenerator
	{
		public Guid CreateGuid()
		{
			return Guid.NewGuid();
		}
	}
}