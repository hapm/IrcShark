using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    public class PrivateMessageReceivedEventArgs : IrcEventArgs
    {
        private UserInfo sender;
        private CTCPCommands cTCPCommand;
        private String cTCPCommandString;
        private String cTCPParameters;

        public PrivateMessageReceivedEventArgs(IrcLine baseLine) : base(baseLine)
        {
            sender = new UserInfo(baseLine);
            String line;
            line = Message;
            if (line[0] == '\x01' && line[line.Length - 1] == '\x01')
            {
                line = line.Substring(1, line.Length - 2);
                cTCPCommandString = line;
                int firstSpace = line.IndexOf(' ');
                if (firstSpace > 0)
                {
                    cTCPCommandString = line.Substring(0, firstSpace);
                    cTCPParameters = line.Substring(firstSpace + 1);
                }
                switch (cTCPCommandString)
                {
                    case "ACTION":
                        cTCPCommand = CTCPCommands.Action;
                        break;
                    case "VERSION":
                        cTCPCommand = CTCPCommands.Version;
                        break;
                    default:
                        cTCPCommand = CTCPCommands.Unkown;
                        break;
                }
            }
            else
            {
                cTCPCommand = CTCPCommands.None;
            }
        }

        public UserInfo Sender
        {
            get { return sender; }
        }

        public String Destination
        {
            get { return BaseLine.Parameters[0]; }
        }

        public String Message
        {
            get { return BaseLine.Parameters[1]; }
        }

        public bool IsCTCP
        {
            get { return cTCPCommand != CTCPCommands.None; }
        }

        public CTCPCommands CTCPCommand
        {
            get { return cTCPCommand; }
        }

        public String CTCPCommandString
        {
            get { return cTCPCommandString; }
        }

        public String CTCPParameters
        {
            get { return cTCPParameters; }
        }
    }
}
