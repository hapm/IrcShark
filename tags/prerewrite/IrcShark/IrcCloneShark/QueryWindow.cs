using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using IrcSharp;
using IrcSharp.Extended;

namespace IrcCloneShark
{
    public partial class QueryWindow : IrcCloneShark.BaseWindow
    {
        UserInfo BoundedUserValue;

        public QueryWindow()
        {
            InitializeComponent();
        }

        public QueryWindow(GUIIrcConnection baseCon, UserInfo user) : base(baseCon)
        {
            BoundedUserValue = user;
            IrcConnection con = baseCon.BaseConnection;
            con.PrivateMessageReceived += new PrivateMessageReceivedEventHandler(Client_PrivateMessageReceived);
            con.NickChangeReceived += new NickChangeReceivedEventHandler(Client_NickChangeReceived);
            con.QuitReceived += new QuitReceivedEventHandler(Client_QuitReceived);
            InitializeComponent();
            Text = user.NickName;
            MdiParent = baseCon.Status.MdiParent;
        }

        public QueryWindow(GUIIrcConnection baseCon, PrivateMessageReceivedEventArgs args)
            : base(baseCon)
        {
            if (args.Destination != args.Client.CurrentNick) throw new ArgumentOutOfRangeException("msg", "Received private message is no direct message");
            BoundedUserValue = args.Sender;
            IrcConnection con = baseCon.BaseConnection;
            con.PrivateMessageReceived += new PrivateMessageReceivedEventHandler(Client_PrivateMessageReceived);
            con.NickChangeReceived += new NickChangeReceivedEventHandler(Client_NickChangeReceived);
            con.QuitReceived += new QuitReceivedEventHandler(Client_QuitReceived);
            InitializeComponent();
            Text = args.Sender.NickName;
            MdiParent = baseCon.Status.MdiParent;
            Client_PrivateMessageReceived(args.Client, args);
        }

        void Client_QuitReceived(Object sender, QuitReceivedEventArgs e)
        {
            if (!e.User.Equals(BoundedUser)) return;
            AddLine(String.Format("{0} has quit from irc", BoundedUser.NickName));
        }

        void Client_NickChangeReceived(Object sender, NickChangeReceivedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new NickChangeReceivedEventHandler(Client_NickChangeReceived), sender, e);
                return;
            }
            if (!e.User.Equals(BoundedUser)) return;
            BoundedUserValue = new UserInfo(e.NewNick, e.User.Ident, e.User.Host, e.Client);
            AddLine(String.Format("{0} changed his nick to {1}", e.User.NickName, e.NewNick));
            Text = e.NewNick;
        }

        void Client_PrivateMessageReceived(Object sender, PrivateMessageReceivedEventArgs args)
        {
            if (args.Destination != base.AssociatedConnection.BaseConnection.CurrentNick) return;
            if (!args.Sender.Equals(BoundedUser)) return;
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

        public UserInfo BoundedUser
        {
            get { return BoundedUserValue; }
        }

        private void QueryWindow_Input(BaseWindow sender, InputEventArgs args)
        {
            AssociatedConnection.BaseConnection.SendLine(String.Format("PRIVMSG {0} :{1}", BoundedUser.NickName, args.Line));
            AddLine(String.Format("[{0:T}] <{1}> {2}", DateTime.Now, AssociatedConnection.BaseConnection.CurrentNick, args.Line));
        }

        private void QueryWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            AssociatedConnection.BaseConnection.PrivateMessageReceived -= new PrivateMessageReceivedEventHandler(Client_PrivateMessageReceived);
            AssociatedConnection.BaseConnection.NickChangeReceived -= new NickChangeReceivedEventHandler(Client_NickChangeReceived);
            AssociatedConnection.BaseConnection.QuitReceived -= new QuitReceivedEventHandler(Client_QuitReceived);
        }
    }
}
