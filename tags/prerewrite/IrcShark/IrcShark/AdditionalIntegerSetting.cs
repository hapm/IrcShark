using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace IrcShark
{
    /// <summary>
    /// Saves an integer setting.
    /// </summary>
    /// <remarks>Use this AdditionalSetting to save an Integer in the default config.</remarks>
    public sealed class AdditionalIntegerSetting : AdditionalSetting
    {
        private int ValueValue;

        public AdditionalIntegerSetting()
            : base(AdditionalSettingTypes.Integer)
        {
        }

        public int IntegerValue
        {
            get { return ValueValue; }
            set { ValueValue = value; }
        }

        [XmlIgnore]
        public override object Value
        {
            get { return ValueValue; }
            set
            {
                if (value is int)
                    ValueValue = (int)value;
                else
                    return;
            }
        }

    }
}
