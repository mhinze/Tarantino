using NUnit.Framework;
using Tarantino.Core.Commons.Model.Specifications;

namespace Tarantino.UnitTests.Core.Commons.Model.Specifications
{
	public class NotSpecificationTester : SpecTestBase
	{
		[Test]
		public void should_provide_satisfaction()
		{
			new NotSpecification<string>(_trueSpec).IsSatisfiedBy(null).ShouldBeFalse();
			new NotSpecification<string>(_falseSpec).IsSatisfiedBy(null).ShouldBeTrue();
		}
	}
}