using System;
using System.Collections.Generic;
using Tarantino.Core.Commons.Services.ListManagement;
using Tarantino.Core.Commons.Services.ListManagement.Impl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Tarantino.UnitTests.Core.Commons.Services.ListManagement
{
	[TestFixture]
	public class EnumerableHelperTester
	{
		[Test]
		public void Returns_Correct_Count()
		{
			int count = EnumerableHelper.Count(new int[] {1, 5, 10});

			Assert.That(count, Is.EqualTo(3));
		}

		[Test]
		public void Returns_Correct_First_Value()
		{
			int first = EnumerableHelper.First(new int[] {1, 5, 10});

			Assert.That(first, Is.EqualTo(1));
		}

		[Test]
		public void Returns_Correct_Value_At_Index()
		{
			int second = EnumerableHelper.ValueAt(new int[] {1, 5, 10}, 1);

			Assert.That(second, Is.EqualTo(5));
		}

		[Test]
		public void Converts_To_Array()
		{
			IEnumerable<int> ints = new int[] {1, 5, 10};
			int[] integerArray = EnumerableHelper.ToArray(ints);

			EnumerableAssert.That(ints, Is.EquivalentTo(integerArray));
		}

		[Test, ExpectedException(ExpectedMessage = "Enumerable is empty", ExceptionType = typeof(ApplicationException))]
		public void Does_Not_Allow_Retrieval_Of_First_Item_From_Empty_Enumerable()
		{
			EnumerableHelper.First(new int[0]);
		}

		[Test, ExpectedException(ExpectedMessage = "Enumerable has no value at index 1", ExceptionType = typeof(ApplicationException))]
		public void Does_Not_Allow_Retrieval_Of_Value_At_Invalid_Index()
		{
			EnumerableHelper.ValueAt(new int[0], 1);
		}

		[Test]
		public void Determines_If_Enumerable_Is_Empty()
		{
			Assert.That(EnumerableHelper.IsEmpty(new int[0]), Is.True);
			Assert.That(EnumerableHelper.IsEmpty(new int[]{5}), Is.False);
		}

		[Test]
		public void Returns_Rich_List()
		{
			IRichList<int> richList = EnumerableHelper.ToRichList(new int[0]);
			Assert.That(richList, Is.TypeOf(typeof(RichList<int>)));
		}
	}
}