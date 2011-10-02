using System.Collections.Generic;
using Tarantino.Core.Commons.Services.RandomDataCreation;


namespace Tarantino.Core.Commons.Services.RandomDataCreation.Impl
{
	
	public class CodeGenerator : ICodeGenerator
	{
		private readonly IRandomCharacterGenerator _characterGenerator;

		public CodeGenerator(IRandomCharacterGenerator characterGenerator)
		{
			_characterGenerator = characterGenerator;
		}

		public string GetRandomCode(int numberOfCharacters)
		{
			var characters = new List<char>();

			for(var i = 0; i < numberOfCharacters; i++)
			{
				var randomCharacter = _characterGenerator.GetRandomCharacter();
				characters.Add(randomCharacter);
			}

			var randomCode = new string(characters.ToArray());
			return randomCode;
		}
	}
}