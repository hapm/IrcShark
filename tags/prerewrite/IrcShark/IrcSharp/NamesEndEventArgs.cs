using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    public class NamesEndEventArgs : IrcEventArgs
    {
        private String[] names;
        private String ChannelNameValue;

        public NamesEndEventArgs(IrcLine baseLine, String[] names, String channelName) : base(baseLine)
        {
            names = names;
            ChannelNameValue = channelName;
        }

        public String[] Names
        {
            get { return names; }
        }

        public string ChannelName
        {
            get { return ChannelNameValue; }
        }
    }
}
