using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;


namespace Tarantino.Core.Commons.Services.Security.Impl
{
	/// <summary>
	/// This class uses a symmetric key algorithm (Rijndael/AES) to encrypt and 
	/// decrypt data. As long as encryption and decryption routines use the same
	/// parameters to generate the keys, the keys are guaranteed to be the same.
	/// The class uses static functions with duplicate code to make it easier to
	/// demonstrate encryption and decryption logic.
	/// </summary>
	
	public class AesEncryptionEngine : IEncryptionEngine
	{
		private const string encryptionPassword = "password";
		private const string encryptionSalt = "password";
		private const int encryptionPasswordIterations = 2;
		private const string encryptionInitVector = "passwordpassword";
		private const int encryptionKeySize = 128;

		public string Decrypt(string input)
		{
			byte[] initVectorBytes = Encoding.ASCII.GetBytes(encryptionInitVector);
			byte[] saltValueBytes = Encoding.ASCII.GetBytes(encryptionSalt);

			byte[] cipherTextBytes = Convert.FromBase64String(input);

			Rfc2898DeriveBytes password =
				new Rfc2898DeriveBytes(encryptionPassword, saltValueBytes, encryptionPasswordIterations);

			byte[] keyBytes = password.GetBytes(encryptionKeySize/8);

			RijndaelManaged symmetricKey = new RijndaelManaged();

			symmetricKey.Mode = CipherMode.CBC;

			ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);

			MemoryStream memoryStream = new MemoryStream(cipherTextBytes);

			CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

			byte[] plainTextBytes = new byte[cipherTextBytes.Length];

			int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

			memoryStream.Close();
			cryptoStream.Close();

			return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
		}

		public string Encrypt(string plainText)
		{
			byte[] initVectorBytes = Encoding.ASCII.GetBytes(encryptionInitVector);
			byte[] saltValueBytes = Encoding.ASCII.GetBytes(encryptionSalt);

			byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

			Rfc2898DeriveBytes password =
				new Rfc2898DeriveBytes(encryptionPassword, saltValueBytes, encryptionPasswordIterations);

			byte[] keyBytes = password.GetBytes(encryptionKeySize/8);

			RijndaelManaged symmetricKey = new RijndaelManaged();

			symmetricKey.Mode = CipherMode.CBC;

			ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);

			MemoryStream memoryStream = new MemoryStream();

			CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

			cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

			cryptoStream.FlushFinalBlock();

			byte[] cipherTextBytes = memoryStream.ToArray();

			memoryStream.Close();
			cryptoStream.Close();

			string cipherText = Convert.ToBase64String(cipherTextBytes);

			return cipherText;
		}
	}
}