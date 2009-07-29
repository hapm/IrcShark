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

        private IrcClient ClientValue;
        private List<IrcLine> InfoLinesValue;
        private bool IsReadingValue;

        public InfoListener(IrcClient client)
        {
            ClientValue = client;
            ClientValue.LineReceived += new LineReceivedEventHandler(HandleLine);
            InfoLinesValue = new List<IrcLine>();
        }

        private void HandleLine(Object sender, LineReceivedEventArgs args)
        {
            if (!args.Line.IsNumeric) return;
            switch (args.Line.Numeric)
            {
                case 371:
                    InfoLinesValue.Add(args.Line);
                    if (!IsReading)
                    {
                        IsReadingValue = true;
                        if (InfoBegin != null) InfoBegin(this, new InfoBeginEventArgs(args.Line));
                    }
                    break;
                case 374:
                    InfoLinesValue.Add(args.Line);
                    if (InfoEnd != null) InfoEnd(this, new InfoEndEventArgs(args.Line, InfoLines));
                    IsReadingValue = false;
                    break;
            }
        }

        public IrcClient Client
        {
            get { return ClientValue; }
        }

        public IrcLine[] InfoLines
        {
            get { return InfoLinesValue.ToArray(); }
        }

        public bool IsReading
        {
            get { return IsReadingValue; }
        }
    }
}