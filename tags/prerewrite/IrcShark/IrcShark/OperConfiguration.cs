using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using IrcShark.Extensions;

namespace IrcShark
{
    public class OperConfiguration
    {
        private String OperNameValue;
        private bool EnabledValue;
        private AdditionalListSetting SettingsValue;

        public OperConfiguration()
        {

        }

        public OperConfiguration(AdditionalListSetting baseData)
        {
            if (baseData.Name != "Oper") throw new ArgumentOutOfRangeException("baseData is no oper configuration");
            try
            {
                OperNameValue = (String)baseData.ListValue["OperName"].Value;
                EnabledValue = (Boolean)baseData.ListValue["Enabled"].Value;
            }
            catch (Exception e)
            {
                throw new ArgumentOutOfRangeException("Missing option for opersettings", e);
            }
        }

        [XmlAttribute]
        public String OperName
        {
            get { return OperNameValue; }
            set { OperNameValue = value; }
        }

        public bool Enabled
        {
            get { return EnabledValue; }
            set { EnabledValue = value; }
        }
    }
}
