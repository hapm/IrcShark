using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace IrcShark
{
    /// <summary>
    /// An additional setting for an extension, what can be saved  in the default config file.
    /// </summary>
    [XmlInclude(typeof(AdditionalListSetting))]
    [XmlInclude(typeof(AdditionalIntegerSetting))]
    [XmlInclude(typeof(AdditionalBooleanSetting))]
    [XmlInclude(typeof(AdditionalStringSetting))]
    public abstract class AdditionalSetting
    {
        String NameValue;
        AdditionalListSetting BaseValue;
        NetworkManagerConfigurationUnit BaseUnitValue;
        AdditionalSettingTypes TypeValue;

        protected AdditionalSetting(AdditionalSettingTypes type)
        {
            NameValue = null;
            BaseValue = null;
            BaseUnitValue = null;
            TypeValue = type;
        }

        private AdditionalSetting(String name, AdditionalListSetting baseSetting)
        {
            if (name.IndexOf('.') > -1)
                throw new ArgumentOutOfRangeException("name", "'.' isn't allowed in setting names");
            NameValue = name;
            BaseValue = baseSetting;
            baseSetting.ListValue.Add(this);
        }

        /// <summary>
        /// The AdditionalSettingList, this setting belongs to.
        /// </summary>
        /// <value>a list of settings</value>
        [XmlIgnore]
        public AdditionalListSetting Base
        {
            get { return BaseValue; }
            internal set { BaseValue = value; }
        }

        /// <summary>
        /// The ConfigurationUnit this setting belongs to.
        /// </summary>
        /// <value>a configuration unit</value>
        [XmlIgnore]
        public NetworkManagerConfigurationUnit BaseUnit
        {
            get 
            { 
                if (BaseValue != null) return BaseValue.BaseUnit;
                return BaseUnitValue;
            }
            internal set { BaseUnitValue = value; }
        }

        /// <summary>
        /// The name of this setting.
        /// </summary>
        [XmlAttribute]
        public String Name 
        {
            get { return NameValue; }
            set
            {
                if (BaseValue != null && BaseValue.ListValue.ContainsSetting(value)) throw new InvalidOperationException("New name '"+value+"' alread exists in '"+BaseValue.Name+"'");
                if (value.IndexOf('.') > -1)
                    throw new InvalidOperationException("'.' isn't allowed in setting names");
                NameValue = value; 
            }
        }

        /// <summary>
        /// The level of this setting.
        /// </summary>
        [XmlIgnore]
        public AdditionalSettingTypes Type
        {
            get { return TypeValue; }
        }

        /// <summary>
        /// The current value of this setting.
        /// </summary>
        [XmlIgnore]
        public abstract Object Value 
        { 
            get;
            set;
        }
    }
}
