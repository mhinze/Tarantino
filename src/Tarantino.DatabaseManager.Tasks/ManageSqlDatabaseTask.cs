using System.IO;
using Tarantino.Core.DatabaseManager.Model;
using Tarantino.Core.DatabaseManager.Services;
using Tarantino.Core.DatabaseManager.Services.Impl;
using NAnt.Core.Attributes;
using Tarantino.DatabaseManager.Core;
using Tarantino.Infrastructure.DatabaseManager.BuildTasks;

namespace Tarantino.DatabaseManager.Tasks
{
	[TaskName("manageSqlDatabase")]
	public class ManageSqlDatabaseTask : Task
	{
		private RequestedDatabaseAction _action = RequestedDatabaseAction.Update;
		private DirectoryInfo _scriptDirectory = new DirectoryInfo(".");
		private string _server = ".";
		private string _database;
		private bool _integratedAuthentication = true;
		private string _username;
		private string _password;

		[TaskAttribute("action"), StringValidator(AllowEmpty = false)]
		public RequestedDatabaseAction Action
		{
			get { return _action; }
			set { _action = value; }
		}

		[StringValidator(AllowEmpty = false), TaskAttribute("scriptDirectory")]
		public DirectoryInfo ScriptDirectory
		{
			get { return _scriptDirectory; }
			set { _scriptDirectory = value; }
		}

		[TaskAttribute("server"), StringValidator(AllowEmpty = false)]
		public string Server
		{
			get { return _server; }
			set { _server = value; }
		}

		[StringValidator(AllowEmpty = false), TaskAttribute("database", Required = true)]
		public string Database
		{
			get { return _database; }
			set { _database = value; }
		}

		[TaskAttribute("integratedAuthentication"), StringValidator(AllowEmpty = false)]
		public bool IntegratedAuthentication
		{
			get { return _integratedAuthentication; }
			set { _integratedAuthentication = value; }
		}

		[TaskAttribute("username")]
		public string Username
		{
			get { return _username; }
			set { _username = value; }
		}

		[TaskAttribute("password")]
		public string Password
		{
			get { return _password; }
			set { _password = value; }
		}

        [TaskAttribute("skipFileNameContaining")]
        public string SkipFileNameContaining { get; set;}

        protected override void ExecuteTask()
		{
			try
			{
			    var manager = new SqlDatabaseManager();
                var settings = new ConnectionSettings(Server, Database, IntegratedAuthentication, Username, Password);
                var taskAttributes = new TaskAttributes(settings, ScriptDirectory.FullName)
                                         {
                                             SkipFileNameContaining = SkipFileNameContaining,
                                             RequestedDatabaseAction = Action,
                                         };

                manager.Upgrade(taskAttributes, this);
			}
			catch
			{
				if (FailOnError)
					throw;
			}
		}
	}
}