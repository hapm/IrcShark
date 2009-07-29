using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp.Extended
{
    /// <summary>
    /// The different states a channel can be in.
    /// </summary>
    public enum ChannelStates { In, NotIn, Joining };

    public delegate void ChannelMessageEventHandler(Object sender, PrivateMessageReceivedEventArgs args);
    public delegate void ChannelNoticeEventHandler(Object sender, NoticeReceivedEventArgs args);
    public delegate void JoinedEventHandler(Object sender, JoinedEventArgs args);
    public delegate void UserJoinEventHandler(Object sender, UserJoinEventArgs args);
    public delegate void UserLeaveEventHandler(Object sender, UserLeaveEventArgs args);
    public delegate void TopicEventHanlder(Object sender, TopicEventArgs args);
    public delegate void BadNickEventHandler(Object sender, BadNickEventArgs args);
    public delegate void PartedEventHandler(Object sender, PartedEventArgs args);
    public delegate void ModeEventHandler(Object sender, ModeReceivedEventArgs args);

    /// <summary>
    /// Represents an irc channel.
    /// </summary>
    public class Channel : IIrcObject, IDisposable
    {
        /// <summary>
        /// This event is called when the channel receives a text emssage.
        /// </summary>
        public event ChannelMessageEventHandler ChannelMessage;
        /// <summary>
        /// This event is raised when the channel received a notice message.
        /// </summary>
        public event ChannelNoticeEventHandler ChannelNotice;
        /// <summary>
        /// This event is raised when the IrcClient successfully joined the channel.
        /// </summary>
        /// <remarks>This event will be only raised by yourself. See <see cref="UserJoin"/>for other users.</remarks>
        public event JoinedEventHandler Joined;
        /// <summary>
        /// This event is raised when a user joins the channel.
        /// </summary>
        public event UserJoinEventHandler UserJoin;
        /// <summary>
        /// This event is raised when a user leaves the channel.
        /// </summary>
        /// <remarks>This event is raised on any action, getting the user to leave: quit, part and kick.</remarks>
        public event UserLeaveEventHandler UserLeave;
        /// <summary>
        /// This event is raised if someone changed the channel topic.
        /// </summary>
        public event TopicEventHanlder TopicReceived;
        /// <summary>
        /// This event is raised if someone changed a mode in the channel.
        /// </summary>
        public event ModeEventHandler Mode;
        public event BadNickEventHandler OnBadNick;
        /// <summary>
        /// This event is raised if the IrcClient parted the channel.
        /// </summary>
        /// <remarks>This event will only be raised by yourself. See <see cref="UserLeave"/> for other users.</remarks>
        public event PartedEventHandler Parted;

        private IrcClient ClientValue;
        private List<ChannelUser> NickListValue;
        private String NameValue;
        private String TopicValue;
        private ChannelStates StatusValue;
        private NamesListener NamesListenerValue;
        private String KeyValue;
        private int UserLimitValue;
        private bool DisposedValue;

        /// <summary>
        /// Gives a ChannelUser for the given nickname if the user is in this channel.
        /// </summary>
        /// <value>the ChannelUser</value>
        public ChannelUser this[String nick]
        {
            get
            {
                foreach (ChannelUser u in NickListValue)
                {
                    if (u.NickName == nick) return u;
                }
                throw new IndexOutOfRangeException(String.Format("Couln't find nickname {0}", nick));
            }
        }

        public Channel(IrcClient Client, String Name)
        {
            ClientValue = Client;
            NameValue = Name;
            PrepareChannel();
        }

        public Channel(JoinReceivedEventArgs baseArgs)
        {
            ClientValue = baseArgs.Client;
            NameValue = baseArgs.ChannelName;
            PrepareChannel();
            PrepareJoining();
        }

        private void PrepareChannel()
        {
            StatusValue = ChannelStates.NotIn;
            NickListValue = new List<ChannelUser>();
            Client.JoinReceived += new JoinReceivedEventHandler(Client_JoinReceived);
            Client.PartReceived += new PartReceivedEventHandler(Client_PartReceived);
            Client.QuitReceived += new QuitReceivedEventHandler(Client_QuitReceived);
            Client.KickReceived += new KickReceivedEventHandler(Client_KickReceived);
            Client.NoticeReceived += new NoticeReceivedEventHandler(Client_NoticeReceived);
            Client.PrivateMessageReceived += new PrivateMessageReceivedEventHandler(Client_PrivateMessageReceived);
            Client.NumericReceived += new NumericReceivedEventHandler(Client_NumericReceived);
            Client.ModeReceived += new ModeReceivedEventHandler(Client_ModeReceived);
        }

        void Client_ModeReceived(Object sender, ModeReceivedEventArgs e)
        {
            if (e.AimArt != ModeArt.Channel) return;
            if (e.Aim != Name) return;
            foreach (Mode m in e.Modes)
            {
                switch (m.Flag.Char)
                {
                    case 'l': //Channel limit changed
                        break;

                    case 'k': //Channel key changed
                        if (m.Art == FlagArt.Set) KeyValue = m.Parameter;
                        else KeyValue = "";
                        break;

                    default: //no standard channelflag
                        if (Client.Standard.UserPrefixFlags.ContainsValue(m.Flag))
                        {
                            //We have a user specific flag here, so we expect parameter is a nickname
                            ChannelUser user;
                            try
                            {
                                user = this[m.Parameter];
                                user.UpdateMode(m);
                            }
                            catch (IndexOutOfRangeException ex)
                            {
                                //we do nothing else for this flag
                            }
                        }
                        break;
                }
            }
            if (Mode != null) Mode(this, e);
        }

        void Client_KickReceived(Object sender, KickReceivedEventArgs e)
        {
            if (Status != ChannelStates.In) return;
            if (e.ChannelName != Name) return;
            ChannelUser user;
            try
            {
                user = this[e.KickedName];
            }
            catch (IndexOutOfRangeException ex)
            {
                return;
            }
            if (UserLeave != null) UserLeave(this, new UserLeaveEventArgs(user, UserLeaveReason.Kicked));
            NickListValue.Remove(user);
        }

        void Client_NoticeReceived(Object sender, NoticeReceivedEventArgs e)
        {
            if (Status != ChannelStates.In) return;
            if (e.Destination != Name) return;
            if (ChannelNotice != null) ChannelNotice(this, e);
        }

        void Client_QuitReceived(Object sender, QuitReceivedEventArgs e)
        {
            if (Status != ChannelStates.In) return;
            ChannelUser user;
            try
            {
                user = this[e.User.NickName];
            }
            catch (IndexOutOfRangeException ex)
            {
                return;
            }
            if (UserLeave != null) UserLeave(this, new UserLeaveEventArgs(user, UserLeaveReason.Quit));
            NickListValue.Remove(user);            
        }

        private void PrepareJoining()
        {
            StatusValue = ChannelStates.Joining;
            NamesListenerValue = new NamesListener(Client);
            NamesListenerValue.NamesEnd += new NamesEndEventHandler(NamesListenerValue_NamesEnd);
        }

        void Client_NumericReceived(Object sender, NumericReceivedEventArgs e)
        {
            if (Status == ChannelStates.NotIn) return;
            switch (e.Numeric)
            {
                case 332:
                    if (e.BaseLine.Parameters[1] != Name) return;
                    TopicValue = e.BaseLine.Parameters[2];
                    break;
            }
        }

        void Client_JoinReceived(Object sender, JoinReceivedEventArgs e)
        {
            if (Disposed) return;
            if (e.ChannelName != Name) return;
            if (Status == ChannelStates.NotIn)
            {
                if (e.User.NickName != Client.MyUserInfo.NickName) return;
                PrepareJoining();
            }
            else if (Status == ChannelStates.In)
            {
                ChannelUser newUser = new ChannelUser(this, e.User.NickName);
                NickListValue.Add(newUser);
                if (UserJoin != null) UserJoin(this, new UserJoinEventArgs(this, newUser, e));
            }
        }

        void Client_PartReceived(Object sender, PartReceivedEventArgs e)
        {
            if (Disposed) return;
            if (e.ChannelName != Name) return;
            if (Status != ChannelStates.In) return;
            if (e.User.NickName != Client.MyUserInfo.NickName)
            {
                ChannelUser user = this[e.User.NickName];
                if (UserLeave != null) UserLeave(this, new UserLeaveEventArgs(user, UserLeaveReason.Parted));
                NickListValue.Remove(user);
            }
            else
            {
                StatusValue = ChannelStates.NotIn;
                if (Parted != null) Parted(this, new PartedEventArgs(this, Client));
                NickListValue.Clear();
                NamesListenerValue = null;
                TopicValue = "";
            }
        }

        /// <summary>
        /// Begin to join this channel.
        /// </summary>
        public void Join()
        {
            Client.Join(Name);
        }

        /// <summary>
        /// Beginn to part this channel.
        /// </summary>
        public void Part()
        {
            Client.Part(Name);
        }

        void NamesListenerValue_NamesEnd(Object sender, NamesEndEventArgs args)
        {
            if (Disposed) return;
            if (Status == ChannelStates.Joining) {
                if (args.ChannelName != Name) return;
                NickListValue.Clear();
                foreach (String name in args.Names)
                {
                    NickListValue.Add(new ChannelUser(this, name));
                }
                //NickListValue.AddRange(args.Names);
                StatusValue = ChannelStates.In;
                if (Joined != null) Joined(this, new JoinedEventArgs(this, Client));
            }
        }

        /// <summary>
        /// The current state of the Channel object.
        /// </summary>
        /// <value>the current state of the Channel object</value>
        public ChannelStates Status
        {
            get { return StatusValue; }
        }

        /// <summary>
        /// The key to join the channel.
        /// </summary>
        /// <value>the channel key</value>
        public String Key
        {
            get { return KeyValue; }
            set
            {
                if (Status == ChannelStates.In)
                {
                    Client.SendLine(String.Format("MODE {0} +k {1}", Name, value));
                }
                else
                    KeyValue = value;
            }
        }

        /// <summary>
        /// The maximum number of users what can be in this channel at the same time.
        /// </summary>
        /// <value>the number of users, 0 means endless</value>
        public int UserLimit
        {
            get { return UserLimitValue; }
            set
            {
                if (Status == ChannelStates.In)
                {
                    Client.SendLine(String.Format("MODE {0} +l {1}", Name, value));
                }
                else
                    UserLimitValue = value;
            }
        }

        void Client_PrivateMessageReceived(Object sender, PrivateMessageReceivedEventArgs e)
        {
            if (Disposed) return;
            if (e.Destination != Name) return;
            if (ChannelMessage != null) ChannelMessage(this, e);
        }

        void Client_OnTopic(IrcClient sender, TopicEventArgs e)
        {
            if (e.ChannelName != Name) return;
            TopicValue = e.Topic;
        }

        /// <summary>
        /// The name of the channel.
        /// </summary>
        /// <remarks>You can't rename a channel after you have created it.</remarks>
        /// <value>the channel name</value>
        public String Name
        {
            get { return NameValue; }
        }

        /// <summary>
        /// The current channel topic.
        /// </summary>
        /// <value>the current channel topic</value>
        public String Topic
        {
            get { return TopicValue; }
        }

        /// <summary>
        /// A list of all users currently in the channel.
        /// </summary>
        /// <remarks>If you never joined the channel, this list will be empty. If you part a channel this list will have the last known list of nicknames.</remarks>
        /// <value>a list of channel users</value>
        public ChannelUser[] NickList
        {
            get
            {
                return NickListValue.ToArray();
            }
        }

        /// <summary>
        /// Sends a text message to the channel.
        /// </summary>
        public void SendMessage(String Message)
        {
            Client.SendLine(String.Format("PRIVMSG {0} :{1}", Name, Message));
        }

        #region IIrcObject Member

        /// <summary>
        /// The IrcCLient this channel belongs to.
        /// </summary>
        /// <value>the IrcClient</value>
        public IrcClient Client
        {
            get { return ClientValue; }
        }

        #endregion

        #region IDisposable Members

        public bool Disposed
        {
            get { return DisposedValue; }
        }

        public void Dispose()
        {
            Client.JoinReceived -= new JoinReceivedEventHandler(Client_JoinReceived);
            Client.PartReceived -= new PartReceivedEventHandler(Client_PartReceived);
            Client.PrivateMessageReceived -= new PrivateMessageReceivedEventHandler(Client_PrivateMessageReceived);
            Client.NumericReceived -= new NumericReceivedEventHandler(Client_NumericReceived);
            Client.QuitReceived -= new QuitReceivedEventHandler(Client_QuitReceived);
            Client.KickReceived -= new KickReceivedEventHandler(Client_KickReceived);
            Client.NoticeReceived -= new NoticeReceivedEventHandler(Client_NoticeReceived);
            DisposedValue = true;
        }

        #endregion
    }
}
