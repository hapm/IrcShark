using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IrcCloneShark
{
    public partial class NetworkManagerForm : Form
    {
        public NetworkManagerForm()
        {
            InitializeComponent();
        }

        public NetworkManagerForm(IrcShark.IrcSharkApplication app)
        {
            InitializeComponent();
            NetManager.BoundedNetworkManager = app.Servers;   
        }

        private void SrvManager_ServerSelected(object sender, IrcShark.ServerSelectedEventArgs args)
        {
            BaseWindow window;
            window = (BaseWindow)this.Owner.ActiveMdiChild;
            window.AssociatedConnection.Server = args.SelectedServer;
            Close();
        }

        private void NetworkManagerForm_Load(object sender, EventArgs e)
        {

        }
    }
}
