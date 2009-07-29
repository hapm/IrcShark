using System;
using System.Collections.Generic;
using System.Text;

namespace IrcShark
{

    /// <summary>
    /// Represents a message, what can be logged by a Logger.
    /// </summary>
    public class LogMessage
    {
        LogLevels level;
        DateTime created;
        string msgText;
        string subject;

        public LogMessage(LogLevels type, string msg, string subject)
        {
            this.level = type;
            this.msgText = msg;
            this.subject = subject;
            this.created = DateTime.Now;
        }

        public LogMessage(LogLevels type, string msg)
        {
            this.level = type;
            this.msgText = msg;
            this.subject = "unknown";
            this.created = DateTime.Now;
        }

        public LogMessage(string msg)
        {
            this.level = LogLevels.Verbose;
            this.msgText = msg;
            this.subject = "unknown";
            this.created = DateTime.Now;
        }

        /// <summary>
        /// The level of this LogMessage.
        /// </summary>
        public LogLevels Level
        {
            get { return level; }
        }

        /// <summary>
        /// The Time when this message was created.
        /// </summary>
        /// <remarks>This time will be automatically set on creation of the LogMessage.</remarks>
        public DateTime Created
        {
            get { return created; }
        }

        /// <summary>
        /// The Message, what will be logged.
        /// </summary>
        public string Message
        {
            get { return msgText; }
        }

        /// <summary>
        /// The subject of this LogMessage.
        /// </summary>
        public string Subject
        {
            get { return subject; }
        }
    }
}
