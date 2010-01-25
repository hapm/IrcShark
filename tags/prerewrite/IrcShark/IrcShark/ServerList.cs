using System;
using System.Collections.Generic;
using System.Text;

namespace IrcShark
{
    /// <summary>
    /// A list of servers for a network.
    /// </summary>
    public class ServerList : List<ServerConfiguration>
    {
        private Network AssociatedNetworkValue;

        public new void Add(ServerConfiguration item)
        {
            if (Contains(item)) return;
            item.BaseUnit = AssociatedNetwork;
            base.Add(item);
        }

        public new void Remove(ServerConfiguration item)
        {
            if (!Contains(item)) return;
            base.Remove(item);
            item.BaseUnit = null;
        }

        public new void RemoveAll(Predicate<ServerConfiguration> match)
        {
            base.RemoveAll(match);
        }

        public Network AssociatedNetwork
        {
            get { return AssociatedNetworkValue; }
            set { AssociatedNetworkValue = value; }
        }
    }
}
