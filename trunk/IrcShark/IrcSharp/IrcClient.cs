using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Threading;

namespace IrcSharp
{
    /// <summary>
    /// Represents a full connection to an irc server with all nesessary members to communicate with it.
    /// </summary>
    /// <remarks>With an instance of IrcClient, you can connect to an irc server and receive many events defined by the standard irc protocol. The messages received are automatically parsed to IrcLine objects to be easier accessible.</remarks>
    public class IrcClient : IIrcObject, IDisposable
    {
        private IrcServerEndPoint ServerAddressValue;
        private StreamReader InValue;
        private StreamWriter OutValue;
        private Thread ReaderThreadValue;
        private Boolean LoggedInValue;
        private TcpClient ClientValue;
        private String UsernameValue;
        private String CurrentNickValue;
        private String NetworkNameValue;
        private IrcStandardDefinition StandardValue;
        private UserInfo MyUserInfoValue;

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

        #region "IrcClient constructors"
        /// <summary>
        /// Creates a new instance of an IrcClient.
        /// </summary>
        public IrcClient()
        {
            ClientValue = new TcpClient();
            LoggedInValue = false;
            CurrentNickValue = "Default";
            UsernameValue = "Default";
            StandardValue = new IrcStandardDefinition(this);
        }
        #endregion

        #region "public IrcClient propertys"
        /// <summary>
        /// Represents the login state of the current connection.
        /// </summary>
        /// <remarks>After connecting to an irc server, an irc client need to identify it self with a nick- and username. Until the identification wasn't accepted bey the server, you are not logged in and can't join channels or execute other irc command. This property help you to determine if you are logged in or ligin is pending at the moment.</remarks>
        /// <value>
        /// true, if the user is logged in correctly
        /// false, if the login is pending or the IrcClient is not connected
        /// </value>
        public bool LoggedIn
        {
            get
            {
                return Connected && LoggedInValue;
            }
        }

        /// <summary>
        /// Determines the connection state of the IrcClient.
        /// </summary>
        /// <value>true, if the connection is open, else false</value>
        public bool Connected
        {
            get
            {
                if (ClientValue == null) return false;
                return ClientValue.Connected;
            }
        }

        /// <summary>
        /// The address of the server, what should be connected to.
        /// </summary>
        /// <value>a server address represented by an <see cref="IrcServerEndPoint"/></value>
        public IrcServerEndPoint ServerAddress
        {
            get
            {
                return ServerAddressValue;
            }
            set
            {
                ServerAddressValue = value;
            }
        }

        /// <summary>
        /// The username that should be used on login.
        /// </summary>
        /// <remarks>The value can only be changed if the <see cref="IrcClient"/> isn't connected.</remarks>
        /// <value>the current username setted</value>
        public String Username
        {
            get
            {
                return UsernameValue;
            }
            set
            {
                UsernameValue = value;
            }
        }

        /// <summary>
        /// Saves the nickname currently set for this irc connection.
        /// </summary>
        /// <remarks>You can't change the value of this property directly. Use <see cref="ChangeNickname"/> to request a nickchange. The connection waits for the acknowledge from the server. This property will never have a nick other than known by the server.</remarks>
        /// <value>the current nickname of the IrcClient</value>
        public String CurrentNick
        {
            get
            {
                return CurrentNickValue;
            }
        }

        /// <summary>
        /// Gives you the networkname of the network the <see cref="IrcClient"/> is connected to at the moment
        /// </summary>
        /// <remarks>This value is only set after a successfull login.</remarks>
        /// <value>a string of the network name</value>
        public String NetworkName
        {
            get
            {
                return NetworkNameValue;
            }
        }

        /// <summary>
        /// The standard, used by this irc connection as described by the server.
        /// </summary>
        /// <remarks>The standard of an irc connection can change while connection is up. See <see cref="IrcStandardDefinition"/> for more inforamtion.</remarks>
        /// <value>the standard</value>
        public IrcStandardDefinition Standard
        {
            get { return StandardValue; }
        }

        /// <summary>
        /// The <see cref="UserInfo"/> for the client it self.
        /// </summary>
        /// <value>the <see cref="UserInfo"/> of the own client</value>
        public UserInfo MyUserInfo
        {
            get { return MyUserInfoValue; }
        }
        #endregion

        #region "public IrcClient methods"
        /// <summary>
        /// Connects to the irc server addressed by <see cref="ServerAddress"/>.
        /// </summary>
        public void Connect()
        {
            IAsyncResult test;
            if (ServerAddress == null) return;
            try
            {
                test = ClientValue.BeginConnect(ServerAddress.Address, ServerAddress.Port, null, this);
                test.AsyncWaitHandle.WaitOne();
                if (Connected)
                {
                    ConnectEventArgs args = new ConnectEventArgs(this);
                    if (OnConnect != null)
                        OnConnect(this, new ConnectEventArgs(this));
                    InValue = new StreamReader(ClientValue.GetStream(), System.Text.Encoding.Default);
                    OutValue = new StreamWriter(ClientValue.GetStream(), System.Text.Encoding.Default);
                    ReaderThreadValue = new Thread(new ThreadStart(ReadLines));
                    ReaderThreadValue.Start();
                    if (!args.Handled)
                    {
                        SendLine("NICK " + CurrentNick);
                        SendLine("USER " + Username + " \"\" \"" + ServerAddress.Address.ToString() + "\" :" + Username);
                    }
                }
                else
                {
                    OnError(new ErrorEventArgs(this, "Couldn't connect to given address"));
                }
            }
            catch (Exception ex)
            {
                OnError(new ErrorEventArgs(this, "Couldn't connect to given address", ex));   
            }
        }

        /// <summary>
        /// Sends a raw irc line to the irc server.
        /// </summary>
        /// <remarks>You can send anything you want. The text is send as is. There is no checking or something.</remarks>
        /// <param name="line">the raw line to send</param>
        public void SendLine(String line)
        {
            if (Connected)
            {
                try
                {
                    OutValue.WriteLine(line);
                    OutValue.Flush();
                }
                catch(Exception ex)
                {
                    OnError(new ErrorEventArgs(this, "Couldn't send line", ex));
                }
            }
        }

        /// <summary>
        /// Sends a request to the server, to change the current nickname used by this connection.
        /// </summary>
        /// <remarks>
        /// 	<para>This method creates a NICK irc command and send it to the server. The property <see cref="CurrentNick"/> will be updated after the client received the acknowledge from server.</para>
        /// 	<para>Be sure that <paramref name="newNick"/> is conform with the <see cref="Standard"/> used by this connection. Else the nickname will not be changed.</para>
        /// </remarks>
        /// <param name="newNick">The new nickname, what should be used.</param>
        public void ChangeNickname(String newNick)
        {
            if (newNick == "") return;
            if (Connected)
            {
                SendLine("NICK " + newNick);
            }
            else
            {
                CurrentNickValue = newNick;
            }
        }

        /// <summary>
        /// Joins a given channel on the irc server.
        /// </summary>
        /// <remarks>The given <paramref name="chanName"/> should follow the given <see cref="Standard"/> of the IrcClient. The IrcClient will wait for the acknowledge of the server.</remarks>
        /// <param name="chanName">the channel name the client should join</param>
        public void Join(String chanName)
        {
            SendLine(String.Format("JOIN {0}", chanName));
        }

        /// <summary>
        /// Parts a given channel on the irc server.
        /// </summary>
        /// <remarks><paramref name="chanName"/> should follow the given <see cref="Standard"/> of the IrcClient. The IrcClient will wait for the acknowledge of the server.</remarks>
        /// <param name="chanName">the channel name the client should join</param>
        public void Part(String chanName)
        {
            SendLine(String.Format("PART {0}", chanName));
        }
        #endregion

        #region "private IrcClient methods"
        /// <summary>
        /// Reads lines from server and parses them to an IrcLine.
        /// </summary>
        private void ReadLines()
        {
            String Line;
            while (Connected)
            {
                try
                {
                    if (InValue == null) return;
                    Line = InValue.ReadLine();
                    if (Line != null && LineReceived != null)
                    {
                        LineReceivedEventArgs args = new LineReceivedEventArgs(new IrcLine(this, Line));
                        if (LineReceived != null) LineReceived(this, args);
                        if (!args.Handled) HandleLine(args);
                    }
                }
                catch (InvalidLineFormatException ex)
                {
                    OnError(new ErrorEventArgs(this, ex.Message, ex));
                    return;
                }
                catch (Exception ex)
                {
                    OnError(new ErrorEventArgs(this, "Couldn't receive line", ex));
                    return;
                }
            }
        }

        /// <summary>
        /// Handles a line received from server.
        /// </summary>
        private void HandleLine(LineReceivedEventArgs e)
        {
            if (e.Line.IsNumeric)
            {
                if (NumericReceived != null) NumericReceived(this, new NumericReceivedEventArgs(e.Line));
                switch (e.Line.Numeric)
                {
                    case 1: // Parse the Server Info
                        CurrentNickValue = e.Line.Parameters[0];
                        NetworkNameValue = e.Line.Parameters[1].Split(' ')[3];
                        MyUserInfoValue = new UserInfo(e.Line.Parameters[1].Split(' ')[6], this);
                        break;

                    case 3: // Parse Welcome-Message
                        LoggedInValue = true;
                        if (OnLogin != null) 
                            OnLogin(this, new LoginEventArgs(NetworkName, CurrentNick, this));
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
                        if (PingReceived != null) PingReceived(this, pingArgs);
                        if (!pingArgs.Handled)
                            if (e.Line.Parameters.Length > 0)
                                SendLine("PONG :" + e.Line.Parameters[0]);
                            else
                                SendLine("PONG");
                        break;

                    case "JOIN": //Parse Join-Message
                        JoinReceivedEventArgs joinArgs = new JoinReceivedEventArgs(e.Line);
                        if (JoinReceived != null) JoinReceived(this, joinArgs);
                        break;

                    case "PART": //Parse Part-Message
                        PartReceivedEventArgs partArgs = new PartReceivedEventArgs(e.Line);
                        if (PartReceived != null) PartReceived(this, partArgs);
                        break;

                    case "QUIT": //Parse Quit-Message
                        QuitReceivedEventArgs quitArgs = new QuitReceivedEventArgs(e.Line);
                        if (QuitReceived != null) QuitReceived(this, quitArgs);
                        break;

                    case "NICK": //Parse Nick-Message
                        NickChangeReceivedEventArgs nickChangeArgs = new NickChangeReceivedEventArgs(e.Line);
                        if (NickChangeReceived != null) NickChangeReceived(this, nickChangeArgs);
                        break;

                    case "MODE": //Parse Mode-Message
                        ModeReceivedEventArgs modeArgs = new ModeReceivedEventArgs(e.Line);
                        if (ModeReceived != null) ModeReceived(this, modeArgs);
                        break;

                    case "NOTICE":
                        NoticeReceivedEventArgs noticeArgs = new NoticeReceivedEventArgs(e.Line);
                        if (NoticeReceived != null) NoticeReceived(this, noticeArgs);
                        break;

                    case "PRIVMSG":
                        PrivateMessageReceivedEventArgs privmsgArgs = new PrivateMessageReceivedEventArgs(e.Line);
                        if (PrivateMessageReceived != null) PrivateMessageReceived(this, privmsgArgs);
                        break;

                    case "KICK":
                        KickReceivedEventArgs kickArgs = new KickReceivedEventArgs(e.Line);
                        if (KickReceived != null) KickReceived(this, kickArgs);
                        break;

                    default:
                        e.Handled = false;
                        break;
                }
            }
        }

        private void OnError(ErrorEventArgs args)
        {
            if (Error != null)
                Error(this, args);
        }
        #endregion

        #region "IIrcObject Member"
        /// <summary>
        /// Returns the associated irc connection.
        /// </summary>
        /// <value>the associated IrcClient</value>
        public IrcClient Client
        {
            get
            {
                return this;
            }
        }
        #endregion

        #region IDisposable Member

        public void Dispose()
        {
            if (ClientValue.Connected)
            {
                ReaderThreadValue.Abort();
                SendLine("QUIT");
                ClientValue.GetStream().Dispose();
            }

        }

        #endregion
    }
}
