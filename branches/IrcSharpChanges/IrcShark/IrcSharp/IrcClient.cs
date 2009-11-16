using System.IO;
// $Id$
// 
// Note:
// 
// Copyright (C) 2009 IrcShark Team
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

namespace IrcSharp
{
    using System;
    using System.Net.Sockets;

    /// <summary>
    /// Manages a simple irc connection on a low raw level.
    /// </summary>
    /// <remarks>
    /// With an instance of IrcClient, you can connect to an irc server and receive many events
    /// defined by the standard irc protocol. The messages received are automatically parsed to
    /// <see cref="IrcLine " /> objects to be easier accessible.
    /// </remarks>
    public class IrcClient
    {
        /// <summary>
        /// Saves the underlying tcp connection.
        /// </summary>
        private TcpClient client;
        
        /// <summary>
        /// This is set to true when the hello message was received from the server.
        /// </summary>
        private bool loggedIn;
        
        /// <summary>
        /// Saves the last nickname set and accepted by the server.
        /// </summary>
        private string currentNickname;
        
        /// <summary>
        /// Saves the username used when logged in to the server.
        /// </summary>
        private string username;
        
        /// <summary>
        /// The name of the network as received on login.
        /// </summary>
        private string network;
        
        /// <summary>
        /// Saves the reader to read from the tcp client.
        /// </summary>
        private StreamReader inStream;
        
        /// <summary>
        /// Saves the writer to write to the tcp client.
        /// </summary>
        private StreamWriter outStream;
        
        /// <summary>
        /// The used standard of this irc connection.
        /// </summary>
        private IrcStandardDefinition usedStandard;

        #region "IrcClient EventHandler"
        public delegate void ConnectEventHandler(object sender, ConnectEventArgs e);
        public delegate void LoginEventHandler(object sender, LoginEventArgs e);
        public delegate void LineReceivedEventHandler(object sender, LineReceivedEventArgs e);
        public delegate void PingReceivedEventHandler(object sender, PingReceivedEventArgs e);
        public delegate void JoinReceivedEventHandler(object sender, JoinReceivedEventArgs e);
        public delegate void PartReceivedEventHandler(object sender, PartReceivedEventArgs e);
        public delegate void QuitReceivedEventHandler(object sender, QuitReceivedEventArgs e);
        public delegate void NickChangeReceivedEventHandler(object sender, NickChangeReceivedEventArgs e);
        public delegate void ModeReceivedEventHandler(object sender, ModeReceivedEventArgs e);
        public delegate void NoticeReceivedEventHandler(object sender, NoticeReceivedEventArgs e);
        public delegate void PrivateMessageReceivedEventHandler(object sender, PrivateMessageReceivedEventArgs e);
        public delegate void NumericReceivedEventHandler(object sender, NumericReceivedEventArgs e);
        public delegate void KickReceivedEventHandler(object sender, KickReceivedEventArgs e);
        public delegate void ErrorEventHandler(object sender, ErrorEventArgs e);
        #endregion
        
        #region "IrcClient events"
        /// <summary>
        /// OnConnect is raised when the connection to an irc server was established.
        /// </summary>
        /// <remarks>This event is raised befor the login to the server.</remarks>
        public event ConnectEventHandler OnConnect;
        /// <summary>
        /// OnLogin event is raised when you succesfully loged in to an irc server.
        /// </summary>
        public event LoginEventHandler OnLogin;
        /// <summary>
        /// This event is raised for all lines received from the server.
        /// </summary>
        /// <remarks>This event is always called when a line was received. The line is parsed to an IrcLine object for easier use.</remarks>
        public event LineReceivedEventHandler LineReceived;
        /// <summary>
        /// PingReceived even tis raised when a PING was received from server.
        /// </summary>
        public event PingReceivedEventHandler PingReceived;
        /// <summary>
        /// JoinReceived is raised when ever a JOIN command was received by the server.
        /// </summary>
        /// <remarks>This event is raised by ALL join lines received, not only the ones of the IrcClient it self. If the IrcClient is in a channel, and another users joins this channel, the event is raised, too.</remarks>
        public event JoinReceivedEventHandler JoinReceived;
        /// <summary>
        /// PartReceived is raised when ever a PART command was received by the server.
        /// </summary>
        /// <remarks>This event is raised by ALL part lines received, not only the ones of the IrcClient it self. If the IrcClient is in a channel, and another users parts this channel, the event is raised, too.</remarks>
        public event PartReceivedEventHandler PartReceived;
        /// <summary>
        /// QuitReceived event is raised for all QUIT lines received from server.
        /// </summary>
        public event QuitReceivedEventHandler QuitReceived;
        /// <summary>
        /// The NickChangeReceived event is always raised wenn a NICK line was received from server.
        /// </summary>
        /// <remarks>This event is send for any NICK line, not only for your own nickchanges.</remarks>
        public event NickChangeReceivedEventHandler NickChangeReceived;
        /// <summary>
        /// ModeReceived event ist raised when the client receives a MODE line.
        /// </summary>
        /// <remarks>The mode line is parsed to make the single modes set easily accessible.</remarks>
        public event ModeReceivedEventHandler ModeReceived;
        /// <summary>
        /// NoticeReceived event is raised for all NOTICE lines received from server.
        /// </summary>
        public event NoticeReceivedEventHandler NoticeReceived;
        /// <summary>
        /// PrivateMessageReceived event is raised for all NOTICE lines received from server.
        /// </summary>
        public event PrivateMessageReceivedEventHandler PrivateMessageReceived;
        /// <summary>
        /// NumericReceived event is raised for all numeric replys received from server.
        /// </summary>
        public event NumericReceivedEventHandler NumericReceived;
        /// <summary>
        /// KickReceived is raised when ever a KICK command was received by the server.
        /// </summary>
        /// <remarks>This event is raised by all kick lines received.</remarks>
        public event KickReceivedEventHandler KickReceived;

        public event ErrorEventHandler Error;
        #endregion
        
        /// <summary>
        /// Initializes a new instance of the IrcClient class without any address to connect to.
        /// </summary>
        public IrcClient()
        {
            client = new TcpClient();
        }
    }
}
