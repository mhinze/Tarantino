using System;
using Tarantino.Deployer.Core.Model;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Tarantino.Deployer.Core.Services.UI;
using Tarantino.Deployer.Core.Services.UI.Impl;
using Environment = Tarantino.Deployer.Core.Services.Configuration.Impl.Environment;

namespace Tarantino.Deployer.UnitTests.Core.Services.UI
{
	[TestFixture]
	public class LabelTextGeneratorTester
	{
		[Test]
		public void Generates_deployment_label_text()
		{
			Deployment deployment = new Deployment();
			Environment environment = new Environment();

			deployment.DeployedBy = "khurwitz";
			deployment.DeployedOn = new DateTime(2007, 4, 15, 8, 32, 45);
			environment.Predecessor = "QA";

			MockRepository mocks = new MockRepository();
			IDeploymentSelectionValidator validator = mocks.CreateMock<IDeploymentSelectionValidator>();

			using (mocks.Record())
			{
				Expect.Call(validator.IsValid("845", deployment)).Return(true);
			}

			using (mocks.Playback())
			{
				ILabelTextGenerator textGenerator = new LabelTextGenerator(validator);
				string text = textGenerator.GetDeploymentText(environment, "845", deployment);

				Assert.That(text, Is.EqualTo("QA on 4/15/2007 8:32 AM by khurwitz"));
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Returns_empty_deployment_string_for_invalid_deployment()
		{
			Environment environment = new Environment();
			environment.Predecessor = "QA";

			MockRepository mocks = new MockRepository();
			IDeploymentSelectionValidator validator = mocks.CreateMock<IDeploymentSelectionValidator>();

			using (mocks.Record())
			{
				Expect.Call(validator.IsValid("845", null)).Return(false);
			}

			using (mocks.Playback())
			{
				ILabelTextGenerator textGenerator = new LabelTextGenerator(validator);
				string text = textGenerator.GetDeploymentText(environment, "845", null);

				Assert.That(text, Is.EqualTo(string.Empty));
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Generates_certification_label_text()
		{
			Deployment deployment = new Deployment();

			deployment.CertifiedBy = "khurwitz";
			deployment.CertifiedOn = new DateTime(2007, 4, 15, 8, 32, 45);

			MockRepository mocks = new MockRepository();
			IDeploymentSelectionValidator validator = mocks.CreateMock<IDeploymentSelectionValidator>();

			using (mocks.Record())
			{
				Expect.Call(validator.IsValid("845", deployment)).Return(true);
			}

			using (mocks.Playback())
			{
				ILabelTextGenerator textGenerator = new LabelTextGenerator(validator);
				string text = textGenerator.GetCertificationText("845", deployment);

				Assert.That(text, Is.EqualTo("4/15/2007 8:32 AM by khurwitz"));
			}

			mocks.VerifyAll();
		}

		[Test]
		public void Returns_empty_certification_string_for_invalid_deployment()
		{
			MockRepository mocks = new MockRepository();
			IDeploymentSelectionValidator validator = mocks.CreateMock<IDeploymentSelectionValidator>();

			using (mocks.Record())
			{
				Expect.Call(validator.IsValid("845", null)).Return(false);
			}

			using (mocks.Playback())
			{
				ILabelTextGenerator textGenerator = new LabelTextGenerator(validator);
				string text = textGenerator.GetCertificationText("845", null);

				Assert.That(text, Is.EqualTo(string.Empty));
			}

			mocks.VerifyAll();
		}
	}
}