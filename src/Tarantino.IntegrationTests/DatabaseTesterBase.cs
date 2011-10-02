using System.Collections.Generic;
using System.Reflection;
using NHibernate;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Tarantino.Core.Commons.Model;
using Tarantino.Infrastructure;
using Tarantino.Infrastructure.Commons.DataAccess.ORMapper;
using Tarantino.Infrastructure.Commons.DataAccess.Repositories;

namespace Tarantino.IntegrationTests
{
	public abstract class DatabaseTesterBase : RepositoryBase
	{
		protected DatabaseTesterBase() : base(new HybridSessionBuilder())
		{
		}

		[SetUp]
		public virtual void SetUp()
		{
			InfrastructureDependencyRegistrar.RegisterInfrastructure();
			ClearTables();
			SetupDatabase();
		}

		[TearDown]
		public virtual void Teardown()
		{
			HybridSessionBuilder.ResetSession(ConfigurationFile);
		}

		protected void AssertObjectCanBePersisted<T>(T persistentObject) where T : PersistentObject
		{
			using (ISession session = GetSession())
			{
				session.SaveOrUpdate(persistentObject);
				session.Flush();
			}

			using (ISession session = GetSession())
			{
				var reloadedObject = session.Load<T>(persistentObject.Id);
				Assert.That(reloadedObject, Is.EqualTo(persistentObject));
				Assert.That(reloadedObject, Is.Not.SameAs(persistentObject));
				AssertObjectsMatch(persistentObject, reloadedObject);
			}
		}

		protected static void AssertObjectsMatch(object obj1, object obj2)
		{
			Assert.AreNotSame(obj1, obj2);
			Assert.AreEqual(obj1, obj2);

			var infos = obj1.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
			foreach (var info in infos)
			{
				var value1 = info.GetValue(obj1, null);
				var value2 = info.GetValue(obj2, null);
				Assert.AreEqual(value1, value2, string.Format("Property {0} doesn't match", info.Name));
			}
		}

		protected void ClearTables()
		{
			var session = GetSession();

			foreach (var table in GetTablesToDelete())
			{
				var hql = string.Format("from {0}", table);
				session.Delete(hql);
			}

			session.Flush();

			HybridSessionBuilder.ResetSession(ConfigurationFile);
		}

		protected void Save(params PersistentObject[] objects)
		{
			var session = GetSession();

			foreach (var persistentObject in objects)
			{
				session.SaveOrUpdate(persistentObject);
			}

			session.Flush();
		}

		protected virtual void SetupDatabase()
		{
		}

		protected abstract IEnumerable<string> GetTablesToDelete();
	}
}