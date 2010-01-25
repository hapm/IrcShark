using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace IrcShark
{


    public partial class NetworkManagerPanel : SettingPanel
    {
        private NetworkManager BoundedNetworkManagerValue;
        private List<TabPage> TabPages;

        public event ServerSelectedEventHandler ServerSelected;

        public NetworkManagerPanel()
        {
            TabPages = new List<TabPage>();
            InitializeComponent();
            Text = "Networks";
        }

        public NetworkManagerPanel(IrcSharkApplication app) : base(app)
        {
            TabPages = new List<TabPage>();
            InitializeComponent();
            Text = "Networks";
            BoundedNetworkManager = app.Servers;
        }

        public NetworkManagerPanel(IrcSharkApplication app, NetworkManager servers)
            : base(app)
        {
            BoundedNetworkManager = servers;
            InitializeComponent();
            LoadSettingTabs();
        }

        [Browsable(false)]
        public NetworkManager BoundedNetworkManager
        {
            get { return BoundedNetworkManagerValue; }
            set 
            { 
                BoundedNetworkManagerValue = value;
                ReloadTree();
                LoadSettingTabs();
            }
        }

        private void ReloadTree()
        {
            ServerList.Nodes.Clear();
            if (BoundedNetworkManager == null) return;
            TreeNode root = TreeNodeFromConfigUnit(BoundedNetworkManager);
            ServerList.Nodes.Add(root);
            ServerList.SelectedNode = root;
            LoadConfigUnit((NetworkManagerConfigurationUnit)root.Tag);
        }

        private TreeNode TreeNodeFromConfigUnit(NetworkManagerConfigurationUnit unit)
        {
            TreeNode result = new TreeNode(unit.Name);
            result.Tag = unit;
            if (unit is NetworkManager)
            {
                NetworkManager unit2 = (NetworkManager)unit;
                foreach (Network subUnit in unit2.Networks)
                {
                    result.Nodes.Add(TreeNodeFromConfigUnit(subUnit));
                }
            }
            else if (unit is Network)
            {
                Network unit2 = (Network)unit;
                foreach (ServerConfiguration subUnit in unit2.Servers)
                {
                    result.Nodes.Add(TreeNodeFromConfigUnit(subUnit));
                }
            }
            return result;
        }

        private void LoadConfigUnit(NetworkManagerConfigurationUnit unit)
        {
            SettingTabs.TabPages.Clear();
            foreach (TabPage tab in TabPages)
            {
                NetworkManagerSettingPanel panel = (NetworkManagerSettingPanel)tab.Controls[0];
                if (panel.IsVisibleFor(unit))
                {
                    panel.CurrentConfigurationUnit = unit;
                    SettingTabs.TabPages.Add(tab);
                }
            }
        }

        private void LoadSettingTabs()
        {
            TabPages.Clear();
            if (BoundedNetworkManager == null) return;
            foreach (Type panelType in BoundedNetworkManager.NetworkManagerSettingPanels)
            {
                ConstructorInfo con = panelType.GetConstructor(new Type[] { typeof(IrcSharkApplication) });
                NetworkManagerSettingPanel panel = (NetworkManagerSettingPanel)con.Invoke(new Object[] { IrcShark });
                if (panel is ServerConfigurationPanel)
                {
                    ServerConfigurationPanel p = (ServerConfigurationPanel)panel;
                    p.ServerSelected += new ServerSelectedEventHandler(HandleServerSelected);
                }
                TabPage tab = new TabPage(panel.Text);
                tab.Controls.Add(panel);
                TabPages.Add(tab);
            }
        }

        void HandleServerSelected(Object sender, ServerSelectedEventArgs args)
        {
            if (ServerSelected != null) ServerSelected(this, args);
        }

        private void ServerList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.IsSelected)
            {
                if (e.Node.Tag is NetworkManagerConfigurationUnit) LoadConfigUnit((NetworkManagerConfigurationUnit)e.Node.Tag);
            }
        }

        private void ServerListPopup_Opening(object sender, CancelEventArgs e)
        {
            if (ServerList.SelectedNode == null)
            {
                e.Cancel = true;
                return;
            }
            DeleteNetworkItem.Enabled = !(ServerList.SelectedNode.Tag is NetworkManager);
            NewServerItem.Enabled = !(ServerList.SelectedNode.Tag is NetworkManager);
            DeleteServerItem.Enabled = (ServerList.SelectedNode.Tag is ServerConfiguration);
        }

        private void NewNetworkItem_Click(object sender, EventArgs e)
        {
            Network newNet = new Network();
            newNet.Name = "New Network";
            BoundedNetworkManager.Networks.Add(newNet);
            TreeNode newNetNode = new TreeNode(newNet.Name);
            newNetNode.Tag = newNet;
            ServerList.Nodes[0].Nodes.Add(newNetNode);
            newNetNode.EnsureVisible();
            ServerList.SelectedNode = newNetNode;
            newNetNode.BeginEdit();
        }

        private void ServerList_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Node == ServerList.Nodes[0]) e.CancelEdit = true;
        }

        private void ServerList_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.CancelEdit) return;
            NetworkManagerConfigurationUnit unit = (NetworkManagerConfigurationUnit)e.Node.Tag;
            if (e.Label == "") e.CancelEdit = true;
            else unit.Name = e.Label;
        }

        private void NewServerItem_Click(object sender, EventArgs e)
        {
            Network addTo;
            ServerConfiguration newServer;
            TreeNode netNode;
            if (ServerList.SelectedNode.Tag is Network)
            {
                netNode = ServerList.SelectedNode;
            }
            else if (ServerList.SelectedNode.Tag is ServerConfiguration)
            {
                netNode = ServerList.SelectedNode.Parent;
            }
            else
            {
                return;
            }
            addTo = (Network)netNode.Tag;
            newServer = new ServerConfiguration();
            newServer.Name = "New Server";
            addTo.Servers.Add(newServer);
            TreeNode newServerNode = new TreeNode(newServer.Name);
            newServerNode.Tag = newServer;
            netNode.Nodes.Add(newServerNode);
            newServerNode.EnsureVisible();
            ServerList.SelectedNode = newServerNode;
            newServerNode.BeginEdit();
        }

        private void DeleteServerItem_Click(object sender, EventArgs e)
        {
            TreeNode toDel = ServerList.SelectedNode;
            if (!(toDel.Tag is ServerConfiguration)) return;
            ServerConfiguration toDelSrv = (ServerConfiguration)ServerList.SelectedNode.Tag;
            Network baseNet = (Network)toDelSrv.BaseUnit;
            TreeNode netNode = ServerList.SelectedNode.Parent;
            ServerList.SelectedNode = netNode;
            netNode.Nodes.Remove(toDel);
            baseNet.Servers.Remove(toDelSrv);
        }

        private void DeleteNetworkItem_Click(object sender, EventArgs e)
        {
            TreeNode netNode;
            Network toDel;
            if (ServerList.SelectedNode.Tag is Network)
            {
                netNode = ServerList.SelectedNode;
            }
            else if (ServerList.SelectedNode.Tag is ServerConfiguration)
            {
                netNode = ServerList.SelectedNode.Parent;
            }
            else
            {
                return;
            }
            toDel = (Network)netNode.Tag;
            ServerList.SelectedNode = netNode.Parent;
            ServerList.SelectedNode.Nodes.Remove(netNode);
            ((NetworkManager)toDel.BaseUnit).Networks.Remove(toDel);
        }
    }
}
