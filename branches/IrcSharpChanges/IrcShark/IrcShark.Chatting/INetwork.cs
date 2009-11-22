/*
 * Erstellt mit SharpDevelop.
 * Benutzer: markus
 * Datum: 11.10.2009
 * Zeit: 21:39
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
namespace IrcShark.Chatting
{
    using System;
    
    /// <summary>
    /// Defines the structure of a chat network configuration.
    /// </summary>
    public interface INetwork : System.Collections.Generic.IEnumerable<IServer>
    {
        /// <summary>
        /// Gets or sets the name of the network.
        /// </summary>
        /// <value>The name of the string.</value>
        string Name { get; set; }
        
        /// <summary>
        /// Gets the server configuration at the given index.
        /// </summary>
        /// <param name="index">The index of the server.</param>
        /// <value>The IServer instance at the given index.</value>
        IServer this[int index] 
        { 
            get; 
        }
        
        /// <summary>
        /// Gets the server configuration with the given name.
        /// </summary>
        /// <param name="name">The name of the server.</param>
        /// <value>The IServer instance.</value>
        IServer this[string name] 
        { 
            get; 
        }
        
        /// <summary>
        /// Adds a new server configuration to the network configuration.
        /// </summary>
        /// <param name="name">The name of the server configuration.</param>
        /// <param name="address">The network address as a string.</param>
        /// <returns>The new IServer instance for the implemented protocol.</returns>
        IServer AddServer(string name, string address);
        
        /// <summary>
        /// Removes the server configuration with the given name.
        /// </summary>
        /// <param name="name">The name of the server to remove.</param>
        /// <returns>If the server was found and removed true, false otherwise.</returns>
        bool RemoveServer(string name);
        
        /// <summary>
        /// Removes the server configuration at the given index.
        /// </summary>
        /// <param name="index">The index of the server configuration to remove.</param>
        void RemoveServer(int index);
    }
}
