using Tarantino.Core.Commons.Services.Caching;
using Tarantino.Core.Commons.Services.Caching.Impl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Tarantino.IntegrationTests.Core.Commons.Services.Caching
{
	[TestFixture]
	public class CacheManagerTester
	{
		[Test]
		public void Correctly_Caches_Object()
		{
			ICacheManager manager1 = new CacheManager();
			Assert.That(manager1.Has("my key"), Is.False);
			Assert.That(manager1.Get<string>("my key"), Is.Null);

			ICacheManager manager2 = new CacheManager();
			manager2.Set("my key", "my cached object");
			Assert.That(manager2.Has("my key"), Is.True);

			ICacheManager manager3 = new CacheManager();
			Assert.That(manager3.Get<string>("my key"), Is.EqualTo("my cached object"));
			Assert.That(manager3.Has("my key"), Is.True);

			ICacheManager manager4 = new CacheManager();
			manager4.Clear();

			ICacheManager manager5 = new CacheManager();
			Assert.That(manager3.Has("my key"), Is.False);
			Assert.That(manager5.Get<string>("my key"), Is.Null);
		}
	}
}