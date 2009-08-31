using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp.Extended
{
    /// <summary>
    /// Represents a full irc-connection with channel management and internal address list.
    /// </summary>
    /// <remarks>This class uses all classes of IrcSharp to manage an irc connection.</remarks>
    public class IrcConnection : IrcClient
    {
        private ChannelManager channels;
        private static int instanceCount = 0;
        private int connectionID;

        public IrcConnection() : base()
        {
            channels = new ChannelManager(this);
            connectionID = ++instanceCount;
        }

        public IrcClient Client
        {
            get { return this; }
        }

        /// <summary>
        /// Gives access to the ChannelManager object for the curent IrcConnection
        /// </summary>
        /// <value>The ChannelManager object of the current connection.</value>
        public ChannelManager Channels
        {
            get { return channels; }
        }

        public int ConnectionID
        {
            get { return connectionID; }
        }

        public void Close()
        {
            if (IsConnected) 
                Dispose();
        }
    }
}
