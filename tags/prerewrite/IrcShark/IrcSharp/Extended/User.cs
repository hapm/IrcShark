using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp.Extended
{
    public class User : UserInfo
    {
        //TODO: Add events for private messages/notices/actions etc.

        public event EventHandler IsValidChanged;

        private List<String> channels;
        private DateTime lastUpdate;
        private bool isAway;
        private bool isValid;

        public User(UserInfo baseInfo, String[] chans) : base(baseInfo)
        {
            isValid = true;
            channels = new List<String>();
            channels.AddRange(chans);
            Client.JoinReceived += new JoinReceivedEventHandler(Client_JoinReceived);
            Client.PartReceived += new PartReceivedEventHandler(Client_PartReceived);
            Client.QuitReceived += new QuitReceivedEventHandler(Client_QuitReceived);
            Client.NickChangeReceived += new NickChangeReceivedEventHandler(Client_NickChangeReceived);
            Client.KickReceived += new KickReceivedEventHandler(Client_KickReceived);
        }

        void Client_KickReceived(Object sender, KickReceivedEventArgs e)
        {
            if (e.KickedName != NickName) return;
            if (!IsIn(e.ChannelName)) return;
            channels.Remove(e.ChannelName);
            if (channels.Count > 0) return;
            isValid = false;
            if (IsValidChanged != null) IsValidChanged(this, new EventArgs());
        }

        void Client_NickChangeReceived(Object sender, NickChangeReceivedEventArgs e)
        {
            if (e.User != (UserInfo)this) return;
            NickName = e.NewNick;
        }

        void Client_QuitReceived(Object sender, QuitReceivedEventArgs e)
        {
            if (e.User != (UserInfo)this) return;
            isValid = false;
            if (IsValidChanged != null) IsValidChanged(this, new EventArgs());
            channels.Clear();
        }

        void Client_PartReceived(Object sender, PartReceivedEventArgs e)
        {
            if (e.User != (UserInfo)this) return;
            if (!IsIn(e.ChannelName)) return;
            channels.Remove(e.ChannelName);
            if (channels.Count > 0) return;
            isValid = false;
            if (IsValidChanged != null) IsValidChanged(this, new EventArgs());
        }

        void Client_JoinReceived(Object sender, JoinReceivedEventArgs e)
        {
            if (e.User == (UserInfo)this) channels.Add(e.ChannelName);
        }

        public bool IsIn(String ChannelName)
        {
            foreach (String currentChan in channels)
            {
                if (currentChan == ChannelName) return true;
            }
            return false;
        }

        public String[] Channels
        {
            get { return channels.ToArray(); }
        }

        public DateTime LastUpdate
        {
            get { return lastUpdate; }
        }

        public bool IsAway
        {
            get { return isAway; }
        }

        public bool IsValid
        {
            get { return isValid; }
        }
    }
}
