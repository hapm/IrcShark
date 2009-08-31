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
            client = client;
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
                    if (MotdBegin != null) MotdBegin(this, new MotdBeginEventArgs(args.Line));
                    break;
                case 372:
                    motdLines.Add(args.Line);
                    break;
                case 376:
                    motdLines.Add(args.Line);
                    if (MotdEnd != null) MotdEnd(this, new MotdEndEventArgs(args.Line, MotdLines));
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
