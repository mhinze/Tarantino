using Tarantino.Core.Commons.Model.Enumerations;

namespace Tarantino.Core.Commons.Services.Repositories
{
	public class Criterion
	{
		private string _attribute;
		private object _value;
		private ComparisonOperator _operator;

		public Criterion()
		{
		}

		public Criterion(string attribute, object value, ComparisonOperator comparisonOperator)
		{
			_attribute = attribute;
			_value = value;
			_operator = comparisonOperator;
		}

		public Criterion(string attribute, object value) : this(attribute, value, ComparisonOperator.Equal)
		{
		}

		public string Attribute
		{
			get { return _attribute; }
			set { _attribute = value; }
		}

		public object Value
		{
			get { return _value; }
			set { _value = value; }
		}

		public ComparisonOperator Operator
		{
			get { return _operator; }
			set { _operator = value; }
		}

		public override bool Equals(object obj)
		{
			Criterion other = (Criterion)obj;

			bool isEqual = other.Attribute == Attribute && other.Value == Value;
			return isEqual;
		}

		public override int GetHashCode()
		{
			string combinedKey = Attribute + Value;
			return combinedKey.GetHashCode();
		}
	}
}