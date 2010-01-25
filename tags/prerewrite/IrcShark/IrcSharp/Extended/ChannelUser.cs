// $Id$
// 
// Note:
// 
// Copyright (C) 2009 Full Name
//  
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp.Extended
{
    public class ChannelUser : IIrcObject
    {
        private String nickName;
        private List<Mode> modes;
        private Channel channel;
        private char[] prefixes;

        public ChannelUser(Channel chan, String name)
        {
            channel = chan;
            modes = new List<Mode>();
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
            nickName = name.Substring(i);
            foreach(char c in tempPrefixes)
            {
                FlagDefinition associatedFlag = Client.Standard.UserPrefixFlags[c];
                modes.Add(new Mode(associatedFlag, FlagArt.Set, nickName));
            }
            prefixes = tempPrefixes.ToArray();
        }

        public Channel Channel
        {
            get { return channel; }
        }

        public String NickName
        {
            get { return nickName; }
        }

        public char[] Prefixes
        {
            get { return (char[])prefixes.Clone(); }
        }

        public bool HasMode(FlagDefinition mode)
        {
            return HasMode(mode.Char);
        }

        public bool HasMode(char flag)
        {
            foreach (Mode m in modes)
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
                modes.Add(m);
                RebuildPrefixes();
            }
            else
            {
                List<Mode> toDelete = new List<Mode>();
                foreach (Mode mo in modes)
                {
                    if (mo.Flag.Char == m.Flag.Char)
                    {
                        toDelete.Add(mo);
                    }
                }
                foreach (Mode mo in toDelete)
                {
                    modes.Remove(mo);
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
            prefixes = newPrefixes.ToArray();
        }

        #region IIrcObject Members

        public IrcClient Client
        {
            get { return Channel.Client; }
        }

        #endregion
    }
}
