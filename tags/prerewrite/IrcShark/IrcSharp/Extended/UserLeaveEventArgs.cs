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

namespace IrcSharp.Extended
{
    public enum UserLeaveReason
    {
        Parted,
        Quit,
        Kicked
    }

    public class UserLeaveEventArgs : EventArgs
    {
        private ChannelUser user;
        private UserLeaveReason reason;

        public UserLeaveEventArgs(ChannelUser user, UserLeaveReason reason)
        {
            this.user = user;
            this.reason = reason;
        }

        public Channel Channel
        {
            get { return user.Channel; }
        }

        public ChannelUser ChannelUser
        {
            get { return user; }
        }

        public UserLeaveReason Reason
        {
            get { return reason; }
        }
    }
}
