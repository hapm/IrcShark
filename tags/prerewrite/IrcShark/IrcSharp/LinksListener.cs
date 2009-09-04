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
    public delegate void LinksBeginEventHandler(Object sender, LinksBeginEventArgs args);
    public delegate void LinksEndEventHandler(Object sender, LinksEndEventArgs args);

    /// <summary>
    /// This listener allows you to listen for a link reply.
    /// </summary>
    /// <remarks>The reply will be captured to the end, and you will be informed when the end is reached.</remarks>
    public class LinksListener : IIrcObject
    {
        public event LinksBeginEventHandler LinksBegin;
        public event LinksEndEventHandler LinksEnd;

        private IrcClient client;
        private List<IrcLine> linksLines;
        private bool isReading;

        public LinksListener(IrcClient client)
        {
            this.client = client;
            client.LineReceived += new LineReceivedEventHandler(HandleLine);
            linksLines = new List<IrcLine>();
        }

        private void HandleLine(Object sender, LineReceivedEventArgs args)
        {
            if (!args.Line.IsNumeric) return;
            switch (args.Line.Numeric)
            {
                case 364:
                    linksLines.Add(args.Line);
                    if (!IsReading)
                    {
                        isReading = true;
                        if (LinksBegin != null)
                        	LinksBegin(this, new LinksBeginEventArgs(args.Line));
                    }
                    break;
                    
                case 365:
                    linksLines.Add(args.Line);
                    if (LinksEnd != null)
                    	LinksEnd(this, new LinksEndEventArgs(args.Line, LinksLines));
                    isReading = false;
                    break;
            }
        }

        public IrcClient Client
        {
            get { return client; }
        }

        public IrcLine[] LinksLines
        {
            get { return linksLines.ToArray(); }
        }

        public bool IsReading
        {
            get { return isReading; }
        }
    }
}
