using BatchJobs.Core.Logging;

namespace BatchJobs.Console
{
	public class LoggerProxy : ILogger
	{
		public string CONFIG_FILE_NAME
		{
			get { return Logger.CONFIG_FILE_NAME; }
		}

		public void Error(object source, object message)
		{
			Logger.Error(source, message);
		}

		public void Warn(object source, object message)
		{
			Logger.Warn(source, message);
		}

		public void Info(object source, object message)
		{
			Logger.Info(source, message);
		}

		public void Fatal(object source, object message)
		{
			Logger.Fatal(source, message);
		}

		public void Debug(object source, object message)
		{
			Logger.Debug(source, message);
		}
	}
}