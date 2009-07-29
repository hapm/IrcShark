using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using IrcSharp;
using IrcSharp.Extended;
using IrcShark;

namespace IrcCloneShark
{
    public partial class ChannelWindow : BaseWindow
    {
        private Channel ChannelValue;

        private class ChannelUserComparer : Comparer<Object>
        {
            public override int Compare(object x, object y)
            {
                //although we inherited from Comparer<Object> we only compare ChannelUser instances
                if (x is ChannelUser && y is ChannelUser)
                {
                    ChannelUser cu1, cu2;
                    cu1 = (ChannelUser)x;
                    cu2 = (ChannelUser)y;
                    //first we'll lookup the indexes of the prefixes
                    char pre1 = '\0', pre2 = '\0';
                    if (cu1.Prefixes.Length > 0) pre1 = cu1.Prefixes[0];
                    if (cu2.Prefixes.Length > 0) pre2 = cu2.Prefixes[0];
                    if (pre1 != pre2)
                    {
                        //first be sure that both have a prefix
                        if (pre1 == '\0') return 1;
                        if (pre2 == '\0') return -1;
                        //look up the prefix index
                        char[] prefixes = cu1.Client.Standard.UserPrefixes;
                        for (int i = 0; i < prefixes.Length; i++)
                        {
                            if (prefixes[i] == pre1) return -1;
                            if (prefixes[i] == pre2) return 1;
                        }
                    }
                    return StringComparer.CurrentCultureIgnoreCase.Compare(cu1.NickName, cu2.NickName);
                }
                else
                    return 0;
            }
        }

        public ChannelWindow()
        {
            InitializeComponent();
            SideList.SortComparer = new ChannelUserComparer();
            SideList.Sorted = true;
        }

        public ChannelWindow(StatusWindow baseCon, Channel baseChannel) : base(baseCon.AssociatedConnection)
        {
            InitializeComponent();
            SideList.SortComparer = new ChannelUserComparer();
            ChannelValue = baseChannel;
            Text = baseChannel.Name;
            MdiParent = baseCon.MdiParent;
            Channel.ChannelMessage += new ChannelMessageEventHandler(Channel_ChannelMessage);
            Channel.ChannelNotice += new ChannelNoticeEventHandler(Channel_ChannelNotice);
            Channel.Joined += new JoinedEventHandler(Channel_Joined);
            Channel.Parted += new PartedEventHandler(Channel_Parted);
            Channel.UserJoin += new UserJoinEventHandler(Channel_UserJoin);
            Channel.UserLeave += new UserLeaveEventHandler(Channel_UserLeave);
            Channel.Mode += new ModeEventHandler(Channel_Mode);
            Input += new InputEventHandler(ChannelWindow_Input);
            Disposed += new EventHandler(ChannelWindow_Disposed);
            if (Channel.Status == ChannelStates.In)
            {
                SideList.Items.AddRange(Channel.NickList);
                SideList.Sorted = true;
                AddLine(String.Format("You joined {0}", Channel.Name));
                if (Channel.Topic != "") AddLine(String.Format("Current Topic is: {0}", Channel.Topic));
            }
        }

        void Channel_Mode(Object sender, ModeReceivedEventArgs args)
        {
            if (SideList.InvokeRequired) SideList.Invoke(new ModeEventHandler(Channel_Mode), sender, args);
            else {
                SideList.RefreshItems();
                StringBuilder modeLine = new StringBuilder();
                for (int i = 1; i < args.BaseLine.Parameters.Length; i++)
                {
                    modeLine.Append(args.BaseLine.Parameters[i]);
                    modeLine.Append(' ');
                }
                AddLine(String.Format("{0} sets mode: {1}", args.Setter, modeLine));
            }
        }

        void Channel_UserLeave(Object sender, UserLeaveEventArgs args)
        {
            if (SideList.InvokeRequired)
                SideList.Invoke(new UserLeaveEventHandler(Channel_UserLeave), sender, args);
            else
                SideList.Items.Remove(args.ChannelUser);
        }

        void Channel_UserJoin(Object sender, UserJoinEventArgs args)
        {
            if (SideList.InvokeRequired)
                SideList.Invoke(new UserJoinEventHandler(Channel_UserJoin), sender, args);
            else
                SideList.Items.Add(args.ChannelUser);
        }

        void Channel_Parted(Object sender, PartedEventArgs args)
        {
            AddLine(String.Format("You parted {0}", Channel.Name));
        }

        void Channel_ChannelNotice(Object sender, NoticeReceivedEventArgs args)
        {
            AddLine(String.Format("[{0:T}] NOTICE <{1}> {2}", DateTime.Now, new UserInfo(args.Sender, args.Client).NickName, args.Message));
        }

        void ChannelWindow_Disposed(object sender, EventArgs e)
        {
            if (Channel == null) return;
            Channel.ChannelMessage -= new ChannelMessageEventHandler(Channel_ChannelMessage);
            Channel.ChannelNotice -= new ChannelNoticeEventHandler(Channel_ChannelNotice);
            Channel.Joined -= new JoinedEventHandler(Channel_Joined);
            Channel.Parted -= new PartedEventHandler(Channel_Parted);
            Channel.UserJoin -= new UserJoinEventHandler(Channel_UserJoin);
            Channel.Mode -= new ModeEventHandler(Channel_Mode);
            Channel.UserLeave -= new UserLeaveEventHandler(Channel_UserLeave);
            ChannelValue = null;
        }

        void Channel_Joined(Object sender, JoinedEventArgs args)
        {
            if (SideList.InvokeRequired)
            {
                SideList.Invoke(new JoinedEventHandler(Channel_Joined), sender, args);
            }
            else
            {
                SideList.Items.Clear();
                SideList.Items.AddRange(Channel.NickList);
                SideList.Sorted = true;
                AddLine(String.Format("You joined {0}", Channel.Name));
                if (Channel.Topic != "") AddLine(String.Format("Current Topic is: {0}", Channel.Topic));
            }
        }

        void ChannelWindow_Input(BaseWindow sender, InputEventArgs args)
        {
            Channel.SendMessage(args.Line);
            AddLine(String.Format("[{0:T}] <{1}> {2}", DateTime.Now, Channel.Client.CurrentNick, args.Line));
        }

        void Channel_ChannelMessage(Object sender, PrivateMessageReceivedEventArgs args)
        {
            if (args.IsCTCP && args.CTCPCommand != CTCPCommands.Action) return;
            String Line;
            String format = "[{0:T}] <{1}> {2}";
            Line = args.Message;
            if (args.IsCTCP)
            {
                format = "[{0:T}] {1} {2}";
                Line = args.CTCPParameters;
            }
            AddLine(String.Format(format, DateTime.Now, args.Sender.NickName, Line));
        }

        void Channel_Join(Channel sender, JoinReceivedEventArgs args)
        {
            AddLine(String.Format("{0} joined the channel ({1})", args.User.NickName, args.User.ToString()));            
        }

        public Channel Channel
        {
            get { return ChannelValue; }
        }

        private void ChannelWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Channel.Part();
            Dispose();
        }
    }
}
