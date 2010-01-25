using System;
using System.Collections.Generic;
using System.Text;
using IrcSharp.Extended;

namespace IrcShark
{
    /// <summary>
    /// A list of irc connections.
    /// </summary>
    public class IrcConnectionList : EventRaisingList<IrcConnection>
    {
        IrcSharkApplication IrcSharkApp;

        public IrcConnectionList(IrcSharkApplication app)
        {
            IrcSharkApp = app;
        }

        public IrcSharkApplication IrcShark
        {
            get { return IrcSharkApp; }
        }

        public IrcConnection GetByID(int id)
        {
            foreach (IrcConnection con in this)
            {
                if (con.ConnectionID == id) return con;
            }
            return null;
        }
    }
}
