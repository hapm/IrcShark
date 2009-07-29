using System;
using System.Collections.Generic;
using System.Text;

namespace IrcShark
{
    public delegate void LogLineDelegate(object sender, LogMessage args);

    /// <summary>
    /// The main class for logging in IrcShark. Any logging-system can register withe the LineLog event to get the lines to log.
    /// </summary>
    public class Logger
    {
        private LogLevels filter;

        public event LogLineDelegate LogLine;

        /// <summary>
        /// Creates a new Logger.
        /// </summary>
        public Logger()
        {
            filter = LogLevels.All;
        }

        /// <summary>
        /// Logs the given LogMessage
        /// </summary>
        public void Log(LogMessage msg)
        {
            if (LogLine != null)
                LogLine(this, msg);
        }

        public void Log(LogLevels type, string msg, string subject)
        {
            Log(new LogMessage(type, msg, subject));
        }

        public void Log(LogLevels type, string msg)
        {
            Log(new LogMessage(type, msg));
        }

        public void Log(string msg)
        {
            Log(new LogMessage(msg));
        }

        /// <summary>
        /// Allows you to set a filter to the message logging.
        /// </summary>
        public LogLevels Filter
        {
            get { return filter; }
            set { filter = value; }
        }
    }
}
