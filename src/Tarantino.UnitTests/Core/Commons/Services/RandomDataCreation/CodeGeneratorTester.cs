using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Tarantino.Core.Commons.Services.RandomDataCreation;
using Tarantino.Core.Commons.Services.RandomDataCreation.Impl;

namespace Tarantino.UnitTests.Core.Commons.Services.RandomDataCreation
{
	[TestFixture]
	public class CodeGeneratorTester
	{
		[Test]
		public void Correctly_generates_random_code()
		{
			MockRepository mocks = new MockRepository();
			IRandomCharacterGenerator characterGenerator = mocks.CreateMock<IRandomCharacterGenerator>();

			using (mocks.Record())
			{
				Expect.Call(characterGenerator.GetRandomCharacter()).Return('k');
				Expect.Call(characterGenerator.GetRandomCharacter()).Return('w');
				Expect.Call(characterGenerator.GetRandomCharacter()).Return('h');
			}

			using (mocks.Playback())
			{
				ICodeGenerator codeGenerator = new CodeGenerator(characterGenerator);
				string code = codeGenerator.GetRandomCode(3);

				Assert.That(code, Is.EqualTo("kwh"));
			}

			mocks.VerifyAll();
		}
	}
}