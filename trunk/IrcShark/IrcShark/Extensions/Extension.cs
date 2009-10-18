// $Id$
// 
// Note:
// 
// Copyright (C) 2009 IrcShark Team
//  
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
        /// Holds the instance of the IrcSharkApplication, this Extension is loaded by.
        /// </summary>
        private IrcSharkApplication application;
        
        /// <summary>
        /// The ExtensionInfo instance what identifies this extension.
        /// </summary>
        private ExtensionInfo info;
        
        /// <summary>
        /// Initializes a new instance of the Extension class.
        /// </summary>
        /// <param name="app">
        /// The <see cref="IrcSharkApplication"/> initialising this instance.
        /// </param>
        /// <param name="info">
        /// The <see cref="ExtensionInfo"/> used by the application to identify this extension.
        /// </param>
        protected Extension(IrcSharkApplication app, ExtensionInfo info)
        {
            if (!info.Trusted)
                throw new ExtensionException(info, "You can't initialise an extension with an untrusted ExtensionInfo");
            if (app == null)
                
            this.info = info;
            application = app;
        }
        
        /// <summary>
        /// Gets the application what loaded this extension.
        /// </summary>
        /// <value>
        /// The IrcSharkApplication instance.
        /// </value>
        public IrcSharkApplication Application
        {
            get { return application; }
        }
            
        /// <summary>
        /// Gets the <see cref="ExtensionInfo"/> used to identify this extension.
        /// </summary>
        /// <value>The ExtensionInfo instance.</value>
        public ExtensionInfo Info 
        {
            get { return info; }
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
    }
}
