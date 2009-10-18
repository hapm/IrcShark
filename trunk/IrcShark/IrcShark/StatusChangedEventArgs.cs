/*
 * Erstellt mit SharpDevelop.
 * Benutzer: markus
 * Datum: 21.09.2009
 * Zeit: 18:29
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
namespace IrcShark
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using IrcShark.Extensions;

    /// <summary>
    /// The <see cref="EventArgs" /> for the StatusChanged event.
    /// </summary>
    public class StatusChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Holds the <see cref="ExtensionInfo" /> of this StatusChangeEventArgs.
        /// </summary>
        private ExtensionInfo extension;
        
        /// <summary>
        /// Holds the new <see cref="ExtensionStates" /> of this StatusChangeEventArgs.
        /// </summary>
        private ExtensionStates status;

        /// <summary>
        /// Initializes a new instance of the StatusChangedEventArgs class.
        /// </summary>
        /// <param name="ext">Information about the extension, what changed its status.</param>
        /// <param name="status">The new status of the extension.</param>
        public StatusChangedEventArgs(ExtensionInfo ext, ExtensionStates status)
        {
            this.extension = ext;
            this.status = status;
        }

        /// <summary>
        /// Gets the ExtensionInfo for the extension, what changed its status.
        /// </summary>
        /// <value>The ExtensionInfo for the extension, what changed its status.</value>
        public ExtensionInfo Extension
        {
            get { return extension; }
        }

        /// <summary>
        /// Gets the new status of the Extension.
        /// </summary>
        /// <value>The new statu of the Extension.</value>
        public ExtensionStates Status
        {
            get { return status; }
        }
    }
}