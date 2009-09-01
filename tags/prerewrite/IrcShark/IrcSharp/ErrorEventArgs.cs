using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    public class ErrorEventArgs : IrcEventArgs
    {
        Exception innerException;
        string message;

        public ErrorEventArgs(IrcClient client, string msg) : base(client)
        {
            message = msg;
        }

        public ErrorEventArgs(IrcClient client, string msg, Exception exception) : base(client)
        {
            innerException = exception;
            message = msg;
        }

        public string Message
        {
            get { return message; }
        }

        public Exception InnerException
        {
            get { return innerException; }
        }
    }
}
