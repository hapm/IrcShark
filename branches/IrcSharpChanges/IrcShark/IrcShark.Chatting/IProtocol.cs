/*
 * Erstellt mit SharpDevelop.
 * Benutzer: markus
 * Datum: 11.10.2009
 * Zeit: 21:30
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
namespace IrcShark.Chatting
{
    using System;

    /// <summary>
    /// Defines the minimum propertys and methods of a chat protocol.
    /// </summary>
    public interface IProtocol
    {
        /// <summary>
        /// Gets a value indicating whether the protocol supports multible networks.
        /// </summary>
        /// <value>Its true, if multible networks are supported, false otherwise.</value>
        bool MultiNetwork { get; }
        
        /// <summary>
        /// Gets a value indicating whether the protocol supports multible servers for one network.
        /// </summary>
        /// <value>Its true, if multible servers are supported, false otherwise.</value>
        bool MultiServer { get; }
        
        /// <summary>
        /// Creates a new network configuration, for the implemented protocol.
        /// </summary>
        /// <param name="name">The name of the network configuration.</param>
        /// <returns>The new instance of the network configuration.</returns>
        INetwork CreateNetwork(string name);
    }
}
