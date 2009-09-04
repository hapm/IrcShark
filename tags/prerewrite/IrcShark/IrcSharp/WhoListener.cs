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
    public delegate void WhoBeginEventHandler(Object sender, WhoBeginEventArgs args);
    public delegate void WhoEndEventHandler(Object sender, WhoEndEventArgs args);

    /// <summary>
    /// This listener allows you to listen for a who reply.
    /// </summary>
    public class WhoListener : IIrcObject
    {
        public event WhoBeginEventHandler WhoBegin;
        public event WhoEndEventHandler WhoEnd;

        private IrcClient client;
        private List<WhoLine> whoLines;
        private bool isReading;

        public WhoListener(IrcClient client)
        {
            this.client = client;
            client.LineReceived += new LineReceivedEventHandler(HandleLine);
            whoLines = new List<WhoLine>();
        }

        private void HandleLine(Object sender, LineReceivedEventArgs args)
        {
            if (!args.Line.IsNumeric)
            	return;
            
            switch (args.Line.Numeric)
            {
                case 352:
                    whoLines.Add(new WhoLine(args.Line));
                    if (!IsReading)
                    {
                        isReading = true;
                        if (WhoBegin != null)
                        	WhoBegin(this, new WhoBeginEventArgs(args.Line));
                    }
                    break;
                    
                case 315:
                    if (WhoEnd != null)
                    	WhoEnd(this, new WhoEndEventArgs(args.Line, WhoLines));
                    isReading = false;
                    break;
            }
        }

        public IrcClient Client
        {
            get { return client; }
        }

        public IrcLine[] WhoLines
        {
            get { return whoLines.ToArray(); }
        }

        public bool IsReading
        {
            get { return isReading; }
        }
    }
}
