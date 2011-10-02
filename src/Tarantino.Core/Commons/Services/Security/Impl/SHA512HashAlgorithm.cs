using System;
using System.Security.Cryptography;
using System.Text;

namespace Tarantino.Core.Commons.Services.Security.Impl
{
	public class SHA512HashAlgorithm : IHashAlgorithm
	{
		public string ComputeHash(string input)
		{
			var sha = new SHA512Managed();

			byte[] bytes = Encoding.UTF8.GetBytes(input);
			byte[] hash = sha.ComputeHash(bytes);
			string base64Hash = Convert.ToBase64String(hash);

			return base64Hash;
		}
	}
}