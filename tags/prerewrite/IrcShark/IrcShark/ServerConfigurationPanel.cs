using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IrcShark
{
    public delegate void ServerSelectedEventHandler(Object sender, ServerSelectedEventArgs args);

    public partial class ServerConfigurationPanel : IrcShark.NetworkManagerSettingPanel
    {
        public event ServerSelectedEventHandler ServerSelected;

        public ServerConfigurationPanel()
        {
            InitializeComponent();
            Text = "Server";
        }

        public ServerConfigurationPanel(IrcSharkApplication app) : base(app)
        {
            InitializeComponent();
            Visibility = SettingPanelVisibility.Server;
            Text = "Server";
        }

        private void ServerConfigurationPanel_CurrentConfigurationUnitChanged(object sender, EventArgs e)
        {
            ServerConfiguration config = (ServerConfiguration)CurrentConfigurationUnit;
            Address.Text = config.Address;
            Port.Text = config.Port.ToString();
            Password.Text = config.Password;
        }

        private void Address_TextChanged(object sender, EventArgs e)
        {
            ServerConfiguration config = (ServerConfiguration)CurrentConfigurationUnit;
            config.Address = Address.Text;
        }

        private void Port_TextChanged(object sender, EventArgs e)
        {
            ServerConfiguration config = (ServerConfiguration)CurrentConfigurationUnit;
            int newPort;
            if (int.TryParse(Port.Text, out newPort)) config.Port = newPort;
        }

        private void Password_TextChanged(object sender, EventArgs e)
        {
            ServerConfiguration config = (ServerConfiguration)CurrentConfigurationUnit;
            config.Password = Password.Text;
        }

        private void SelectServerButton_Click(object sender, EventArgs e)
        {
            if (ServerSelected != null) ServerSelected(this, new ServerSelectedEventArgs((ServerConfiguration)CurrentConfigurationUnit));
        }

        private void ServerConfigurationPanel_Load(object sender, EventArgs e)
        {

        }
    }
}
