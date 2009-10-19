/*
 * Erstellt mit SharpDevelop.
 * Benutzer: markus
 * Datum: 16.10.2009
 * Zeit: 13:36
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
namespace IrcShark.Extensions.Chatting
{
    using System;
    using System.Collections.Generic;
    using IrcShark.Chatting;
    using IrcShark.Extensions;

    /// <summary>
    /// The ChatManagerExtension allows to manage connections to chat servers
    /// with different protocols.
    /// </summary>
    public class ChatManagerExtension : Extension
    {
        /// <summary>
        /// Saves a list of all registred protocols.
        /// </summary>
        private List<IProtocol> registredProtocols;
        
        /// <summary>
        /// Saves a list of all open connections.
        /// </summary>
        private List<IConnection> openConnections;
        
        /// <summary>
        /// Initializes a new instance of the ChatManagerExtension class.
        /// </summary>
        /// <param name="app">The app instance creating this IrcSharkApplication.</param>
        /// <param name="info">The info belonging to this extension.</param>
        public ChatManagerExtension(ExtensionContext context) : base(context)
        {
            registredProtocols = new List<IProtocol>();
            openConnections = new List<IConnection>();
        }
        
        /// <summary>
        /// Starts the ChatManagerExtension.
        /// </summary>
        public override void Start() 
        {            
        }
        
        /// <summary>
        /// Stops the ChatManagerExtension.
        /// </summary>
        public override void Stop() 
        {            
        }
    }
}
