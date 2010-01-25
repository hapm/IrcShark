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
    /// The panel for the general network configurations.
    /// </summary>
    public partial class GeneralNetworkManagerSettingPanel : IrcShark.NetworkManagerSettingPanel
    {
        public GeneralNetworkManagerSettingPanel()
        {
            InitializeComponent();
            Text = "General";
        }

        public GeneralNetworkManagerSettingPanel(IrcSharkApplication app) : base(app)
        {
            Visibility = SettingPanelVisibility.Main | SettingPanelVisibility.Network;
            InitializeComponent();
            Text = "General";
        }

        private void GeneralServerManagerSettingPanel_CurrentConfigurationUnitChanged(object sender, EventArgs e)
        {
            if (CurrentConfigurationUnit.IsInheritedNickname)
                Nickname.ForeColor = Color.Gray;
            else
                Nickname.ForeColor = SystemColors.WindowText;
            Nickname.Text = CurrentConfigurationUnit.InheritedNickname;

            if (CurrentConfigurationUnit.IsInheritedAlternativeNickname)
                AlternativeNickname.ForeColor = Color.Gray;
            else
                AlternativeNickname.ForeColor = SystemColors.WindowText;
            AlternativeNickname.Text = CurrentConfigurationUnit.InheritedAlternativeNickname;

            if (CurrentConfigurationUnit.IsInheritedRealname)
                Username.ForeColor = Color.Gray;
            else
                Username.ForeColor = SystemColors.WindowText;
            Username.Text = CurrentConfigurationUnit.InheritedRealname;

            EMailAddress.Text = "";

            if (CurrentConfigurationUnit.IsInheritedIdent)
                Ident.ForeColor = Color.Gray;
            else
                Ident.ForeColor = SystemColors.WindowText;
            Ident.Text = CurrentConfigurationUnit.InheritedIdent;

            Perform.Lines = CurrentConfigurationUnit.Perform;
        }

        private void Nickname_Enter(object sender, EventArgs e)
        {
            Nickname.ForeColor = SystemColors.WindowText;
        }

        private void Nickname_Leave(object sender, EventArgs e)
        {
            if (CurrentConfigurationUnit.IsInheritedNickname)
                Nickname.ForeColor = Color.Gray;
            else
                Nickname.ForeColor = SystemColors.WindowText;
            Nickname.Text = CurrentConfigurationUnit.InheritedNickname;
        }

        private void Nickname_TextChanged(object sender, EventArgs e)
        {
            CurrentConfigurationUnit.Nickname = Nickname.Text;
        }

        private void AlternativeNickname_TextChanged(object sender, EventArgs e)
        {
            CurrentConfigurationUnit.AlternativeNickname = AlternativeNickname.Text;
        }

        private void AlternativeNickname_Enter(object sender, EventArgs e)
        {
            AlternativeNickname.ForeColor = SystemColors.WindowText;
        }

        private void AlternativeNickname_Leave(object sender, EventArgs e)
        {
            if (CurrentConfigurationUnit.IsInheritedAlternativeNickname)
                AlternativeNickname.ForeColor = Color.Gray;
            else
                AlternativeNickname.ForeColor = SystemColors.WindowText;
            AlternativeNickname.Text = CurrentConfigurationUnit.InheritedAlternativeNickname;
        }

        private void Username_TextChanged(object sender, EventArgs e)
        {
            CurrentConfigurationUnit.Realname = Username.Text;
        }

        private void Username_Enter(object sender, EventArgs e)
        {
            Username.ForeColor = SystemColors.WindowText;
        }

        private void Username_Leave(object sender, EventArgs e)
        {
            if (CurrentConfigurationUnit.IsInheritedRealname)
                Username.ForeColor = Color.Gray;
            else
                Username.ForeColor = SystemColors.WindowText;
            Username.Text = CurrentConfigurationUnit.InheritedRealname;
        }

        private void Ident_TextChanged(object sender, EventArgs e)
        {
            CurrentConfigurationUnit.Ident = Ident.Text;
        }

        private void Ident_Enter(object sender, EventArgs e)
        {
            Ident.ForeColor = SystemColors.WindowText;
        }

        private void Ident_Leave(object sender, EventArgs e)
        {
            if (CurrentConfigurationUnit.IsInheritedIdent)
                Ident.ForeColor = Color.Gray;
            else
                Ident.ForeColor = SystemColors.WindowText;
            Ident.Text = CurrentConfigurationUnit.InheritedIdent;
        }
    }
}
