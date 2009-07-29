using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    public delegate void NamesBeginEventHandler(Object sender, NamesBeginEventArgs args);
    public delegate void NamesEndEventHandler(Object sender, NamesEndEventArgs args);

    public class NamesListener : IIrcObject
    {
        public event NamesBeginEventHandler NamesBegin;
        public event NamesEndEventHandler NamesEnd;

        private IrcClient ClientValue;
        private List<String> NamesValue;
        private bool IsReadingValue;

        private String ChannelNameValue;

        public NamesListener(IrcClient client)
        {
            ClientValue = client;
            ClientValue.LineReceived += new LineReceivedEventHandler(HandleLine);
            NamesValue = new List<String>();
        }

        private void HandleLine(Object sender, LineReceivedEventArgs args)
        {
            if (!args.Line.IsNumeric) return;
            switch (args.Line.Numeric)
            {
                case 353:
                    ChannelNameValue = args.Line.Parameters[2];

                    foreach (String s in args.Line.Parameters[3].Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                    {
                        NamesValue.Add(s);
                    }

                    if (!IsReading)
                    {
                        IsReadingValue = true;
                        if (NamesBegin != null) NamesBegin(this, new NamesBeginEventArgs(args.Line));
                    }
                    break;
                case 366:
                    if (NamesEnd != null) NamesEnd(this, new NamesEndEventArgs(args.Line, Names, ChannelNameValue));
                    IsReadingValue = false;
                    break;
            }
        }

        public IrcClient Client
        {
            get { return ClientValue; }
        }

        public String[] Names
        {
            get { return NamesValue.ToArray(); }
        }

        public bool IsReading
        {
            get { return IsReadingValue; }
        }
    }
}
