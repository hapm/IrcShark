using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    public delegate void ChannelListBeginEventHandler(Object sender, ChannelListBeginEventArgs args);
    public delegate void ChannelListEndEventHandler(Object sender, ChannelListEndEventArgs args);

    /// <summary>
    /// This listener allows you to listen for a channel list, requested by a LIST command.
    /// </summary>
    public class ChannelListListener : IIrcObject
    {
        public event ChannelListBeginEventHandler ChannelListBegin;
        public event ChannelListEndEventHandler ChannelListEnd;

        private IrcClient client;
        private List<ChannelListLine> channelListLines;
        private bool isReading;

        public ChannelListListener(IrcClient client)
        {
            client = client;
            client.LineReceived += new LineReceivedEventHandler(HandleLine);
            channelListLines = new List<ChannelListLine>();
        }

        private void HandleLine(Object sender, LineReceivedEventArgs args)
        {
            if (!args.Line.IsNumeric) return;
            if (!IsReading && args.Line.Numeric != 321) return;
            switch (args.Line.Numeric)
            {
                case 321:
                    isReading = true;
                    if (ChannelListBegin != null) ChannelListBegin(this, new ChannelListBeginEventArgs(args.Line));
                    break;
                case 322:
                    channelListLines.Add(new ChannelListLine(args.Line));
                    break;
                case 323:
                    if (ChannelListEnd != null) ChannelListEnd(this, new ChannelListEndEventArgs(args.Line, ChannelListLines));
                    isReading = false;
                    break;
            }
        }

        public IrcClient Client
        {
            get { return client; }
        }

        public ChannelListLine[] ChannelListLines
        {
            get { return channelListLines.ToArray(); }
        }

        public bool IsReading
        {
            get { return isReading; }
        }
    }
}