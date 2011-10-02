using System;

namespace Tarantino.Core.Commons.Services.Environment
{
	public interface IGuidGenerator
	{
		Guid CreateGuid();
	}
}