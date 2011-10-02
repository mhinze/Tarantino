using Tarantino.Core.Commons.Model.Enumerations;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Tarantino.UnitTests.Core.Commons.Model.Enumerations
{
	[TestFixture]
	public class BoundedEnumerationTester
	{
		[Test]
		public void Should_return_enumeration_by_bounded_value()
		{
			BoundedTestEnumeration e = BoundedTestEnumeration.FromBoundedValue<BoundedTestEnumeration>(15);
			Assert.That(e.Lower, Is.EqualTo(11));
			Assert.That(e.Upper, Is.EqualTo(20));
			Assert.That(e, Is.EqualTo(BoundedTestEnumeration.Blue));
			
		}

		class BoundedTestEnumeration : BoundedEnumeration<int>
		{
			public static readonly BoundedTestEnumeration Red = new BoundedTestEnumeration(1, "Red", 0, 10);
			public static readonly BoundedTestEnumeration Blue = new BoundedTestEnumeration(2, "Blue", 11, 20);

			public BoundedTestEnumeration()
			{
			}

			public BoundedTestEnumeration(int value, string displayName, int lower, int upper)
				: base(value, displayName, lower, upper)
			{
			}
		}
	}
}