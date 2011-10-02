using System;
using System.Collections.Generic;

namespace Tarantino.Core.Commons.Services.ListManagement.Impl
{
	public class RichList<T> : List<T>, IRichList<T>
	{
		public RichList()
		{
		}

		public RichList(IEnumerable<T> collection) : base(collection)
		{
		}

		public RichList(int capacity) : base(capacity)
		{
		}

		public new IRichList<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter)
		{
			return new RichList<TOutput>(base.ConvertAll(converter));
		}

		public new IRichList<T> FindAll(Predicate<T> match)
		{
			return new RichList<T>(base.FindAll(match));
		}

		public new IRichList<T> GetRange(int index, int count)
		{
			return new RichList<T>(base.GetRange(index, count));
		}
	}
}