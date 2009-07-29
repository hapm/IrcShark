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

        private IrcClient ClientValue;
        private List<IrcLine> MotdLinesValue;
        private bool IsReadingValue;

        public MotdListener(IrcClient client)
        {
            ClientValue = client;
            ClientValue.LineReceived += new LineReceivedEventHandler(HandleLine);
            MotdLinesValue = new List<IrcLine>();
        }

        private void HandleLine(Object sender, LineReceivedEventArgs args)
        {
            if (!args.Line.IsNumeric) return;
            if (!IsReading && args.Line.Numeric != 375) return;
            switch (args.Line.Numeric)
            {
                case 375:
                    IsReadingValue = true;
                    MotdLinesValue.Clear();
                    MotdLinesValue.Add(args.Line);
                    if (MotdBegin != null) MotdBegin(this, new MotdBeginEventArgs(args.Line));
                    break;
                case 372:
                    MotdLinesValue.Add(args.Line);
                    break;
                case 376:
                    MotdLinesValue.Add(args.Line);
                    if (MotdEnd != null) MotdEnd(this, new MotdEndEventArgs(args.Line, MotdLines));
                    IsReadingValue = false;
                    break;
            }
        }

        public IrcClient Client
        {
            get { return ClientValue; }
        }

        public IrcLine[] MotdLines
        {
            get { return MotdLinesValue.ToArray(); }
        }

        public bool IsReading
        {
            get { return IsReadingValue; }
        }
    }
}
