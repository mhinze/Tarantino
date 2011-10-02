using System.Collections.Generic;
using Tarantino.Core.Commons.Model.Enumerations;
using Tarantino.Core.Commons.Services.ListManagement.Impl;
using Tarantino.Core.Commons.Services.Repositories;

namespace Tarantino.Core.Commons.Services.Repositories
{
	public class CriterionSet
	{
		private List<Criterion> _criteria = new List<Criterion>();

		private string _orderBy;
		private SortOrder _sortOrder;

		public string OrderBy
		{
			get { return _orderBy; }
			set { _orderBy = value; }
		}

		public SortOrder SortOrder
		{
			get { return _sortOrder; }
			set { _sortOrder = value; }
		}

		public override bool Equals(object obj)
		{
			CriterionSet other = (CriterionSet)obj;
			IEnumerable<Criterion> criteria = GetCriteria();
			IEnumerable<Criterion> otherCriteria = other.GetCriteria();

			if (EnumerableHelper.Count(criteria) != EnumerableHelper.Count(otherCriteria))
			{
				return false;
			}
			else if (SortOrder != other.SortOrder || OrderBy != other.OrderBy)
			{
				return false;
			}
			else
			{
				bool criterionSetsEqual = true;

				foreach (Criterion criterion in criteria)
				{
					bool matchingCriterionFound = false;

					foreach (Criterion otherCriterion in otherCriteria)
					{
						object value1 = otherCriterion.Value;
						object value2 = criterion.Value;

						bool keyMatches = otherCriterion.Attribute == criterion.Attribute;
						bool valueMatches = (value1 == null && value2 == null) || (value1 != null && value1.Equals(value2));
						bool operatorMatches = otherCriterion.Operator == criterion.Operator;

						if (keyMatches && valueMatches && operatorMatches)
						{
							matchingCriterionFound = true;
							break;
						}
					}

					if (!matchingCriterionFound)
					{
						criterionSetsEqual = false;
						break;
					}
				}

				return criterionSetsEqual;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = 0;

			foreach (Criterion criterion in _criteria)
			{
				hashCode += criterion.GetHashCode();
			}

			return hashCode;
		}

		public Criterion AddCriterion(Criterion criterion)
		{
			_criteria.Add(criterion);
			return criterion;
		}

		public IEnumerable<Criterion> GetCriteria()
		{
			return _criteria;
		}
	}
}