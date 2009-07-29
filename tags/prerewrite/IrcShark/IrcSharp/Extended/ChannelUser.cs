using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp.Extended
{
    public class ChannelUser : IIrcObject
    {
        private String NickNameValue;
        private List<Mode> ModesValue;
        private Channel ChannelValue;
        private char[] PrefixesValue;

        public ChannelUser(Channel chan, String name)
        {
            ChannelValue = chan;
            ModesValue = new List<Mode>();
            List<char> tempPrefixes = new List<char>();
            int i;
            for (i = 0; i < name.Length; i++)
            {
                if (Client.Standard.UserPrefixFlags.ContainsKey(name[i]))
                {
                    tempPrefixes.Add(name[i]);
                }
                else
                {
                    break;
                }
            }
            NickNameValue = name.Substring(i);
            foreach(char c in tempPrefixes)
            {
                FlagDefinition associatedFlag = Client.Standard.UserPrefixFlags[c];
                ModesValue.Add(new Mode(associatedFlag, FlagArt.Set, NickNameValue));
            }
            PrefixesValue = tempPrefixes.ToArray();
        }

        public Channel Channel
        {
            get { return ChannelValue; }
        }

        public String NickName
        {
            get { return NickNameValue; }
        }

        public char[] Prefixes
        {
            get { return (char[])PrefixesValue.Clone(); }
        }

        public bool HasMode(FlagDefinition mode)
        {
            return HasMode(mode.Char);
        }

        public bool HasMode(char flag)
        {
            foreach (Mode m in ModesValue)
            {
                if (m.Flag.Char == flag) return true;
            }
            return false;
        }

        public override String ToString()
        {
            StringBuilder result = new StringBuilder();
            if (Prefixes.Length > 0) result.Append(Prefixes[0]);
            result.Append(NickName);
            return result.ToString();
        }

        internal void UpdateMode(Mode m)
        {
            if (m.Art == FlagArt.Set)
            {
                if (HasMode(m.Flag.Char)) return;
                ModesValue.Add(m);
                RebuildPrefixes();
            }
            else
            {
                List<Mode> toDelete = new List<Mode>();
                foreach (Mode mo in ModesValue)
                {
                    if (mo.Flag.Char == m.Flag.Char)
                    {
                        toDelete.Add(mo);
                    }
                }
                foreach (Mode mo in toDelete)
                {
                    ModesValue.Remove(mo);
                }
                RebuildPrefixes();
            }
        }

        private void RebuildPrefixes()
        {
            List<char> newPrefixes = new List<char>();
            foreach (KeyValuePair<char, FlagDefinition> f in Client.Standard.UserPrefixFlags)
            {
                if (HasMode(f.Value.Char)) newPrefixes.Add(f.Key);
            }
            PrefixesValue = newPrefixes.ToArray();
        }

        #region IIrcObject Members

        public IrcClient Client
        {
            get { return Channel.Client; }
        }

        #endregion
    }
}
