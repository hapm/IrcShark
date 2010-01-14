// <copyright file="IrcStandardDefinition.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the IrcStandardDefinition class.</summary>

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
    
    /// <summary>
    /// This class saves the standard, used by a server, the associated <see cref="IrcShark.Chatting.Irc.IrcClient"/> is connected to.
    /// </summary>
    public class IrcStandardDefinition : IIrcObject
    {
        /// <summary>
        /// Saves the IrcClient for this IrcStandardDefinition.
        /// </summary>
        private IrcClient client;
        
        /// <summary>
        /// Initializes a new instance of the IrcStandardDefinition class and associates it with the given client.
        /// </summary>
        /// <param name="client">
        /// The <see cref="IrcClient"/>, this StandardDefinition belongs to.
        /// </param>
        public IrcStandardDefinition(IrcClient client)
        {
            this.client = client;
        }
        
        /// <summary>
        /// Gets all supportet channel prefixes.
        /// </summary>
        /// <value>
        /// A char array of all supportet channel prefixes.
        /// </value>
        /// <remarks>
        /// The channel prefixes are used to mark a sender or target of irc messages as a channel.
        /// </remarks>
        public char[] ChannelPrefixes
        {
            get { return new char[] { '#', '&' }; }
        }
        
        /// <summary>
        /// Gets all user prefixes.
        /// </summary>
        /// <value>
        /// A char array of available user prefixes.
        /// </value>
        /// <remarks>
        /// User prefixes are used in WHO replys to show the channel mode of the user.
        /// </remarks>
        public char[] UserPrefixes
        {
            get { return new char[] { '@', '+' }; }
        }
        
        /// <summary>
        /// Gets all available user flags of this IrcStandardDefinition.
        /// </summary>
        /// <value>
        /// A FlagDefinition array of user flags.
        /// </value>
        public FlagDefinition[] UserFlags 
        {
            get 
            {
                return new FlagDefinition[] 
                {
                    new FlagDefinition('i', ModeArt.User), // invisible flag
                    new FlagDefinition('s', ModeArt.User), // receipt of server notices
                    new FlagDefinition('w', ModeArt.User), // receive wallops
                    new FlagDefinition('o', ModeArt.User)  // ircop flag
                };
            }
        }
        
        /// <summary>
        /// Gets all available user prefix flags.
        /// </summary>
        /// <value>
        /// An array of FlagDefinitions.
        /// </value>
        public FlagDefinition[] UserPrefixFlags 
        {
            get 
            {
                // TODO: Add this user prefix <-> flag definitions
                // UserPrefixFlagsValue.Add(UserPrefixes[0], ChannelFlagsValue[0]);
                // UserPrefixFlagsValue.Add(UserPrefixes[1], ChannelFlagsValue[1]);
                return new FlagDefinition[] { };
            }
        }
        
        /// <summary>
        /// Gets all available channel flags.
        /// </summary>
        /// <value>An array of FlagDefinitions.</value>
        public FlagDefinition[] ChannelFlags
        {
            get 
            {
                return new FlagDefinition[] 
                {
                    new FlagDefinition('o', ModeArt.Channel, FlagParameter.Required), // channel op
                    new FlagDefinition('v', ModeArt.Channel, FlagParameter.Required), // voice
                    new FlagDefinition('p', ModeArt.Channel), // private channel
                    new FlagDefinition('s', ModeArt.Channel), // secret channel
                    new FlagDefinition('i', ModeArt.Channel), // invite only channel
                    new FlagDefinition('t', ModeArt.Channel), // only ops can change topic
                    new FlagDefinition('n', ModeArt.Channel), // no messages from outsiders
                    new FlagDefinition('m', ModeArt.Channel), // moderated channel
                    new FlagDefinition('l', ModeArt.Channel, FlagParameter.Required, FlagParameter.None), // limited users
                    new FlagDefinition('b', ModeArt.Channel, FlagParameter.Optional, FlagParameter.Required), // ban hostmask
                    new FlagDefinition('k', ModeArt.Channel, FlagParameter.Required, FlagParameter.None), // private channel
                };
            }
        }
        
        /// <summary>
        /// Gets an identifiing string for the version of this standard definition.
        /// </summary>
        /// <value>The version used for the IrcStandardDefinition.</value>
        /// <remarks>
        /// The default is "rfc1459" what is a very old but still used rfc for the irc protocol.
        /// </remarks>
        public string Version
        {
            get { return "rfc1459"; }
        }
        
        #region IIrcObject implementation
        /// <summary>
        /// Gets the client the standard instance belongs to.
        /// </summary>
        /// <value>
        /// The client the standard instance belongs to.
        /// </value>
        public IrcClient Client 
        {
            get { return client; }
        }
        #endregion
        
        /// <summary>
        /// Checks if the given string can be used as a channelname in this standard.
        /// </summary>
        /// <param name="channelName">The name to test.</param>
        /// <returns>Returns true, if the name can be used, false otherwise.</returns>
        public bool IsAllowedChannel(string channelName) 
        {
            if (channelName.Length < 2)
                return false;
            
            foreach (char pre in ChannelPrefixes)
            {
                if (channelName[0] == pre)
                    return true;
            }
            return false;            
        }
        
        /// <summary>
        /// Gets the FlagDefinition for the given flag if it is a user prefix flag.
        /// </summary>
        /// <param name="flag">The flag character.</param>
        /// <returns>The FlagDefinition for the given flag or null if the flag wasn't found.</returns>
        public FlagDefinition GetUserPrefixFlag(char flag) 
        {
            foreach (FlagDefinition f in UserPrefixFlags) 
            {
                if (f.Character == flag)
                    return f;
            }
            return null;
        }
    }
}
