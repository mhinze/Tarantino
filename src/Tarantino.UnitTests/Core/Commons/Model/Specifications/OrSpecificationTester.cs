using NUnit.Framework;
using Tarantino.Core.Commons.Model.Specifications;

namespace Tarantino.UnitTests.Core.Commons.Model.Specifications
{
	public class OrSpecificationTester : SpecTestBase
	{
		[Test]
		public void should_provide_satisfaction()
		{
			new OrSpecification<string>(_trueSpec, _falseSpec).IsSatisfiedBy(null).ShouldBeTrue();
			new OrSpecification<string>(_falseSpec, _falseSpec).IsSatisfiedBy(null).ShouldBeFalse();
			new OrSpecification<string>(_trueSpec, _trueSpec).IsSatisfiedBy(null).ShouldBeTrue();
			new OrSpecification<string>(_falseSpec, _trueSpec).IsSatisfiedBy(null).ShouldBeTrue();
		} 
	}
}