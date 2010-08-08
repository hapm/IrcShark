// <copyright file="ExtensionContext.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the ExtensionContext class.</summary>

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
    using System.IO;
    
    /// <summary>
    /// The ExtensionContext class saves informations about the context, the Extension is running in.
    /// </summary>
    /// <remarks>
    /// The context of an extension holds all information an extension needs to run. It gives access 
    /// to all needed informations as propertys.
    /// </remarks>
    public class ExtensionContext : MarshalByRefObject, IContext
    {
        /// <summary>
        /// Saves the instance of the ExtensionInfo for the Extension, this ExtensionContext was created for.
        /// </summary>
        private ExtensionInfo info;
        
        /// <summary>
        /// The IrcSharkApplication instance the ExtensionContext was created for.
        /// </summary>
        private IrcSharkApplication app;
        
        /// <summary>
        /// Saves the context specific config path.
        /// </summary>
        private string settingPath;
        
        /// <summary>
        /// Initializes a new instance of the ExtensionContext class.
        /// </summary>
        internal ExtensionContext(IrcSharkApplication app, ExtensionInfo info)
        {
            this.app = app;
            this.info = info;
            foreach (string path in app.SettingsDirectorys)
            {
                if (Directory.Exists(Path.Combine(path, info.Class)))
                {
                    this.settingPath = Path.Combine(path, info.Class);
                }
            }
            
            if (this.settingPath == null)
            {
                this.settingPath = Path.Combine(app.SettingsDirectorys.Default, info.Class);
                Directory.CreateDirectory(this.settingPath);
            }
        }
        
        /// <summary>
        /// Initializes a new instance of the ExtensionContext class.
        /// </summary>
        internal ExtensionContext(IrcSharkApplication app, Mono.Addins.TypeExtensionNode node)
        {
            this.app = app;
            foreach (string path in app.SettingsDirectorys)
            {
                if (Directory.Exists(Path.Combine(path, node.Type.Name)))
                {
                    this.settingPath = Path.Combine(path, node.Type.Name);
                }
            }
            
            if (this.settingPath == null)
            {
                this.settingPath = Path.Combine(app.SettingsDirectorys.Default, node.Type.Name);
                Directory.CreateDirectory(this.settingPath);
            }
        }
        
        /// <summary>
        /// Initializes a new instance of the ExtensionContext class.
        /// </summary>
        internal ExtensionContext(IrcSharkApplication app, IExtension ext)
        {
            this.app = app;
            string className = typeof(ExtensionContext).Name;
            foreach (string path in app.SettingsDirectorys)
            {
                if (Directory.Exists(Path.Combine(path, className)))
                {
                    this.settingPath = Path.Combine(path, className);
                }
            }
            
            if (this.settingPath == null)
            {
                this.settingPath = Path.Combine(app.SettingsDirectorys.Default, className);
                Directory.CreateDirectory(this.settingPath);
            }
        }
        
        /// <summary>
        /// Gets the IrcSharkapplication this ExtensionContext was created for.
        /// </summary>
        /// <value>
        /// The IrcSharkApplication instance.
        /// </value>
        public IrcSharkApplication Application
        {
            get { return app; }
        }
        
        /// <summary>
        /// Gets the ExtensionInfo for the Extension, this context was created for.
        /// </summary>
        /// <value>
        /// The ExtensionInfo.
        /// </value>
        public ExtensionInfo Info
        {
            get { return info; }
        }
        
        /// <summary>
        /// Gets the path where settings should be saved in this context.
        /// </summary>
        public string SettingPath
        {
            get { return settingPath; }
        }
        
        /// <summary>
        /// Gets the Logger instance for this context.
        /// </summary>
        /// <value>
        /// The Logger instance to log to.
        /// </value>
        public Logger Log {
            get {
                return Application.Log;
            }
        }
        
        /// <summary>
        /// Gets the IrcShark settings.
        /// </summary>
        /// <value>
        /// The Settings instance holding all IrcShark settings.
        /// </value>
        public Settings Settings {
            get {
                return Application.Settings;
            }
        }
    }
}
