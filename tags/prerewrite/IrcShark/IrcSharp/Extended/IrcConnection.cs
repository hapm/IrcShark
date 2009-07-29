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
        private ChannelManager ChannelsValue;
        private static int InstanceCount = 0;
        private int ConnectionIDValue;

        public IrcConnection() : base()
        {
            ChannelsValue = new ChannelManager(this);
            ConnectionIDValue = ++InstanceCount;
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
            get { return ChannelsValue; }
        }

        public int ConnectionID
        {
            get { return ConnectionIDValue; }
        }

        public void Close()
        {
            if (Connected) 
                Dispose();
        }
    }
}
