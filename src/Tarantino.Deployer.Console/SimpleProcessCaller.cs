using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Tarantino.Deployer.Console
{
	public class SimpleProcessCaller
	{
		private Process _process;
		private readonly string _fileName;
		private readonly string _arguments;
		private readonly string _workingDirectory;

		public SimpleProcessCaller(string fileName, string arguments)
			: this(fileName, arguments, AppDomain.CurrentDomain.BaseDirectory)
		{
		}

		public SimpleProcessCaller(string fileName)
			: this(fileName, string.Empty, AppDomain.CurrentDomain.BaseDirectory)
		{
		}

		public SimpleProcessCaller(string fileName, string arguments, string workingDirectory)
		{
			_fileName = fileName;
			_arguments = arguments;
			_workingDirectory = workingDirectory;
		}

		public Action<string> StdOutReceived;
		public Action<string> StdErrorReceived;

		public int ExitCode { get; private set; }

		public void ExecuteProcess()
		{
			StartProcess();

			while (!_process.HasExited)
			{
				Thread.Sleep(1000);
			}

			ExitCode = _process.ExitCode;
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
			           				FileName = _fileName,
			           				Arguments = _arguments,
			           				WorkingDirectory = _workingDirectory
			           			}
			           	};

			_process.Start();

			new MethodInvoker(ReadStdOut).BeginInvoke(null, null);
			new MethodInvoker(ReadStdErr).BeginInvoke(null, null);
		}

		protected virtual void ReadStdOut()
		{
			SendOutput(_process.StandardOutput, StdOutReceived);
		}

		protected virtual void ReadStdErr()
		{
			SendOutput(_process.StandardError, StdErrorReceived);
		}

		private static void SendOutput(TextReader reader, Action<string> handler)
		{
			string str;
			while ((str = reader.ReadLine()) != null)
			{
				if (handler != null)
				{
					handler(str);
				}
			}
		}
	}
}