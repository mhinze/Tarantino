using System.Collections.Generic;
using Tarantino.Core.Commons.Model.Enumerations;
using Tarantino.Core.Commons.Services.ListManagement;
using Tarantino.Core.Commons.Services.ListManagement.Impl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Tarantino.IntegrationTests.Core.Commons.Services.ListManagement
{
	[TestFixture]
	public class EnumerationHelperTester
	{
		[Test]
		public void CorrectlyRetrievesAllEnumeratedValuesForAGivenEnumerationType()
		{
			IEnumerationHelper helper = new EnumerationHelper();
			IEnumerable<PersistenceMode> modes = helper.GetAll<PersistenceMode>();
			EnumerableAssert.That(modes, Is.EquivalentTo(new PersistenceMode[] { PersistenceMode.Live, PersistenceMode.Archive }));
		}

		[Test]
		public void CorrectlyDeterminesAbsoluteDifference()
		{
			IEnumerationHelper helper = new EnumerationHelper();
			Assert.That(helper.DetermineAbsoluteDifference(PersistenceMode.Archive, PersistenceMode.Live), Is.EqualTo(1));
		}
	}
}