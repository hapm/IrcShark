using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using IrcShark.Extensions;

namespace IrcShark
{
    public partial class OperSettingPanel : IrcShark.NetworkManagerSettingPanel
    {
        private AdditionalBooleanSetting ActiveValue;

        public OperSettingPanel()
        {
            InitializeComponent();
            Text = "Oper";
        }

        public OperSettingPanel(IrcSharkApplication app) : base(app)
        {
            InitializeComponent();
            Text = "Oper";
        }

        private void OperSettingPanel_CurrentConfigurationUnitChanged(object sender, EventArgs args)
        {
            AdditionalListSetting config;
            if (CurrentConfigurationUnit.AdditionalSettings.ListValue.ContainsSetting("Oper", AdditionalSettingTypes.List))
            {
                config = (AdditionalListSetting)CurrentConfigurationUnit.AdditionalSettings.ListValue["Oper"];
            }
            else
            {
                config = new AdditionalListSetting();
                config.Name = "Oper";
                CurrentConfigurationUnit.AdditionalSettings.ListValue.Add(config);
            }
            CheckOperConfig(config);
            OperActive.Checked = ActiveValue.BooleanValue;
        }

        private void CheckOperConfig(AdditionalListSetting config)
        {
            if (config.ListValue.ContainsSetting("Active", AdditionalSettingTypes.Boolean))
                ActiveValue = (AdditionalBooleanSetting)config.ListValue["Active"];
            else
            {
                ActiveValue = new AdditionalBooleanSetting();
                ActiveValue.Name = "Active";
                ActiveValue.BooleanValue = false;
                config.ListValue.Add(ActiveValue);
            }
        }

        private void OperActive_CheckedChanged(object sender, EventArgs e)
        {
            ActiveValue.BooleanValue = OperActive.Checked;
        }
    }
}
