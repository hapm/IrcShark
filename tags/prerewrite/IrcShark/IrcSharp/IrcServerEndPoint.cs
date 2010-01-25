// $Id$
// 
// Note:
// 
// Copyright (C) 2009 Full Name
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
