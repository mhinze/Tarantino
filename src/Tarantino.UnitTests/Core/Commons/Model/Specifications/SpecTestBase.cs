using NUnit.Framework;
using Tarantino.Core.Commons.Model.Specifications;

namespace Tarantino.UnitTests.Core.Commons.Model.Specifications
{
	[TestFixture]
	public class SpecTestBase
	{
		protected BaseSpecification<string> _trueSpec;
		protected BaseSpecification<string> _falseSpec;

		[TestFixtureSetUp]
		public void SetUp()
		{
			_trueSpec = new GenericSpecification(x => true);
			_falseSpec = new GenericSpecification(x => false);
		}
 
	}
}