// <copyright file="ExtensionManager.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the ExtensionManager class.</summary>

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
[assembly:IrcShark.Extensions.ProvidesRole(InternalName="IrcShark.ExtensionManager", NameResource="ExtensionManagerRole", DescriptionResource="ExtensionManagerRoleDescription")]
namespace IrcShark
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Security.Permissions;
    using System.Text;
    using System.Xml.Serialization;
    
    using Mono.Addins;
    
    using IrcShark.Extensions;
    using IrcShark.Security;
    using IrcShark.Translation;
    
    /// <summary>
    /// The delegate describing the event handler for the StatusChanged event.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">The arguments for the event.</param>
    public delegate void StatusChangedEventHandler(object sender, StatusChangedEventArgs e);
    
    /// <summary>
    /// The states an extension can have.
    /// </summary>
    [Flags]
    public enum ExtensionStates
    {
        /// <summary>
        /// If the extension is in the Available state, it is installed but not loaded.
        /// </summary>
        Available = 1,
        
        /// <summary>
        /// If the extension is in the Loaded state, it is installed and loaded and
        /// can be used by other extensions.
        /// </summary>
        Loaded = 2,
        
        /// <summary>
        /// If the extension ist in the MarkedForUnload state, it will be unloaded at
        /// the next IrcShark restart.
        /// </summary>
        MarkedForUnload = 3
    }
    
    /// <summary>
    /// This class represents the manager for all extensions loaded by an IrcShark instance.
    /// </summary>
    public class ExtensionManager : IEnumerable<KeyValuePair<string, IExtension>>
    {
        /// <summary>
        /// Saves the application instance this ExtensionManager belongs to.
        /// </summary>
        private IrcSharkApplication application;
        
        /// <summary>
        /// Saves a collection of all available extensions.
        /// </summary>
        private ExtensionNodeList availableExtensions;
        
        /// <summary>
        /// Saves a list of all loaded extensions.
        /// </summary>
        private Dictionary<string, IExtension> loaded;
        
        /// <summary>
        /// Initializes a new instance of the ExtensionManager class for the given IrcSharkApplication.
        /// </summary>
        /// <param name="app">The application, this ExtensionManager belongs to.</param>
        public ExtensionManager(IrcSharkApplication app)
        {
            
            if (app.Extensions != null)
            {
                throw new ArgumentException("The given IrcSharkApplication already has an ExtensionManager", "app");
            }
            
            HashAvailableExtensions();
            
            loaded = new Dictionary<string, IExtension>();
            
            application = app;
            application.Log.Log(new LogMessage(Logger.CoreChannel, 1008, LogLevel.Information, Messages.Info1008_ExtensionsWaitForLoading, application.Settings.LoadedExtensions.Count));
            //HashAvailableExtensions();
        }

        /// <summary>
        /// This event is raised when an extension chages its auto load status.
        /// </summary>
        public event StatusChangedEventHandler StatusChanged;
        
        /// <summary>
        /// Gets the instance of the application this ExtensionManager belongs to.
        /// </summary>
        /// <value>The application instance.</value>
        public IrcSharkApplication Application
        {
            get { return application; }
        }

        /// <summary>
        /// Gets all extensions found in the extension directory.
        /// </summary>
        /// <value>An array of ExtensionInfo.</value>
        public IrcShark.Extensions.ExtensionAttribute[] AvailableExtensions
        {
            get 
            {
                IrcShark.Extensions.ExtensionAttribute[] result = new IrcShark.Extensions.ExtensionAttribute[availableExtensions.Count];
                for (int i = 0; i < availableExtensions.Count; i++)
                {
                    TypeExtensionNode<IrcShark.Extensions.ExtensionAttribute> extNode = availableExtensions[i] as TypeExtensionNode<IrcShark.Extensions.ExtensionAttribute>;
                    result[i] = extNode.Data;
                }
                return result;
            }
        }

        /// <summary>
        /// Gets all Extensions in the ExtensionManager.
        /// </summary>
        /// <value>A ValueCollection of all Extensions.</value>
        public Dictionary<string, IExtension>.ValueCollection Values
        {
            get { return loaded.Values; }
        }

        /// <summary>
        /// Gets all ExtensionInfos for the Extensions in the ExtensionManager.
        /// </summary>
        /// <value>A ValueCollection of all ExtensionInfos.</value>
        public Dictionary<string, IExtension>.KeyCollection Keys
        {
            get { return loaded.Keys; }
        }

        /// <summary>
        /// Gets the count of loaded <see cref="Extension"/>s.
        /// </summary>
        /// <value>The number of loaded Extensions.</value>
        public int Count
        {
            get { return loaded.Count; }
        }
        
        /// <summary>
        /// Gets the Extension belonging to the given ExtensionInfo.
        /// </summary>
        /// <param name="key">The ExtensionInfo to lookup.</param>
        /// <value>The Extension for the given ExtensionInfo.</value>
        public IExtension this[string id]
        {
            //TODO fix this to use AddinManager
            get 
            { 
                foreach (IExtension ext in loaded.Values)
                {
                    if (ext.Id == id)
                        return ext;
                }
                return null;
            }
        }

        /// <summary>
        /// Checks if the given extension is loaded or not.
        /// </summary>
        /// <param name="id">The id of the Extension to check.</param>
        /// <returns>True, if the extension is loaded, else false.</returns>
        public bool IsStarted(string id)
        {
            return (this[id] != null);
        }

        /// <summary>
        /// Loads the given extension.
        /// </summary>
        /// <param name="id">The id of the extension to load.</param>
        [RolePermission(SecurityAction.Demand, Roles="IrcShark.ExtensionManager")]
        public void Start(string id)
        {
            IExtension ext = this[id];
            if (ext == null) 
            {
                TypeExtensionNode<IrcShark.Extensions.ExtensionAttribute> node = GetExtensionNode(id);
                ext = node.CreateInstance() as IExtension;
                if (ext.Id == null)
                {
                    loaded.Add(ext.GetType().Name, ext);
                }
                else
                {
                    loaded.Add(ext.Id, ext);
                }
            }
            AddinManager.GetExtensionNodes(typeof(IExtension));
            AddinManager.LoadAddin(null, id);
        }
        
        /// <summary>
        /// Stops the extension with the given id.
        /// </summary>
        /// <param name="id">The id of the extension to stop.</param>
        [RolePermission(SecurityAction.Demand, Roles="IrcShark.ExtensionManager")]
        public void Stop(string id)
        {
            IExtension ext = this[id];
            if (ext != null)
            {
                ext.Stop();
                loaded.Remove(id);                
            }
        }
        
        /// <summary>
        /// Gets the TypeExtensioNode for the given extension id.
        /// </summary>
        /// <param name="id">The id of the extension to get the TypeExtensionNode for.</param>
        /// <returns>The TypeExtensionNode for the extension or null if there is no extension with the given id available.</returns>
        private TypeExtensionNode<IrcShark.Extensions.ExtensionAttribute> GetExtensionNode(string id)
        {
            foreach (TypeExtensionNode<IrcShark.Extensions.ExtensionAttribute> extNode in availableExtensions) 
            {
                if (extNode.Data.Id == id)
                {
                    return extNode;
                }
            }
            return null;
        }

        /// <summary>
        /// Loads all extensions what are configurated to be loaded.
        /// </summary>
        public void StartEnabledExtensions()
        {
            foreach (TypeExtensionNode<IrcShark.Extensions.ExtensionAttribute> extNode in AddinManager.GetExtensionNodes(typeof(IExtension))) {
                IExtension ext = extNode.CreateInstance() as IExtension;
                if (extNode.Id == null) 
                    loaded.Add(ext.GetType().Name, ext);
                else
                    loaded.Add(ext.Id, ext);
            }
            
            // we need to add this extra loop because the Start methods can access the loaded list.
            foreach (IExtension ext in loaded.Values) {
                ext.Start(new IrcShark.Extensions.ExtensionContext(application, ext));                
            }
        }

        /// <summary>
        /// Trys to get the extension for the given ExtensionInfo.
        /// </summary>
        /// <param name="key">The ExtensionInfo to lookup.</param>
        /// <param name="value">The Extension out parameter to set to the Extension reference if found.</param>
        /// <returns>Returns true, if the <see cref="Extension"/> was found, false otherwise.</returns>
        public bool TryGetValue(string key, out IExtension value) {
            return loaded.TryGetValue(key, out value);
        }

        #region IDisposable Members
        /// <summary>
        /// Disposes the ExtensionManager.
        /// </summary>
        public void Dispose()
        {
            foreach (IExtension ext in loaded.Values)
            {
                ext.Stop();
            }
        }
        #endregion

        #region IEnumerable<KeyValuePair<TKey,TValue>> Members
        /// <summary>
        /// Gets a generic enumerator for this collection.
        /// </summary>
        /// <returns>The generic enumerator.</returns>
        public IEnumerator<KeyValuePair<string, IExtension>> GetEnumerator()
        {
            return loaded.GetEnumerator();
        }
        #endregion

        #region IEnumerable Members
        /// <summary>
        /// Gets an enumerator for this collection.
        /// </summary>
        /// <returns>The enumerator.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return loaded.GetEnumerator();
        }
        #endregion

        /// <summary>
        /// Raises the Status Changed event.
        /// </summary>
        /// <param name="addinId">The id of the addin, that changed its status.</param>
        /// <param name="newState">The state the extension was changed to.</param>
        protected void OnStatusChanged(string addinId, ExtensionStates newState)
        {
            if (StatusChanged != null) 
            {
                StatusChanged(this, new StatusChangedEventArgs(addinId, newState));
            }
        }
        
        /// <summary>
        /// Scans all extension directorys for available extensions.
        /// </summary>
        private void HashAvailableExtensions()
        {
            availableExtensions = AddinManager.GetExtensionNodes(typeof(IExtension));
        }
    }
}
