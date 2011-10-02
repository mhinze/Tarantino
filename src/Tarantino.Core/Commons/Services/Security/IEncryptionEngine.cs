namespace Tarantino.Core.Commons.Services.Security
{
	public interface IEncryptionEngine
	{
		string Encrypt(string input);
		string Decrypt(string input);
	}
}