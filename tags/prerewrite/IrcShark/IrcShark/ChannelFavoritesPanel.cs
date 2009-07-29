using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IrcShark
{
    public partial class ChannelFavoritesPanel : NetworkManagerSettingPanel
    {
        public ChannelFavoritesPanel()
        {
            InitializeComponent();
            Text = "Channels";
        }

        public ChannelFavoritesPanel(IrcSharkApplication app) : base(app)
        {
            Visibility = SettingPanelVisibility.Main | SettingPanelVisibility.Network;
            InitializeComponent();
            Text = "Channels";
        }
    }
}
