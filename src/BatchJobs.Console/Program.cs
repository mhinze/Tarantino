using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using BatchJobs.Core;

namespace BatchJobs.Console
{
	public class Program
	{
		public const string JobagentfactorytypeKey = "JobAgentFactoryType";

		public Func<string> GetFactoryTypeName = () => ConfigurationManager.AppSettings[JobagentfactorytypeKey];

		private static void Main(string[] args)
		{
			Logger.EnsureInitialized();
			try
			{
				new Program().Run(args);
			}
			catch (Exception e)
			{
				System.Console.WriteLine(e.Message);
				Logger.Fatal(typeof(Program), string.Format("Failure in Job, bubbled to Batch Runner: {0}", Logger.SerializeException(e)));

				var sender = new LogFileToEmailSender();
				
				sender.Send(string.Join(",", args));
				Environment.ExitCode = 100;
			}
		}

		public virtual void Run(string[] args)
		{
			if (args.Length == 0)
			{
				Logger.Fatal(this, "Command Line Instance Name Not Specified");
				System.Console.WriteLine("One of the following instance names must be specified:");
				foreach (var name in Factory().GetInstanceNames())
				{
					System.Console.WriteLine(name);
				}
			}
			else
			{
				Logger.Info(this, string.Format("Command Line Specified Instance Name: {0}", args[0]));
				IJobAgent jobAgent = Factory().Create(args[0]);
				Logger.Info(this, "Executing the Job");

				jobAgent.Execute();
				Logger.Info(this, string.Format("Job Execution Complete: {0}", args[0]));
			}
		}

		public virtual IJobAgentFactory Factory()
		{
			string unparsedTypename = GetFactoryTypeName();
			string typename;
			string assemblyname;

			try
			{
				Logger.Info(this, string.Format("Parsing assembly and type names from string \"{0}\"", unparsedTypename));
				assemblyname = unparsedTypename.Split(',')[1].Trim();
				typename = unparsedTypename.Split(',')[0].Trim();
			}
			catch (Exception)
			{
				Logger.Fatal(this, String.Format("Could not parse the typename from the application configuration. configuration value: \"{0}\", configuration key: \"{1}\"", unparsedTypename, JobagentfactorytypeKey));
				throw new InvalidOperationException("Could not parse the typename from the application configuration.");
			}

			Assembly a = null;
			try
			{
				Logger.Info(this, string.Format("Loading Assembly {0}", assemblyname));
				a = Assembly.Load(assemblyname);
			}
			catch (FileNotFoundException e)
			{
				Logger.Fatal(this, string.Format("Unable to load assembly {0}", assemblyname));
				System.Console.WriteLine(e.Message);
			}
			Type classType = a.GetType(typename);
			Logger.Info(this, string.Format("Creating instance of {0}", classType));
			if (classType.GetConstructors().Any( x => x.GetParameters().Length == 1))
			{
				return (IJobAgentFactory)Activator.CreateInstance(classType, new LoggerProxy());
			} 
			return (IJobAgentFactory)Activator.CreateInstance(classType);

		}
	}
}