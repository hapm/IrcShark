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
    public class ModeReceivedEventArgs : IrcEventArgs
    {
        private String setter;
        private String AimValue;
        private Mode[] ModesValue;
        private ModeArt AimArtValue;

        public ModeReceivedEventArgs(IrcLine BaseLine) : base(BaseLine)
        {
            long currentParam;
            FlagArt currentArt = FlagArt.Set;
            List<Mode> modes = new List<Mode>();
            List<FlagDefinition> flags = new List<FlagDefinition>();
            setter = BaseLine.Prefix;
            AimValue = BaseLine.Parameters[0];
            if (Client.Standard.IsAllowedChannel(AimValue))
            {
                flags.AddRange(Client.Standard.ChannelFlags);
                flags.AddRange(Client.Standard.UserPrefixFlags.Values);
                AimArtValue = ModeArt.Channel;
            }
            else
            {
                flags.AddRange(Client.Standard.UserFlags);
                AimArtValue = ModeArt.User;
            }
            currentParam = 2;

            foreach (char c in BaseLine.Parameters[1])
            {
                if (c == '+')
                	currentArt = FlagArt.Set;
                else if (c == '-')
                	currentArt = FlagArt.Unset;
                else
                {
                    foreach (FlagDefinition currentFlag in flags)
                    {
                        if (currentFlag.Char == c)
                        {
                            if (currentParam < BaseLine.Parameters.Length && currentFlag.IsParameter(currentArt, BaseLine.Parameters[currentParam]))
                            {
                                modes.Add(new Mode(currentFlag, currentArt, BaseLine.Parameters[currentParam]));
                                currentParam++;
                            }
                            else if (!currentFlag.NeedParameter(currentArt))
                            {
                                modes.Add(new Mode(currentFlag, currentArt));
                            }
                            break;
                        }
                    }
                }
            }
            ModesValue = modes.ToArray();
        }

        public Mode[] Modes
        {
            get { return (Mode[])ModesValue.Clone(); }
        }

        public String Setter
        {
            get { return setter; }
        }

        public String Aim
        {
            get { return AimValue; }
        }

        public ModeArt AimArt
        {
            get { return AimArtValue; }
        }
    }
}
