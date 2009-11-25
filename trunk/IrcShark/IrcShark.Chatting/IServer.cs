/*
 * Erstellt mit SharpDevelop.
 * Benutzer: markus
 * Datum: 11.10.2009
 * Zeit: 21:50
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
namespace IrcShark.Chatting
{
    using System;

    /// <summary>
    /// Defines the structur of a server address for chat protocols.
    /// </summary>
    public interface IServer
    {
        /// <summary>
        /// Gets or sets the display name of a server.
        /// </summary>
        /// <value>The name as a string.</value>
        string Name { get; set; }
        
        /// <summary>
        /// Gets or sets the address of the server as a string.
        /// </summary>
        /// <value>
        /// The address of the server as a string.
        /// </value>
        string Address { get; set; }
        
        /// <summary>
        /// Gets the network, the server configuration was created for.
        /// </summary>
        /// <value>The network instance.</value>
        INetwork Network { get; }
    }
}
