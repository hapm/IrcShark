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

namespace IrcSharp
{
    public class WhoLine : IrcLine
    {
        private Boolean IsAwayValue;
        private Mode[] ModesValue;
        private Boolean IsOperValue;
        private int HopCountValue;
        private String RealNameValue;

        private UserInfo UserValue;

        public WhoLine(IrcLine baseLine) : base(baseLine)
        {
            if(baseLine.Numeric != 352)
            	throw new ArgumentOutOfRangeException("baseLine", "RPL_WHOREPLY 352 expected");
            if(Parameters.Length < 8)
            	throw new ArgumentOutOfRangeException("baseLine", "Need a minimum of 8 parameters");
            
            UserValue = new UserInfo(Parameters[5], Parameters[2], Parameters[3], Client);
            List<Mode> modes = new List<Mode>();
            int i = 1;
            
            IsAwayValue = Parameters[6][0] == 'G';
            IsOperValue = Parameters[6][i] == '*';
            
            if(IsOper)
            	i++;
            
            for(; i < Parameters[6].Length; i++)
            {
                if(Client.Standard.UserPrefixFlags.ContainsKey(Parameters[6][i]))
                {
                    modes.Add(new Mode(Client.Standard.UserPrefixFlags[Parameters[6][i]], FlagArt.Set, User.NickName));
                }
            }

            ModesValue = modes.ToArray();
            
            RealNameValue = Parameters[7];

            if(!int.TryParse(RealNameValue.Substring(1, RealNameValue.IndexOf(" ")), out HopCountValue))
            	throw new ArgumentOutOfRangeException("baseLine", "Invalid hop count, integer expected");
            
            RealNameValue = RealNameValue.Substring(RealNameValue.IndexOf(" ") + 1);
        }

        public string Channel
        {
            get { return Parameters[1]; }
        }

        public string Server
        {
            get { return Parameters[4]; }
        }

        public bool IsAway
        {
            get { return IsAwayValue; }
        }

        public bool IsOper
        {
            get { return IsOperValue; }
        }

        public int HopCount
        {
            get { return HopCountValue; }
        }

        public Mode[] Modes
        {
            get { return (Mode[])ModesValue.Clone(); }
        }

        public UserInfo User
        {
            get { return UserValue; }
        }
    }
}
