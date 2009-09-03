using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    public class TopicEventArgs : IrcEventArgs
    {
        public TopicEventArgs(IrcLine baseLine) : base(baseLine)
        {
            if (baseLine.Numeric != 332)
            	throw new ArgumentOutOfRangeException("baseLine", "TOPIC_RPL 332 expected");
        }

        public String ChannelName
        {
            get { return BaseLine.Parameters[1]; }
        }

        public String Topic
        {
            get { return BaseLine.Parameters[2]; }
        }
    }
}
