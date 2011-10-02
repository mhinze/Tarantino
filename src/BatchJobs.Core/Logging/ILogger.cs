using System;

namespace BatchJobs.Core.Logging
{
	public interface ILogger
	{
		string CONFIG_FILE_NAME { get; }
		void Error(object source, object message);
		void Warn(object source, object message);
		void Info(object source, object message);
		void Fatal(object source, object message);
		void Debug(object source, object message);
	}
}