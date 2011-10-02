using System;
using System.Collections.Generic;

namespace Tarantino.Core.Commons.Services.ListManagement.Impl
{
	public class EnumerableHelper
	{
		public static T First<T>(IEnumerable<T> enumerable)
		{
			if (IsEmpty(enumerable))
			{
				throw new ApplicationException("Enumerable is empty");
			}
			return ToRichList(enumerable)[0];
		}

		public static IRichList<T> ToRichList<T>(IEnumerable<T> enumerable)
		{
			return new RichList<T>(enumerable);
		}

		public static bool IsEmpty<T>(IEnumerable<T> enumerable)
		{
			return Count(enumerable) == 0;
		}

		public static int Count<T>(IEnumerable<T> enumerable)
		{
			return ToRichList(enumerable).Count;
		}

		public static T ValueAt<T>(IEnumerable<T> enumerable, int index)
		{
			if (Count(enumerable) < index + 1)
			{
				throw new ApplicationException(string.Format("Enumerable has no value at index {0}", index));
			}

			return ToRichList(enumerable)[index];
		}

		public static T[] ToArray<T>(IEnumerable<T> enumerable)
		{
			T[] array = new List<T>(enumerable).ToArray();
			return array;
		}
	}
}