namespace Tarantino.DatabaseManager
{
	partial class Main
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
			this.txtServer = new System.Windows.Forms.TextBox();
			this.txtDatabase = new System.Windows.Forms.TextBox();
			this.txtUsername = new System.Windows.Forms.TextBox();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.lblServer = new System.Windows.Forms.Label();
			this.lblDatabase = new System.Windows.Forms.Label();
			this.lblUsername = new System.Windows.Forms.Label();
			this.lblPassword = new System.Windows.Forms.Label();
			this.lblIntegratedSecurity = new System.Windows.Forms.Label();
			this.chkIntegratedSecurity = new System.Windows.Forms.CheckBox();
			this.cboAction = new System.Windows.Forms.ComboBox();
			this.lblAction = new System.Windows.Forms.Label();
			this.btnExecute = new System.Windows.Forms.Button();
			this.lblScriptFolder = new System.Windows.Forms.Label();
			this.txtScriptFolder = new System.Windows.Forms.TextBox();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtServer
			// 
			this.txtServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtServer.Location = new System.Drawing.Point(83, 69);
			this.txtServer.Name = "txtServer";
			this.txtServer.Size = new System.Drawing.Size(380, 20);
			this.txtServer.TabIndex = 3;
			// 
			// txtDatabase
			// 
			this.txtDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtDatabase.Location = new System.Drawing.Point(83, 95);
			this.txtDatabase.Name = "txtDatabase";
			this.txtDatabase.Size = new System.Drawing.Size(380, 20);
			this.txtDatabase.TabIndex = 4;
			// 
			// txtUsername
			// 
			this.txtUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtUsername.Location = new System.Drawing.Point(83, 121);
			this.txtUsername.Name = "txtUsername";
			this.txtUsername.Size = new System.Drawing.Size(380, 20);
			this.txtUsername.TabIndex = 5;
			// 
			// txtPassword
			// 
			this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtPassword.Location = new System.Drawing.Point(83, 147);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.Size = new System.Drawing.Size(380, 20);
			this.txtPassword.TabIndex = 6;
			// 
			// lblServer
			// 
			this.lblServer.AutoSize = true;
			this.lblServer.Location = new System.Drawing.Point(36, 72);
			this.lblServer.Name = "lblServer";
			this.lblServer.Size = new System.Drawing.Size(41, 13);
			this.lblServer.TabIndex = 4;
			this.lblServer.Text = "Server:";
			// 
			// lblDatabase
			// 
			this.lblDatabase.AutoSize = true;
			this.lblDatabase.Location = new System.Drawing.Point(21, 98);
			this.lblDatabase.Name = "lblDatabase";
			this.lblDatabase.Size = new System.Drawing.Size(56, 13);
			this.lblDatabase.TabIndex = 5;
			this.lblDatabase.Text = "Database:";
			// 
			// lblUsername
			// 
			this.lblUsername.AutoSize = true;
			this.lblUsername.Location = new System.Drawing.Point(19, 124);
			this.lblUsername.Name = "lblUsername";
			this.lblUsername.Size = new System.Drawing.Size(58, 13);
			this.lblUsername.TabIndex = 6;
			this.lblUsername.Text = "Username:";
			// 
			// lblPassword
			// 
			this.lblPassword.AutoSize = true;
			this.lblPassword.Location = new System.Drawing.Point(21, 150);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size(56, 13);
			this.lblPassword.TabIndex = 7;
			this.lblPassword.Text = "Password:";
			// 
			// lblIntegratedSecurity
			// 
			this.lblIntegratedSecurity.AutoSize = true;
			this.lblIntegratedSecurity.Location = new System.Drawing.Point(43, 174);
			this.lblIntegratedSecurity.Name = "lblIntegratedSecurity";
			this.lblIntegratedSecurity.Size = new System.Drawing.Size(34, 13);
			this.lblIntegratedSecurity.TabIndex = 8;
			this.lblIntegratedSecurity.Text = "SSPI:";
			// 
			// chkIntegratedSecurity
			// 
			this.chkIntegratedSecurity.AutoSize = true;
			this.chkIntegratedSecurity.Location = new System.Drawing.Point(84, 174);
			this.chkIntegratedSecurity.Name = "chkIntegratedSecurity";
			this.chkIntegratedSecurity.Size = new System.Drawing.Size(15, 14);
			this.chkIntegratedSecurity.TabIndex = 7;
			this.chkIntegratedSecurity.UseVisualStyleBackColor = true;
			// 
			// cboAction
			// 
			this.cboAction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.cboAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboAction.FormattingEnabled = true;
			this.cboAction.Location = new System.Drawing.Point(83, 42);
			this.cboAction.Name = "cboAction";
			this.cboAction.Size = new System.Drawing.Size(379, 21);
			this.cboAction.TabIndex = 2;
			// 
			// lblAction
			// 
			this.lblAction.AutoSize = true;
			this.lblAction.Location = new System.Drawing.Point(37, 45);
			this.lblAction.Name = "lblAction";
			this.lblAction.Size = new System.Drawing.Size(40, 13);
			this.lblAction.TabIndex = 13;
			this.lblAction.Text = "Action:";
			// 
			// btnExecute
			// 
			this.btnExecute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnExecute.Location = new System.Drawing.Point(388, 200);
			this.btnExecute.Name = "btnExecute";
			this.btnExecute.Size = new System.Drawing.Size(75, 23);
			this.btnExecute.TabIndex = 8;
			this.btnExecute.Text = "&Execute";
			this.btnExecute.UseVisualStyleBackColor = true;
			this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
			// 
			// lblScriptFolder
			// 
			this.lblScriptFolder.AutoSize = true;
			this.lblScriptFolder.Location = new System.Drawing.Point(8, 19);
			this.lblScriptFolder.Name = "lblScriptFolder";
			this.lblScriptFolder.Size = new System.Drawing.Size(69, 13);
			this.lblScriptFolder.TabIndex = 15;
			this.lblScriptFolder.Text = "Script Folder:";
			// 
			// txtScriptFolder
			// 
			this.txtScriptFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtScriptFolder.Location = new System.Drawing.Point(82, 16);
			this.txtScriptFolder.Name = "txtScriptFolder";
			this.txtScriptFolder.Size = new System.Drawing.Size(351, 20);
			this.txtScriptFolder.TabIndex = 0;
			// 
			// btnBrowse
			// 
			this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBrowse.Location = new System.Drawing.Point(439, 16);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(24, 20);
			this.btnBrowse.TabIndex = 1;
			this.btnBrowse.Text = "...";
			this.btnBrowse.UseVisualStyleBackColor = true;
			// 
			// Main
			// 
			this.ClientSize = new System.Drawing.Size(475, 235);
			this.Controls.Add(this.btnBrowse);
			this.Controls.Add(this.txtScriptFolder);
			this.Controls.Add(this.lblScriptFolder);
			this.Controls.Add(this.btnExecute);
			this.Controls.Add(this.lblAction);
			this.Controls.Add(this.cboAction);
			this.Controls.Add(this.chkIntegratedSecurity);
			this.Controls.Add(this.lblIntegratedSecurity);
			this.Controls.Add(this.lblPassword);
			this.Controls.Add(this.lblUsername);
			this.Controls.Add(this.lblDatabase);
			this.Controls.Add(this.lblServer);
			this.Controls.Add(this.txtPassword);
			this.Controls.Add(this.txtUsername);
			this.Controls.Add(this.txtDatabase);
			this.Controls.Add(this.txtServer);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(491, 271);
			this.Name = "Main";
			this.Text = "Tarantino.DatabaseManager";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtServer;
		private System.Windows.Forms.TextBox txtDatabase;
		private System.Windows.Forms.TextBox txtUsername;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.Label lblServer;
		private System.Windows.Forms.Label lblDatabase;
		private System.Windows.Forms.Label lblUsername;
		private System.Windows.Forms.Label lblPassword;
		private System.Windows.Forms.Label lblIntegratedSecurity;
		private System.Windows.Forms.CheckBox chkIntegratedSecurity;
		private System.Windows.Forms.ComboBox cboAction;
		private System.Windows.Forms.Label lblAction;
		private System.Windows.Forms.Button btnExecute;
		private System.Windows.Forms.Label lblScriptFolder;
		private System.Windows.Forms.TextBox txtScriptFolder;
		private System.Windows.Forms.Button btnBrowse;
	}
}