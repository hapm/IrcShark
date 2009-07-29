using System;
using System.Collections.Generic;
using System.Text;

namespace IrcShark
{
    /// <summary>
    /// Represents a network in the network configuration.
    /// </summary>
    public class Network : NetworkManagerConfigurationUnit
    {
        private ServerList ServersValue;

        public Network()
        {
            ServersValue = new ServerList();
            ServersValue.AssociatedNetwork = this;
        }

        public ServerList Servers
        {
            get { return ServersValue; }
        }
    }
}
