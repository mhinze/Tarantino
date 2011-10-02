namespace Tarantino.Core.Commons.Services.Security
{
	public interface IHashAlgorithm
	{
		string ComputeHash(string input);
	}
}