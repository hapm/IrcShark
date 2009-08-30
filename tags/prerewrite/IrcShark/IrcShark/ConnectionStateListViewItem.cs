using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace IrcShark
{
    class ConnectionStateListViewItem : ListViewItem
    {
        delegate void UpdateStatusDelegate();
        IrcSharp.Extended.IrcConnection connection;

        public ConnectionStateListViewItem(IrcSharp.Extended.IrcConnection con)
        {
            Tag = con;
            Text = con.ConnectionID.ToString();
            ListViewSubItem subItem;
            subItem = new ListViewSubItem(this, con.NetworkName);
            if (subItem.Text == "")
                subItem.Text = "[Unnamed]";
            SubItems.Add(subItem);
            subItem = new ListViewSubItem(this, "");
            SubItems.Add(subItem);
            UpdateStatus();
            con.Connected += new IrcSharp.ConnectEventHandler(Client_OnConnect);
            con.Login += new IrcSharp.LoginEventHandler(Client_OnLogin);
        }

        void Client_OnLogin(object sender, IrcSharp.LoginEventArgs e)
        {
            UpdateStatus();
        }

        void Client_OnConnect(object sender, IrcSharp.ConnectEventArgs e)
        {
            UpdateStatus();
        }

        void UpdateStatus()
        {
            if (this.ListView == null) 
                return;
            if (this.ListView.InvokeRequired)
            {
                ListView.Invoke(new UpdateStatusDelegate(UpdateStatus));
                return;
            }
            IrcSharp.Extended.IrcConnection con = Connection;
            SubItems[0].Text = con.ConnectionID.ToString();
            if (!string.IsNullOrEmpty(con.NetworkName))
                SubItems[1].Text = Connection.NetworkName;
            else
                SubItems[1].Text = "[Unnamed]";
            if (con.IsLoggedIn)
                SubItems[2].Text = String.Format("Connected, {0} channel open", con.Channels.Count);
            else if(con.IsConnected)
                SubItems[2].Text = "Connected, logging in...";
            else
                SubItems[2].Text = "Disconnected";
        }

        public IrcSharp.Extended.IrcConnection Connection
        {
            get
            {
                return (IrcSharp.Extended.IrcConnection)Tag;
            }
        }
    }
}
