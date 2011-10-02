namespace Tarantino.Deployer
{
	partial class DeployPackage
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.btnDeploy = new System.Windows.Forms.Button();
			this.lblApplication = new System.Windows.Forms.Label();
			this.cboApplication = new System.Windows.Forms.ComboBox();
			this.cboEnvironment = new System.Windows.Forms.ComboBox();
			this.lblEnvironment = new System.Windows.Forms.Label();
			this.lblVersion = new System.Windows.Forms.Label();
			this.cboVersion = new System.Windows.Forms.ComboBox();
			this.lblDeployedLabel = new System.Windows.Forms.Label();
			this.lblDeployed = new System.Windows.Forms.Label();
			this.tabMenu = new System.Windows.Forms.TabControl();
			this.tabDeploy = new System.Windows.Forms.TabPage();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.txtUsername = new System.Windows.Forms.TextBox();
			this.lblPassword = new System.Windows.Forms.Label();
			this.lblUsername = new System.Windows.Forms.Label();
			this.lblCertified = new System.Windows.Forms.Label();
			this.lblCertifiedLabel = new System.Windows.Forms.Label();
			this.tabCertify = new System.Windows.Forms.TabPage();
			this.cboCertifyVersion = new System.Windows.Forms.ComboBox();
			this.btnCertify = new System.Windows.Forms.Button();
			this.lblCertifyVersion = new System.Windows.Forms.Label();
			this.tabCheckVersion = new System.Windows.Forms.TabPage();
			this.grdDeployments = new System.Windows.Forms.DataGridView();
			this.Version = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.DeployedOn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.DeployedBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Result = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.CertifiedOn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.CertifiedBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Output = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.bndDeployments = new System.Windows.Forms.BindingSource(this.components);
			this.tabMenu.SuspendLayout();
			this.tabDeploy.SuspendLayout();
			this.tabCertify.SuspendLayout();
			this.tabCheckVersion.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdDeployments)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bndDeployments)).BeginInit();
			this.SuspendLayout();
			// 
			// btnDeploy
			// 
			this.btnDeploy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDeploy.Location = new System.Drawing.Point(451, 143);
			this.btnDeploy.Name = "btnDeploy";
			this.btnDeploy.Size = new System.Drawing.Size(75, 23);
			this.btnDeploy.TabIndex = 19;
			this.btnDeploy.Text = "&Deploy";
			this.btnDeploy.UseVisualStyleBackColor = true;
			// 
			// lblApplication
			// 
			this.lblApplication.AutoSize = true;
			this.lblApplication.Location = new System.Drawing.Point(22, 15);
			this.lblApplication.Name = "lblApplication";
			this.lblApplication.Size = new System.Drawing.Size(62, 13);
			this.lblApplication.TabIndex = 2;
			this.lblApplication.Text = "Application:";
			// 
			// cboApplication
			// 
			this.cboApplication.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.cboApplication.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboApplication.FormattingEnabled = true;
			this.cboApplication.Location = new System.Drawing.Point(90, 12);
			this.cboApplication.Name = "cboApplication";
			this.cboApplication.Size = new System.Drawing.Size(462, 21);
			this.cboApplication.TabIndex = 3;
			// 
			// cboEnvironment
			// 
			this.cboEnvironment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.cboEnvironment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboEnvironment.FormattingEnabled = true;
			this.cboEnvironment.Location = new System.Drawing.Point(90, 39);
			this.cboEnvironment.Name = "cboEnvironment";
			this.cboEnvironment.Size = new System.Drawing.Size(462, 21);
			this.cboEnvironment.TabIndex = 5;
			// 
			// lblEnvironment
			// 
			this.lblEnvironment.AutoSize = true;
			this.lblEnvironment.Location = new System.Drawing.Point(15, 42);
			this.lblEnvironment.Name = "lblEnvironment";
			this.lblEnvironment.Size = new System.Drawing.Size(69, 13);
			this.lblEnvironment.TabIndex = 4;
			this.lblEnvironment.Text = "Environment:";
			// 
			// lblVersion
			// 
			this.lblVersion.AutoSize = true;
			this.lblVersion.Location = new System.Drawing.Point(17, 66);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(51, 13);
			this.lblVersion.TabIndex = 6;
			this.lblVersion.Text = "Version:";
			// 
			// cboVersion
			// 
			this.cboVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.cboVersion.FormattingEnabled = true;
			this.cboVersion.Location = new System.Drawing.Point(74, 63);
			this.cboVersion.Name = "cboVersion";
			this.cboVersion.Size = new System.Drawing.Size(452, 21);
			this.cboVersion.TabIndex = 18;
			// 
			// lblDeployedLabel
			// 
			this.lblDeployedLabel.AutoSize = true;
			this.lblDeployedLabel.Location = new System.Drawing.Point(13, 91);
			this.lblDeployedLabel.Name = "lblDeployedLabel";
			this.lblDeployedLabel.Size = new System.Drawing.Size(55, 13);
			this.lblDeployedLabel.TabIndex = 10;
			this.lblDeployedLabel.Text = "Deployed:";
			// 
			// lblDeployed
			// 
			this.lblDeployed.AutoSize = true;
			this.lblDeployed.Location = new System.Drawing.Point(74, 91);
			this.lblDeployed.Name = "lblDeployed";
			this.lblDeployed.Size = new System.Drawing.Size(0, 13);
			this.lblDeployed.TabIndex = 11;
			// 
			// tabMenu
			// 
			this.tabMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
									| System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tabMenu.Controls.Add(this.tabDeploy);
			this.tabMenu.Controls.Add(this.tabCertify);
			this.tabMenu.Controls.Add(this.tabCheckVersion);
			this.tabMenu.Location = new System.Drawing.Point(12, 76);
			this.tabMenu.Name = "tabMenu";
			this.tabMenu.SelectedIndex = 0;
			this.tabMenu.Size = new System.Drawing.Size(540, 198);
			this.tabMenu.TabIndex = 12;
			// 
			// tabDeploy
			// 
			this.tabDeploy.Controls.Add(this.txtPassword);
			this.tabDeploy.Controls.Add(this.txtUsername);
			this.tabDeploy.Controls.Add(this.lblPassword);
			this.tabDeploy.Controls.Add(this.lblUsername);
			this.tabDeploy.Controls.Add(this.lblCertified);
			this.tabDeploy.Controls.Add(this.lblCertifiedLabel);
			this.tabDeploy.Controls.Add(this.cboVersion);
			this.tabDeploy.Controls.Add(this.btnDeploy);
			this.tabDeploy.Controls.Add(this.lblDeployed);
			this.tabDeploy.Controls.Add(this.lblDeployedLabel);
			this.tabDeploy.Controls.Add(this.lblVersion);
			this.tabDeploy.Location = new System.Drawing.Point(4, 22);
			this.tabDeploy.Name = "tabDeploy";
			this.tabDeploy.Padding = new System.Windows.Forms.Padding(3);
			this.tabDeploy.Size = new System.Drawing.Size(532, 172);
			this.tabDeploy.TabIndex = 0;
			this.tabDeploy.Text = "Deploy";
			this.tabDeploy.UseVisualStyleBackColor = true;
			// 
			// txtPassword
			// 
			this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtPassword.Location = new System.Drawing.Point(74, 37);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(452, 20);
			this.txtPassword.TabIndex = 17;
			// 
			// txtUsername
			// 
			this.txtUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtUsername.Location = new System.Drawing.Point(74, 11);
			this.txtUsername.Name = "txtUsername";
			this.txtUsername.Size = new System.Drawing.Size(452, 20);
			this.txtUsername.TabIndex = 16;
			// 
			// lblPassword
			// 
			this.lblPassword.AutoSize = true;
			this.lblPassword.Location = new System.Drawing.Point(12, 40);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size(56, 13);
			this.lblPassword.TabIndex = 15;
			this.lblPassword.Text = "Password:";
			// 
			// lblUsername
			// 
			this.lblUsername.AutoSize = true;
			this.lblUsername.Location = new System.Drawing.Point(10, 14);
			this.lblUsername.Name = "lblUsername";
			this.lblUsername.Size = new System.Drawing.Size(58, 13);
			this.lblUsername.TabIndex = 14;
			this.lblUsername.Text = "Username:";
			// 
			// lblCertified
			// 
			this.lblCertified.AutoSize = true;
			this.lblCertified.Location = new System.Drawing.Point(74, 117);
			this.lblCertified.Name = "lblCertified";
			this.lblCertified.Size = new System.Drawing.Size(0, 13);
			this.lblCertified.TabIndex = 13;
			// 
			// lblCertifiedLabel
			// 
			this.lblCertifiedLabel.AutoSize = true;
			this.lblCertifiedLabel.Location = new System.Drawing.Point(20, 117);
			this.lblCertifiedLabel.Name = "lblCertifiedLabel";
			this.lblCertifiedLabel.Size = new System.Drawing.Size(48, 13);
			this.lblCertifiedLabel.TabIndex = 12;
			this.lblCertifiedLabel.Text = "Certified:";
			// 
			// tabCertify
			// 
			this.tabCertify.Controls.Add(this.cboCertifyVersion);
			this.tabCertify.Controls.Add(this.btnCertify);
			this.tabCertify.Controls.Add(this.lblCertifyVersion);
			this.tabCertify.Location = new System.Drawing.Point(4, 22);
			this.tabCertify.Name = "tabCertify";
			this.tabCertify.Padding = new System.Windows.Forms.Padding(3);
			this.tabCertify.Size = new System.Drawing.Size(532, 172);
			this.tabCertify.TabIndex = 1;
			this.tabCertify.Text = "Certify";
			this.tabCertify.UseVisualStyleBackColor = true;
			// 
			// cboCertifyVersion
			// 
			this.cboCertifyVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.cboCertifyVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboCertifyVersion.FormattingEnabled = true;
			this.cboCertifyVersion.Location = new System.Drawing.Point(74, 13);
			this.cboCertifyVersion.Name = "cboCertifyVersion";
			this.cboCertifyVersion.Size = new System.Drawing.Size(452, 21);
			this.cboCertifyVersion.TabIndex = 18;
			// 
			// btnCertify
			// 
			this.btnCertify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCertify.Location = new System.Drawing.Point(451, 134);
			this.btnCertify.Name = "btnCertify";
			this.btnCertify.Size = new System.Drawing.Size(75, 23);
			this.btnCertify.TabIndex = 12;
			this.btnCertify.Text = "&Certify";
			this.btnCertify.UseVisualStyleBackColor = true;
			this.btnCertify.Click += new System.EventHandler(this.btnCertify_Click);
			// 
			// lblCertifyVersion
			// 
			this.lblCertifyVersion.AutoSize = true;
			this.lblCertifyVersion.Location = new System.Drawing.Point(17, 16);
			this.lblCertifyVersion.Name = "lblCertifyVersion";
			this.lblCertifyVersion.Size = new System.Drawing.Size(51, 13);
			this.lblCertifyVersion.TabIndex = 17;
			this.lblCertifyVersion.Text = "Version:";
			// 
			// tabCheckVersion
			// 
			this.tabCheckVersion.Controls.Add(this.grdDeployments);
			this.tabCheckVersion.Location = new System.Drawing.Point(4, 22);
			this.tabCheckVersion.Name = "tabCheckVersion";
			this.tabCheckVersion.Padding = new System.Windows.Forms.Padding(3);
			this.tabCheckVersion.Size = new System.Drawing.Size(532, 172);
			this.tabCheckVersion.TabIndex = 2;
			this.tabCheckVersion.Text = "Check Version";
			this.tabCheckVersion.UseVisualStyleBackColor = true;
			// 
			// grdDeployments
			// 
			this.grdDeployments.AllowUserToAddRows = false;
			this.grdDeployments.AllowUserToDeleteRows = false;
			this.grdDeployments.AllowUserToResizeColumns = false;
			this.grdDeployments.AllowUserToResizeRows = false;
			this.grdDeployments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.grdDeployments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.grdDeployments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Version,
            this.DeployedOn,
            this.DeployedBy,
            this.Result,
            this.CertifiedOn,
            this.CertifiedBy,
            this.Output});
			this.grdDeployments.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdDeployments.Location = new System.Drawing.Point(3, 3);
			this.grdDeployments.MultiSelect = false;
			this.grdDeployments.Name = "grdDeployments";
			this.grdDeployments.RowHeadersVisible = false;
			this.grdDeployments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.grdDeployments.Size = new System.Drawing.Size(526, 184);
			this.grdDeployments.TabIndex = 0;
			// 
			// Version
			// 
			this.Version.HeaderText = "Version";
			this.Version.Name = "Version";
			this.Version.ReadOnly = true;
			this.Version.Width = 73;
			// 
			// DeployedOn
			// 
			this.DeployedOn.HeaderText = "Deployed";
			this.DeployedOn.Name = "DeployedOn";
			this.DeployedOn.ReadOnly = true;
			this.DeployedOn.Width = 77;
			// 
			// DeployedBy
			// 
			this.DeployedBy.HeaderText = "Deployed By";
			this.DeployedBy.Name = "DeployedBy";
			this.DeployedBy.ReadOnly = true;
			this.DeployedBy.Width = 92;
			// 
			// Result
			// 
			this.Result.HeaderText = "Result";
			this.Result.Name = "Result";
			this.Result.Width = 62;
			// 
			// CertifiedOn
			// 
			this.CertifiedOn.HeaderText = "Certified";
			this.CertifiedOn.Name = "CertifiedOn";
			this.CertifiedOn.ReadOnly = true;
			this.CertifiedOn.Width = 70;
			// 
			// CertifiedBy
			// 
			this.CertifiedBy.HeaderText = "Certified By";
			this.CertifiedBy.Name = "CertifiedBy";
			this.CertifiedBy.ReadOnly = true;
			this.CertifiedBy.Width = 85;
			// 
			// Output
			// 
			this.Output.HeaderText = "Output";
			this.Output.Name = "Output";
			this.Output.Visible = false;
			this.Output.Width = 64;
			// 
			// DeployPackage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(568, 285);
			this.Controls.Add(this.tabMenu);
			this.Controls.Add(this.cboApplication);
			this.Controls.Add(this.cboEnvironment);
			this.Controls.Add(this.lblApplication);
			this.Controls.Add(this.lblEnvironment);
			this.MinimumSize = new System.Drawing.Size(584, 321);
			this.Name = "DeployPackage";
			this.Text = "Deploy Package";
			this.tabMenu.ResumeLayout(false);
			this.tabDeploy.ResumeLayout(false);
			this.tabDeploy.PerformLayout();
			this.tabCertify.ResumeLayout(false);
			this.tabCertify.PerformLayout();
			this.tabCheckVersion.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grdDeployments)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bndDeployments)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnDeploy;
		private System.Windows.Forms.Label lblApplication;
		private System.Windows.Forms.ComboBox cboApplication;
		private System.Windows.Forms.ComboBox cboEnvironment;
		private System.Windows.Forms.Label lblEnvironment;
		private System.Windows.Forms.Label lblVersion;
		private System.Windows.Forms.ComboBox cboVersion;
		private System.Windows.Forms.Label lblDeployedLabel;
		private System.Windows.Forms.Label lblDeployed;
		private System.Windows.Forms.TabControl tabMenu;
		private System.Windows.Forms.TabPage tabDeploy;
		private System.Windows.Forms.TabPage tabCertify;
		private System.Windows.Forms.ComboBox cboCertifyVersion;
		private System.Windows.Forms.Button btnCertify;
		private System.Windows.Forms.Label lblCertifyVersion;
		private System.Windows.Forms.TabPage tabCheckVersion;
		private System.Windows.Forms.Label lblCertified;
		private System.Windows.Forms.Label lblCertifiedLabel;
		private System.Windows.Forms.BindingSource bndDeployments;
		private System.Windows.Forms.DataGridView grdDeployments;
		private System.Windows.Forms.DataGridViewTextBoxColumn Version;
		private System.Windows.Forms.DataGridViewTextBoxColumn DeployedOn;
		private System.Windows.Forms.DataGridViewTextBoxColumn DeployedBy;
		private System.Windows.Forms.DataGridViewTextBoxColumn Result;
		private System.Windows.Forms.DataGridViewTextBoxColumn CertifiedOn;
		private System.Windows.Forms.DataGridViewTextBoxColumn CertifiedBy;
		private System.Windows.Forms.DataGridViewTextBoxColumn Output;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.TextBox txtUsername;
		private System.Windows.Forms.Label lblPassword;
		private System.Windows.Forms.Label lblUsername;
	}
}