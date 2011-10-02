using Tarantino.Core.Commons.Services.ListManagement;
using Tarantino.Core.Commons.Services.ListManagement.Impl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Tarantino.UnitTests.Core.Commons.Services.ListManagement
{
	[TestFixture]
	public class RichListTester
	{
		[Test]
		public void Correctly_Converts_Items()
		{
			int[] ints = new int[]{5, 10};
			long[] longs = new long[] { 5, 10 };

			IRichList<int> list = new RichList<int>(ints);

			IRichList<long> longList = list.ConvertAll<long>(
				delegate(int input)
					{
						return (long) input;
					});

			Assert.That(longList, Is.EqualTo(longs));

			Assert.That(longList[0], Is.TypeOf(typeof(long)));
			Assert.That(longList[1], Is.TypeOf(typeof(long)));
		}

		[Test]
		public void Correctly_Gets_Range()
		{
			int[] ints = new int[]{5, 10, 20};

			IRichList<int> list = new RichList<int>(ints);

			IRichList<int> range = list.GetRange(1, 2);

			Assert.That(range, Is.EqualTo(new RichList<int>(new int[]{10, 20})));
		}

		[Test]
		public void Correctly_Finds_All()
		{
			IRichList<int> ints = new RichList<int>(new int[]{5, 10, 20});

			IRichList<int> smallInts = ints.FindAll(delegate(int i) { return i < 11; });

			Assert.That(smallInts, Is.EqualTo(new RichList<int>(new int[]{5, 10})));
		}

		[Test]
		public void Constructors_Work()
		{
			IRichList<int> list1 = new RichList<int>();
			Assert.That(list1, Is.TypeOf(typeof(RichList<int>)));

			IRichList<int> list2 = new RichList<int>(5);
			Assert.That(list2.Capacity, Is.EqualTo(5));
		}
	}
}