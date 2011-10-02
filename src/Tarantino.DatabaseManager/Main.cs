using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Tarantino.Core.Commons.Services.Configuration.Impl;
using Tarantino.DatabaseManager.Core;

namespace Tarantino.DatabaseManager
{
	public partial class Main : Form
	{
		public Main()
		{
			

			InitializeComponent();

			chkIntegratedSecurity.CheckedChanged += chkIntegratedSecurity_OnCheckedChanged;
			btnBrowse.Click += btnBrowse_OnClick;

			cboAction.Items.Add("Create");
			cboAction.Items.Add("Update");
			cboAction.Items.Add("Drop");
			cboAction.Items.Add("Rebuild");
			cboAction.SelectedIndex = 0;

			IConfigurationReader reader = new ConfigurationReader(new ApplicationConfiguration());

			var scriptFolder = reader.GetOptionalSetting("ScriptFolder") ?? string.Empty;
			var server = reader.GetOptionalSetting("Server") ?? string.Empty;
			var database = reader.GetOptionalSetting("Database") ?? string.Empty;
			var username = reader.GetOptionalSetting("Username") ?? string.Empty;
			var password = reader.GetOptionalSetting("Password") ?? string.Empty;
			var integratedSecurity = reader.GetOptionalBooleanSetting("IntegratedSecurity") ?? false;

			txtScriptFolder.Text = scriptFolder;
			txtServer.Text = server;
			txtDatabase.Text = database;
			txtUsername.Text = username;
			txtPassword.Text = password;
			chkIntegratedSecurity.Checked = integratedSecurity;

			updateAuthenticationFields();
		}

		private void btnBrowse_OnClick(object sender, EventArgs e)
		{
			var dialog = new FolderBrowserDialog
			             	{
			             		RootFolder = Environment.SpecialFolder.Desktop,
			             		SelectedPath = AppDomain.CurrentDomain.BaseDirectory,
			             		ShowNewFolderButton = false,
			             		Description =
			             			"Please select the database script folder that contains the 'Create' and 'Update' sub-folders"
			             	};
			var result = dialog.ShowDialog(this);

			if (result == DialogResult.OK)
			{
				txtScriptFolder.Text = dialog.SelectedPath;
			}
		}

		private void chkIntegratedSecurity_OnCheckedChanged(object sender, EventArgs e)
		{
			updateAuthenticationFields();
		}

		private void updateAuthenticationFields()
		{
			txtUsername.Enabled = !chkIntegratedSecurity.Checked;
			txtPassword.Enabled = !chkIntegratedSecurity.Checked;

			if (chkIntegratedSecurity.Checked)
			{
				txtUsername.Text = string.Empty;
				txtPassword.Text = string.Empty;
			}
		}

		private void addArgument(StringBuilder sb, string argName, string argValue)
		{
			sb.Append(" -D:");
			sb.Append(argName);
			sb.Append("=\"");
			sb.Append(argValue);
			sb.Append("\"");
			return;
		}

		private void RunCommandLine(string commandLine, string args)
		{
			var processForm = new ProcessProgressForm {ProcessCommand = commandLine};

			if (args != null)
				processForm.ProcessArguments = args;

			processForm.ProcessWorkingDir = AppDomain.CurrentDomain.BaseDirectory;
			processForm.StandardErrorColor = Color.Maroon;
			processForm.StandardOutColor = Color.Blue;
			processForm.Text = "Database process output";
			processForm.ErrorDialogMessage = "Error running database process";
			processForm.BeginProcess();
			processForm.ShowDialog(this);
		}

		private void btnExecute_Click(object sender, EventArgs e)
		{
			var arguments = new StringBuilder("-buildfile:databaseManagerTargets.build");
			addArgument(arguments, "database.script.directory", txtScriptFolder.Text);
			addArgument(arguments, "database.server", txtServer.Text);
			addArgument(arguments, "database.name", txtDatabase.Text);
			addArgument(arguments, "database.integrated", chkIntegratedSecurity.Checked.ToString().ToLower());
			addArgument(arguments, "database.username", txtUsername.Text);
			addArgument(arguments, "database.password", txtPassword.Text);
			addArgument(arguments, "action", cboAction.SelectedItem.ToString());

			IConfigurationReader reader = new ConfigurationReader(new ApplicationConfiguration());

			RunCommandLine(string.Format(@"{0}\nant.exe", reader.GetRequiredSetting("NAntFolder")), arguments.ToString());
		}
	}
}