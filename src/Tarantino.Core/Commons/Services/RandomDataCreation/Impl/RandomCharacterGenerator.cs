using System;
using Tarantino.Core.Commons.Services.RandomDataCreation;


namespace Tarantino.Core.Commons.Services.RandomDataCreation.Impl
{
	
	public class RandomCharacterGenerator : IRandomCharacterGenerator
	{
		private const int _lastLetter = 25;
		private const int _1 = 49;
		private const int _a = 97;

		private readonly IRandomNumberGenerator _numberGenerator;

		public RandomCharacterGenerator(IRandomNumberGenerator numberGenerator)
		{
			_numberGenerator = numberGenerator;
		}

		public char GetRandomCharacter()
		{
			var randomNumber = _numberGenerator.GenerateRandomNumber(34);

			var isLetter = randomNumber <= _lastLetter;

			var characterIndex = isLetter ? randomNumber + _a : (randomNumber - 26) + _1;
			var randomCharacter = Convert.ToChar(characterIndex);

			return randomCharacter;
		}
	}
}