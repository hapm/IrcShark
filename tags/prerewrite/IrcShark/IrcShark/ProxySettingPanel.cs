using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IrcShark
{
    /// <summary>
    /// The panel representing the proxy configuration for a network or server.
    /// </summary>
    public partial class ProxySettingPanel : IrcShark.NetworkManagerSettingPanel
    {
        public ProxySettingPanel()
        {
            InitializeComponent();
            Text = "Proxy";
        }

        public ProxySettingPanel(IrcSharkApplication app) : base(app)
        {
            Visibility = SettingPanelVisibility.All;
            InitializeComponent();
            Text = "Proxy";
        }
    }
}
