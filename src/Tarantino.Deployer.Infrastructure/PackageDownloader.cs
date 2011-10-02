using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;
using Ionic.Zip;

namespace Tarantino.Deployer.Infrastructure
{
	public class ExtractResult
	{
		public string Version { get; set; }
		public string Executable { get; set; }
		public string WorkingDirectory { get; set; }
	}

	public class PackageDownloader
	{
		public static ExtractResult DownloadAndExtract(string application, string environment, string version, string downloadUrl, string zipFile, string username, string password)
		{
			if (string.IsNullOrEmpty(version))
			{
				version = GetLatestVersion(username, password, downloadUrl);
			}

			var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
			var applicationDirectory = baseDirectory + @"packages\" + application;
			var versionDirectory = applicationDirectory + @"\" + version;

			if (!Directory.Exists(versionDirectory))
			{
				var fullZipFile = baseDirectory + @"/" + zipFile + ".zip";
				File.Delete(fullZipFile);

				var client = new WebClient
				{
					Credentials = new NetworkCredential(username, password)
				};

				var fullUrl = downloadUrl + "/" + version + "/" + zipFile + ".zip";
				client.DownloadFile(fullUrl, fullZipFile);

				Directory.CreateDirectory(applicationDirectory);

				try
				{
					Directory.Delete(versionDirectory, true);
				}
				catch (DirectoryNotFoundException) { }

				Directory.CreateDirectory(versionDirectory);

				using (var zip = ZipFile.Read(fullZipFile))
				{
					foreach (var entry in zip)
					{
						entry.Extract(versionDirectory, ExtractExistingFileAction.OverwriteSilently);
					}
				}
			}

			var environmentDeploymentBat = versionDirectory + @"\" + environment + ".bat";

			var result = new ExtractResult { Version = version, Executable = environmentDeploymentBat, WorkingDirectory = versionDirectory };
			return result;
		}

		private static string GetLatestVersion(string username, string password, string downloadUrl)
		{
			var client = new WebClient
			{
				Credentials = new NetworkCredential(username, password)
			};

			var html = client.DownloadString(downloadUrl);
			var xhtml = html.Replace("<br>", "<br />");

			var document = new XmlDocument();
			document.LoadXml(xhtml);

			var anchors = document.GetElementsByTagName("a");

			var filteredUrls = new List<string>();

			foreach (XmlNode anchor in anchors)
			{
				if (!anchor.InnerText.Contains(".tcbuildid") && !anchor.InnerText.Contains("latest.lastFinished") && !anchor.InnerText.Contains("latest.lastSuccessful"))
				{
					var urlWithVersion = anchor.InnerText;
					var urlParts = urlWithVersion.Split('/');

					var version = urlParts[urlParts.Length - 1];
					filteredUrls.Add(version);
				}
			}

			return filteredUrls[0];
		}


	}
}