using System;
using System.Collections;
using System.Collections.Generic;
using Tarantino.Core.Commons.Model.Enumerations;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Tarantino.UnitTests.Core.Commons.Model.Enumerations
{
	[TestFixture]
	public class EnumerationTester
	{
		[Test]
		public void Should_correctly_determine_absolute_difference()
		{
			Assert.That(Enumeration.AbsoluteDifference(PersistenceMode.Archive, PersistenceMode.Live), Is.EqualTo(1));
		}

		[Test]
		public void Should_determine_enumerations_of_different_type_arent_equal()
		{
			Assert.That(PersistenceMode.Archive, Is.Not.EqualTo(2));
		}

		[Test]
		public void Should_compare_correctly_for_equality()
		{
			Assert.That(PersistenceMode.Live.CompareTo(PersistenceMode.Archive), Is.EqualTo(-1));
			Assert.That(PersistenceMode.Archive.CompareTo(PersistenceMode.Live), Is.EqualTo(1));
			Assert.That(PersistenceMode.Live.CompareTo(PersistenceMode.Live), Is.EqualTo(0));
		}

		[Test]
		public void Should_compare_objects_of_other_types()
		{
			Assert.That(PersistenceMode.Live.Equals(DBNull.Value), Is.False);
		}

		[Test]
		public void Can_sort_enumeration_values()
		{
			var modes = new[] {PersistenceMode.Archive, PersistenceMode.Live};
			Array.Sort(modes);

			Assert.That(modes, Is.EqualTo(new[] { PersistenceMode.Live, PersistenceMode.Archive }));
		}

		[Test]
		public void Should_determine_enumeration_never_equals_null()
		{
			Assert.That(PersistenceMode.Live, Is.Not.EqualTo(null));
		}

		[Test]
		public void Enumerations_of_different_types_are_not_equal()
		{
			Assert.That(PersistenceMode.Live, Is.Not.EqualTo(TestEnumeration.Red));
		}

		[Test]
		public void Should_return_all_enumerated_values()
		{
			IEnumerable<TestEnumeration> values = Enumeration.GetAll<TestEnumeration>();

			EnumerableAssert.That(values, Has.Count(2));
			EnumerableAssert.Contains(values, TestEnumeration.Red);
			EnumerableAssert.Contains(values, TestEnumeration.Blue);
		}

		[Test]
		public void Should_return_all_enumerated_values_from_type()
		{
			IEnumerable values = Enumeration.GetAll(typeof(TestEnumeration));

			var strongTypeValues = new List<TestEnumeration>();
			foreach (var enumValue in values)
			{
				strongTypeValues.Add((TestEnumeration)enumValue);
			}

			EnumerableAssert.That(strongTypeValues, Has.Count(2));
			EnumerableAssert.Contains(strongTypeValues, TestEnumeration.Red);
			EnumerableAssert.Contains(strongTypeValues, TestEnumeration.Blue);
		}

		[Test]
		public void Should_correctly_return_hash_code_of_integer_value()
		{
			Assert.That(TestEnumeration.Red.GetHashCode(), Is.EqualTo(TestEnumeration.Red.Value.GetHashCode()));
		}

		[Test]
		public void Should_return_enumerated_value_by_value()
		{
			var value = Enumeration.FromValue<TestEnumeration>(2);

			Assert.AreSame(TestEnumeration.Blue, value);
			Assert.AreNotSame(TestEnumeration.Red, value);
		}

		[Test]
		public void Should_return_enumerated_value_by_display_name()
		{
			var value = Enumeration.FromDisplayName<TestEnumeration>("Red");

			Assert.AreNotSame(TestEnumeration.Blue, value);
			Assert.AreSame(TestEnumeration.Red, value);
		}

		[Test]
		public void Should_correctly_compare_two_enumerated_values()
		{
			Assert.AreEqual(TestEnumeration.Red, TestEnumeration.Red);
			Assert.AreEqual(new TestEnumeration(1, "Red"), TestEnumeration.Red);
			Assert.AreNotEqual(TestEnumeration.Blue, TestEnumeration.Red);
			Assert.AreNotEqual(new TestEnumeration(2, "Red"), TestEnumeration.Red);
		}

		[Test, ExpectedException(ExceptionType = typeof(ApplicationException), ExpectedMessage = "'Green' is not a valid display name in Tarantino.UnitTests.Core.Commons.Model.Enumerations.EnumerationTester+TestEnumeration")]
		public void Should_throw_exception_when_enumeration_display_name_cannot_be_parsed()
		{
			Enumeration.FromDisplayName<TestEnumeration>("Green");
		}

		[Test, ExpectedException(ExceptionType = typeof(ApplicationException), ExpectedMessage = "'3' is not a valid value in Tarantino.UnitTests.Core.Commons.Model.Enumerations.EnumerationTester+TestEnumeration")]
		public void Should_throw_exception_when_enumeration_value_cannot_be_parsed()
		{
			Enumeration.FromValue<TestEnumeration>(3);
		}

		class TestEnumeration : Enumeration
		{
			public static readonly TestEnumeration Red = new TestEnumeration(1, "Red");
			public static readonly TestEnumeration Blue = new TestEnumeration(2, "Blue");

			public TestEnumeration()
			{
			}

			public TestEnumeration(int value, string displayName) : base(value, displayName)
			{
			}
		}
	}
}