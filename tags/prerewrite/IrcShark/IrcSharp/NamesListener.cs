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
    public delegate void NamesBeginEventHandler(Object sender, NamesBeginEventArgs args);
    public delegate void NamesEndEventHandler(Object sender, NamesEndEventArgs args);

    public class NamesListener : IIrcObject
    {
        public event NamesBeginEventHandler NamesBegin;
        public event NamesEndEventHandler NamesEnd;

        private IrcClient client;
        private List<String> names;
        private bool isReading;

        private String ChannelNameValue;

        public NamesListener(IrcClient client)
        {
            this.client = client;
            client.LineReceived += new LineReceivedEventHandler(HandleLine);
            names = new List<String>();
        }

        private void HandleLine(Object sender, LineReceivedEventArgs args)
        {
            if (!args.Line.IsNumeric) return;
            switch (args.Line.Numeric)
            {
                case 353:
                    ChannelNameValue = args.Line.Parameters[2];

                    foreach (String s in args.Line.Parameters[3].Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                    {
                        names.Add(s);
                    }

                    if (!IsReading)
                    {
                        isReading = true;
                        if (NamesBegin != null)
                        	NamesBegin(this, new NamesBeginEventArgs(args.Line));
                    }
                    break;
                    
                case 366:
                    if (NamesEnd != null)
                    	NamesEnd(this, new NamesEndEventArgs(args.Line, Names, ChannelNameValue));
                    
                    isReading = false;
                    break;
            }
        }

        public IrcClient Client
        {
            get { return client; }
        }

        public String[] Names
        {
            get { return names.ToArray(); }
        }

        public bool IsReading
        {
            get { return isReading; }
        }
    }
}
