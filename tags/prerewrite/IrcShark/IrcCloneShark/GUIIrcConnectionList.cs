using System;
using System.Collections.Generic;
using System.Text;
using IrcSharp.Extended;

namespace IrcCloneShark
{
    public class GUIIrcConnectionList : EventRaisingList<GUIIrcConnection>
    {
        IrcCloneSharkExtension ExtensionsValue;

        public GUIIrcConnectionList(IrcCloneSharkExtension ext)
        {
            ExtensionsValue = ext;
        }

        public IrcCloneSharkExtension Extension
        {
            get { return ExtensionsValue; }
        }

        public GUIIrcConnection GetByID(int id)
        {
            foreach (GUIIrcConnection con in this)
            {
                if (con.ConnectionID == id)
                    return con;
            }
            return null;   
        }
    }
}
