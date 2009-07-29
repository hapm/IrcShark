using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    public class NamesEndEventArgs : IrcEventArgs
    {
        private String[] NamesValue;
        private String ChannelNameValue;

        public NamesEndEventArgs(IrcLine baseLine, String[] names, String channelName)
            : base(baseLine)
        {
            NamesValue = names;
            ChannelNameValue = channelName;
        }

        public String[] Names
        {
            get { return NamesValue; }
        }

        public string ChannelName
        {
            get { return ChannelNameValue; }
        }
    }
}
