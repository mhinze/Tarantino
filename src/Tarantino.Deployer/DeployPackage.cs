using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Tarantino.Deployer.Core.Model;
using Tarantino.Deployer.Core.Services;
using Tarantino.Deployer.Core.Services.Configuration;
using Tarantino.Deployer.Core.Services.UI;
using StructureMap;
using Tarantino.Deployer.Infrastructure;
using Application = Tarantino.Deployer.Core.Services.Configuration.Impl.Application;
using Environment = Tarantino.Deployer.Core.Services.Configuration.Impl.Environment;

namespace Tarantino.Deployer
{
	public partial class DeployPackage : Form
	{
		private string _version;

		public DeployPackage()
		{
			_version = null;
			DeployerInfrastructureDependencyRegistrar.RegisterInfrastructure();

			InitializeComponent();
			wireEvents();
			populateApplicationDropdown();
		}

		private void populateVersions()
		{
			var repository = ObjectFactory.GetInstance<IDeploymentRepository>();

			var certified = repository.FindCertified(SelectedApplication.Name, SelectedEnvironment.Predecessor);
			var uncertified = repository.FindSuccessfulUncertified(SelectedApplication.Name, SelectedEnvironment.Name);

			populateVersionDropdown(certified, cboVersion);
			populateVersionDropdown(uncertified, cboCertifyVersion);

			populateDeploymentGrid(repository);
		}

		private void populateDeploymentGrid(IDeploymentRepository repository)
		{
			var rowFactory = ObjectFactory.GetInstance<IDeploymentRowFactory>();

			grdDeployments.Rows.Clear();

			var deployments = repository.Find(SelectedApplication.Name, SelectedEnvironment.Name);

			var rowNumber = 0;
			foreach (var deployment in deployments)
			{
				string[] deploymentRow = rowFactory.ConstructRow(deployment);
				grdDeployments.Rows.Add(deploymentRow);

				if (deployment.Result == DeploymentResult.Failure)
				{
					grdDeployments.Rows[rowNumber].DefaultCellStyle.BackColor = Color.Pink;
				}

				rowNumber++;
			}
		}

		private void btnCertify_Click(object sender, EventArgs e)
		{
			var selectedVersion = cboCertifyVersion.SelectedItem as Deployment;

			var certifier = ObjectFactory.GetInstance<IVersionCertifier>();
			certifier.Certify(selectedVersion);

			if (selectedVersion != null)
			{
				populateVersions();
			}
			else
			{
				MessageBox.Show("Please select a version!");
			}
		}

		private void btnDeploy_OnClick(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;

			var result = PackageDownloader.DownloadAndExtract(SelectedApplication.Name, SelectedEnvironment.Name, cboVersion.Text,
			                                                              SelectedApplication.Url, SelectedApplication.ZipFile, txtUsername.Text,
			                                                              txtPassword.Text);

			_version = result.Version;

			Cursor = Cursors.Arrow;

			RunCommandLine(result.Executable, result.WorkingDirectory);
		}

		private void grdDeployments_OnDoubleClick(object sender, EventArgs e)
		{
			var selectedRows = grdDeployments.SelectedRows;

			if (selectedRows.Count == 1)
			{
				var repository = ObjectFactory.GetInstance<IDeploymentRepository>();

				var deploymentId = new Guid(selectedRows[0].Cells[6].FormattedValue.ToString());
				var deployment = repository.GetById(deploymentId);
				var outputText = deployment.Output.Output;
				var outputWindow = new DeploymentOutput { Output = outputText};
				outputWindow.ShowDialog();
			}
		}

		private void RunCommandLine(string executable, string workingDirectory, string arguments = null)
		{
			var processForm = new ProcessProgressForm {ProcessCompleted = processCompleted};

			processForm.ProcessCommand = executable;

			if (arguments != null)
			{
				processForm.ProcessArguments = arguments;
			}

			processForm.ProcessWorkingDir = workingDirectory;
			processForm.BeginProcess();
			processForm.ShowDialog(this);
		}

		private void processCompleted(string output, bool failed)
		{
			var recorder = ObjectFactory.GetInstance<IDeploymentRecorder>();
			recorder.RecordDeployment(SelectedApplication.Name, SelectedEnvironment.Name, output, _version, failed);

			populateVersions();
		}

		private static void populateVersionDropdown(IEnumerable<Deployment> deployments, ComboBox combo)
		{
			combo.Text = string.Empty;
			combo.DataSource = deployments;
		}

		private void populateEnvironments()
		{
			cboEnvironment.Items.Clear();

			var environments = SelectedApplication.Environments;

			if (environments.Count > 0)
			{
				foreach (Environment environment in environments)
				{
					cboEnvironment.Items.Add(environment);
				}

				cboEnvironment.SelectedIndex = 0;
			}
		}

		private Application SelectedApplication
		{
			get { return (Application)cboApplication.SelectedItem; }
		}

		private Environment SelectedEnvironment
		{
			get { return (Environment)cboEnvironment.SelectedItem; }
		}

		private void populateApplicationDropdown()
		{
			var repository = ObjectFactory.GetInstance<IApplicationRepository>();

			foreach (Application application in repository.GetAll())
			{
				cboApplication.Items.Add(application);
			}

			cboApplication.SelectedIndex = 0;
		}

		private void CboVersionOnTextChanged(object sender, EventArgs e)
		{
			var deployment = cboVersion.SelectedItem as Deployment;
			var version = cboVersion.Text;

			var generator = ObjectFactory.GetInstance<ILabelTextGenerator>();
			lblDeployed.Text = generator.GetDeploymentText(SelectedEnvironment, version, deployment);
			lblCertified.Text = generator.GetCertificationText(version, deployment);
		}

		private void cboApplication_OnSelectedIndexChanged(object sender, EventArgs e)
		{
			txtUsername.Text = SelectedApplication.Username;
			txtPassword.Text = SelectedApplication.Password;

			populateEnvironments();
		}

		private void cboEnvironment_OnSelectedIndexChanged(object sender, EventArgs e)
		{
			populateVersions();
		}

		private void wireEvents()
		{
			cboApplication.SelectedIndexChanged += cboApplication_OnSelectedIndexChanged;
			cboEnvironment.SelectedIndexChanged += cboEnvironment_OnSelectedIndexChanged;
			cboVersion.TextChanged += CboVersionOnTextChanged;
			btnDeploy.Click += btnDeploy_OnClick;
			grdDeployments.DoubleClick += grdDeployments_OnDoubleClick;
		}
	}
}