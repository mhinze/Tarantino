using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Tarantino.Core.Commons.Services.Repositories;
using Tarantino.Core.WebManagement.Model;
using Tarantino.Core.WebManagement.Services.Repositories;
using Tarantino.Infrastructure.WebManagement.DataAccess.Repositories;

namespace Tarantino.UnitTests.Infrastructure.WebManagement.DataAccess.Repositories
{
	[TestFixture]
	public class ApplicationInstanceRepositoryTester
	{
		[Test]
		public void Can_correctly_gets_all_application_instances()
		{
			var instance1 = new ApplicationInstance();
			var instance2 = new ApplicationInstance();
			var instances = new[] { instance1, instance2 };

			var mocks = new MockRepository();
			var objectRepository = mocks.CreateMock<IPersistentObjectRepository>();

			using (mocks.Record())
			{
				objectRepository.ConfigurationFile = "webmanagement.hibernate.cfg.xml";
				Expect.Call(objectRepository.GetAll<ApplicationInstance>()).Return(instances);
			}

			using (mocks.Playback())
			{
				IApplicationInstanceRepository repository = new ApplicationInstanceRepository(objectRepository);
				IEnumerable<ApplicationInstance> actualInstances = repository.GetAll();
				
				EnumerableAssert.That(actualInstances, Is.EqualTo(instances));
			}
		}

		[Test]
		public void Can_correctly_gets_single_application_instance()
		{
			var id = Guid.NewGuid();
			var instance = new ApplicationInstance();

			var mocks = new MockRepository();
			var objectRepository = mocks.CreateMock<IPersistentObjectRepository>();

			using (mocks.Record())
			{
				objectRepository.ConfigurationFile = "webmanagement.hibernate.cfg.xml";
				Expect.Call(objectRepository.GetById<ApplicationInstance>(id)).Return(instance);
			}

			using (mocks.Playback())
			{
				IApplicationInstanceRepository repository = new ApplicationInstanceRepository(objectRepository);
				var actualInstance = repository.GetById(id);

				Assert.That(actualInstance, Is.SameAs(instance));
			}
		}

		[Test]
		public void Can_correctly_save_application_instance()
		{
			var instance = new ApplicationInstance();

			var mocks = new MockRepository();
			var objectRepository = mocks.CreateMock<IPersistentObjectRepository>();

			using (mocks.Record())
			{
				objectRepository.ConfigurationFile = "webmanagement.hibernate.cfg.xml";
				objectRepository.Save(instance);
			}

			using (mocks.Playback())
			{
				IApplicationInstanceRepository repository = new ApplicationInstanceRepository(objectRepository);
				repository.Save(instance);
			}
		}

		[Test]
		public void Can_correctly_delete_application_instance()
		{
			var instance = new ApplicationInstance();

			var mocks = new MockRepository();
			var objectRepository = mocks.CreateMock<IPersistentObjectRepository>();

			using (mocks.Record())
			{
				objectRepository.ConfigurationFile = "webmanagement.hibernate.cfg.xml";
				objectRepository.Delete(instance);
			}

			using (mocks.Playback())
			{
				IApplicationInstanceRepository repository = new ApplicationInstanceRepository(objectRepository);
				repository.Delete(instance);
			}
		}

		[Test]
		public void Can_correctly_gets_application_instances_by_machine_name_and_maintenance_host_header()
		{
			var instance = new ApplicationInstance();

			var criteria = new CriterionSet();
			criteria.AddCriterion(new Criterion("MachineName", "MyMachine"));
			criteria.AddCriterion(new Criterion("MaintenanceHostHeader", "MyDomain"));

			var mocks = new MockRepository();
			var objectRepository = mocks.CreateMock<IPersistentObjectRepository>();

			using (mocks.Record())
			{
				objectRepository.ConfigurationFile = "webmanagement.hibernate.cfg.xml";
				Expect.Call(objectRepository.FindFirst<ApplicationInstance>(criteria)).Return(instance);
			}

			using (mocks.Playback())
			{
				IApplicationInstanceRepository repository = new ApplicationInstanceRepository(objectRepository);
				var actualInstance = repository.GetByMaintenanceHostHeaderAndMachineName("MyDomain", "MyMachine");

				Assert.That(actualInstance, Is.SameAs(instance));
			}
		}

		[Test]
		public void Can_correctly_gets_application_instances_by_host_header()
		{
			var instance1 = new ApplicationInstance();
			var instance2 = new ApplicationInstance();
			var instances = new[] { instance1, instance2 };

			var criteria = new CriterionSet();
			criteria.AddCriterion(new Criterion("UniqueHostHeader", "MyHostHeader"));

			var mocks = new MockRepository();
			var objectRepository = mocks.CreateMock<IPersistentObjectRepository>();

			using (mocks.Record())
			{
				objectRepository.ConfigurationFile = "webmanagement.hibernate.cfg.xml";
				Expect.Call(objectRepository.FindAll<ApplicationInstance>(criteria)).Return(instances);
			}

			using (mocks.Playback())
			{
				IApplicationInstanceRepository repository = new ApplicationInstanceRepository(objectRepository);
				IEnumerable<ApplicationInstance> actualInstances = repository.GetByHostHeader("MyHostHeader");

				EnumerableAssert.That(actualInstances, Is.EqualTo(instances));
			}
		}
	}
}