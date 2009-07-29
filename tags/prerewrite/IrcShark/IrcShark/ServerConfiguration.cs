using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using IrcSharp;

namespace IrcShark
{
    /// <summary>
    /// The configuration for a server.
    /// </summary>
    public class ServerConfiguration : NetworkManagerConfigurationUnit
    {
        private String PasswordValue;
        private String AddressValue;
        private int PortValue;

        public ServerConfiguration()
        {
            PortValue = 6667;
        }

        [XmlAttribute]
        public String Password
        {
            get { return PasswordValue; }
            set { PasswordValue = value; }
        }

        [XmlAttribute]
        public String Address
        {
            get { return AddressValue; }
            set { AddressValue = value; }
        }

        [XmlAttribute]
        public int Port
        {
            get { return PortValue; }
            set { PortValue = value; }
        }

        public IrcServerEndPoint ToIrcEndPoint()
        {
            IrcServerEndPoint result = new IrcServerEndPoint(Address, Port);
            return result;
        }

        public void ApplyTo(IrcClient con)
        {
            con.ChangeNickname(InheritedNickname);
            con.Username = InheritedIdent;
            con.ServerAddress = ToIrcEndPoint();
        }
    }
}
