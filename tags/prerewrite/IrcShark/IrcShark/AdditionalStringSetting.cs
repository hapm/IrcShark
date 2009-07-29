using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace IrcShark
{
    /// <summary>
    /// Saves a String setting.
    /// </summary>
    /// <remarks>Use this AdditionalSetting to save a string in the default config.</remarks>
    public sealed class AdditionalStringSetting : AdditionalSetting
    {
        String ValueValue;

        public AdditionalStringSetting()
            : base(AdditionalSettingTypes.String)
        {
        }

        [XmlIgnore]
        public override object Value
        {
            get { return ValueValue; }
            set
            {
                if (value is String)
                    ValueValue = (String)value;
                else
                    return;
            }
        }

        public String StringValue
        {
            get { return ValueValue; }
            set { ValueValue = value; }
        }
    }
}
