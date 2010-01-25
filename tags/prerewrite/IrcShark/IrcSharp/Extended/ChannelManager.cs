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
    //TODO: Folgende Events sind doppelt:
    // public delegate void JoinedEventHandler(Object sender, JoinedEventArgs e);
    // public delegate void PartedEventHandler(Object sender, PartedEventArgs e);

    public class ChannelManager : Dictionary<String, Channel>, IIrcObject
    {
        private IrcClient client;

        public event JoinedEventHandler Joined;
        public event PartedEventHandler Parted;

        public ChannelManager(IrcClient BaseClient)
        {
            client = BaseClient;
            Client.JoinReceived += new JoinReceivedEventHandler(Client_JoinReceived);
        }

        private void Client_JoinReceived(Object sender, JoinReceivedEventArgs e)
        {
            if (e.User.NickName == Client.MyUserInfo.NickName)
            {
                if (ContainsKey(e.ChannelName)) return;
                Channel newChannel = new Channel(e);
                newChannel.Joined += new JoinedEventHandler(ChannelManager_Joined);
                newChannel.Parted += new PartedEventHandler(ChannelManager_Parted);
                Add(newChannel.Name, newChannel);
                e.Handled = true;
            }
        }

        void ChannelManager_Joined(Object sender, JoinedEventArgs args)
        {
            if (!ContainsKey(args.Channel.Name))
            {
                Add(args.Channel.Name, args.Channel);
            }
            if (Joined != null) Joined(this, args);
        }

        void ChannelManager_Parted(Object sender, PartedEventArgs args)
        {
            if (!ContainsKey(args.Channel.Name)) return;
            if (Parted != null) Parted(this, args);
            Remove(args.Channel.Name);
            args.Channel.Dispose();
        }

        public Channel[] ChannelsByList(String[] ChannelNames)
        {
            List<Channel> result = new List<Channel>();
            foreach (String ch in ChannelNames)
            {
                if (ContainsKey(ch)) result.Add(this[ch]);
            }
            return result.ToArray();
        }

        #region IIrcObject Member

        public IrcClient Client
        {
            get { return client; }
        }

        #endregion
    }
}
