/*
 * Erstellt mit SharpDevelop.
 * Benutzer: markus
 * Datum: 11.10.2009
 * Zeit: 22:11
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
namespace IrcShark.Chatting
{
    using System;

    /// <summary>
    /// Represents a connection to a chatting network.
    /// </summary>
    public interface IConnection
    {
        /// <summary>
        /// Gets information about the server, the client ist connected to.
        /// </summary>
        /// <value>
        /// The server for the connection.
        /// </value>
        IServer Server { get; }
        
        /// <summary>
        /// Gets or sets the alias name used in this connection.
        /// </summary>
        /// <value>
        /// The nickname as a string.
        /// </value>
        string Nickname { get; set; }
        
        /// <summary>
        /// Gets or sets the username as used for this connection.
        /// </summary>
        /// <value>The username as a string.</value>
        string UserName { get; set; }
    }
}
