namespace Tarantino.Deployer.Core.Services
{
	public interface IVersionNumberParser
	{
		int Parse(string output);
	}
}