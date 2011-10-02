namespace Tarantino.Core.Commons.Model.Enumerations
{
	public class ComparisonOperator : Enumeration
	{
		public static readonly ComparisonOperator Equal = new ComparisonOperator(1, "Equal");
		public static readonly ComparisonOperator NotEqual = new ComparisonOperator(2, "NotEqual");
		public static readonly ComparisonOperator GreaterThan = new ComparisonOperator(3, "GreaterThan");
		public static readonly ComparisonOperator LessThan = new ComparisonOperator(4, "LessThan");

		public ComparisonOperator()
		{
		}

		public ComparisonOperator(int value, string displayName) : base(value, displayName)
		{
		}
	}
}