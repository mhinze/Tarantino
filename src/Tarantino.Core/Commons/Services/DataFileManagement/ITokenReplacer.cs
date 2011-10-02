

namespace Tarantino.Core.Commons.Services.DataFileManagement
{
	
	public interface ITokenReplacer
	{
		void Replace(string token, string tokenValue);
		string Text { get; set; }
	}
}