using System;
using NUnit.Framework;
using Tarantino.Core.Commons.Model.Specifications;
using Tarantino.UnitTests;

namespace Tarantino.UnitTests.Core.Commons.Model.Specifications
{
	[TestFixture]
	public class BaseSpecificationTester : SpecTestBase
	{

		[Test]
		public void should_properly_determine_satisfaction()
		{
			_trueSpec.IsSatisfiedBy(null).ShouldBeTrue();
			_trueSpec.IsNotSatisfiedBy(null).ShouldBeFalse();
		} 

		[Test]
		public void can_use_or_plus_operator()
		{

			(_trueSpec + _trueSpec).IsSatisfiedBy(null).ShouldEqual(true);
			(_falseSpec + _falseSpec).IsSatisfiedBy(null).ShouldEqual(false);
			(_trueSpec + _falseSpec).IsSatisfiedBy(null).ShouldEqual(true);
			(_falseSpec + _trueSpec).IsSatisfiedBy(null).ShouldEqual(true);
		}

		[Test]
		public void can_use_and_asterisk_operator()
		{
			(_trueSpec * _trueSpec).IsSatisfiedBy(null).ShouldEqual(true);
			(_falseSpec * _falseSpec).IsSatisfiedBy(null).ShouldEqual(false);
			(_trueSpec * _falseSpec).IsSatisfiedBy(null).ShouldEqual(false);
			(_falseSpec * _trueSpec).IsSatisfiedBy(null).ShouldEqual(false);
		}

		[Test]
		public void can_use_not_bang_operator()
		{
			(!_trueSpec).IsSatisfiedBy(null).ShouldEqual(false);
			(!_falseSpec).IsSatisfiedBy(null).ShouldEqual(true);
		}

		[Test]
		public void can_use_composed_operator()
		{
			(_trueSpec * _falseSpec + _trueSpec).IsSatisfiedBy(null).ShouldBeTrue();
			(_trueSpec * (_falseSpec + _trueSpec)).IsSatisfiedBy(null).ShouldBeTrue();
			(_trueSpec * (_falseSpec + _falseSpec)).IsSatisfiedBy(null).ShouldBeFalse();
			(_trueSpec * (_falseSpec + !_falseSpec)).IsSatisfiedBy(null).ShouldBeTrue();
		} 

		[Test]
		public void can_use_and_method()
		{
			_trueSpec.And(_trueSpec).IsSatisfiedBy(null).ShouldBeTrue();
			_trueSpec.And(_falseSpec).IsSatisfiedBy(null).ShouldBeFalse();
			_falseSpec.And(_falseSpec).IsSatisfiedBy(null).ShouldBeFalse();
			_falseSpec.And(_trueSpec).IsSatisfiedBy(null).ShouldBeFalse();
		}

		[Test]
		public void can_use_or_method()
		{
			_trueSpec.Or(_trueSpec).IsSatisfiedBy(null).ShouldBeTrue();
			_trueSpec.Or(_falseSpec).IsSatisfiedBy(null).ShouldBeTrue();
			_falseSpec.Or(_falseSpec).IsSatisfiedBy(null).ShouldBeFalse();
			_falseSpec.Or(_trueSpec).IsSatisfiedBy(null).ShouldBeTrue();
		}

		[Test]
		public void can_use_or_not_method()
		{
			_trueSpec.OrNot(_trueSpec).IsSatisfiedBy(null).ShouldBeTrue();
			_trueSpec.OrNot(_falseSpec).IsSatisfiedBy(null).ShouldBeTrue();
			_falseSpec.OrNot(_falseSpec).IsSatisfiedBy(null).ShouldBeTrue();
			_falseSpec.OrNot(_trueSpec).IsSatisfiedBy(null).ShouldBeFalse();
		}

		[Test]
		public void can_use_and_not_method()
		{
			_trueSpec.AndNot(_trueSpec).IsSatisfiedBy(null).ShouldBeFalse();
			_trueSpec.AndNot(_falseSpec).IsSatisfiedBy(null).ShouldBeTrue();
			_falseSpec.AndNot(_falseSpec).IsSatisfiedBy(null).ShouldBeFalse();
			_falseSpec.AndNot(_trueSpec).IsSatisfiedBy(null).ShouldBeFalse();
		}

		[Test]
		public void can_use_composed_methods()
		{
			_trueSpec.And(_falseSpec.Or(_trueSpec)).AndNot(_falseSpec).IsSatisfiedBy(null).ShouldBeTrue();
		} 
	}

	class GenericSpecification : BaseSpecification<string>
	{
		private readonly Func<string, bool> _testMethod;

		public GenericSpecification(Func<string, bool> testMethod)
		{
			_testMethod = testMethod;
		}

		public override bool IsSatisfiedBy(string objectToTestSatisfaction)
		{
			return _testMethod(objectToTestSatisfaction);
		}
	}
}