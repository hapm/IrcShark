using System;
using System.Collections.Generic;
using System.Text;

namespace IrcShark
{
    public class ServerSelectedEventArgs : EventArgs
    {
        private ServerConfiguration SelectedServerValue;

        public ServerSelectedEventArgs(ServerConfiguration server)
        {
            SelectedServerValue = server;
        }

        public ServerConfiguration SelectedServer
        {
            get { return SelectedServerValue; }
        }
    }
}
