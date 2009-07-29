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

        private IrcClient ClientValue;
        private List<WhoLine> WhoLinesValue;
        private bool IsReadingValue;

        public WhoListener(IrcClient client)
        {
            ClientValue = client;
            ClientValue.LineReceived += new LineReceivedEventHandler(HandleLine);
            WhoLinesValue = new List<WhoLine>();
        }

        private void HandleLine(Object sender, LineReceivedEventArgs args)
        {
            if (!args.Line.IsNumeric) return;
            switch (args.Line.Numeric)
            {
                case 352:
                    WhoLinesValue.Add(new WhoLine(args.Line));
                    if (!IsReading)
                    {
                        IsReadingValue = true;
                        if (WhoBegin != null) WhoBegin(this, new WhoBeginEventArgs(args.Line));
                    }
                    break;
                case 315:
                    if (WhoEnd != null) WhoEnd(this, new WhoEndEventArgs(args.Line, WhoLines));
                    IsReadingValue = false;
                    break;
            }
        }

        public IrcClient Client
        {
            get { return ClientValue; }
        }

        public IrcLine[] WhoLines
        {
            get { return WhoLinesValue.ToArray(); }
        }

        public bool IsReading
        {
            get { return IsReadingValue; }
        }
    }
}
