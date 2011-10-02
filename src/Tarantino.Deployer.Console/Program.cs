using System.IO;
using System.Linq;
using System.Text;
using StructureMap;
using Tarantino.Deployer.Core.Services;
using Tarantino.Deployer.Core.Services.Configuration;
using Tarantino.Deployer.Infrastructure;
using Environment=System.Environment;

namespace Tarantino.Deployer.Console
{
	public class Program
	{
		private static readonly StringBuilder _buildOutput = new StringBuilder();
		private static bool _buildFailed;

		public static void Main(string[] args)
		{
			DeployerInfrastructureDependencyRegistrar.RegisterInfrastructure();

			if (args.Length != 4)
			{
				OutputUsageAndClose("Supply the correct number of command line arguments");
			}

			var requestedAction = args[0];
			var requestedApplicationName = args[1];
			var requestedEnvironment = args[2];
			var requestedVersion = args[3];

			var applicationRepository = ObjectFactory.GetInstance<IApplicationRepository>();

			var selectedApplication = applicationRepository.GetByName(requestedApplicationName);

			if (selectedApplication == null)
			{
				OutputUsageAndClose("Specify an application name included in the configuration file 'Tarantino.Deployer.Console.exe.config'");
			}

			if (selectedApplication != null)
			{
				var selectedEnvironment = selectedApplication.GetEnvironmentByName(requestedEnvironment);

				if (selectedEnvironment == null)
				{
					OutputUsageAndClose(
						string.Format(
							"Specify an environment included in the configuration file 'Tarantino.Deployer.Console.exe.config' for application '{0}'",
							selectedApplication.Name));
				}

				if (selectedEnvironment != null)
				{
					if (requestedAction == "Deploy")
					{
						string versionNumber = requestedVersion;

						if (requestedVersion == "CurrentCertified")
						{
							var repository = ObjectFactory.GetInstance<IDeploymentRepository>();
							var certifiedDeployments = repository.FindCertified(selectedApplication.Name, selectedEnvironment.Predecessor);
							if (certifiedDeployments.Count() > 0)
							{
								var lastCertified = certifiedDeployments.OrderByDescending(d => d.CertifiedOn).ElementAt(0);
								versionNumber = lastCertified.Version;
							}
						}
						else if (requestedVersion == "Current")
						{
							versionNumber = null;
						}

						var result = PackageDownloader.DownloadAndExtract(selectedApplication.Name, selectedEnvironment.Name, versionNumber,
						                                                              selectedApplication.Url, selectedApplication.ZipFile,
						                                                              selectedApplication.Username, selectedApplication.Password);

						var caller = new SimpleProcessCaller(result.Executable, string.Empty, result.WorkingDirectory)
						{
							StdOutReceived = Caller_OnStdOutReceived,
							StdErrorReceived = Caller_OnStdErrorReceived
						};

						caller.ExecuteProcess();
						var exitCode = caller.ExitCode;

						var recorder = ObjectFactory.GetInstance<IDeploymentRecorder>();
						var version = recorder.RecordDeployment(selectedApplication.Name, selectedEnvironment.Name, _buildOutput.ToString(), result.Version, _buildFailed);

						if (exitCode == 0)
						{
							using (var writer = new StreamWriter("TarantinoDeploymentVersionNumber.txt"))
							{
								writer.Write(version);
							}
						}
						else
						{
							Environment.Exit(1);
						}
					}
					else if (requestedAction == "Certify")
					{
						var repository = ObjectFactory.GetInstance<IDeploymentRepository>();
						var uncertifiedDeployments = repository.FindSuccessfulUncertified(selectedApplication.Name, selectedEnvironment.Name);

						var deployments = uncertifiedDeployments.Where(d => d.Version == requestedVersion).OrderByDescending(d => d.DeployedOn);

						if (deployments.Count() > 0)
						{
							var deployment = deployments.ElementAt(0);
							var certifier = ObjectFactory.GetInstance<IVersionCertifier>();
							certifier.Certify(deployment);
						}
						else
						{
							OutputUsageAndClose("When certifying a deployment, you must specify a valid deployment that has not already been certified");
						}
					}
					else
					{
						OutputUsageAndClose("Specify either 'Deploy' or 'Certify' for the <Action> argument");
					}
				}
			}
		}

		private static void Caller_OnStdOutReceived(string output)
		{
			Out(output);
			_buildOutput.AppendLine(output);
		}

		private static void Caller_OnStdErrorReceived(string output)
		{
			Out(output);
			_buildOutput.AppendLine(output);
			_buildFailed = true;
		}

		private static void OutputUsageAndClose(string customMessage)
		{
			Out("\n\nINVALID USAGE: Incorrect Command Line Arguments Supplied\n");
			Out("USAGE: Tarantino.Deployer.Console <Action> <Application> <Environment> <Version>");
			Out("  Action = {Deploy, Certify}");
			Out("  Application = Name of application to deploy");
			Out("  Environment = Environment to deploy application");
			Out("  Version = {Current, CurrentCertified, <Version Number>}\n");
			Out("Example 1: Deploy 'PetStore', Version '1.0.4255.0' to 'Test' environment");
			Out("  Tarantino.Deployer.Console Deploy PetStore Test 1.0.4255.0\n");
			Out("Example 2: Deploy the latest version of 'PetStore' to 'Test' environment");
			Out("  Tarantino.Deployer.Console Deploy PetStore Test Current\n");
			Out("Example 3: Deploy the latest certified version of 'PetStore' to 'Test' environment");
			Out("  Tarantino.Deployer.Console Deploy PetStore Test CurrentCertified\n");
			Out("Example 4: Certify 'PetStore', Version '1.0.4255.0' in the 'Test' environment");
			Out("  Tarantino.Deployer.Console Certify PetStore Test 1.0.4255.0\n\n\n");
			Out("TO CORRECT: " + customMessage);

			Environment.Exit(1);
		}

		private static void Out(string message)
		{
			System.Console.Out.WriteLine(message);
		}
	}
}