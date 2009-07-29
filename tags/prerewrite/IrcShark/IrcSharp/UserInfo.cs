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
        private static Regex HostRegex = new Regex("([^!]*)!([^@]*)@(.*)", RegexOptions.Compiled & RegexOptions.Singleline);
        private String NickNameValue;
        private String IdentValue;
        private String HostValue;
        private IrcLine BaseLineValue;
        private IrcClient ClientValue;

        public UserInfo(IrcLine BaseLine)
        {
            BaseLineValue = BaseLine;
            Match hostPieces;
            hostPieces = HostRegex.Match(BaseLine.Prefix);
            if (hostPieces.Success)
            {
                NickNameValue = hostPieces.Groups[1].Value;
                IdentValue = hostPieces.Groups[2].Value;
                HostValue = hostPieces.Groups[3].Value;
                ClientValue = BaseLine.Client;
            }
            else
            {
                // Exception schmeiﬂen

            }
        }

        public UserInfo(String FullHost, IrcClient Client)
        {
            Match hostPieces;
            hostPieces = HostRegex.Match(FullHost);
            if (hostPieces.Success)
            {
                NickNameValue = hostPieces.Groups[1].Value;
                IdentValue = hostPieces.Groups[2].Value;
                HostValue = hostPieces.Groups[3].Value;
                ClientValue = Client;
            }
            else
            {
                // Exception schmeiﬂen

            }
        }

        public UserInfo(String NickName, String Ident, String Host, IrcClient Client)
        {
            NickNameValue = NickName;
            IdentValue = Ident;
            HostValue = Host;
            ClientValue = Client;
        }

        public UserInfo(UserInfo baseInfo)
        {
            BaseLineValue = baseInfo.BaseLine;
            NickNameValue = baseInfo.NickName;
            IdentValue = baseInfo.Ident;
            HostValue = baseInfo.Host;
            ClientValue = baseInfo.Client;
        }

        public IrcLine BaseLine
        {
            get { return BaseLineValue; }
        }

        public String Host
        {
            get { return HostValue; }
        }

        public String Ident
        {
            get { return IdentValue; }
        }

        public String NickName
        {
            get { return NickNameValue; }
            protected internal set { NickNameValue = value; }
        }

        public override bool Equals(Object toCompare)
        {
            if (toCompare is UserInfo)
            {
                UserInfo info = (UserInfo)toCompare;
                if (!info.Host.Equals(Host)) return false;
                if (!info.Ident.Equals(Ident)) return false;
                if (!info.NickName.Equals(NickName)) return false;
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
            get { return ClientValue; }
        }

        #endregion
    }
}
