using System.Reflection;


namespace Tarantino.Core.Commons.Services.Environment
{
	
	public interface IAssemblyContext
	{
		Assembly GetExecutingAssembly();
		string GetAssemblyVersion();
	}
}