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
            client = client;
            client.LineReceived += new LineReceivedEventHandler(HandleLine);
            whoLines = new List<WhoLine>();
        }

        private void HandleLine(Object sender, LineReceivedEventArgs args)
        {
            if (!args.Line.IsNumeric) return;
            switch (args.Line.Numeric)
            {
                case 352:
                    whoLines.Add(new WhoLine(args.Line));
                    if (!IsReading)
                    {
                        isReading = true;
                        if (WhoBegin != null) WhoBegin(this, new WhoBeginEventArgs(args.Line));
                    }
                    break;
                case 315:
                    if (WhoEnd != null) WhoEnd(this, new WhoEndEventArgs(args.Line, WhoLines));
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
