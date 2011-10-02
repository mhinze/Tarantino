using System;
using Tarantino.Core.Commons.Model.Enumerations;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Tarantino.Core.Commons.Services.Repositories;

namespace Tarantino.UnitTests.Core.Commons.Services.Repositories
{
	[TestFixture]
	public class CriterionSetTester
	{
		[Test]
		public void Property_accessors_work()
		{
			CriterionSet criteria = new CriterionSet();

			Assert.AreEqual(null, criteria.OrderBy);
			criteria.OrderBy = "OrderBy";
			Assert.AreEqual("OrderBy", criteria.OrderBy);

			Assert.AreEqual(null, criteria.SortOrder);
			criteria.SortOrder = SortOrder.Descending;
			Assert.AreEqual(SortOrder.Descending, criteria.SortOrder);
		}

		[Test]
		public void Can_add_criterion()
		{
			CriterionSet set = new CriterionSet();

			Criterion criterion1 = new Criterion("A1", "V1");
			Criterion criterion2 = new Criterion("A2", "V2");

			set.AddCriterion(criterion1);
			set.AddCriterion(criterion2);

			Assert.That(set.GetCriteria(), Is.EquivalentTo(new Criterion[] { criterion1, criterion2 }));
		}

		[Test]
		public void Correctly_compares_equal_sets()
		{
			CriterionSet set1 = new CriterionSet();
			CriterionSet set2 = new CriterionSet();

			set1.AddCriterion(new Criterion("A1", "V1", ComparisonOperator.Equal));
			set1.AddCriterion(new Criterion("A2", "V2", ComparisonOperator.Equal));

			set2.AddCriterion(new Criterion("A1", "V1", ComparisonOperator.Equal));
			set2.AddCriterion(new Criterion("A2", "V2", ComparisonOperator.Equal));

			Assert.That(set1, Is.EqualTo(set2));
			Assert.That(set2, Is.EqualTo(set1));
		}

		[Test]
		public void Calculates_hash_code_based_on_criterion()
		{
			CriterionSet set1 = new CriterionSet();

			Criterion criterion1 = new Criterion("A1", "V1", ComparisonOperator.Equal);
			Criterion criterion2 = new Criterion("A2", "V2", ComparisonOperator.Equal);

			set1.AddCriterion(criterion1);
			set1.AddCriterion(criterion2);

			Assert.That(set1.GetHashCode(), Is.EqualTo(criterion1.GetHashCode() + criterion2.GetHashCode()));
		}

		[Test]
		public void Correctly_compares_equal_sets_both_having_null()
		{
			CriterionSet set1 = new CriterionSet();
			CriterionSet set2 = new CriterionSet();

			set1.AddCriterion(new Criterion("A1", null, ComparisonOperator.Equal));
			set2.AddCriterion(new Criterion("A1", null, ComparisonOperator.Equal));

			Assert.That(set1, Is.EqualTo(set2));
			Assert.That(set2, Is.EqualTo(set1));
		}

		[Test]
		public void Correctly_compares_unequal_sets_based_on_sorting()
		{
			CriterionSet set1 = new CriterionSet();
			CriterionSet set2 = new CriterionSet();

			set1.OrderBy = "A1";
			set2.OrderBy = "B1";

			set1.SortOrder = SortOrder.Ascending;
			set2.SortOrder = SortOrder.Ascending;

			set1.AddCriterion(new Criterion("A1", null, ComparisonOperator.Equal));
			set2.AddCriterion(new Criterion("A1", null, ComparisonOperator.Equal));

			Assert.That(set1, Is.Not.EqualTo(set2));
			Assert.That(set2, Is.Not.EqualTo(set1));
		}

		[Test]
		public void Correctly_compares_equal_sets_based_on_sorting()
		{
			CriterionSet set1 = new CriterionSet();
			CriterionSet set2 = new CriterionSet();

			set1.OrderBy = "A1";
			set2.OrderBy = "A1";

			set1.SortOrder = SortOrder.Ascending;
			set2.SortOrder = SortOrder.Ascending;

			set1.AddCriterion(new Criterion("A1", null, ComparisonOperator.Equal));
			set2.AddCriterion(new Criterion("A1", null, ComparisonOperator.Equal));

			Assert.That(set1, Is.EqualTo(set2));
			Assert.That(set2, Is.EqualTo(set1));
		}

		[Test]
		public void Correctly_compares_equal_sets_one_having_null()
		{
			CriterionSet set1 = new CriterionSet();
			CriterionSet set2 = new CriterionSet();

			set1.AddCriterion(new Criterion("A1", "B1", ComparisonOperator.Equal));
			set2.AddCriterion(new Criterion("A1", null, ComparisonOperator.Equal));

			Assert.That(set1, Is.Not.EqualTo(set2));
			Assert.That(set2, Is.Not.EqualTo(set1));
		}

		[Test]
		public void Correctly_compares_complex_equal_sets()
		{
			CriterionSet set1 = new CriterionSet();
			CriterionSet set2 = new CriterionSet();

			string myString = "abc";

			set1.AddCriterion(new Criterion("A1", myString));
			set1.AddCriterion(new Criterion("A2", new DateTime(2007, 4, 15), ComparisonOperator.GreaterThan));

			set2.AddCriterion(new Criterion("A1", myString));
			set2.AddCriterion(new Criterion("A2", new DateTime(2007, 4, 15), ComparisonOperator.GreaterThan));

			Assert.That(set1, Is.EqualTo(set2));
			Assert.That(set2, Is.EqualTo(set1));
		}

		[Test]
		public void Correctly_compares_unequal_sets_with_same_number_of_elements_but_different_keys()
		{
			CriterionSet set1 = new CriterionSet();
			CriterionSet set2 = new CriterionSet();

			set1.AddCriterion(new Criterion("A1", "V1", ComparisonOperator.Equal));
			set1.AddCriterion(new Criterion("A2", "V2", ComparisonOperator.Equal));

			set2.AddCriterion(new Criterion("A1", "V1", ComparisonOperator.Equal));
			set2.AddCriterion(new Criterion("A3", "V2", ComparisonOperator.Equal));

			Assert.That(set1, Is.Not.EqualTo(set2));
			Assert.That(set2, Is.Not.EqualTo(set1));
		}

		[Test]
		public void Correctly_compares_unequal_sets_with_same_number_of_elements_but_different_values()
		{
			CriterionSet set1 = new CriterionSet();
			CriterionSet set2 = new CriterionSet();

			set1.AddCriterion(new Criterion("A1", "V1", ComparisonOperator.Equal));
			set1.AddCriterion(new Criterion("A2", "V2", ComparisonOperator.Equal));

			set2.AddCriterion(new Criterion("A1", "V1", ComparisonOperator.Equal));
			set2.AddCriterion(new Criterion("A2", "V3", ComparisonOperator.Equal));

			Assert.That(set1, Is.Not.EqualTo(set2));
			Assert.That(set2, Is.Not.EqualTo(set1));
		}

		[Test]
		public void Correctly_compares_unequal_sets_with_same_number_of_elements_but_different_number_of_values()
		{
			CriterionSet set1 = new CriterionSet();
			CriterionSet set2 = new CriterionSet();

			set1.AddCriterion(new Criterion("A1", "V1", ComparisonOperator.Equal));
			set1.AddCriterion(new Criterion("A2", "V2", ComparisonOperator.Equal));

			set2.AddCriterion(new Criterion("A1", "V1", ComparisonOperator.Equal));

			Assert.That(set1, Is.Not.EqualTo(set2));
			Assert.That(set2, Is.Not.EqualTo(set1));
		}

		[Test]
		public void Correctly_compares_empty_set_with_non_empty_set()
		{
			CriterionSet set1 = new CriterionSet();
			CriterionSet set2 = new CriterionSet();

			set2.AddCriterion(new Criterion("A1", "V1", ComparisonOperator.Equal));

			Assert.That(set1, Is.Not.EqualTo(set2));
			Assert.That(set2, Is.Not.EqualTo(set1));
		}

		[Test]
		public void Correctly_compares_equal_empty_sets()
		{
			CriterionSet set1 = new CriterionSet();

			CriterionSet set2 = new CriterionSet();

			Assert.That(set1, Is.EqualTo(set2));
			Assert.That(set2, Is.EqualTo(set1));
		}

		[Test]
		public void Does_not_add_null_valued_criteria()
		{
			Criterion nullCriterion = new Criterion("A1", null, ComparisonOperator.Equal);

			CriterionSet set1 = new CriterionSet();
			set1.AddCriterion(nullCriterion);

			Assert.That(set1.GetCriteria(), Is.EqualTo(new Criterion[] { nullCriterion }));
		}
	}
}