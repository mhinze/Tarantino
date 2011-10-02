using System;
using System.Configuration;
using System.IO;
using System.Net.Mail;

namespace BatchJobs.Console
{
	public class LogFileToEmailSender
	{
		private const string ToEmailKey = "BatchLogFileToEmail";
		private const string FromEmailKey = "BatchLogFileFromEmail";
		private const string SmtpHostKey = "BatchLogFileSmtpHost";
		private const string FileLocationKey = "BatchLogFileLocation";

		public Func<string> GetToEmail = () => ConfigurationManager.AppSettings[ToEmailKey];
		public Func<string> GetFromEmail = () => ConfigurationManager.AppSettings[FromEmailKey];
		public Func<string> GetSmtpHost = () => ConfigurationManager.AppSettings[SmtpHostKey];
		public Func<string> GetFileLocation = () => ConfigurationManager.AppSettings[FileLocationKey];


		public void Send(string args)
		{
			if (!IsConfiguredForEmail())
			{
				Logger.Warn(this, "Note: Emailing of errors has not been configured for this job");
				return;
			}

			var message = CreateMessage(args);
			var client = new SmtpClient {Host = GetSmtpHost()};
			client.Send(message);
		}

		private bool IsConfiguredForEmail()
		{
			var isConfigured = true;
			if (string.IsNullOrEmpty(GetToEmail())) isConfigured = false;
			if (string.IsNullOrEmpty(GetFromEmail())) isConfigured = false;
			if (string.IsNullOrEmpty(GetSmtpHost())) isConfigured = false;	
			return isConfigured;
		}

		public MailMessage CreateMessage(string args)
		{
			var message = new MailMessage(GetFromEmail(), GetToEmail())
			              	{
			              		Subject = String.Format("[{0}] Error on Batch Job", args),
			              		Body = GetFileText()
			              	};
			return message;
		}

		public string GetFileText()
		{
			if (!string.IsNullOrEmpty(GetFileLocation()))
			{
				using (var fs = new FileStream(GetFileLocation(), FileMode.Open, FileAccess.Read))
				{
					using (var sr = new StreamReader(fs))
					{

						return sr.ReadToEnd();
					}
				}
			}
			return "Log File Location Not Configured so Log File could not be attached, Error on batch occurred";
		}
	}
}