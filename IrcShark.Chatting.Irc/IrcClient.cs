// <copyright file="IrcClient.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the IrcClient class.</summary>

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
    using System.IO;
    using System.Net.Sockets;

    /// <summary>
    /// Manages a simple irc connection on a low raw level.
    /// </summary>
    /// <remarks>
    /// With an instance of IrcClient, you can connect to an irc server and receive many events
    /// defined by the standard irc protocol. The messages received are automatically parsed to
    /// <see cref="IrcLine " /> objects to be easier accessible. After connection was established,
    /// you should call <see cref="ReceiveLine" /> to get the lines from the server.
    /// </remarks>
    public class IrcClient : IDisposable
    {
        /// <summary>
        /// Saves the underlying tcp connection.
        /// </summary>
        private TcpClient client;
        
        /// <summary>
        /// Saves the info about the connected user it self.
        /// </summary>
        private UserInfo self;
        
        /// <summary>
        /// This is set to true when the hello message was received from the server.
        /// </summary>
        private bool loggedIn;
        
        /// <summary>
        /// Saves the last nickname set and accepted by the server.
        /// </summary>
        private string currentNickname;
        
        /// <summary>
        /// Saves the username.
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
        
        /// <summary>
        /// Saves the address, that the client should connect to.
        /// </summary>
        private IrcServerEndPoint serverAddress;
        
        /// <summary>
        /// Initializes a new instance of the IrcClient class without any address to connect to.
        /// </summary>
        public IrcClient()
        {
            client = new TcpClient();
            currentNickname = "Default";
            username = "Default";
            usedStandard = new IrcStandardDefinition(this);
        }

        #region "IrcClient EventHandler"
        /// <summary>
        /// The handler for the IrcClient.OnConnect event.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event args for this event.</param>
        public delegate void ConnectEventHandler(object sender, ConnectEventArgs e);
        
        /// <summary>
        /// The handler for the IrcClient.OnLogin event.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event args for this event.</param>
        public delegate void LoginEventHandler(object sender, LoginEventArgs e);
        
        /// <summary>
        /// The handler for the IrcClient.LineReceived event.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event args for this event.</param>
        public delegate void LineReceivedEventHandler(object sender, LineReceivedEventArgs e);
        
        /// <summary>
        /// The handler for the IrcClient.PingReceived event.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event args for this event.</param>
        public delegate void PingReceivedEventHandler(object sender, PingReceivedEventArgs e);
        
        /// <summary>
        /// The handler for the IrcClient.JoinReceived event.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event args for this event.</param>
        public delegate void JoinReceivedEventHandler(object sender, JoinReceivedEventArgs e);
        
        /// <summary>
        /// The handler for the IrcClient.PartReceived event.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event args for this event.</param>
        public delegate void PartReceivedEventHandler(object sender, PartReceivedEventArgs e);
        
        /// <summary>
        /// The handler for the IrcClient.QuitReceived event.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event args for this event.</param>
        public delegate void QuitReceivedEventHandler(object sender, QuitReceivedEventArgs e);
        
        /// <summary>
        /// The handler for the IrcClient.NickChangeReceived event.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event args for this event.</param>
        public delegate void NickChangeReceivedEventHandler(object sender, NickChangeReceivedEventArgs e);
        
        /// <summary>
        /// The handler for the IrcClient.ModeReceived event.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event args for this event.</param>
        public delegate void ModeReceivedEventHandler(object sender, ModeReceivedEventArgs e);
        
        /// <summary>
        /// The handler for the IrcClient.NoticeReceived event.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event args for this event.</param>
        public delegate void NoticeReceivedEventHandler(object sender, NoticeReceivedEventArgs e);
        
        /// <summary>
        /// The handler for the IrcClient.MessageReceived event.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event args for this event.</param>
        public delegate void MessageReceivedEventHandler(object sender, MessageReceivedEventArgs e);
        
        /// <summary>
        /// The handler for the IrcClient.NumericReceived event.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event args for this event.</param>
        public delegate void NumericReceivedEventHandler(object sender, NumericReceivedEventArgs e);
        
        /// <summary>
        /// The handler for the IrcClient.KickReceived event.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event args for this event.</param>
        public delegate void KickReceivedEventHandler(object sender, KickReceivedEventArgs e);
        
        /// <summary>
        /// The handler for the IrcClient.Error event.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event args for this event.</param>
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
        /// PrivateMessageReceived event is raised for all MSG lines received from server.
        /// </summary>
        public event MessageReceivedEventHandler MessageReceived;
        
        /// <summary>
        /// NumericReceived event is raised for all numeric replys received from server.
        /// </summary>
        public event NumericReceivedEventHandler NumericReceived;
        
        /// <summary>
        /// KickReceived is raised when ever a KICK command was received by the server.
        /// </summary>
        /// <remarks>This event is raised by all kick lines received.</remarks>
        public event KickReceivedEventHandler KickReceived;

        /// <summary>
        /// The Error event is raised when there is an error with the irc connection.
        /// </summary>
        /// <remarks>
        /// This event is raised if the connection aborts unnormal or
        /// there was a malformed line received from the server.
        /// </remarks>
        public event ErrorEventHandler Error;
        #endregion
        
        /// <summary>
        /// Gets the standard used on this irc connection.
        /// </summary>
        /// <value>The used standard.</value>
        public IrcStandardDefinition Standard
        {
            get { return usedStandard; }
        }
        
        /// <summary>
        /// Gets a value indicating whether the client is connected or not.
        /// </summary>
        /// <value>Its true, if the connection is up and running, false otherwise.</value>
        public bool Connected
        {
            get
            {
                if (client == null)
                {
                    return false;
                }
                
                return client.Connected;
            }
        }
        
        /// <summary>
        /// Gets a value indicating whether there are lines available.
        /// </summary>
        /// <value>Its true when there are lines to receive, false otherwise.</value>
        /// <remarks>
        /// If the value is true, <see cref="ReceiveLine" /> should be used to read all
        /// until LinesAvailable get false.
        /// </remarks>
        public bool LinesAvailable
        {
            get
            {
                if (client == null)
                {
                    return false;
                }
                
                if (!client.Connected)
                {
                    return false;
                }
                
                return client.Available > 0;
            }
        }
        
        /// <summary>
        /// Gets or sets the address of the server, the client connects to.
        /// </summary>
        /// <value>The address as an EndPoint.</value>
        public IrcServerEndPoint ServerAddress
        {
            get { return serverAddress; }
            set { serverAddress = value; }
        }
        
        /// <summary>
        /// Gets or sets the clients current nickname.
        /// </summary>
        /// <value>The nickname as a string.</value>
        public string CurrentNickname
        {
            get { return currentNickname; }
            set
            {
                if (Connected)
                {
                    SendLine(string.Format("NICK {0}", value));
                }
                else
                {
                    currentNickname = value;
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the username used to connect to an irc server.
        /// </summary>
        /// <value>The username as a string.</value>
        /// <remarks>
        /// The username can only be changed when the client is not connected, because
        /// it is set on login and can't be changed afterwards.
        /// </remarks>
        public string Username
        {
            get
            {
                return username;
            }
            
            set
            {
                if (Connected)
                {
                    username = value;
                }
            }
        }
        
        /// <summary>
        /// Gets the name of the network, the client is connected to.
        /// </summary>
        /// <value>The name of the network or null if the client isn't connected at the moment.</value>
        public string Network
        {
            get { return network; }
        }
        
        /// <summary>
        /// Gets a value indicating whether there was a successfull login on this connection or not.
        /// </summary>
        /// <remarks>After connecting to an irc server, an irc client need to identify it self with a nick- and username. Until the identification wasn't accepted bey the server, you are not logged in and can't join channels or execute other irc command. This property help you to determine if you are logged in or ligin is pending at the moment.</remarks>
        /// <value>
        /// The value is true, if the user is logged in correctly and
        /// false, if the login is pending or the IrcClient is not connected.
        /// </value>
        public bool LoggedIn
        {
            get { return Connected && loggedIn; }
        }
        
        /// <summary>
        /// Gets the UserInfo for the user his self.
        /// </summary>
        /// <value>The UserInfo of the client user.</value>
        public UserInfo Self
        {
            get { return self; }
        }
        
        /// <summary>
        /// Connects to the irc server addressed by <see cref="ServerAddress"/>.
        /// </summary>
        public void Connect()
        {
            IAsyncResult test;
            if (ServerAddress == null)
            {
                return;
            }
            
            try
            {
                test = client.BeginConnect(ServerAddress.Address, ServerAddress.Port, null, this);
                test.AsyncWaitHandle.WaitOne();
                if (Connected)
                {
                    bool handled = OnOnConnect();
                    
                    inStream = new StreamReader(client.GetStream(), System.Text.Encoding.Default);
                    outStream = new StreamWriter(client.GetStream(), System.Text.Encoding.Default);
                    
                    if (!handled)
                    {
                        SendLine(string.Format("NICK {0}", CurrentNickname));
                        SendLine("USER " + Username + " \"\" \"" + ServerAddress.Address.ToString() + "\" :" + Username);
                    }
                }
                else
                {
                    OnError("Couldn't connect to given address");
                }
            }
            catch (Exception ex)
            {
                OnError("Couldn't connect to given address", ex);
            }
        }
        
        /// <summary>
        /// Reads a new line from the server.
        /// </summary>
        /// <remarks>
        /// This method blocks the calling thread until a new line was received from server.
        /// Only use this method, if you want to bypass the automatically raised events and kine handling.
        /// If you want to have this features use <see cref="IrcClient.ReceiveLine" /> instead.
        /// </remarks>
        /// <returns>The received line.</returns>
        public IrcLine ReadLine()
        {
            string line;
            if (Connected)
            {
                try
                {
                    if (inStream == null)
                    {
                        return null;
                    }
                    
                    line = inStream.ReadLine();
                    if (line != null)
                    {
                        IrcLine ircLine = new IrcLine(this, line);
                        return ircLine;
                    }
                }
                catch (InvalidLineFormatException ex)
                {
                    OnError(ex.Message, ex);
                }
                catch (Exception ex)
                {
                    OnError("Couldn't receive line", ex);
                }
            }
            
            return null;
        }
        
        /// <summary>
        /// Reads the next line, raise all events and handle it if needed.
        /// </summary>
        /// <remarks>
        /// This method does the same as <see cref="IrcClient.ReadLine" /> does but additionally
        /// raise all events of the line. If the line is a PING line, it is handled and automatically
        /// answered by an according PONG.
        /// </remarks>
        public void ReceiveLine()
        {
            IrcLine line = ReadLine();
            if (line == null)
            {
                return;
            }
            
            LineReceivedEventArgs args = new LineReceivedEventArgs(line);
            if (LineReceived != null)
            {
                LineReceived(this, args);
            }
            
            if (!args.Handled)
            {
                HandleLine(args);
            }
        }

        /// <summary>
        /// Sends a raw irc line to the irc server.
        /// </summary>
        /// <remarks>You can send anything you want. The text is send as is. There is no checking or something.</remarks>
        /// <param name="line">The raw line to send.</param>
        public void SendLine(string line)
        {
            if (Connected)
            {
                try
                {
                    outStream.WriteLine(line);
                    outStream.Flush();
                }
                catch (Exception ex)
                {
                    OnError("Couldn't send line", ex);
                }
            }
        }
        
        /// <summary>
        /// Sends a message to a channel or nick (in a query).
        /// </summary>
        /// <param name="message">The message to send.</param>
        /// <param name="receiver">The receiver (a channel or a nick).</param>
        public void SendMessage(string message, string receiver)
        {
            if (Connected)
            {
                SendLine(String.Format("PRIVMSG {0} :{1}", receiver, message));
            }
        }
        
        /// <summary>
        /// Sends a message to a channel, nick (directly).
        /// </summary>
        /// <param name="message">The message to send.</param>
        /// <param name="receiver">The receiver (a channel or a nick).</param>
        public void SendNotice(string message, string receiver)
        {
            if (Connected)
            {
                SendLine(string.Format("NOTICE {0} :{1}", receiver, message));
            }
        }

        /// <summary>
        /// Joins a given channel on the irc server.
        /// </summary>
        /// <remarks>
        /// The given <paramref name="chanName"/> should follow the given <see cref="Standard"/> of the IrcClient.
        /// The IrcClient will wait for the acknowledge of the server.
        /// </remarks>
        /// <param name="chanName">The channel name the client should join.</param>
        public void Join(string chanName)
        {
            SendLine(string.Format("JOIN {0}", chanName));
        }

        /// <summary>
        /// Parts a given channel on the irc server.
        /// </summary>
        /// <remarks>
        /// The <paramref name="chanName"/> should follow the given <see cref="Standard"/> of the IrcClient.
        /// The IrcClient will wait for the acknowledge of the server.
        /// </remarks>
        /// <param name="chanName">The channel name the client should join.</param>
        public void Part(string chanName)
        {
            SendLine(string.Format("PART {0}", chanName));
        }
        
        /// <summary>
        /// Quits the IRC connection with a Message.
        /// </summary>
        /// <param name="quitMsg">The message to use when quitting.</param>
        public void Quit(string quitMsg)
        {
            if (Connected)
            {
                SendLine(String.Format("QUIT :{0}", quitMsg));
            }
        }
        
        /// <summary>
        /// Quits the IRC Connection without a Message.
        /// </summary>
        public void Quit()
        {
            if (Connected)
            {
                SendLine("QUIT");
            }
        }
        
        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
            inStream.Dispose();
            outStream.Dispose();
            client.Close();
        }
        
        /// <summary>
        /// Fires the Error event.
        /// </summary>
        /// <param name="msg">The message for the error.</param>
        /// <param name="ex">The exception that occured.</param>
        protected virtual void OnError(string msg, Exception ex)
        {
            if (Error != null)
            {
                ErrorEventArgs args = new ErrorEventArgs(this, msg, ex);
                Error(this, args);
            }
        }
        
        /// <summary>
        /// Fires the Error event.
        /// </summary>
        /// <param name="msg">The message for the error.</param>
        protected virtual void OnError(string msg)
        {
            if (Error != null)
            {
                ErrorEventArgs args = new ErrorEventArgs(this, msg);
                Error(this, args);
            }
        }

        /// <summary>
        /// Handles a line received from server.
        /// </summary>
        /// <param name="e">The arguments for the received line.</param>
        private void HandleLine(LineReceivedEventArgs e)
        {
            if (e.Line.IsNumeric)
            {
                if (NumericReceived != null)
                {
                    NumericReceived(this, new NumericReceivedEventArgs(e.Line));
                }
                
                switch (e.Line.Numeric)
                {
                    case 1: // Parse the Server Info
                        currentNickname = e.Line.Parameters[0];
                        network = e.Line.Parameters[1].Split(' ')[3];
                        self = new UserInfo(this, e.Line.Parameters[1].Split(' ')[6]);
                        break;

                    case 3: // Parse Welcome-Message
                        OnOnLogin();                        
                        break;
                        
                    case 376: // End of MOTD message
                        //OnOnLogin();                        
                        break;
                }
            }
            else
            {
                e.Handled = true;
                switch (e.Line.Command)
                {
                    case "PING": // Handle the Ping here
                        PingReceivedEventArgs pingArgs = new PingReceivedEventArgs(e.Line);
                        if (PingReceived != null)
                        {
                            PingReceived(this, pingArgs);
                        }
                        
                        if (!pingArgs.Handled)
                        {
                            if (e.Line.Parameters.Length > 0)
                            {
                                SendLine("PONG :" + e.Line.Parameters[0]);
                            }
                            else
                            {
                                SendLine("PONG");
                            }
                        }
                        
                        break;

                    case "JOIN": // Parse Join-Message
                        JoinReceivedEventArgs joinArgs = new JoinReceivedEventArgs(e.Line);
                        if (JoinReceived != null)
                        {
                            JoinReceived(this, joinArgs);
                        }
                        
                        break;

                    case "PART": // Parse Part-Message
                        PartReceivedEventArgs partArgs = new PartReceivedEventArgs(e.Line);
                        if (PartReceived != null)
                        {
                            PartReceived(this, partArgs);
                        }
                        
                        break;

                    case "QUIT": // Parse Quit-Message
                        QuitReceivedEventArgs quitArgs = new QuitReceivedEventArgs(e.Line);
                        if (QuitReceived != null)
                        {
                            QuitReceived(this, quitArgs);
                        }
                        
                        break;

                    case "NICK": // Parse Nick-Message
                        if (e.Line.Client.ToString() == this.ToString())
                        {
                            this.currentNickname = e.Line.Parameters[0];
                        }
                        
                        NickChangeReceivedEventArgs nickChangeArgs = new NickChangeReceivedEventArgs(e.Line);
                        if (NickChangeReceived != null)
                        {
                            NickChangeReceived(this, nickChangeArgs);
                        }
                        
                        break;

                    case "MODE": // Parse Mode-Message
                        ModeReceivedEventArgs modeArgs = new ModeReceivedEventArgs(e.Line);
                        if (ModeReceived != null)
                        {
                            ModeReceived(this, modeArgs);
                        }
                        
                        break;

                    case "NOTICE": // Parse Notice-Message
                        NoticeReceivedEventArgs noticeArgs = new NoticeReceivedEventArgs(e.Line);
                        if (NoticeReceived != null)
                        {
                            NoticeReceived(this, noticeArgs);
                        }
                        
                        break;

                    case "PRIVMSG": // Parse Private-Message
                        MessageReceivedEventArgs privmsgArgs = new MessageReceivedEventArgs(e.Line);
                        if (MessageReceived != null)
                        {
                            MessageReceived(this, privmsgArgs);
                        }
                        
                        break;

                    case "KICK": // Parse Kick-Message
                        OnLineReceived(e.Line);
                        break;

                    default:
                        e.Handled = false;
                        break;
                }
            }
        }

        /// <summary>
        /// Fires the OnLogin event.
        /// </summary>
        protected virtual void OnOnLogin()
        {
            loggedIn = true;
            if (OnLogin != null) {
                OnLogin(this, new LoginEventArgs(Network, CurrentNickname, this));
            }
        }

        /// <summary>
        /// Fires the OnConnect event.
        /// </summary>
        protected virtual bool OnOnConnect()
        {
            if (OnConnect != null) {
                ConnectEventArgs args = new ConnectEventArgs(this);
                OnConnect(this, args);
                return args.Handled;
            }
            
            return false;
        }

        /// <summary>
        /// Fires the LineReceived event.
        /// </summary>
        /// <param name="line">The line that was received.</param>
        protected virtual void OnLineReceived(IrcLine line)
        {
            KickReceivedEventArgs kickArgs = new KickReceivedEventArgs(line);
            if (KickReceived != null) {
                KickReceived(this, kickArgs);
            }
        }
    }
}
