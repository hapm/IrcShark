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
    public class IrcStandardDefinition : IIrcObject
    {
        private IrcClient client;
        private bool receivedISupport;
        private List<FlagDefinition> userFlags;
        private List<FlagDefinition> channelFlags;
        private char[] channelPrefixes;
        private char[] statusMessagePrefixes;
        private char[] userPrefixes;
        private Dictionary<char, FlagDefinition> UserPrefixFlagsValue;
        private Dictionary<String, int> maxTargets;
        private Dictionary<char, int> safeChannels;
        private Dictionary<char, int> maxChannels;
        private String version;
        private int maxChannelLength;
        private int maxKickLength;
        private int maxNickLength;
        private int maxTopicLength;
        private int maxModesPerLine;
        private bool hasSaveList;
        private char banExceptionMode;
        private char inviteExceptionMode;
        private static Regex iSupportRegex = new Regex(@"(?<Negativ>-)?(?<Param>[A-Za-z]{1,20})(?:=(?<Value>[\w\x21-\x2F\x3A-\x40\x5B-\x60\x7B-\x7E]*))?");

        public IrcStandardDefinition(IrcClient baseClient)
        {
            channelPrefixes = new char[] { '#', '&' };
            UserPrefixFlagsValue = new Dictionary<char, FlagDefinition>();
            userPrefixes = new char[] { '@', '+' };
            userFlags = new List<FlagDefinition>();
            userFlags.Add(new FlagDefinition('i', ModeArt.User)); // invisible flag
            userFlags.Add(new FlagDefinition('s', ModeArt.User)); // receipt of server notices
            userFlags.Add(new FlagDefinition('w', ModeArt.User)); // receive wallops
            userFlags.Add(new FlagDefinition('o', ModeArt.User)); // ircop flag
            channelFlags = new List<FlagDefinition>();
            channelFlags.Add(new FlagDefinition('o', ModeArt.Channel, FlagParameter.Needed)); // channel op
            UserPrefixFlagsValue.Add(UserPrefixes[0], channelFlags[0]);
            channelFlags.Add(new FlagDefinition('v', ModeArt.Channel, FlagParameter.Needed)); // voice
            UserPrefixFlagsValue.Add(UserPrefixes[1], channelFlags[1]);
            channelFlags.Add(new FlagDefinition('p', ModeArt.Channel)); // private channel
            channelFlags.Add(new FlagDefinition('s', ModeArt.Channel)); // secret channel
            channelFlags.Add(new FlagDefinition('i', ModeArt.Channel)); // invite only channel
            channelFlags.Add(new FlagDefinition('t', ModeArt.Channel)); // only ops can change topic
            channelFlags.Add(new FlagDefinition('n', ModeArt.Channel)); // no messages from outsiders
            channelFlags.Add(new FlagDefinition('m', ModeArt.Channel)); // moderated channel
            channelFlags.Add(new FlagDefinition('l', ModeArt.Channel, FlagParameter.Needed, FlagParameter.NotAllowed, new Regex("^\\d*$"))); // limited users
            channelFlags.Add(new FlagDefinition('b', ModeArt.Channel, FlagParameter.Optional, FlagParameter.Needed)); // ban hostmask
            channelFlags.Add(new FlagDefinition('k', ModeArt.Channel, FlagParameter.Needed, FlagParameter.NotAllowed)); // private channel
            statusMessagePrefixes = null;
            maxTargets = new Dictionary<String, int>();
            safeChannels = new Dictionary<char, int>();
            maxChannels = new Dictionary<char, int>();
            maxChannelLength = 200;
            maxKickLength = 0;
            maxNickLength = 9;
            maxTopicLength = 0;
            hasSaveList = false;
            banExceptionMode = '\0';
            inviteExceptionMode = '\0';
            maxModesPerLine = 3;
            version = "rfc1459";
            client = baseClient;
            Client.NumericReceived += new NumericReceivedEventHandler(Client_NumericReceived);
        }

        private void Client_NumericReceived(Object sender, NumericReceivedEventArgs e)
        {
            if (e.Numeric != 5) return;
            receivedISupport = true;
            int tempInt;
            String tempString1;
            String tempString2;
            FlagDefinition tempDef = null;
            foreach (String param in e.BaseLine.Parameters)
            {
                Match m = iSupportRegex.Match(param);
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
                                int.TryParse(m.Groups["Value"].Value, out maxChannelLength);
                                break;

                            case "CHANTYPES":
                                channelPrefixes = m.Groups["Value"].Value.ToCharArray();
                                break;

                            case "EXCEPTS":
                                if (m.Groups["Value"].Success)
                                	banExceptionMode = m.Groups["Value"].Value[0];
                                else
                                	banExceptionMode = 'e';
                                
                                break;

                            case "IDCHAN":
                                safeChannels.Clear();
                                foreach (String saveChan in m.Groups["Value"].Value.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                                {
                                    if (int.TryParse(saveChan.Substring(2), out tempInt))
                                        safeChannels.Add(saveChan[0], tempInt);
                                }
                                break;

                            case "INVEX":
                                if (m.Groups["Value"].Success) banExceptionMode = m.Groups["Value"].Value[0];
                                else inviteExceptionMode = 'I';
                                break;

                            case "KICKLEN":
                                int.TryParse(m.Groups["Value"].Value, out maxKickLength);
                                break;

                            case "MAXLIST":
                                //TODO: Implement max list modes
                                break;

                            case "MODES":
                                int.TryParse(m.Groups["Value"].Value, out maxModesPerLine);
                                break;

                            case "NETWORK":
                                //TODO: no idea what to do with the Networkname here ...
                                break;

                            case "NICKLEN":
                                int.TryParse(m.Groups["Value"].Value, out maxNickLength);
                                break;

                            case "PREFIX":
                                UserPrefixFlagsValue.Clear();
                                tempString1 = m.Groups["Value"].Value;
                                tempString2 = tempString1.Substring(tempString1.Length / 2+1);
                                tempString1 = tempString1.Substring(1, tempString1.Length / 2 - 1);
                                userPrefixes = tempString2.ToCharArray();
                                for (int i = 0; i < tempString1.Length; i++)
                                {
                                    FlagDefinition chanFlag = GetChannelFlag(tempString1[i]);
                                    if (chanFlag == null)
                                    {
                                        chanFlag = new FlagDefinition(tempString1[i], ModeArt.Channel, FlagParameter.Needed);
                                        channelFlags.Add(chanFlag);
                                    }
                                    UserPrefixFlagsValue.Add(tempString2[i], chanFlag);
                                }
                                break;

                            case "SAFELIST":
                                hasSaveList = true;
                                break;

                            case "STATUSMSG":
                                statusMessagePrefixes = m.Groups["Value"].Value.ToCharArray();
                                break;

                            case "STD":
                                version = m.Groups["Value"].Value;
                                break;

                            case "TARGMAX":
                                maxTargets.Clear();
                                foreach (String cmd in m.Groups["Value"].Value.Split(",".ToCharArray()))
                                {
                                    tempInt = cmd.IndexOf(':');
                                    if (tempInt >= 0)
                                    {
                                        tempString1 = cmd.Substring(0, tempInt);
                                        tempString2 = cmd.Substring(tempInt+1);
                                        if (int.TryParse(tempString2, out tempInt))
                                            maxTargets.Add(tempString1, tempInt);
                                    }
                                }
                                break;

                            case "TOPICLEN":
                                int.TryParse(m.Groups["Value"].Value, out maxTopicLength);
                                break;

                            case "CHANMODES":
                                String[] modes;
                                modes = m.Groups["Value"].Value.Split(",".ToCharArray());
                                if (modes.Length < 4) //TODO: KA was passieren soll wenn flasche anzahl an modes
                                	break;
                                
                                channelFlags.Clear();
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
                                        channelFlags.Add(tempDef);
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
            get { return receivedISupport; }
        }

        public FlagDefinition[] UserFlags
        {
            get { return userFlags.ToArray(); }
        }

        public FlagDefinition[] ChannelFlags
        {
            get { return channelFlags.ToArray(); }
        }

        public char[] ChannelPrefixes
        {
            get { return (char[])channelPrefixes.Clone(); }
        }

        public char[] StatusMessagePrefixes
        {
            get 
            { 
                if (statusMessagePrefixes == null)
                	return null;
                
                return (char[])statusMessagePrefixes.Clone();
            }
        }

        public char[] UserPrefixes
        {
            get { return (char[])userPrefixes.Clone(); }
        }

        public ConstantDictionary<char, FlagDefinition> UserPrefixFlags
        {
            get { return new ConstantDictionary<char, FlagDefinition>(UserPrefixFlagsValue); }
        }

        public ConstantDictionary<String, int> MaxTargets
        {
            get { return new ConstantDictionary<String, int>(maxTargets); }
        }

        public String Version
        {
            get { return version; }
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
            get { return maxChannelLength; }
        }

        public int MaxKickLength
        {
            get { return maxKickLength; }
        }

        public int MaxNickLength
        {
            get { return maxNickLength; }
        }

        public int MaxTopicLength
        {
            get { return maxTopicLength; }
        }

        public bool HasSaveList
        {
            get { return hasSaveList; }
        }

        public char BanExceptionMode
        {
            get { return banExceptionMode; }
        }

        public int MaxModesPerLine
        {
            get { return maxModesPerLine; }
        }

        public char InviteExceptionMode
        {
            get { return inviteExceptionMode; }
        }

        public ConstantDictionary<char, int> SafeChannels
        {
            get { return new ConstantDictionary<char, int>(safeChannels); }
        }

        public ConstantDictionary<char, int> MaxChannels
        {
            get { return new ConstantDictionary<char, int>(maxChannels); }
        }

        public FlagDefinition GetChannelFlag(char flag)
        {
            foreach(FlagDefinition fDef in ChannelFlags)
            {
                if (fDef.Char == flag)
                	return fDef;
            }
            return null;
        }

        public char GetPrefixForFlag(FlagDefinition flag)
        {
            if (!UserPrefixFlagsValue.ContainsValue(flag)) return '\0';
            foreach (KeyValuePair<char, FlagDefinition> f in UserPrefixFlagsValue)
            {
                if (f.Value == flag)
                	return f.Key;
            }
            return '\0';
        }

        #region IIrcObject Members

        public IrcClient Client
        {
            get { return client; }
        }

        #endregion
    }
}
