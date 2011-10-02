using System.IO;
using Tarantino.Core.DatabaseManager.Services;
using NAnt.Core.Attributes;
using Tarantino.DatabaseManager.Core;
using Tarantino.Infrastructure.DatabaseManager.BuildTasks;

namespace Tarantino.DatabaseManager.Tasks
{
	[TaskName("importExcelToSql")]
	public class ImportExcelToSqlTask : Task
	{
		public ImportExcelToSqlTask()
		{
			Server = ".";
			IntegratedAuthentication = true;
		}

		[StringValidator(AllowEmpty = false), TaskAttribute("excelFile")]
		public FileInfo ExcelFile { get; set; }

		[TaskAttribute("server"), StringValidator(AllowEmpty = false)]
		public string Server { get; set; }

		[StringValidator(AllowEmpty = false), TaskAttribute("database", Required = true)]
		public string Database { get; set; }

		[TaskAttribute("integratedAuthentication"), StringValidator(AllowEmpty = false)]
		public bool IntegratedAuthentication { get; set; }

		[TaskAttribute("username")]
		public string Username { get; set; }

		[TaskAttribute("password")]
		public string Password { get; set; }

		protected override void ExecuteTask()
		{
			try
			{
				InfrastructureDependencyRegistrar.RegisterInfrastructure();
				var importer = new ServiceLocator().CreateInstance<IExcelImporter>();
				importer.Import(ExcelFile.FullName, Server, Database, IntegratedAuthentication, Username, Password, this);
			}
			catch
			{
				if (FailOnError)
					throw;
			}
		}
	}
}