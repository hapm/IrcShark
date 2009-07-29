using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace IrcShark
{
    /// <summary>
    /// A boolean additional setting.
    /// </summary>
    public sealed class AdditionalBooleanSetting : AdditionalSetting
    {
        private bool ValueValue;

        public AdditionalBooleanSetting()
            : base(AdditionalSettingTypes.Boolean)
        {
        }

        [XmlIgnore]
        public override object Value
        {
            get { return ValueValue; }
            set
            {
                if (value is bool)
                    ValueValue = (bool)value;
                else
                    return;
            }
        }

        public bool BooleanValue
        {
            get { return ValueValue; }
            set { ValueValue = value; }
        }
    }
}
