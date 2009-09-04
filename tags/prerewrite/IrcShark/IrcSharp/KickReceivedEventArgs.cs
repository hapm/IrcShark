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

namespace IrcSharp
{
    public class KickReceivedEventArgs : IrcEventArgs
    {
        private UserInfo kicker;
        private String kickedName;
        private String channelName;
        private String ReasonValue;

        public KickReceivedEventArgs(IrcLine BaseLine) : base(BaseLine)
        {
            kicker = new UserInfo(BaseLine);
            kickedName = BaseLine.Parameters[1];
            channelName = BaseLine.Parameters[0];
            ReasonValue = BaseLine.Parameters[2];
        }

        public UserInfo Kicker
        {
            get { return kicker; }
        }

        public String KickedName
        {
            get { return kickedName; }
        }

        public String ChannelName
        {
            get { return channelName; }
        }

        public String Reason
        {
            get { return ReasonValue; }
        }
    }
}
