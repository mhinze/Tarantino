using System;
using Tarantino.Deployer.Core.Model;
using Tarantino.Deployer.Core.Services;
using Tarantino.Deployer.Core.Services.Impl;
using Tarantino.Core.Commons.Services.Environment;
using Tarantino.Core.Commons.Services.Security;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace Tarantino.Deployer.UnitTests.Core.Services
{
	[TestFixture]
	public class VersionCertifierTester
	{
		[Test]
		public void Certifies_deployment()
		{
			var deployment = new Deployment();

			var mocks = new MockRepository();
			var clock = mocks.CreateMock<ISystemClock>();
			var securityContext = mocks.CreateMock<ISecurityContext>();
			var repository = mocks.CreateMock<IDeploymentRepository>();

			using (mocks.Record())
			{
				Expect.Call(clock.GetCurrentDateTime()).Return(new DateTime(2007, 4, 15));
				Expect.Call(securityContext.GetCurrentUsername()).Return("khurwitz");
				repository.Save(deployment);
			}

			using (mocks.Playback())
			{
				IVersionCertifier certifier = new VersionCertifier(clock, securityContext, repository);
				certifier.Certify(deployment);
				
				Assert.That(deployment.CertifiedBy, Is.EqualTo("khurwitz"));
				Assert.That(deployment.CertifiedOn, Is.EqualTo(new DateTime(2007, 4, 15)));
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Does_not_certify_undefined_deployment()
		{
			IVersionCertifier certifier = new VersionCertifier(null, null, null);
			certifier.Certify(null);
		}
	}
}