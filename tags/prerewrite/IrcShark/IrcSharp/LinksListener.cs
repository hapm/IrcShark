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

        private IrcClient ClientValue;
        private List<IrcLine> LinksLinesValue;
        private bool IsReadingValue;

        public LinksListener(IrcClient client)
        {
            ClientValue = client;
            ClientValue.LineReceived += new LineReceivedEventHandler(HandleLine);
            LinksLinesValue = new List<IrcLine>();
        }

        private void HandleLine(Object sender, LineReceivedEventArgs args)
        {
            if (!args.Line.IsNumeric) return;
            switch (args.Line.Numeric)
            {
                case 364:
                    LinksLinesValue.Add(args.Line);
                    if (!IsReading) {
                        IsReadingValue = true;
                        if (LinksBegin != null) LinksBegin(this, new LinksBeginEventArgs(args.Line));
                    }
                    break;
                case 365:
                    LinksLinesValue.Add(args.Line);
                    if (LinksEnd != null) LinksEnd(this, new LinksEndEventArgs(args.Line, LinksLines));
                    IsReadingValue = false;
                    break;
            }
        }

        public IrcClient Client
        {
            get { return ClientValue; }
        }

        public IrcLine[] LinksLines
        {
            get { return LinksLinesValue.ToArray(); }
        }

        public bool IsReading
        {
            get { return IsReadingValue; }
        }
    }
}
