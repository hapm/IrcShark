// <copyright file="UserInfo.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the UserInfo class.</summary>

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
namespace IrcShark.Chatting.Irc
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Holds host informations about a user.
    /// </summary>
    public class UserInfo : IIrcObject
    {
        /// <summary>
        /// This regex is used for parsing a mirc user address into its different parts.
        /// </summary>
        private static Regex hostRegex = new Regex("([^!@]+)!([^!@]+)@([^!@]+)", RegexOptions.Compiled & RegexOptions.Singleline);
        
        /// <summary>
        /// Saves the name of the user.
        /// </summary>
        private string nickName;
        
        /// <summary>
        /// Saves the ident of the user.
        /// </summary>
        private string ident;
        
        /// <summary>
        /// Saves the host of the user.
        /// </summary>
        private string host;
        
        /// <summary>
        /// If the user host was created by an IrcLine, it is saved here.
        /// </summary>
        private IrcLine baseLine;
        
        /// <summary>
        /// The client, this UserInfo belongs to.
        /// </summary>
        private IrcClient client;
        
        /// <summary>
        /// Initializes a new instance of the UserInfo class based on the host.
        /// </summary>
        /// <param name="client">
        /// The <see cref="IrcClient"/> this UserInfo belongs to.
        /// </param>
        /// <param name="host">
        /// A host as described in rfc 1459 as a <see cref="System.String"/>.
        /// </param>
        public UserInfo(IrcClient client, string host)
        {
            Match hostPieces;
            hostPieces = hostRegex.Match(host);
            if (hostPieces.Success)
            {
                nickName = hostPieces.Groups[1].Value;
                ident = hostPieces.Groups[2].Value;
                this.host = hostPieces.Groups[3].Value;
                this.client = client;
            }
            else
            {
                throw new ArgumentException("Malformed userhost can't be parsed correctly", "host");
            }
        }
        
        /// <summary>
        /// Initializes a new instance of the UserInfo class based on the given <see cref="IrcLine" />.
        /// </summary>
        /// <param name="baseLine">
        /// The <see cref="IrcLine"/> this UserInfo was build from.
        /// </param>
        public UserInfo(IrcLine baseLine)
        {
            this.baseLine = baseLine;
            Match hostPieces;
            hostPieces = hostRegex.Match(BaseLine.Prefix);
            if (hostPieces.Success)
            {
                nickName = hostPieces.Groups[1].Value;
                ident = hostPieces.Groups[2].Value;
                this.host = hostPieces.Groups[3].Value;
                this.client = BaseLine.Client;
            }
            else
            {
                throw new ArgumentException("Malformed userhost can't be parsed correctly", "baseLine");
            }
        }

        /// <summary>
        /// Initializes a new instance of the UserInfo class with the given values.
        /// </summary>
        /// <param name="nickName">The nickname of the user.</param>
        /// <param name="ident">The ident of the user.</param>
        /// <param name="host">The user host.</param>
        /// <param name="client">The client, where the user was seen on.</param>
        public UserInfo(string nickName, string ident, string host, IrcClient client)
        {
            this.nickName = nickName;
            this.ident = ident;
            this.host = host;
            this.client = client;
        }
        
        /// <summary>
        /// Initializes a new instance of the UserInfo class, based on an existing UserInfo.
        /// </summary>
        /// <param name="source">The UserInfo instance to copy from.</param>
        public UserInfo(UserInfo source) 
        {
            baseLine = source.BaseLine;
            client = source.Client;
            host = source.Host;
            ident = source.Ident;
            nickName = source.NickName;
        }
        
        /// <summary>
        /// Gets the nickname of the user.
        /// </summary>
        /// <value>
        /// The nickname of this UserInfo.
        /// </value>
        public string NickName 
        {
            get { return nickName; }
        }
        
        /// <summary>
        /// Gets the ident of the user.
        /// </summary>
        /// <value>
        /// The ident of this UserInfo.
        /// </value>
        public string Ident 
        {
            get { return ident; }
        }
        
        /// <summary>Gets the host of the user.</summary>
        /// <value>
        /// The host of this UserInfo.
        /// </value>
        public string Host 
        {
            get { return host; }
        }
        
        /// <summary>
        /// Gets the IrcLine, this UserInfo was build from.
        /// </summary>
        /// <value>
        /// The <see cref="IrcLine"/>, this UserInfo was build from.
        /// </value>
        /// <remarks>
        /// This property is null if UserInfo wasn't build from an IrcLine but from a raw user host.
        /// </remarks>
        public IrcLine BaseLine
        {
            get { return baseLine; }
        }

        #region IIrcObject implementation
        /// <summary>
        /// Gets the IrcClient, this UserInfo belongs to.
        /// </summary>
        /// <value>
        /// The <see cref="IrcClient"/> this UserInfo belongs to.
        /// </value>
        public IrcClient Client 
        {
            get { return client; }
        }
        #endregion
        
        /// <summary>
        /// Compare this UserInfo with other objects.
        /// </summary>
        /// <param name="obj">
        /// The object to compare with.
        /// </param>
        /// <returns>
        /// True if obj is a UserInfo representing the same host as this UserInfo,
        /// false otherwise.
        /// </returns>
        public override bool Equals(object obj)
        {
            UserInfo info = obj as UserInfo;
            if (info != null)
            {                
                if (!info.Host.Equals(Host))
                    return false;
                if (!info.Ident.Equals(Ident))
                    return false;
                if (!info.NickName.Equals(NickName))
                    return false;
                
                return true;
            }
            else
                return base.Equals(obj);
        }
        
        /// <summary>
        /// Gets the hashcode of this UserInfo.
        /// </summary>
        /// <returns>The hashcode as an int.</returns>
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
        
        /// <summary>
        /// Gives back the raw host this UserInfo was created from.
        /// </summary>
        /// <returns>
        /// The full raw host as a <see cref="System.String"/>.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0}!{1}@{2}", NickName, Ident, Host);
        }
    }
}
