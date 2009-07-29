using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    /// <summary>
    /// By implementing the IIrcObject, all objects of the class need to be "bound" to an irc connection.
    /// </summary>
    /// <remarks>There are many objects belonging to an irc connection. All these objects should implement this interface, so u can always get the associated connection from them. You can get it over the IrcClient property.</remarks>
    public interface IIrcObject
    {
        /// <summary>
        /// Returns the associated irc connection.
        /// </summary>
        /// <value>the associated IrcClient</value>
        IrcClient Client
        {
            get;
        }
    }
}
