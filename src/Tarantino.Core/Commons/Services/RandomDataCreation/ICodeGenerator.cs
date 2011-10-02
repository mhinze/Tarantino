
using Tarantino.Core;

namespace Tarantino.Core.Commons.Services.RandomDataCreation
{
	
	public interface ICodeGenerator
	{
		string GetRandomCode(int numberOfCharacters);
	}
}