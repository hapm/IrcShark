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

        private IrcClient client;
        private List<String> names;
        private bool isReading;

        private String ChannelNameValue;

        public NamesListener(IrcClient client)
        {
            client = client;
            client.LineReceived += new LineReceivedEventHandler(HandleLine);
            names = new List<String>();
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
                        names.Add(s);
                    }

                    if (!IsReading)
                    {
                        isReading = true;
                        if (NamesBegin != null) NamesBegin(this, new NamesBeginEventArgs(args.Line));
                    }
                    break;
                case 366:
                    if (NamesEnd != null) NamesEnd(this, new NamesEndEventArgs(args.Line, Names, ChannelNameValue));
                    isReading = false;
                    break;
            }
        }

        public IrcClient Client
        {
            get { return client; }
        }

        public String[] Names
        {
            get { return names.ToArray(); }
        }

        public bool IsReading
        {
            get { return isReading; }
        }
    }
}
