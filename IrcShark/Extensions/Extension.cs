// <copyright file="Extension.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the Extension class.</summary>

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
namespace IrcShark.Extensions
{
    using System;

    /// <summary>
    /// Classes deriving from this class can be loaded as an Extension in IrcShark.
    /// </summary>
    public abstract class Extension : MarshalByRefObject, IExtensionObject
    {
        /// <summary>
        /// Saves the ExtensionContext belonging to the Extension instance.
        /// </summary>
        private ExtensionContext context;
        
        /// <summary>
        /// Initializes a new instance of the Extension class.
        /// </summary>
        /// <param name="info">
        /// The <see cref="ExtensionContext"/> for this extension.
        /// </param>
        protected Extension(ExtensionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context", "You must specify a context");
            }
            
            if (!context.Info.Trusted)
            {
                throw new ExtensionException(context.Info, "You can't initialise an extension with an untrusted ExtensionInfo");
            }
            
            this.context = context;
        }
        
        /// <summary>
        /// Gets the context of this Extension.
        /// </summary>
        /// <value>The context.</value>
        public ExtensionContext Context
        {
            get { return context; }
        }
        
        /// <summary>
        /// Gets the extension this IExtensionObject belongs to.
        /// </summary>
        /// <value>
        /// The extension instance.
        /// </value>
        public Extension BelongsTo
        {
            get { return this; }
        }

        /// <summary>
        /// Starts the extension after the initialisation of IrcShark.
        /// </summary>
        public abstract void Start();

        /// <summary>
        /// Stops the extension before IrcShark quits or the extension is unloaded.
        /// </summary>
        public abstract void Stop();
    }
}
