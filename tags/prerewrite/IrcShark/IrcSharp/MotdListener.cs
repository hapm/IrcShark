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
    public delegate void MotdBeginEventHandler(Object sender, MotdBeginEventArgs args);
    public delegate void MotdEndEventHandler(Object sender, MotdEndEventArgs args);

    public class MotdListener : IIrcObject
    {
        public event MotdBeginEventHandler MotdBegin;
        public event MotdEndEventHandler MotdEnd;

        private IrcClient client;
        private List<IrcLine> motdLines;
        private bool isReading;

        public MotdListener(IrcClient client)
        {
            this.client = client;
            client.LineReceived += new LineReceivedEventHandler(HandleLine);
            motdLines = new List<IrcLine>();
        }

        private void HandleLine(Object sender, LineReceivedEventArgs args)
        {
            if (!args.Line.IsNumeric) return;
            if (!IsReading && args.Line.Numeric != 375) return;
            switch (args.Line.Numeric)
            {
                case 375:
                    isReading = true;
                    motdLines.Clear();
                    motdLines.Add(args.Line);
                    if (MotdBegin != null)
                    	MotdBegin(this, new MotdBeginEventArgs(args.Line));
                    break;
                    
                case 372:
                    motdLines.Add(args.Line);
                    break;
                    
                case 376:
                    motdLines.Add(args.Line);
                    if (MotdEnd != null)
                    	MotdEnd(this, new MotdEndEventArgs(args.Line, MotdLines));
                    isReading = false;
                    break;
            }
        }

        public IrcClient Client
        {
            get { return client; }
        }

        public IrcLine[] MotdLines
        {
            get { return motdLines.ToArray(); }
        }

        public bool IsReading
        {
            get { return isReading; }
        }
    }
}
