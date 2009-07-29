using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using IrcShark.Extensions;

namespace IrcShark
{
    /// <summary>
    /// Represents a configuration unit for the NetworkManager.
    /// </summary>
    public class NetworkManagerConfigurationUnit
    {
        private NetworkManagerConfigurationUnit BaseUnitValue;
        private String NameValue;
        private String NicknameValue;
        private String AlternativeNicknameValue;
        private String IdentValue;
        private String RealnameValue;
        private String[] PerformValue;
        private AdditionalListSetting AdditionalSettingsValue;

        public event EventHandler NameChanged;

        public NetworkManagerConfigurationUnit()
        {
            AdditionalSettingsValue = new AdditionalListSetting();
        }

        [XmlIgnore]
        public NetworkManagerConfigurationUnit BaseUnit
        {
            get { return BaseUnitValue; }
            set { BaseUnitValue = value; }
        }

        [XmlIgnore]
        public NetworkManagerConfigurationUnit TopUnit
        {
            get 
            {
                if (BaseUnitValue == null) return this;
                else return BaseUnitValue.TopUnit;
            }
        }

        [XmlAttribute]
        public String Name
        {
            get { return NameValue; }
            set 
            { 
                NameValue = value;
                if (NameChanged != null) NameChanged(this, new EventArgs());
            }
        }

        [XmlAttribute("Nick")]
        public String Nickname
        {
            get { return NicknameValue; }
            set 
            {
                if (value == null || value == "") NicknameValue = null;
                else if (BaseUnit != null && value == BaseUnit.InheritedNickname) NicknameValue = null;
                else NicknameValue = value; 
            }
        }

        [XmlAttribute("AltNick")]
        public String AlternativeNickname
        {
            get { return AlternativeNicknameValue; }
            set 
            {
                if (value == null || value == "") AlternativeNicknameValue = null;
                else if (BaseUnit != null && value == BaseUnit.InheritedAlternativeNickname) AlternativeNicknameValue = null;
                else AlternativeNicknameValue = value; 
            }
        }

        [XmlAttribute]
        public String Ident
        {
            get { return IdentValue; }
            set 
            {
                if (value == null || value == "") IdentValue = null;
                else if (BaseUnit != null && value == BaseUnit.InheritedIdent) IdentValue = null;
                else IdentValue = value; 
            }
        }

        [XmlAttribute]
        public String Realname
        {
            get { return RealnameValue; }
            set 
            {
                if (value == null || value == "") RealnameValue = null;
                else if (BaseUnit != null && value == BaseUnit.InheritedRealname) RealnameValue = null;
                else RealnameValue = value; 
            }
        }

        public String[] Perform
        {
            get { return PerformValue; }
            set { PerformValue = value; }
        }

        public AdditionalListSetting AdditionalSettings
        {
            get { return AdditionalSettingsValue; }
            set { AdditionalSettingsValue = value; }
        }

        #region "Inherited Members"

        [XmlIgnore]
        public String InheritedNickname
        {
            get
            {
                if (NicknameValue != null)
                    return NicknameValue;
                if (BaseUnitValue != null)
                    return BaseUnitValue.InheritedNickname;
                return null;
            }
            set
            {
                if (value == null || value == "") NicknameValue = null;
                else if (BaseUnit != null && value == BaseUnit.InheritedNickname) NicknameValue = null;
                else NicknameValue = value;
            }
        }

        [XmlIgnore]
        public String InheritedAlternativeNickname
        {
            get
            {
                if (AlternativeNicknameValue != null)
                    return AlternativeNicknameValue;
                if (BaseUnitValue != null)
                    return BaseUnitValue.InheritedAlternativeNickname;
                return null;
            }
            set
            {
                if (value == null || value == "") AlternativeNicknameValue = null;
                else if (BaseUnit != null && value == BaseUnit.InheritedAlternativeNickname) AlternativeNicknameValue = null;
                else AlternativeNicknameValue = value;
            }
        }

        [XmlIgnore]
        public String InheritedIdent
        {
            get
            {
                if (IdentValue != null)
                    return IdentValue;
                if (BaseUnitValue != null)
                    return BaseUnitValue.InheritedIdent;
                return null;
            }
            set
            {
                if (value == null || value == "") IdentValue = null;
                else if (BaseUnit != null && value == BaseUnit.InheritedIdent) IdentValue = null;
                else IdentValue = value;
            }
        }

        [XmlIgnore]
        public String InheritedRealname
        {
            get
            {
                if (RealnameValue != null)
                    return RealnameValue;
                if (BaseUnitValue != null)
                    return BaseUnitValue.InheritedRealname;
                return null;
            }
            set
            {
                if (value == null || value == "") RealnameValue = null;
                else if (BaseUnit != null && value == BaseUnit.InheritedRealname) RealnameValue = null;
                else RealnameValue = value;
            }
        }

        [XmlIgnore]
        public String[] InheritedPerform
        {
            get { return PerformValue; }
            set { PerformValue = value; }
        }

        public AdditionalSetting InheritedAdditionalData(String key)
        {
            /*if (AdditionalSettings.ListValue.ContainsSetting(key)) 
                return AdditionalSettings[key];
            else if (BaseUnit != null) 
                return BaseUnit.InheritedAdditionalData(key);
            else */
                return null;
        }

        #endregion

        [XmlIgnore]
        public bool IsInheritedNickname
        {
            get { return (NicknameValue == null); }
        }

        [XmlIgnore]
        public bool IsInheritedAlternativeNickname
        {
            get { return (AlternativeNicknameValue == null); }
        }

        [XmlIgnore]
        public bool IsInheritedIdent
        {
            get { return (IdentValue == null); }
        }

        [XmlIgnore]
        public bool IsInheritedRealname
        {
            get { return (RealnameValue == null); }
        }


        public bool IsInheritedAdditionalData(String key)
        {
            return !AdditionalSettings.ListValue.ContainsSetting(key);            
        }

    }
}
