using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace IrcSharp
{
    public class IrcServerEndPoint : IPEndPoint
    {
        private String password;
        private int needIdentd;
        private String serverHost;

        public IrcServerEndPoint(String ServerAddress, int Port) : base(0,0)
        {
            IPAddress[] addresses;
            serverHost = ServerAddress;
            base.Port = Port;
            addresses = Dns.GetHostEntry(ServerAddress).AddressList;
            base.Address = addresses[0];
        }

        public String ServerHost
        {
            get
            {
                return serverHost;
            }
        }
    }
}
