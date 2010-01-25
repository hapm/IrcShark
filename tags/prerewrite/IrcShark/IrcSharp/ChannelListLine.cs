// $Id$
// 
// Note:
// 
// Copyright (C) 2009 Full Name
//  
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace IrcSharp
{
    public class ChannelListLine : IrcLine
    {
        private int userCount;
        private String modes;
        private String topic;

        public ChannelListLine(IrcLine baseLine) : base(baseLine)
        {
            if (baseLine.Numeric != 332)
            	throw new ArgumentOutOfRangeException("baseLine", "CHANNELLIST_RPL 322 expected");
            if (Parameters.Length < 3)
            	throw new ArgumentOutOfRangeException("baseLine", "Need a minimum of 3 parameters");

            if (!int.TryParse(Parameters[2], out userCount))
            	throw new ArgumentOutOfRangeException("baseLine", "Invalid user count, integer expected");

            if (Parameters.Length > 3)
            {
                Regex ModeTopicRegex = new Regex(@"(?:\[\+([^ \]]*)] )?(.*)");
                Match m = ModeTopicRegex.Match(Parameters[3]);
                if (m.Success)
                {
                    modes = m.Groups[1].Value;
                    topic = m.Groups[2].Value;
                }
                else
                {
                    modes = "";
                    topic = "";
                }
            }
            else
            {
                modes = "";
                topic = "";
            }
        }

        public string ChannelName
        {
            get { return Parameters[1]; }
        }

        public int UserCount
        {
            get { return userCount; }
        }
    }
}
