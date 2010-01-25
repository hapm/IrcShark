using System;
using System.Collections.Generic;
using System.Text;

namespace IrcShark
{
    public sealed class AdditionalSettingList : List<AdditionalSetting>
    {
        AdditionalListSetting BaseSettingValue;

        internal AdditionalSettingList(AdditionalListSetting baseSetting)
        {
            BaseSettingValue = baseSetting;
        }

        public bool ContainsSetting(String SettingName)
        {
            foreach (AdditionalSetting setting in this)
            {
                if (setting.Name == SettingName) return true;
            }
            return false;
        }

        public bool ContainsSetting(String SettingName, AdditionalSettingTypes type)
        {
            foreach (AdditionalSetting setting in this)
            {
                if (setting.Name == SettingName) return (setting.Type == type);
            }
            return false;
        }

        public AdditionalSetting this[String name]
        {
            get
            {
                foreach (AdditionalSetting setting in this)
                {
                    if (setting.Name == name) return setting;
                }
                throw new IndexOutOfRangeException("Setting for given name " + name + " wasen't found");
            }
        }

        public new void Add(AdditionalSetting setting)
        {
            if (ContainsSetting(setting.Name)) return;
            base.Add(setting);
            setting.Base = BaseSettingValue;
        }

        public new void AddRange(IEnumerable<AdditionalSetting> collection)
        {
            foreach (AdditionalSetting setting in collection)
            {
                if (ContainsSetting(setting.Name)) return;
                Add(setting);
            }
        }

        public new void Remove(AdditionalSetting toDel)
        {
            foreach (AdditionalSetting setting in this)
            {
                if (setting == toDel)
                {
                    setting.Base = null;
                    base.Remove(setting);
                }
            }
        }

        public new void RemoveAt(int index)
        {
            this[index].Base = null;
            base.RemoveAt(index);
        }

        public new void RemoveAll(Predicate<AdditionalSetting> match)
        {
            throw new NotSupportedException("this operation is not allowed for this object");
        }

        public new void RemoveRange(int index, int count)
        {
            for (int i = index;i<index+count;i++) this[i].Base = null;
            base.RemoveRange(index, count);
        }

        public AdditionalSetting GetSettingByPath(String path)
        {
            String mySetting;
            if (path.IndexOf('.') > -1)
            {
                mySetting = path.Substring(0, path.IndexOf('.'));
                path = path.Substring(path.IndexOf('.') + 1);
                AdditionalSetting set = this[mySetting];
                if (!(set is AdditionalListSetting)) throw new InvalidCastException("Setting '" + mySetting + "' is no list setting");
                AdditionalListSetting set1 = (AdditionalListSetting)set;
                return set1.ListValue.GetSettingByPath(path);
            }
            else
            {
                return this[path];
            }
        }

        public new void Clear()
        {
            foreach (AdditionalSetting setting in this)
                setting.Base = null;
            base.Clear();
        }
    }
}
