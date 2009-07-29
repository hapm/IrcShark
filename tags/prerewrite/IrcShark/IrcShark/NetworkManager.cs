using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace IrcShark
{
    public class NetworkManager : NetworkManagerConfigurationUnit
    {
        private NetworkList NetworksValue;
        private List<Type> NetworkSettingPanelsValue;
        IrcSharkApplication IrcSharkValue;

        private NetworkManager()
        {
            NetworksValue = new NetworkList();
            NetworkSettingPanelsValue = new List<Type>();
            NetworkSettingPanelsValue.Add(typeof(GeneralNetworkManagerSettingPanel));
            NetworkSettingPanelsValue.Add(typeof(ServerConfigurationPanel));
            NetworkSettingPanelsValue.Add(typeof(ChannelFavoritesPanel));
            NetworkSettingPanelsValue.Add(typeof(ProxySettingPanel));
            NetworkSettingPanelsValue.Add(typeof(OperSettingPanel));
            NetworksValue.AssociatedServerManager = this;
        }

        public NetworkManager(IrcSharkApplication app)
        {
            IrcSharkValue = app;
            NetworkSettingPanelsValue = new List<Type>();
            NetworksValue = new NetworkList();
            NetworksValue.AssociatedServerManager = this;
            NetworkSettingPanelsValue.Add(typeof(GeneralNetworkManagerSettingPanel));
            NetworkSettingPanelsValue.Add(typeof(ServerConfigurationPanel));
            NetworkSettingPanelsValue.Add(typeof(ChannelFavoritesPanel));
            NetworkSettingPanelsValue.Add(typeof(ProxySettingPanel));
            NetworkSettingPanelsValue.Add(typeof(OperSettingPanel));
        }

        [XmlIgnore]
        public IrcSharkApplication IrcShark
        {
            get { return IrcSharkValue; }
            private set 
            {
                IrcSharkValue = value;
            }
        }

        public void SaveServers()
        {
            FileStream serverFile = new FileStream(IrcSharkValue.SettingPath + "Servers.xml", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            XmlSerializer configSerializer = new XmlSerializer(typeof(NetworkManager));
            configSerializer.Serialize(serverFile, this);
            serverFile.Close();
        }

        public static NetworkManager LoadServers(IrcSharkApplication app)
        {
            try
            {
                FileStream serverFile = new FileStream(app.SettingPath + "Servers.xml", FileMode.Open, FileAccess.Read, FileShare.None);
                XmlSerializer configSerializer = new XmlSerializer(typeof(NetworkManager));
                Object result = configSerializer.Deserialize(serverFile);
                NetworkManager srvman = (NetworkManager)result;
                serverFile.Close();
                srvman.IrcShark = app;
                return srvman;
            }
            catch (Exception)
            {
                NetworkManager result = new NetworkManager();
                result.IrcShark = app;
                result.Nickname = "Default";
                result.AlternativeNickname = "D3fault";
                result.Realname = "unnamed";
                result.Perform = new String[] { "" };
                result.Name = "Main Settings";
                result.Ident = "Default";
                return result;
            }
        }

        public NetworkList Networks
        {
            get { return NetworksValue; }
        }

        [XmlIgnore]
        public Type[] NetworkManagerSettingPanels
        {
            get { return NetworkSettingPanelsValue.ToArray(); }
        }
    }
}
