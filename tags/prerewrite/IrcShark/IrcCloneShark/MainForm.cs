using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using IrcSharp.Extended;
using IrcShark;

namespace IrcCloneShark
{
    public partial class MainForm : Form
    {
        private GUIIrcConnectionList ConnectionsValue;
        private IrcCloneSharkExtension ExtensionValue;

        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(IrcCloneSharkExtension ext)
        {
            InitializeComponent();
            ExtensionValue = ext;
            ConnectionsValue =  new GUIIrcConnectionList(ext);
        }

        void IrcConnections_Added(object sender, AddedEventArgs<IrcConnection> args)
        {
            ConnectionsValue.Add(new GUIIrcConnection(ExtensionValue, args.Item));
        }

        public GUIIrcConnection[] Connections
        {
            get { return ConnectionsValue.ToArray(); }
        }

        public ToolStripPanel SwitchBar
        {
            get { return SwitchBarValue; }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (GUIIrcConnection con in Connections)
            {
                con.BaseConnection.Close();
                e.Cancel = false;
            }
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild is BaseWindow)
            {
                BaseWindow child = (BaseWindow)ActiveMdiChild;
                child.AssociatedConnection.Connect();
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void HaltMenuItem_Click(object sender, EventArgs e)
        {
            return;
        }

        private void SelectServerItem_Click(object sender, EventArgs e)
        {
            NetworkManagerForm srvman = new NetworkManagerForm(ExtensionValue.IrcShark);
            AddOwnedForm(srvman);
            srvman.TopLevel = true;
            srvman.Show();
        }

        private void IRCSharkGUItem_CheckedChanged(object sender, EventArgs e)
        {
            ExtensionValue.IrcShark.ShowGUI = IRCSharkGUItem.Checked;
        }

        private void FileMenu_DropDownOpening(object sender, EventArgs e)
        {
            BaseWindow win = (BaseWindow)ActiveMdiChild;
            ConnectItem.Enabled = !win.AssociatedConnection.BaseConnection.IsConnected;
            DisconnectItem.Enabled = win.AssociatedConnection.BaseConnection.IsConnected;
        }

        private void NewConnectionMenuItem_Click(object sender, EventArgs e)
        {
            ExtensionValue.IrcShark.Connections.Add(new IrcConnection());
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            IrcConnectionList cons = ExtensionValue.IrcShark.Connections;
            if (cons.Count == 0)
            {
                cons.Add(new IrcConnection());
            }
            foreach (IrcConnection con in cons)
            {
                ConnectionsValue.Add(new GUIIrcConnection(ExtensionValue, con));
            }
            cons.Added += new AddedEventHandler<IrcConnection>(IrcConnections_Added);
            if (ExtensionValue.IrcShark.Servers.Networks.Count > 0 && ExtensionValue.IrcShark.Servers.Networks[0].Servers.Count > 0)
                ConnectionsValue[0].Server = ExtensionValue.IrcShark.Servers.Networks[0].Servers[0];
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (GUIIrcConnection con in Connections)
            {
                con.BaseConnection.Close();
            }
            Application.Exit();
        }

        private void DebugMenu_DropDownOpening(object sender, EventArgs e)
        {
            try
            {
                BaseWindow win = (BaseWindow)ActiveMdiChild;
                debugRawToolStripMenuItem.Checked = win.AssociatedConnection.Debugging;
                debugRawToolStripMenuItem.Enabled = true;
            }
            catch (Exception)
            {
                debugRawToolStripMenuItem.Checked = false;
                debugRawToolStripMenuItem.Enabled = false;
            }
        }

        private void debugRawToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild is BaseWindow)
            {
                BaseWindow win = (BaseWindow)ActiveMdiChild;
                win.AssociatedConnection.Debugging = !win.AssociatedConnection.Debugging;
            }
        }

        private void überIRCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.Show();
        }
    }
}
