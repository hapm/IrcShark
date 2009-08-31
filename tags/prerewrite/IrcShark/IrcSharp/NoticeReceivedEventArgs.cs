using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    public class NoticeReceivedEventArgs : IrcEventArgs
    {
        public NoticeReceivedEventArgs(IrcLine baseLine) : base(baseLine)
        {
        }

        public String Sender
        {
            get { return BaseLine.Prefix; }
        }

        public String Destination
        {
            get { return BaseLine.Parameters[0]; }
        }

        public String Message
        {
            get { return BaseLine.Parameters[1]; }
        }
    }
}
