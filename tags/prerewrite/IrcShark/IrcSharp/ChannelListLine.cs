using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace IrcSharp
{
    public class ChannelListLine : IrcLine
    {
        private int UserCountValue;
        private String ModesValue;
        private String TopicValue;

        public ChannelListLine(IrcLine baseLine) : base(baseLine)
        {
            if (baseLine.Numeric != 332) throw new ArgumentOutOfRangeException("baseLine", "CHANNELLIST_RPL 322 expected");
            if (Parameters.Length < 3) throw new ArgumentOutOfRangeException("baseLine", "Need a minimum of 3 parameters");
            

            if (!int.TryParse(Parameters[2], out UserCountValue)) throw new ArgumentOutOfRangeException("baseLine", "Invalid user count, integer expected");

            if (Parameters.Length > 3)
            {
                Regex ModeTopicRegex = new Regex(@"(?:\[\+([^ \]]*)] )?(.*)");
                Match m = ModeTopicRegex.Match(Parameters[3]);
                if (m.Success)
                {
                    ModesValue = m.Groups[1].Value;
                    TopicValue = m.Groups[2].Value;
                }
                else
                {
                    ModesValue = "";
                    TopicValue = "";
                }
            }
            else
            {
                ModesValue = "";
                TopicValue = "";
            }
        }

        public string ChannelName
        {
            get { return Parameters[1]; }
        }

        public int UserCount
        {
            get { return UserCountValue; }
        }
    }
}
