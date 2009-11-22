// <copyright file="MessageReceivedEventArgs.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the MessageReceivedEventArgs class.</summary>

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

    /// <summary>
    /// The MessageReceivedEventArgs belongs to the <see cref="MessageReceivedEventHandler" /> and the <see cref="IrcClient.MessageReceived" /> event.
    /// </summary>
    public class MessageReceivedEventArgs : IrcEventArgs
    {
        /// <summary>
        /// Saves the sender of the message.
        /// </summary>
        private UserInfo sender;
        
        /// <summary>
        /// Saves the ctcp command if the message is a ctcp message.
        /// </summary>
        private CtcpCommands ctcpCommand;
        
        /// <summary>
        /// Saves the ctcp command if the message is a ctcp message.
        /// </summary>
        private string ctcpCommandString;
        
        /// <summary>
        /// Saves the parameters of a ctcp command.
        /// </summary>
        private string ctcpParameters;

        /// <summary>
        /// Initializes a new instance of the MessageReceivedEventArgs class.
        /// </summary>
        /// <param name="line">The line with the message.</param>
        public MessageReceivedEventArgs(IrcLine line) : base(line)
        {
            sender = new UserInfo(line);
            string l;
            l = Message;
            
            if (l[0] == '\x01' && l[l.Length - 1] == '\x01')
            {
                l = l.Substring(1, l.Length - 2);
                ctcpCommandString = l;
                int firstSpace = l.IndexOf(' ');
                
                if (firstSpace > 0)
                {
                    ctcpCommandString = l.Substring(0, firstSpace);
                    ctcpParameters = l.Substring(firstSpace + 1);
                }
                
                switch (ctcpCommandString)
                {
                    case "ACTION":
                        ctcpCommand = CtcpCommands.Action;
                        break;
                        
                    case "VERSION":
                        ctcpCommand = CtcpCommands.Version;
                        break;
                        
                    default:
                        ctcpCommand = CtcpCommands.Unkown;
                        break;
                }
            }
            else
            {
                ctcpCommand = CtcpCommands.None;
            }
        }

        /// <summary>
        /// Gets the sender of the message.
        /// </summary>
        /// <value>The UserInfo about the sender.</value>
        public UserInfo Sender
        {
            get { return sender; }
        }

        /// <summary>
        /// Gets the destination, the message was sent to.
        /// </summary>
        /// <value>The name of the receiver.</value>
        public string Destination
        {
            get { return Line.Parameters[0]; }
        }

        /// <summary>
        /// Gets the message sent.
        /// </summary>
        /// <value>The message as string.</value>
        public string Message
        {
            get { return Line.Parameters[1]; }
        }

        /// <summary>
        /// Gets a value indicating whether the message is a ctcp message.
        /// </summary>
        /// <value>If the message is a ctcp message true, false otherwise.</value>
        public bool IsCtcp
        {
            get { return ctcpCommand != CtcpCommands.None; }
        }

        /// <summary>
        /// Gets the type of ctcp command.
        /// </summary>
        /// <value>The ctcp command type.</value>
        public CtcpCommands CtcpCommand
        {
            get { return ctcpCommand; }
        }

        /// <summary>
        /// Gets the name of the ctcp command.
        /// </summary>
        /// <value>The name of the command as a string.</value>
        public string CtcpCommandString
        {
            get { return ctcpCommandString; }
        }

        /// <summary>
        /// Gets the parameters of the ctcp command.
        /// </summary>
        /// <value>The parameters as a string.</value>
        public string CtcpParameters
        {
            get { return ctcpParameters; }
        }
    }
}
