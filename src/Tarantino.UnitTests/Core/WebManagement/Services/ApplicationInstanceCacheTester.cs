using System;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Tarantino.Core.Commons.Services.Environment;
using Tarantino.Core.Commons.Services.Web;
using Tarantino.Core.WebManagement.Model;
using Tarantino.Core.WebManagement.Services;
using Tarantino.Core.WebManagement.Services.Impl;

namespace Tarantino.UnitTests.Core.WebManagement.Services
{
	[TestFixture]
	public class ApplicationInstanceCacheTester
	{
		[Test]
		public void Correctly_retrieves_application_instance_from_first_level_cache()
		{
			ApplicationInstance instance = new ApplicationInstance();

			MockRepository mocks = new MockRepository();
			IWebContext webContext = mocks.CreateMock<IWebContext>();

			using (mocks.Record())
			{
				Expect.Call(webContext.GetItem<ApplicationInstance>(ApplicationInstance.CacheKey)).Return(instance);
			}

			using (mocks.Playback())
			{
				IApplicationInstanceCache cache = new ApplicationInstanceCache(webContext, null);
				Assert.That(cache.GetCurrent(), Is.SameAs(instance));
			}
		}

		[Test]
		public void Correctly_retrieves_application_instance_from_second_level_cache()
		{
			ApplicationInstance instance = new ApplicationInstance();

			MockRepository mocks = new MockRepository();
			IWebContext webContext = mocks.CreateMock<IWebContext>();

			using (mocks.Record())
			{
				Expect.Call(webContext.GetItem<ApplicationInstance>(ApplicationInstance.CacheKey)).Return(null);
				Expect.Call(webContext.GetCacheItem<ApplicationInstance>(ApplicationInstance.CacheKey)).Return(instance);
			}

			using (mocks.Playback())
			{
				IApplicationInstanceCache cache = new ApplicationInstanceCache(webContext, null);
				Assert.That(cache.GetCurrent(), Is.SameAs(instance));
			}
		}

		[Test]
		public void Correctly_returns_null_when_cached_instance_is_not_found()
		{
			MockRepository mocks = new MockRepository();
			IWebContext webContext = mocks.CreateMock<IWebContext>();

			using (mocks.Record())
			{
				Expect.Call(webContext.GetItem<ApplicationInstance>(ApplicationInstance.CacheKey)).Return(null);
				Expect.Call(webContext.GetCacheItem<ApplicationInstance>(ApplicationInstance.CacheKey)).Return(null);
			}

			using (mocks.Playback())
			{
				IApplicationInstanceCache cache = new ApplicationInstanceCache(webContext, null);
				Assert.That(cache.GetCurrent(), Is.Null);
			}
		}

		[Test]
		public void Correctly_caches_application_instance()
		{
			ApplicationInstance instance = new ApplicationInstance();

			MockRepository mocks = new MockRepository();
			IWebContext context = mocks.CreateMock<IWebContext>();
			ISystemClock clock = mocks.CreateMock<ISystemClock>();

			using (mocks.Record())
			{
				Expect.Call(clock.GetCurrentDateTime()).Return(new DateTime(2007, 4, 15));
				context.SetItem(ApplicationInstance.CacheKey, instance);
				context.SetCacheItem(ApplicationInstance.CacheKey, instance, new DateTime(2007, 4, 15).AddMinutes(1), TimeSpan.Zero);
			}

			using (mocks.Playback())
			{
				IApplicationInstanceCache cache = new ApplicationInstanceCache(context, clock);
				cache.Set(ApplicationInstance.CacheKey, instance);
			}
		}
	}
}