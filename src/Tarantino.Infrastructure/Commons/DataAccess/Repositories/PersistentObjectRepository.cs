using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using Tarantino.Core.Commons.Model;
using Tarantino.Core.Commons.Model.Enumerations;
using Tarantino.Core.Commons.Services.Repositories;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;

namespace Tarantino.Infrastructure.Commons.DataAccess.Repositories
{
	public class PersistentObjectRepository : RepositoryBase, IPersistentObjectRepository
	{
		public PersistentObjectRepository(ISessionBuilder sessionBuilder) : base(sessionBuilder)
		{
		}

		public IEnumerable<T> GetAll<T>()
		{
			using (ISession session = GetSession())
			{
				var criteria = session.CreateCriteria(typeof(T));
				criteria.SetCacheable(true);
				var list = criteria.List<T>();
				return list;
			}
		}

		public T GetById<T>(Guid id) where T : PersistentObject
		{
			using (ISession session = GetSession())
			{
				T item = session.Load<T>(id);
				return item;
			}
		}

		public T GetByIdWithoutClosingSession<T>(Guid id) where T : PersistentObject
		{
			var item = GetSession().Load<T>(id);
			return item;
		}

		public void Delete(PersistentObject persistentObject)
		{
			using (ISession session = GetSession())
			{
				session.Delete(persistentObject);
				session.Flush();
			}
		}

		public void Save(PersistentObject persistentObject)
		{
			using (ISession session = GetSession())
			{
				session.SaveOrUpdate(persistentObject);
				session.Flush();
			}
		}

		public void Revert(PersistentObject persistentObject)
		{
			using (ISession session = GetSession())
			{
				session.Refresh(persistentObject);
			}
		}

		public IEnumerable<T> FindAll<T>(CriterionSet criterionSet)
		{
			using (ISession session = GetSession())
			{
				var persistentObjects = new List<T>();

				var criteria = session.CreateCriteria(typeof(T));
				criteria.SetCacheable(true);

				foreach (var criterion in criterionSet.GetCriteria())
				{
					ICriterion expression;

					if (criterion.Operator == ComparisonOperator.GreaterThan)
					{
						expression = Restrictions.Gt(criterion.Attribute, criterion.Value);
					}
					else if (criterion.Operator == ComparisonOperator.LessThan)
					{
						expression = Restrictions.Lt(criterion.Attribute, criterion.Value);
					}
					else if (criterion.Operator == ComparisonOperator.NotEqual)
					{
						if (criterion.Value == null)
						{
							expression = Restrictions.IsNotNull(criterion.Attribute);
						}
						else
						{
							expression = Restrictions.Not(Restrictions.Eq(criterion.Attribute, criterion.Value));
						}
					}
					else
					{
						if (criterion.Value == null)
						{
							expression = Restrictions.IsNull(criterion.Attribute);
						}
						else
						{
							expression = Restrictions.Eq(criterion.Attribute, criterion.Value);
						}
					}

					criteria.Add(expression);
				}

				if (criterionSet.OrderBy != null)
				{
					if (criterionSet.SortOrder == SortOrder.Descending)
					{
						criteria.AddOrder(Order.Desc(criterionSet.OrderBy));
					}
					else
					{
						criteria.AddOrder(Order.Asc(criterionSet.OrderBy));
					}
				}

				var list = criteria.List();
				foreach (T entity in list)
				{
					persistentObjects.Add(entity);
				}

				return persistentObjects;
			}
		}

		public T FindFirst<T>(CriterionSet criterionSet) where T : class
		{
			var persistentObjects = new List<T>(FindAll<T>(criterionSet));

			var persistentObject = (persistentObjects.Count > 0) ? persistentObjects[0] : null;

			return persistentObject;
		}
	}
}