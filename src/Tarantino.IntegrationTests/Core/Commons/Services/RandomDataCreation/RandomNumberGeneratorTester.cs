using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Tarantino.Core.Commons.Services.RandomDataCreation;
using Tarantino.Core.Commons.Services.RandomDataCreation.Impl;

namespace Tarantino.IntegrationTests.Core.Commons.Services.RandomDataCreation
{
	[TestFixture]
	public class RandomNumberGeneratorTester
	{
		[Test]
		public void Correctly_Generates_Unique_Non_Zero_Character()
		{
			Dictionary<int, int> numberFrequencies = new Dictionary<int, int>();

			IRandomNumberGenerator generator = new RandomNumberGenerator();

			for(int i = 0; i < 1000; i++)
			{
				int randomNumber = generator.GenerateRandomNumber(25);

				Assert.That(randomNumber, Is.GreaterThanOrEqualTo(0));
				Assert.That(randomNumber, Is.LessThanOrEqualTo(25));

				if (numberFrequencies.ContainsKey(randomNumber))
				{
					numberFrequencies[randomNumber]++;
				}
				else
				{
					numberFrequencies[randomNumber] = 1;
				}
			}

			Assert.That(numberFrequencies.ContainsKey(0));
			Assert.That(numberFrequencies.ContainsKey(25));

			foreach (KeyValuePair<int, int> numberFrequency in numberFrequencies)
			{
				Assert.That(numberFrequency.Value < 100);
			}
		}
	}
}