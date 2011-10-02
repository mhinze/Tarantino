using NUnit.Framework;
using Tarantino.Core.Commons.Model.Specifications;

namespace Tarantino.UnitTests.Core.Commons.Model.Specifications
{
	public class AndSpecificationTester : SpecTestBase
	{
		[Test]
		public void should_provide_satisfaction()
		{
			new AndSpecification<string>(_trueSpec, _falseSpec).IsSatisfiedBy(null).ShouldBeFalse();
			new AndSpecification<string>(_falseSpec, _falseSpec).IsSatisfiedBy(null).ShouldBeFalse();
			new AndSpecification<string>(_trueSpec, _trueSpec).IsSatisfiedBy(null).ShouldBeTrue();
			new AndSpecification<string>(_falseSpec, _trueSpec).IsSatisfiedBy(null).ShouldBeFalse();
		}
	}
}