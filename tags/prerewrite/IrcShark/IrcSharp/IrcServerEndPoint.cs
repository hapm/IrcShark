using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace IrcSharp
{
    public class IrcServerEndPoint : IPEndPoint
    {
        private String PasswordValue;
        private int NeedIdentd;
        private String ServerHostValue;

        public IrcServerEndPoint(String ServerAddress, int Port) : base(0,0)
        {
            IPAddress[] Addresses;
            ServerHostValue = ServerAddress;
            base.Port = Port;
            Addresses = Dns.GetHostEntry(ServerAddress).AddressList;
            base.Address = Addresses[0];
        }

        public String ServerHost
        {
            get
            {
                return ServerHostValue;
            }
        }
    }
}
