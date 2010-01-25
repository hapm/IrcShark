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
