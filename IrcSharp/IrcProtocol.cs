// <copyright file="IrcProtocol.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Place a summary here.</summary>

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
namespace IrcSharp
{
    using System;
    using IrcShark.Chatting;

    /// <summary>
    /// Describes the internet relay chat protocol and its parameters.
    /// </summary>
    /// <remarks>
    /// This class is a singleton and therfor can't be instanciated more than once.
    /// </remarks>
    public class IrcProtocol : IProtocol
    {
        /// <summary>
        /// Saves the singleton instance.
        /// </summary>
        private static IrcProtocol instance;
        
        /// <summary>
        /// Prevents a default instance of the IrcProtocol class from being created.
        /// </summary>
        private IrcProtocol()
        {
        }
        
        /// <summary>
        /// Gets a value indicating whether the protocol supports multible networks.
        /// </summary>
        /// <value>Its true, if multible networks are supported, false otherwise.</value>
        public bool MultiNetwork 
        {
            get { return true; }
        }
        
        /// <summary>
        /// Gets a value indicating whether the protocol supports multible servers for one network.
        /// </summary>
        /// <value>Its true, if multible servers are supported, false otherwise.</value>
        public bool MultiServer 
        {
            get { return true; }
        }
        
        /// <summary>
        /// Gets the name of the protocol.
        /// </summary>
        /// <value>The name as a string.</value>
        public string Name 
        {
            get { return "IRC"; }
        }
        
        /// <summary>
        /// Gets the instance of the protocol representation class.
        /// </summary>
        /// <returns>The singleton instance.</returns>
        public static IrcProtocol GetInstance() 
        {
            if (instance == null)
                instance = new IrcProtocol();
            return instance;
        }
        
        /// <summary>
        /// Creates a new network configuration, for the implemented protocol.
        /// </summary>
        /// <param name="name">The name of the network configuration.</param>
        /// <returns>The new instance of the network configuration.</returns>
        public INetwork CreateNetwork(string name)
        {
            return new IrcNetwork(name);
        }
    }
}
