using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace IrcShark
{
    /// <summary>
    /// Saves a list of settings.
    /// </summary>
    /// <remarks>Use this AdditionalSetting to save a list of additional settings in the default config.</remarks>
    public sealed class AdditionalListSetting : AdditionalSetting
    {
        private AdditionalSettingList ListValueValue;

        public AdditionalListSetting()
            : base(AdditionalSettingTypes.List)
        {
            ListValueValue = new AdditionalSettingList(this);
        }

        [XmlIgnore]
        public override Object Value
        {
            get { return ListValue.ToArray(); }
            set 
            {
                if (value is AdditionalSetting[])
                {
                    ListValue.Clear();
                    ListValue.AddRange((AdditionalSetting[])value);
                }
                else
                    return;
            }
        }

        public AdditionalSettingList ListValue
        {
            get { return ListValueValue; }
            set { ListValueValue = value; }
        }
    }
}
