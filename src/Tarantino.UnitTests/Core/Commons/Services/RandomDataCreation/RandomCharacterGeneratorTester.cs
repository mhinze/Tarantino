using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Tarantino.Core.Commons.Services.RandomDataCreation;
using Tarantino.Core.Commons.Services.RandomDataCreation.Impl;

namespace Tarantino.UnitTests.Core.Commons.Services.RandomDataCreation
{
	[TestFixture]
	public class RandomCharacterGeneratorTester
	{
		[Test]
		public void Correctly_returns_each_possible_character()
		{
			MockRepository mocks = new MockRepository();
			IRandomNumberGenerator numberGenerator = mocks.CreateMock<IRandomNumberGenerator>();

			using (mocks.Record())
			{
				for(int randomNumber = 0; randomNumber < 35; randomNumber++)
				{
					Expect.Call(numberGenerator.GenerateRandomNumber(34)).Return(randomNumber);
				}
			}

			using (mocks.Playback())
			{
				IRandomCharacterGenerator generator = new RandomCharacterGenerator(numberGenerator);

				Assert.That(generator.GetRandomCharacter(), Is.EqualTo('a'));
				Assert.That(generator.GetRandomCharacter(), Is.EqualTo('b'));
				Assert.That(generator.GetRandomCharacter(), Is.EqualTo('c'));
				Assert.That(generator.GetRandomCharacter(), Is.EqualTo('d'));
				Assert.That(generator.GetRandomCharacter(), Is.EqualTo('e'));
				Assert.That(generator.GetRandomCharacter(), Is.EqualTo('f'));
				Assert.That(generator.GetRandomCharacter(), Is.EqualTo('g'));
				Assert.That(generator.GetRandomCharacter(), Is.EqualTo('h'));
				Assert.That(generator.GetRandomCharacter(), Is.EqualTo('i'));
				Assert.That(generator.GetRandomCharacter(), Is.EqualTo('j'));
				Assert.That(generator.GetRandomCharacter(), Is.EqualTo('k'));
				Assert.That(generator.GetRandomCharacter(), Is.EqualTo('l'));
				Assert.That(generator.GetRandomCharacter(), Is.EqualTo('m'));
				Assert.That(generator.GetRandomCharacter(), Is.EqualTo('n'));
				Assert.That(generator.GetRandomCharacter(), Is.EqualTo('o'));
				Assert.That(generator.GetRandomCharacter(), Is.EqualTo('p'));
				Assert.That(generator.GetRandomCharacter(), Is.EqualTo('q'));
				Assert.That(generator.GetRandomCharacter(), Is.EqualTo('r'));
				Assert.That(generator.GetRandomCharacter(), Is.EqualTo('s'));
				Assert.That(generator.GetRandomCharacter(), Is.EqualTo('t'));
				Assert.That(generator.GetRandomCharacter(), Is.EqualTo('u'));
				Assert.That(generator.GetRandomCharacter(), Is.EqualTo('v'));
				Assert.That(generator.GetRandomCharacter(), Is.EqualTo('w'));
				Assert.That(generator.GetRandomCharacter(), Is.EqualTo('x'));
				Assert.That(generator.GetRandomCharacter(), Is.EqualTo('y'));
				Assert.That(generator.GetRandomCharacter(), Is.EqualTo('z'));

				Assert.That(generator.GetRandomCharacter(), Is.EqualTo('1'));
				Assert.That(generator.GetRandomCharacter(), Is.EqualTo('2'));
				Assert.That(generator.GetRandomCharacter(), Is.EqualTo('3'));
				Assert.That(generator.GetRandomCharacter(), Is.EqualTo('4'));
				Assert.That(generator.GetRandomCharacter(), Is.EqualTo('5'));
				Assert.That(generator.GetRandomCharacter(), Is.EqualTo('6'));
				Assert.That(generator.GetRandomCharacter(), Is.EqualTo('7'));
				Assert.That(generator.GetRandomCharacter(), Is.EqualTo('8'));
				Assert.That(generator.GetRandomCharacter(), Is.EqualTo('9'));
			}

			mocks.VerifyAll();
		}
	}
}