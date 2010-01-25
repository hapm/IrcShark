using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using IrcSharp;
using IrcSharp.Extended;
using System.IO;

namespace IrcShark
{
    public partial class StatusPanel : SettingPanel
    {
        private delegate void LogDelegate(LogMessage msg);

        public StatusPanel()
        {
            InitializeComponent();
            Init();
        }

        public StatusPanel(IrcSharkApplication app) : base(app)
        {
            InitializeComponent();
            Disposed += new EventHandler(StatusPanel_Disposed);
            Init();
        }

        void StatusPanel_Disposed(object sender, EventArgs e)
        {
            IrcShark.Connections.Added -= new AddedEventHandler<IrcConnection>(Connections_Added);
            IrcShark.Connections.Removed -= new RemovedEventHandler<IrcConnection>(Connections_Removed);
            IrcShark.Logger.LogLine -= new LogLineDelegate(Logger_LogLine);
        }

        void Init()
        {
            Text = "Status";
            IrcShark.Connections.Added += new AddedEventHandler<IrcConnection>(Connections_Added);
            IrcShark.Connections.Removed += new RemovedEventHandler<IrcConnection>(Connections_Removed);
            IrcShark.Logger.LogLine += new LogLineDelegate(Logger_LogLine);
            LoadList();
            LoadLog();
        }

        private void LoadLog()
        {
            if (IrcShark.CurrentLogFile != "")
            {
                try
                {
                    StreamReader reader = new StreamReader(new FileStream(IrcShark.CurrentLogFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
                    logBox.AppendText(reader.ReadToEnd());
                    reader.Close();
                    logBox.Select(logBox.Text.Length, 0);
                    logBox.ScrollToCaret();
                }
                catch (IOException) { }
            }
        }

        void Logger_LogLine(object sender, LogMessage args)
        {
            Log(args);
        }

        void Log(LogMessage msg)
        {
            if (logBox.InvokeRequired)
                logBox.Invoke(new LogDelegate(Log), msg);
            else
            {
                string.Format("[{0}][{1}: {2}] {3}", msg.Created.ToShortTimeString(), msg.Level, msg.Subject, msg.Message);
                logBox.Select(logBox.Text.Length, 0);
                logBox.ScrollToCaret();
            }
        }

        void Connections_Removed(object sender, RemovedEventArgs<IrcConnection> args)
        {
            foreach (ConnectionStateListViewItem item in ConnectionListView.Items)
            {
                if (item.Connection == args.Item)
                {
                    ConnectionListView.Items.Remove(item);
                    break;
                }
            }
        }

        void Connections_Added(object sender, AddedEventArgs<IrcConnection> args)
        {
            ConnectionStateListViewItem item = new ConnectionStateListViewItem(args.Item);
            ConnectionListView.Items.Add(item);
        }

        public void LoadList()
        {
            foreach (IrcConnection con in IrcShark.Connections)
            {
                ListViewItem item = new ConnectionStateListViewItem(con);
                ConnectionListView.Items.Add(item);
            }
        }

        private void ConnectionMenu_Opening(object sender, CancelEventArgs e)
        {
            ConnectMenuItem.Visible = false;
            DisconnectMenuItem.Visible = false;
            NetworkMenuItem.Enabled = ConnectionListView.SelectedItems.Count == 1;
            RemoveConnectionMenuItem.Enabled = ConnectionListView.SelectedItems.Count > 0;
            foreach (ConnectionStateListViewItem item in ConnectionListView.SelectedItems)
            {
                if (item.Connection.IsConnected)
                    DisconnectMenuItem.Visible = true;
                else
                    ConnectMenuItem.Visible = true;
            }
            ToolStripMenuItem netItem;
            NetworkMenuItem.DropDownItems.Clear();
            foreach (Network n in IrcShark.Servers.Networks)
            {
                if (n.Servers.Count == 0) 
                    continue;
                netItem = new ToolStripMenuItem(n.Name);
                netItem.Tag = n;
                NetworkMenuItem.DropDownItems.Add(netItem);
                netItem.Click += new EventHandler(NetworkItem_Click);
            }
        }

        void NetworkItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            Network n = (Network)item.Tag;
            if (ConnectionListView.SelectedItems.Count != 1)
                return;
            ConnectionStateListViewItem lvItem = (ConnectionStateListViewItem)ConnectionListView.SelectedItems[0];
            try
            {
                n.Servers[0].ApplyTo(lvItem.Connection);
            }
            catch (System.Net.Sockets.SocketException)
            {
                MessageBox.Show(this, "The specified address couldnt be resolved!", "Couldn't apply the network");
            }
        }

        private void NewConnectionMenuItem_Click(object sender, EventArgs e)
        {
            IrcConnection con = new IrcConnection();
            IrcShark.Connections.Add(con);
        }

        private void DisconnectMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ConnectionStateListViewItem item in ConnectionListView.SelectedItems)
            {
                if (item.Connection.IsConnected)
                    item.Connection.Close(); //TODO change to quit method when implemented
            }
        }

        private void ConnectMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ConnectionStateListViewItem item in ConnectionListView.SelectedItems)
            {
                if (!item.Connection.IsConnected)
                {
                    try
                    {
                        item.Connection.Connect();
                    }
                    catch (Exception)
                    {}
                }
            }
        }

        private void ConnectionListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadChannelList();
        }

        private void LoadChannelList()
        {
            channelList.Items.Clear();
            if (ConnectionListView.SelectedItems.Count != 1)
            {
                channelList.Enabled = false;
                return;
            }
            ConnectionStateListViewItem item = (ConnectionStateListViewItem)ConnectionListView.SelectedItems[0];
            if (!item.Connection.IsLoggedIn)
                return;
            foreach (KeyValuePair<string, Channel> ch in item.Connection.Channels)
            {
                string pre = "";
                if (ch.Value.Status == ChannelStates.In)
                {
                    pre = new string(ch.Value[item.Connection.CurrentNick].Prefixes);
                }
                if (pre == "")
                    channelList.Items.Add(string.Format("{0} ({1})", ch.Key, pre));
                else
                    channelList.Items.Add(string.Format("{0}", ch.Key));
            }
        }
    }
}
