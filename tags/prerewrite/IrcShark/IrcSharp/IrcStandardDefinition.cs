using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace IrcSharp
{
    public class IrcStandardDefinition : IIrcObject
    {
        private IrcClient ClientValue;
        private bool ReceivedISupportValue;
        private List<FlagDefinition> UserFlagsValue;
        private List<FlagDefinition> ChannelFlagsValue;
        private char[] ChannelPrefixesValue;
        private char[] StatusMessagePrefixesValue;
        private char[] UserPrefixesValue;
        private Dictionary<char, FlagDefinition> UserPrefixFlagsValue;
        private Dictionary<String, int> MaxTargetsValue;
        private Dictionary<char, int> SafeChannelsValue;
        private Dictionary<char, int> MaxChannelsValue;
        private String VersionValue;
        private int MaxChannelLengthValue;
        private int MaxKickLengthValue;
        private int MaxNickLengthValue;
        private int MaxTopicLengthValue;
        private int MaxModesPerLineValue;
        private bool HasSaveListValue;
        private char BanExceptionModeValue;
        private char InviteExceptionModeValue;
        private static Regex ISupportRegex = new Regex(@"(?<Negativ>-)?(?<Param>[A-Za-z]{1,20})(?:=(?<Value>[\w\x21-\x2F\x3A-\x40\x5B-\x60\x7B-\x7E]*))?");

        public IrcStandardDefinition(IrcClient baseClient)
        {
            ChannelPrefixesValue = new char[] { '#', '&' };
            UserPrefixFlagsValue = new Dictionary<char, FlagDefinition>();
            UserPrefixesValue = new char[] { '@', '+' };
            UserFlagsValue = new List<FlagDefinition>();
            UserFlagsValue.Add(new FlagDefinition('i', ModeArt.User)); // invisible flag
            UserFlagsValue.Add(new FlagDefinition('s', ModeArt.User)); // receipt of server notices
            UserFlagsValue.Add(new FlagDefinition('w', ModeArt.User)); // receive wallops
            UserFlagsValue.Add(new FlagDefinition('o', ModeArt.User)); // ircop flag
            ChannelFlagsValue = new List<FlagDefinition>();
            ChannelFlagsValue.Add(new FlagDefinition('o', ModeArt.Channel, FlagParameter.Needed)); // channel op
            UserPrefixFlagsValue.Add(UserPrefixes[0], ChannelFlagsValue[0]);
            ChannelFlagsValue.Add(new FlagDefinition('v', ModeArt.Channel, FlagParameter.Needed)); // voice
            UserPrefixFlagsValue.Add(UserPrefixes[1], ChannelFlagsValue[1]);
            ChannelFlagsValue.Add(new FlagDefinition('p', ModeArt.Channel)); // private channel
            ChannelFlagsValue.Add(new FlagDefinition('s', ModeArt.Channel)); // secret channel
            ChannelFlagsValue.Add(new FlagDefinition('i', ModeArt.Channel)); // invite only channel
            ChannelFlagsValue.Add(new FlagDefinition('t', ModeArt.Channel)); // only ops can change topic
            ChannelFlagsValue.Add(new FlagDefinition('n', ModeArt.Channel)); // no messages from outsiders
            ChannelFlagsValue.Add(new FlagDefinition('m', ModeArt.Channel)); // moderated channel
            ChannelFlagsValue.Add(new FlagDefinition('l', ModeArt.Channel, FlagParameter.Needed, FlagParameter.NotAllowed, new Regex("^\\d*$"))); // limited users
            ChannelFlagsValue.Add(new FlagDefinition('b', ModeArt.Channel, FlagParameter.Optional, FlagParameter.Needed)); // ban hostmask
            ChannelFlagsValue.Add(new FlagDefinition('k', ModeArt.Channel, FlagParameter.Needed, FlagParameter.NotAllowed)); // private channel
            StatusMessagePrefixesValue = null;
            MaxTargetsValue = new Dictionary<String, int>();
            SafeChannelsValue = new Dictionary<char, int>();
            MaxChannelsValue = new Dictionary<char, int>();
            MaxChannelLengthValue = 200;
            MaxKickLengthValue = 0;
            MaxNickLengthValue = 9;
            MaxTopicLengthValue = 0;
            HasSaveListValue = false;
            BanExceptionModeValue = '\0';
            InviteExceptionModeValue = '\0';
            MaxModesPerLineValue = 3;
            VersionValue = "rfc1459";
            ClientValue = baseClient;
            Client.NumericReceived += new NumericReceivedEventHandler(Client_NumericReceived);
        }

        private void Client_NumericReceived(Object sender, NumericReceivedEventArgs e)
        {
            if (e.Numeric != 5) return;
            ReceivedISupportValue = true;
            int tempInt;
            String tempString1;
            String tempString2;
            FlagDefinition tempDef = null;
            foreach (String param in e.BaseLine.Parameters)
            {
                Match m = ISupportRegex.Match(param);
                if (m.Success)
                {
                    if (m.Groups["Negativ"].Success) //TODO: handle a reset to defaults
                    {
                    }
                    else //handle the setting of a new value
                    {
                        switch (m.Groups["Param"].Value)
                        {
                            case "CHANNELLEN":
                                int.TryParse(m.Groups["Value"].Value, out MaxChannelLengthValue);
                                break;

                            case "CHANTYPES":
                                ChannelPrefixesValue = m.Groups["Value"].Value.ToCharArray();
                                break;

                            case "EXCEPTS":
                                if (m.Groups["Value"].Success) BanExceptionModeValue = m.Groups["Value"].Value[0];
                                else BanExceptionModeValue = 'e';
                                break;

                            case "IDCHAN":
                                SafeChannelsValue.Clear();
                                foreach (String saveChan in m.Groups["Value"].Value.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                                {
                                    if (int.TryParse(saveChan.Substring(2), out tempInt))
                                        SafeChannelsValue.Add(saveChan[0], tempInt);
                                }
                                break;

                            case "INVEX":
                                if (m.Groups["Value"].Success) BanExceptionModeValue = m.Groups["Value"].Value[0];
                                else InviteExceptionModeValue = 'I';
                                break;

                            case "KICKLEN":
                                int.TryParse(m.Groups["Value"].Value, out MaxKickLengthValue);
                                break;

                            case "MAXLIST":
                                //TODO: Implement max list modes
                                break;

                            case "MODES":
                                int.TryParse(m.Groups["Value"].Value, out MaxModesPerLineValue);
                                break;

                            case "NETWORK":
                                //TODO: no idea what to do with the Networkname here ...
                                break;

                            case "NICKLEN":
                                int.TryParse(m.Groups["Value"].Value, out MaxNickLengthValue);
                                break;

                            case "PREFIX":
                                UserPrefixFlagsValue.Clear();
                                tempString1 = m.Groups["Value"].Value;
                                tempString2 = tempString1.Substring(tempString1.Length / 2+1);
                                tempString1 = tempString1.Substring(1, tempString1.Length / 2 - 1);
                                UserPrefixesValue = tempString2.ToCharArray();
                                for (int i = 0; i < tempString1.Length; i++)
                                {
                                    FlagDefinition chanFlag = GetChannelFlag(tempString1[i]);
                                    if (chanFlag == null)
                                    {
                                        chanFlag = new FlagDefinition(tempString1[i], ModeArt.Channel, FlagParameter.Needed);
                                        ChannelFlagsValue.Add(chanFlag);
                                    }
                                    UserPrefixFlagsValue.Add(tempString2[i], chanFlag);
                                }
                                break;

                            case "SAFELIST":
                                HasSaveListValue = true;
                                break;

                            case "STATUSMSG":
                                StatusMessagePrefixesValue = m.Groups["Value"].Value.ToCharArray();
                                break;

                            case "STD":
                                VersionValue = m.Groups["Value"].Value;
                                break;

                            case "TARGMAX":
                                MaxTargetsValue.Clear();
                                foreach (String cmd in m.Groups["Value"].Value.Split(",".ToCharArray()))
                                {
                                    tempInt = cmd.IndexOf(':');
                                    if (tempInt >= 0)
                                    {
                                        tempString1 = cmd.Substring(0, tempInt);
                                        tempString2 = cmd.Substring(tempInt+1);
                                        if (int.TryParse(tempString2, out tempInt))
                                            MaxTargetsValue.Add(tempString1, tempInt);
                                    }
                                }
                                break;

                            case "TOPICLEN":
                                int.TryParse(m.Groups["Value"].Value, out MaxTopicLengthValue);
                                break;

                            case "CHANMODES":
                                String[] modes;
                                modes = m.Groups["Value"].Value.Split(",".ToCharArray());
                                if (modes.Length < 4) break; //TODO: KA was passieren soll wenn flasche anzahl an modes
                                ChannelFlagsValue.Clear();
                                for (int i=0; i < 4; i++)
                                {
                                    foreach(char c in modes[i])
                                    {
                                        switch(i)
                                        {
                                            case 0:
                                                tempDef = new FlagDefinition(c, ModeArt.Channel, FlagParameter.Optional, FlagParameter.Needed);
                                                break;
                                            case 1:
                                                tempDef = new FlagDefinition(c, ModeArt.Channel, FlagParameter.Needed, FlagParameter.Needed);
                                                break;
                                            case 2:
                                                tempDef = new FlagDefinition(c, ModeArt.Channel, FlagParameter.Needed, FlagParameter.NotAllowed);
                                                break;
                                            case 3:
                                                tempDef = new FlagDefinition(c, ModeArt.Channel, FlagParameter.NotAllowed, FlagParameter.NotAllowed);
                                                break;
                                        }
                                        ChannelFlagsValue.Add(tempDef);
                                    }
                                }
                                break;
                            default:
                                //TODO: no idea what to do with unknown ISUPPORT infos
                                break;
                        }
                    }
                }
            }
        }

        public bool ReceivedISupport
        {
            get { return ReceivedISupportValue; }
        }

        public FlagDefinition[] UserFlags
        {
            get { return UserFlagsValue.ToArray(); }
        }

        public FlagDefinition[] ChannelFlags
        {
            get { return ChannelFlagsValue.ToArray(); }
        }

        public char[] ChannelPrefixes
        {
            get { return (char[])ChannelPrefixesValue.Clone(); }
        }

        public char[] StatusMessagePrefixes
        {
            get 
            { 
                if (StatusMessagePrefixesValue == null) return null;
                return (char[])StatusMessagePrefixesValue.Clone();
            }
        }

        public char[] UserPrefixes
        {
            get { return (char[])UserPrefixesValue.Clone(); }
        }

        public ConstantDictionary<char, FlagDefinition> UserPrefixFlags
        {
            get { return new ConstantDictionary<char, FlagDefinition>(UserPrefixFlagsValue); }
        }

        public ConstantDictionary<String, int> MaxTargets
        {
            get { return new ConstantDictionary<String, int>(MaxTargetsValue); }
        }

        public String Version
        {
            get { return VersionValue; }
        }

        public bool IsAllowedChannel(String ChannelName)
        {
            if (ChannelName.Length < 2)
                return false;
            foreach (char pre in ChannelPrefixes)
            {
                if (ChannelName[0] == pre)
                    return true;
            }
            return false;
        }

        public int MaxChannelLength
        {
            get { return MaxChannelLengthValue; }
        }

        public int MaxKickLength
        {
            get { return MaxKickLengthValue; }
        }

        public int MaxNickLength
        {
            get { return MaxNickLengthValue; }
        }

        public int MaxTopicLength
        {
            get { return MaxTopicLengthValue; }
        }

        public bool HasSaveList
        {
            get { return HasSaveListValue; }
        }

        public char BanExceptionMode
        {
            get { return BanExceptionModeValue; }
        }

        public int MaxModesPerLine
        {
            get { return MaxModesPerLineValue; }
        }

        public char InviteExceptionMode
        {
            get { return InviteExceptionModeValue; }
        }

        public ConstantDictionary<char, int> SafeChannels
        {
            get { return new ConstantDictionary<char, int>(SafeChannelsValue); }
        }

        public ConstantDictionary<char, int> MaxChannels
        {
            get { return new ConstantDictionary<char, int>(MaxChannelsValue); }
        }

        public FlagDefinition GetChannelFlag(char flag)
        {
            foreach(FlagDefinition fDef in ChannelFlags)
            {
                if (fDef.Char == flag) return fDef;
            }
            return null;
        }

        public char GetPrefixForFlag(FlagDefinition flag)
        {
            if (!UserPrefixFlagsValue.ContainsValue(flag)) return '\0';
            foreach (KeyValuePair<char, FlagDefinition> f in UserPrefixFlagsValue)
            {
                if (f.Value == flag) return f.Key;
            }
            return '\0';
        }

        #region IIrcObject Members

        public IrcClient Client
        {
            get { return ClientValue; }
        }

        #endregion
    }
}
