// <copyright file="ModeReceivedEventArgs.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the ModeReceivedEventArgs class.</summary>

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
namespace IrcSharp
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The ModeReceivedEventArgs belongs to the <see cref="IrcClient.ModeReceivedEventHandler" /> and the <see cref="IrcClient.ModeReceived" /> event.
    /// </summary>
    public class ModeReceivedEventArgs : IrcEventArgs
    {
        /// <summary>
        /// Saves the name of the setter.
        /// </summary>
        private string setter;
        
        /// <summary>
        /// Saves the target, the mode was changed for.
        /// </summary>
        private string aim;
        
        /// <summary>
        /// Saves the list of modes set.
        /// </summary>
        private Mode[] modes;
        
        /// <summary>
        /// Saves the type of hte aim.
        /// </summary>
        private ModeArt aimArt;

        /// <summary>
        /// Initializes a new instance of the ModeReceivedEventArgs class.
        /// </summary>
        /// <param name="line">The line of the mode command.</param>
        public ModeReceivedEventArgs(IrcLine line) : base(line)
        {
            long currentParam;
            FlagArt currentArt = FlagArt.Set;
            List<Mode> modes = new List<Mode>();
            List<FlagDefinition> flags = new List<FlagDefinition>();
            setter = line.Prefix;
            aim = line.Parameters[0];
            if (Client.Standard.IsAllowedChannel(aim))
            {
                flags.AddRange(Client.Standard.ChannelFlags);
                flags.AddRange(Client.Standard.UserPrefixFlags);
                aimArt = ModeArt.Channel;
            }
            else
            {
                flags.AddRange(Client.Standard.UserFlags);
                aimArt = ModeArt.User;
            }
            currentParam = 2;

            foreach (char c in line.Parameters[1])
            {
                if (c == '+')
                    currentArt = FlagArt.Set;
                else if (c == '-')
                    currentArt = FlagArt.Unset;
                else
                {
                    foreach (FlagDefinition currentFlag in flags)
                    {
                        if (currentFlag.Character == c)
                        {
                            if (currentParam < line.Parameters.Length && currentFlag.IsParameter(currentArt, line.Parameters[currentParam]))
                            {
                                modes.Add(new Mode(currentFlag, currentArt, line.Parameters[currentParam]));
                                currentParam++;
                            }
                            else if (!currentFlag.NeedsParameter(currentArt))
                            {
                                modes.Add(new Mode(currentFlag, currentArt));
                            }
                            break;
                        }
                    }
                }
            }
            this.modes = modes.ToArray();
        }

        /// <summary>
        /// Gets all modes set by the setter.
        /// </summary>
        /// <value>An array of all changed modes.</value>
        public Mode[] Modes
        {
            get { return (Mode[])modes.Clone(); }
        }

        /// <summary>
        /// Gets the name of the setter.
        /// </summary>
        /// <value>The name as a string.</value>
        public string Setter
        {
            get { return setter; }
        }

        /// <summary>
        /// Gets the name of the aim, the mode was changed for.
        /// </summary>
        /// <value>The name as a string.</value>
        public string Aim
        {
            get { return aim; }
        }

        /// <summary>
        /// Gets the art of mode change.
        /// </summary>
        /// <value>The ModeArt.</value>
        public ModeArt AimArt
        {
            get { return aimArt; }
        }
    }
}
