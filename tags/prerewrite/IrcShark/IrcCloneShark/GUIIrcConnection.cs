using System;
using System.Collections.Generic;
using System.Text;
using IrcSharp;
using IrcSharp.Extended;
using IrcShark;

namespace IrcCloneShark
{
    /// <summary>
    /// Represents a graphical visualisation of an IrcConnection.
    /// </summary>
    /// <remarks>An instance of this class will automatically create windows for the different channels and querys from an IrcConnection.</remarks>
    public class GUIIrcConnection
    {
        public delegate void WindowOpenedEventHandler(GUIIrcConnection sender, WindowOpenedEventArgs args);

        public event WindowOpenedEventHandler WindowOpened;

        private IrcCloneSharkExtension ExtensionValue;
        private IrcConnection BaseConnectionValue;
        private ServerConfiguration ServerValue;
        private StatusWindow StatusValue;
        private DebugWindow DebugValue;
        private Dictionary<String, ChannelWindow> ChannelWindowsValue;
        private QueryWindowList QueryWindowsValue;
        private TextTheme ThemeValue;
        private WindowSwitchToolStrip SwitchStripValue;

        public GUIIrcConnection(IrcCloneSharkExtension ext, IrcConnection baseCon)
        {
            ExtensionValue = ext;
            BaseConnectionValue = baseCon;
            StatusValue = new StatusWindow(this);
            SwitchStripValue = new WindowSwitchToolStrip(this);
            MainForm.SwitchBar.Controls.Add(SwitchStripValue);
            StatusValue.Show();
            ChannelWindowsValue = new Dictionary<String, ChannelWindow>();
            QueryWindowsValue = new QueryWindowList();
            BaseConnectionValue.Channels.Joined += new JoinedEventHandler(Channels_Joined);
            BaseConnectionValue.Channels.Parted += new PartedEventHandler(Channels_Parted);
            BaseConnectionValue.PrivateMessageReceived += new PrivateMessageReceivedEventHandler(Client_PrivateMessageReceived);
        }

        void Client_PrivateMessageReceived(Object sender, PrivateMessageReceivedEventArgs e)
        {
            if (StatusValue.InvokeRequired)
            {
                StatusValue.Invoke(new PrivateMessageReceivedEventHandler(Client_PrivateMessageReceived), sender, e);
                return;
            }
            IrcClient client = (IrcClient)sender;
            if (e.Destination == client.CurrentNick)
            {
                if (e.IsCTCP && e.CTCPCommand != CTCPCommands.Action) return;
                //We received a private message
                QueryWindow query;
                if (QueryWindowsValue.HasOpenQuery(e.Sender)) return;
                //TODO: raise an event here for an incoming query-chat
                query = new QueryWindow(this, e);
                QueryWindowsValue.Add(query);
                query.Show();
                query.FormClosed += new System.Windows.Forms.FormClosedEventHandler(QueryWindow_FormClosed);
                if (WindowOpened != null) WindowOpened(this, new WindowOpenedEventArgs(query));
            }
        }

        void QueryWindow_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            QueryWindow query = (QueryWindow)sender;
            if (!QueryWindowsValue.Contains(query)) return;
            QueryWindowsValue.Remove(query);
        }

        void Channels_Parted(Object sender, PartedEventArgs e)
        {
            ChannelWindowsValue.Remove(e.Channel.Name);
        }

        void Channels_Joined(Object sender, JoinedEventArgs e)
        {
            if (StatusValue.InvokeRequired)
            {
                StatusValue.Invoke(new JoinedEventHandler(Channels_Joined), sender, e);
            }
            else
            {
                ChannelWindow chan;
                if (ChannelWindowsValue.ContainsKey(e.Channel.Name))
                {
                    chan = ChannelWindowsValue[e.Channel.Name];
                }
                else
                {
                    chan = new ChannelWindow(Status, e.Channel);
                    ChannelWindowsValue.Add(chan.Channel.Name, chan);
                    if (WindowOpened != null) WindowOpened(this, new WindowOpenedEventArgs(chan));
                }
                chan.Visible = true;
            }
        }

        public bool Debugging
        {
            get { return DebugValue != null; }
            set
            {
                if (value == true && DebugValue == null)
                {
                    DebugValue = new DebugWindow(this);
                    DebugValue.FormClosed += new System.Windows.Forms.FormClosedEventHandler(DebugValue_FormClosed);
                    DebugValue.Show();
                    if (WindowOpened != null) WindowOpened(this, new WindowOpenedEventArgs(DebugValue));
                }
                else if (value == false && DebugValue != null)
                {
                    DebugValue.FormClosed -= new System.Windows.Forms.FormClosedEventHandler(DebugValue_FormClosed);
                    DebugValue.Close();
                    DebugValue.Dispose();
                    DebugValue = null;
                }
            }
        }

        void DebugValue_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            Debugging = false;
        }

        /// <summary>
        /// All ChannelWindows for this connection.
        /// </summary>
        /// <remarks>This list doesn't need to have the same entries a BaseConnection.Channels. There can me more entries if you leaved a channel but didn't close the associated ChannelWindow.</remarks>
        /// <value>A dictionary of ChannelWindows.</value>
        public ConstantDictionary<String,ChannelWindow> ChannelWindows
        {
            get { return new ConstantDictionary<String,ChannelWindow>(ChannelWindowsValue); }
        }

        public QueryWindow[] QueryWindows
        {
            get { return QueryWindowsValue.ToArray(); }
        }

        public MainForm MainForm
        {
            get { return ExtensionValue.MainForm; }
        }

        public StatusWindow Status
        {
            get { return StatusValue; }
        }

        public TextTheme Theme
        {
            get { return ThemeValue; }
            set { ThemeValue = value; }
        }

        public ServerConfiguration Server
        {
            get { return ServerValue; }
            set { ServerValue = value; }
        }

        /// <summary>
        /// The IrcConnection this GUIIrcConnection belongs to.
        /// </summary>
        /// <value>the IrcConnection.</value>
        public IrcConnection BaseConnection
        {
            get { return BaseConnectionValue; }
        }

        /// <summary>
        /// An unique identification number for this connection.
        /// </summary>
        /// <remarks>Use this ID to refere to this connection. The ID is valid until the connection is destroyed or until IrcShark shutdown.</remarks>
        public int ConnectionID
        {
            get { return BaseConnectionValue.ConnectionID; }
        }

        public void Connect()
        {
            if (BaseConnectionValue.IsConnected) return;
            if (Server == null) return;
            Server.ApplyTo(BaseConnectionValue);
            BaseConnectionValue.Connect();
        }
    }
}
