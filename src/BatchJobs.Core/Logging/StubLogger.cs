namespace BatchJobs.Core.Logging
{
	public class StubLogger : ILogger
	{
		public void Info(object source, object message)
		{
		}

		public void Warn(object source, object message)
		{
		}

		public string CONFIG_FILE_NAME
		{
			get { return ""; }
		}

		public void Error(object source, object message)
		{
		}

		public void Fatal(object source, object message)
		{
		}

		public void Debug(object source, object message)
		{
		}
	}
}