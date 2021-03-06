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
using System.Text.RegularExpressions;

namespace IrcSharp
{
    /// <summary>
    /// Holds host informations about a user.
    /// </summary>
    public class UserInfo : IIrcObject
    {
        private static Regex hostRegex = new Regex("([^!]*)!([^@]*)@(.*)", RegexOptions.Compiled & RegexOptions.Singleline);
        private String nickName;
        private String ident;
        private String host;
        private IrcLine baseLine;
        private IrcClient client;

        public UserInfo(IrcLine BaseLine)
        {
            baseLine = BaseLine;
            Match hostPieces;
            hostPieces = hostRegex.Match(BaseLine.Prefix);
            if (hostPieces.Success)
            {
                nickName = hostPieces.Groups[1].Value;
                ident = hostPieces.Groups[2].Value;
                host = hostPieces.Groups[3].Value;
                client = BaseLine.Client;
            }
            else
            {
                // TODO: trow Exception

            }
        }

        public UserInfo(String FullHost, IrcClient Client)
        {
            Match hostPieces;
            hostPieces = hostRegex.Match(FullHost);
            if (hostPieces.Success)
            {
                nickName = hostPieces.Groups[1].Value;
                ident = hostPieces.Groups[2].Value;
                host = hostPieces.Groups[3].Value;
                client = Client;
            }
            else
            {
                // TODO: trow Exception

            }
        }

        public UserInfo(String NickName, String Ident, String Host, IrcClient Client)
        {
            nickName = NickName;
            ident = Ident;
            host = Host;
            client = Client;
        }

        public UserInfo(UserInfo baseInfo)
        {
            baseLine = baseInfo.BaseLine;
            nickName = baseInfo.NickName;
            ident = baseInfo.Ident;
            host = baseInfo.Host;
            client = baseInfo.Client;
        }

        public IrcLine BaseLine
        {
            get { return baseLine; }
        }

        public String Host
        {
            get { return host; }
        }

        public String Ident
        {
            get { return ident; }
        }

        public String NickName
        {
            get { return nickName; }
            protected internal set { nickName = value; }
        }

        public override bool Equals(Object toCompare)
        {
            if (toCompare is UserInfo)
            {
                UserInfo info = (UserInfo)toCompare;
                
                if (!info.Host.Equals(Host))
                	return false;
                if (!info.Ident.Equals(Ident))
                	return false;
                if (!info.NickName.Equals(NickName))
                	return false;
                
                return true;
            }
            else
                return base.Equals(toCompare);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public new String ToString()
        {
            return String.Format("{0}!{1}@{2}", NickName, Ident, Host);
        }

        #region IIrcObject Member

        public IrcClient Client
        {
            get { return client; }
        }

        #endregion
    }
}
