using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace Tarantino.Deployer
{
	public delegate void DataReceivedHandler(object sender, DataReceivedEventArgs e);

	public class DataReceivedEventArgs : EventArgs
	{
		public string Text;

		public DataReceivedEventArgs(string text)
		{
			Text = text;
		}
	}

	public class ProcessCaller : AsyncOperation
	{
		private string _fileName;
		private string _arguments;
		private string _workingDirectory;
		private int _sleepTime = 500;

		public string FileName
		{
			get { return _fileName; }
			set { _fileName = value; }
		}

		public string Arguments
		{
			get { return _arguments; }
			set { _arguments = value; }
		}

		public string WorkingDirectory
		{
			get { return _workingDirectory; }
			set { _workingDirectory = value; }
		}

		public int SleepTime
		{
			get { return _sleepTime; }
			set { _sleepTime = value; }
		}

		public event DataReceivedHandler StdOutReceived;

		public event DataReceivedHandler StdErrReceived;

		private Process _process;

		public ProcessCaller(ISynchronizeInvoke isi)
			: base(isi)
		{
		}

		protected override void DoWork()
		{
			StartProcess();

			while (! _process.HasExited)
			{
				Thread.Sleep(SleepTime);
				if (CancelRequested)
				{
					_process.Kill();
					AcknowledgeCancel();
				}
			}
		}

		protected virtual void StartProcess()
		{
			_process = new Process
			           	{
			           		StartInfo =
			           			{
			           				UseShellExecute = false,
			           				RedirectStandardOutput = true,
			           				RedirectStandardError = true,
			           				CreateNoWindow = true,
			           				FileName = FileName,
			           				Arguments = Arguments,
			           				WorkingDirectory = WorkingDirectory
			           			}
			           	};
			_process.Start();

			new MethodInvoker(ReadStdOut).BeginInvoke(null, null);
			new MethodInvoker(ReadStdErr).BeginInvoke(null, null);
		}

		protected virtual void ReadStdOut()
		{
			string str;
			while ((str = _process.StandardOutput.ReadLine()) != null)
			{
				FireAsync(StdOutReceived, this, new DataReceivedEventArgs(str));
			}
		}

		protected virtual void ReadStdErr()
		{
			string str;
			while ((str = _process.StandardError.ReadLine()) != null)
			{
				FireAsync(StdErrReceived, this, new DataReceivedEventArgs(str));
			}
		}
	}
}