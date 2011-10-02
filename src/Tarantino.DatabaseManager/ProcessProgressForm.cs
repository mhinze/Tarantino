using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Tarantino.DatabaseManager
{
	public class ProcessProgressForm : Form
	{
		private Container components = null;
		private RichTextBox rtbOutput;
		private Button btnCancel;
		private Button btnClose;
		private ProcessCaller _processCaller;
		private Color _stdOutColor = Color.Blue;
		private Color _strErrColor = Color.Maroon;
		private string _errorDialogMessage = "Error running process";

		public ProcessProgressForm()
		{
			InitializeComponent();
			_processCaller = new ProcessCaller(this);
			_processCaller.StdErrReceived += new DataReceivedHandler(_processCaller_StdErrReceived);
			_processCaller.StdOutReceived += new DataReceivedHandler(_processCaller_StdOutReceived);
			_processCaller.Completed += new EventHandler(ProcessCallerCompletedOrCancelled);
			_processCaller.Cancelled += new EventHandler(ProcessCallerCompletedOrCancelled);
			_processCaller.Failed += new ThreadExceptionEventHandler(_processCaller_Failed);
		}

		public string ProcessArguments
		{
			get { return _processCaller.Arguments; }
			set { _processCaller.Arguments = value; }
		}

		public string ProcessCommand
		{
			get { return _processCaller.FileName; }
			set { _processCaller.FileName = value; }
		}

		public string ProcessWorkingDir
		{
			get { return _processCaller.WorkingDirectory; }
			set { _processCaller.WorkingDirectory = value; }
		}

		public Color StandardOutColor
		{
			get { return _stdOutColor; }
			set { _stdOutColor = value; }
		}

		public Color StandardErrorColor
		{
			get { return _strErrColor; }
			set { _strErrColor = value; }
		}

		public string ErrorDialogMessage
		{
			get { return _errorDialogMessage; }
			set { _errorDialogMessage = value; }
		}

		public void BeginProcess()
		{
			Cursor = Cursors.AppStarting;
			btnClose.Enabled = false;
			_processCaller.Start();
			btnCancel.Enabled = true;
		}

		private void AppendOutputToRichTextBox(string data, Color color)
		{
			rtbOutput.ForeColor = color;
			rtbOutput.AppendText(data + Environment.NewLine);
			//Hack to make the rich text box scroll down as output is added
			//int line = User32ApiHelper.SendMessage(this.rtbOutput.Handle, User32ApiHelper.EM_GETFIRSTVISIBLELINE, 0, 0);
			//int linecount = User32ApiHelper.SendMessage(this.rtbOutput.Handle, User32ApiHelper.EM_GETLINECOUNT, 0, 0);
			//User32ApiHelper.SendMessage(this.rtbOutput.Handle, User32ApiHelper.EM_LINESCROLL, 0, (uint)(linecount - line - 2));
			rtbOutput.Focus();
			rtbOutput.ScrollToCaret();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessProgressForm));
			this.rtbOutput = new System.Windows.Forms.RichTextBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// rtbOutput
			// 
			this.rtbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.rtbOutput.Location = new System.Drawing.Point(7, 7);
			this.rtbOutput.Name = "rtbOutput";
			this.rtbOutput.Size = new System.Drawing.Size(442, 383);
			this.rtbOutput.TabIndex = 0;
			this.rtbOutput.TabStop = false;
			this.rtbOutput.Text = "";
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(316, 397);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(62, 20);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.Location = new System.Drawing.Point(387, 397);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(62, 20);
			this.btnClose.TabIndex = 2;
			this.btnClose.Text = "C&lose";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// ProcessProgressForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(456, 424);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.rtbOutput);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ProcessProgressForm";
			this.Text = "Progress Output";
			this.ResumeLayout(false);

		}

		#endregion

		private void btnCancel_Click(object sender, EventArgs e)
		{
			_processCaller.Cancel();
		}

		private void _processCaller_StdErrReceived(object sender, DataReceivedEventArgs e)
		{
			AppendOutputToRichTextBox(e.Text, _strErrColor);
		}

		private void _processCaller_StdOutReceived(object sender, DataReceivedEventArgs e)
		{
			AppendOutputToRichTextBox(e.Text, _stdOutColor);
		}

		private void ProcessCallerCompletedOrCancelled(object sender, EventArgs e)
		{
			Cursor = Cursors.Default;
			btnCancel.Enabled = false;
			btnClose.Enabled = true;
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void _processCaller_Failed(object sender, ThreadExceptionEventArgs e)
		{
			MessageBox.Show(e.Exception.Message, _errorDialogMessage, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			ProcessCallerCompletedOrCancelled(null, null);
		}
	}
}