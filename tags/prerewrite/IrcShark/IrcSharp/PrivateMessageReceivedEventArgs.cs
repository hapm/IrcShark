using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    public class PrivateMessageReceivedEventArgs : IrcEventArgs
    {
        private UserInfo sender;
        private CTCPCommands CTCPCommandValue; // TODO: Names...
        private String CTCPCommandStringValue;
        private String CTCPParametersValue;

        public PrivateMessageReceivedEventArgs(IrcLine baseLine)
            : base(baseLine)
        {
            sender = new UserInfo(baseLine);
            String line;
            line = Message;
            if (line[0] == '\x01' && line[line.Length - 1] == '\x01')
            {
                line = line.Substring(1, line.Length - 2);
                CTCPCommandStringValue = line;
                int firstSpace = line.IndexOf(' ');
                if (firstSpace > 0)
                {
                    CTCPCommandStringValue = line.Substring(0, firstSpace);
                    CTCPParametersValue = line.Substring(firstSpace + 1);
                }
                switch (CTCPCommandStringValue)
                {
                    case "ACTION":
                        CTCPCommandValue = CTCPCommands.Action;
                        break;
                    case "VERSION":
                        CTCPCommandValue = CTCPCommands.Version;
                        break;
                    default:
                        CTCPCommandValue = CTCPCommands.Unkown;
                        break;
                }
            }
            else
            {
                CTCPCommandValue = CTCPCommands.None;
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
            get { return CTCPCommandValue != CTCPCommands.None; }
        }

        public CTCPCommands CTCPCommand
        {
            get { return CTCPCommandValue; }
        }

        public String CTCPCommandString
        {
            get { return CTCPCommandStringValue; }
        }

        public String CTCPParameters
        {
            get { return CTCPParametersValue; }
        }
    }
}
