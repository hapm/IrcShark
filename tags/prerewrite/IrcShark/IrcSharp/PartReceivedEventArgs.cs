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
    public class PartReceivedEventArgs : IrcEventArgs
    {
        private String channelName;
        private String partMessage;
        private UserInfo user;

        public PartReceivedEventArgs(IrcLine BaseLine) : base(BaseLine)
        {
            user = new UserInfo(BaseLine);
            channelName = BaseLine.Parameters[0];
            
            if (BaseLine.Parameters.Length > 1)
                partMessage = BaseLine.Parameters[1];
        }

        public PartReceivedEventArgs(String ChannelName, UserInfo PartedUser) : base(PartedUser.Client)
        {
            channelName = ChannelName;
            user = PartedUser;
        }

        public String ChannelName
        {
            get { return channelName; }
        }

        public String PartMessage
        {
            get { return partMessage; }
        }

        public UserInfo User
        {
            get { return user; }
        }
    }
}
