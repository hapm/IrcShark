using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    public delegate void InfoBeginEventHandler(Object sender, InfoBeginEventArgs args);
    public delegate void InfoEndEventHandler(Object sender, InfoEndEventArgs args);

    public class InfoListener : IIrcObject
    {
        public event InfoBeginEventHandler InfoBegin;
        public event InfoEndEventHandler InfoEnd;

        private IrcClient client;
        private List<IrcLine> infoLines;
        private bool isReading;

        public InfoListener(IrcClient client)
        {
            this.client = client;
            client.LineReceived += new LineReceivedEventHandler(HandleLine);
            infoLines = new List<IrcLine>();
        }

        private void HandleLine(Object sender, LineReceivedEventArgs args)
        {
            if (!args.Line.IsNumeric) return;
            switch (args.Line.Numeric)
            {
                case 371:
                    infoLines.Add(args.Line);
                    if (!IsReading)
                    {
                        isReading = true;
                        if (InfoBegin != null)
                        	InfoBegin(this, new InfoBeginEventArgs(args.Line));
                    }
                    break;
                    
                case 374:
                    infoLines.Add(args.Line);
                    if (InfoEnd != null)
                    	InfoEnd(this, new InfoEndEventArgs(args.Line, InfoLines));
                    isReading = false;
                    break;
            }
        }

        public IrcClient Client
        {
            get { return client; }
        }

        public IrcLine[] InfoLines
        {
            get { return infoLines.ToArray(); }
        }

        public bool IsReading
        {
            get { return isReading; }
        }
    }
}