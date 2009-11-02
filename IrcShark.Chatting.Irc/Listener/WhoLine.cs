// <copyright file="WhoLine.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the WhoLine class.</summary>

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
namespace IrcShark.Chatting.Irc.Listener
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The WhoLine represents a line in a who reply as sent from an irc server.
    /// </summary>
    public class WhoLine : IrcLine
    {
        /// <summary>
        /// Saves if the user is away or not.
        /// </summary>
        private bool isAway;
        
        /// <summary>
        /// Saves the user modes.
        /// </summary>
        private Mode[] modes;
        
        /// <summary>
        /// Saves if the user is an oper.
        /// </summary>
        private bool isOper;
        
        /// <summary>
        /// Saves the number of hubs between the user and the own client.
        /// </summary>
        private int hopCount;
        
        /// <summary>
        /// Saves the real name of the user.
        /// </summary>
        private string realName;

        /// <summary>
        /// Saves the UserInfo for the current user.
        /// </summary>
        private UserInfo user;

        /// <summary>
        /// Initializes a new instance of the WhoLine class.
        /// </summary>
        /// <param name="line">The line that is the base of the WhoLine.</param>
        public WhoLine(IrcLine line) : base(line)
        {
            if (line.Numeric != 352)
            {
                throw new ArgumentOutOfRangeException("line", "RPL_WHOREPLY 352 expected");
            }
            
            if (Parameters.Length < 8)
            {
                throw new ArgumentOutOfRangeException("line", "Need a minimum of 8 parameters");
            }
            
            user = new UserInfo(Parameters[5], Parameters[2], Parameters[3], Client);
            List<Mode> modes = new List<Mode>();
            int i = 1;
            
            isAway = Parameters[6][0] == 'G';
            isOper = Parameters[6][i] == '*';
            
            if (IsOper)
            {
                i++;
            }
            
            for (; i < Parameters[6].Length; i++)
            {
                FlagDefinition flag = Client.Standard.GetUserPrefixFlag(Parameters[6][i]);
                if (flag != null)
                {
                    modes.Add(new Mode(flag, FlagArt.Set, User.NickName));
                }
            }

            this.modes = modes.ToArray();
            
            realName = Parameters[7];

            if (!int.TryParse(realName.Substring(1, realName.IndexOf(" ")), out hopCount))
            {
                throw new ArgumentOutOfRangeException("line", "Invalid hop count, integer expected");
            }
            
            realName = realName.Substring(realName.IndexOf(" ") + 1);
        }

        /// <summary>
        /// Gets the name of the channel.
        /// </summary>
        /// <value>The name as a string.</value>
        public string Channel
        {
            get { return Parameters[1]; }
        }

        /// <summary>
        /// Gets the server, the user is connected to.
        /// </summary>
        /// <value>The server address as a string.</value>
        public string Server
        {
            get { return Parameters[4]; }
        }

        /// <summary>
        /// Gets a value indicating whether the user is away or not.
        /// </summary>
        /// <value>Its true if the user is away, false otherwise.</value>
        public bool IsAway
        {
            get { return isAway; }
        }

        /// <summary>
        /// Gets a value indicating whether the user is an oper.
        /// </summary>
        /// <value>Its true if the user is an oper, false otherwise.</value>
        public bool IsOper
        {
            get { return isOper; }
        }

        /// <summary>
        /// Gets the number of hops between the client and the listed user.
        /// </summary>
        /// <value>The number of hops (servers) between yourself and the user.</value>
        public int HopCount
        {
            get { return hopCount; }
        }

        /// <summary>
        /// Gets the modes set for the user.
        /// </summary>
        /// <value>All modes as an array.</value>
        public Mode[] Modes
        {
            get { return (Mode[])modes.Clone(); }
        }

        /// <summary>
        /// Gets the UserInfo for the user.
        /// </summary>
        /// <value>The UserInfo for the user.</value>
        public UserInfo User
        {
            get { return user; }
        }
    }
}
