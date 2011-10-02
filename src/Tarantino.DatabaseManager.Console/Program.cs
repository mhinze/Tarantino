using System;
using System.IO;
using Tarantino.Core.DatabaseManager.Model;
using Tarantino.Core.DatabaseManager.Services.Impl;

namespace Tarantino.DatabaseManager.Console
{
    public class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length != 4 && args.Length != 6)
            {
                InvalidArguments();
                return;
            }

            ConnectionSettings settings = null;

            var deployer = new ConsoleDatabaseDeployer();

            var action = (RequestedDatabaseAction)Enum.Parse(typeof(RequestedDatabaseAction), args[0]);
            string server = args[1];
            string database = args[2];
            string scriptDirectory = args[3];
            
            if (args.Length == 4)
            {
                settings = new ConnectionSettings(server, database, true, null, null);
            }

            else if (args.Length == 6)
            {
                string username = args[4];
                string password = args[5];

                settings = new ConnectionSettings(server, database, false, username, password);
            }

            if (deployer.UpdateDatabase(settings, scriptDirectory, action))
            {
                return;
            }    

            Environment.ExitCode = 1;
        }

        private static void InvalidArguments()
        {
            System.Console.WriteLine("Invalid Arguments");
            System.Console.WriteLine( Path.GetFileName(typeof(Program).Assembly.Location) + @" Action(Create|Update|Rebuild) .\SqlExpress DatabaseName  .\DatabaseScripts\ ");
            System.Console.WriteLine("-- or --");
            System.Console.WriteLine( Path.GetFileName(typeof(Program).Assembly.Location) + @" Action(Create|Update|Rebuild) .\SqlExpress DatabaseName  .\DatabaseScripts\ Username Password");
        }
    }
}