using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IrcShark;
using IrcShark.Extensions;
using IrcSharp;

namespace Logging
{
    public class LoggingExtension : Extension
    {
        IrcSharkApplication app;

        public LoggingExtension(IrcSharkApplication app, ExtensionInfo info)
            : base("Logging", info)
        {
            this.app = app;
            IrcShark.Connections.Added += new IrcSharp.Extended.AddedEventHandler<IrcSharp.Extended.IrcConnection>(Connections_Added);
            IrcShark.Connections.Removed += new IrcSharp.Extended.RemovedEventHandler<IrcSharp.Extended.IrcConnection>(Connections_Removed);
        }

        void Connections_Removed(object sender, IrcSharp.Extended.RemovedEventArgs<IrcSharp.Extended.IrcConnection> args)
        {
            args.Item.LineReceived -= new LineReceivedEventHandler(Item_LineReceived);
        }

        void Item_LineReceived(object sender, LineReceivedEventArgs e)
        {
            IrcShark.Logger.Log(LogLevels.Verbose, e.Line.ToString(), ((IrcSharp.Extended.IrcConnection)sender).ConnectionID.ToString());
        }

        void Connections_Added(object sender, IrcSharp.Extended.AddedEventArgs<IrcSharp.Extended.IrcConnection> args)
        {
            args.Item.LineReceived += new LineReceivedEventHandler(Item_LineReceived);
        }

        public IrcSharkApplication IrcShark
        {
            get { return app; }
        }
    }
}
