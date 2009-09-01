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
                    if (!IsReading) {
                        isReading = true;
                        if (LinksBegin != null) LinksBegin(this, new LinksBeginEventArgs(args.Line));
                    }
                    break;
                case 365:
                    linksLines.Add(args.Line);
                    if (LinksEnd != null) LinksEnd(this, new LinksEndEventArgs(args.Line, LinksLines));
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
