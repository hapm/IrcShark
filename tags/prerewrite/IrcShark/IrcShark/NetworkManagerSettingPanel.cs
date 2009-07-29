using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IrcShark
{
    [Flags]
    public enum SettingPanelVisibility
    {
        Main = 1,
        Network = 2,
        Server = 4,
        All = 7
    }

    /// <summary>
    /// The NetworkManagerSettingPanel can be used as the SettingPanel, but the panels derived from this class, are used as subpanels of NetworkManagerPanel.
    /// </summary>
    /// <remarks>Setting panels derived from this class are used to show network or server specific settings.</remarks>
    public partial class NetworkManagerSettingPanel : SettingPanel
    {
        private NetworkManagerConfigurationUnit CurrentConfigurationUnitValue;
        private SettingPanelVisibility VisibilityValue;

        public event EventHandler CurrentConfigurationUnitChanged;
        public event EventHandler CurrentConfigurationUnitChanging;

        public NetworkManagerSettingPanel()
        {
            InitializeComponent();
        }

        public NetworkManagerSettingPanel(IrcSharkApplication app) : base(app)
        {
            InitializeComponent();
            Visibility = SettingPanelVisibility.All;
        }

        public SettingPanelVisibility Visibility
        {
            get { return VisibilityValue; }
            protected set { VisibilityValue = value; }
        }

        public NetworkManagerConfigurationUnit CurrentConfigurationUnit
        {
            get { return CurrentConfigurationUnitValue; }
            set 
            {
                if (!IsVisibleFor(value)) return;
                if (CurrentConfigurationUnitChanging != null) CurrentConfigurationUnitChanging(this, new EventArgs());
                CurrentConfigurationUnitValue = value; 
                if (CurrentConfigurationUnitChanged != null) CurrentConfigurationUnitChanged(this, new EventArgs());
            }
        }

        public bool IsVisibleFor(NetworkManagerConfigurationUnit unit)
        {
            if (unit is NetworkManager && (Visibility & SettingPanelVisibility.Main) == SettingPanelVisibility.Main) return true;
            else if (unit is Network && (Visibility & SettingPanelVisibility.Network) == SettingPanelVisibility.Network) return true;
            else if (unit is ServerConfiguration && (Visibility & SettingPanelVisibility.Server) == SettingPanelVisibility.Server) return true;
            else if ((Visibility & SettingPanelVisibility.All) == SettingPanelVisibility.All) return true;
            return false;
        }
    }
}
