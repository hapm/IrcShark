using System;
using System.Collections.Generic;
using System.Text;

namespace IrcShark
{
    public class NetworkList : List<Network>
    {
        private NetworkManager AssociatedServerManagerValue;

        public NetworkManager AssociatedServerManager
        {
            get { return AssociatedServerManagerValue; }
            set { AssociatedServerManagerValue = value; }
        }

        public new void Add(Network item)
        {
            if (Contains(item)) return;
            item.BaseUnit = AssociatedServerManager;
            base.Add(item);
        }

        public new void Remove(Network item)
        {
            if (!Contains(item)) return;
            base.Remove(item);
            item.BaseUnit = null;
        }

        public new void RemoveAll(Predicate<Network> match)
        {
            base.RemoveAll(match);
        }
    }
}
