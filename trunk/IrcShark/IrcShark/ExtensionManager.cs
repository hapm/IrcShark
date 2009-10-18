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
namespace IrcShark
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;
    
    using IrcShark.Extensions;
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
    public class ExtensionManager : IEnumerable<KeyValuePair<ExtensionInfo, Extension>>
    {
        /// <summary>
        /// Saves the application instance this ExtensionManager belongs to.
        /// </summary>
        private IrcSharkApplication application;
        
        /// <summary>
        /// Saves a collection of all available extensions.
        /// </summary>
        private ExtensionInfoCollection availableExtensions;
        
        /// <summary>
        /// Saves a list of all loaded extensions.
        /// </summary>
        private Dictionary<ExtensionInfo, Extension> extensions;
        
        /// <summary>
        /// Initializes a new instance of the ExtensionManager class for the given IrcSharkApplication.
        /// </summary>
        /// <param name="app">The application, this ExtensionManager belongs to.</param>
        public ExtensionManager(IrcSharkApplication app)
        {
            if (app.Extensions != null)
                throw new ArgumentException("the given IrcSharkApplication already has an ExtensionManager", "app");
            application = app;
            extensions = new Dictionary<ExtensionInfo, Extension>();
            availableExtensions = new ExtensionInfoCollection();
            application.Log.Log(new LogMessage(Logger.CoreChannel, 1008, LogLevel.Information, Messages.Info1008_ExtensionsWaitForLoading, application.Settings.LoadedExtensions.Count));
            HashAvailableExtensions();
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
        public ExtensionInfo[] AvailableExtensions
        {
            get { return availableExtensions.ToArray(); }
        }

        /// <summary>
        /// Gets all Extensions in the ExtensionManager.
        /// </summary>
        /// <value>A ValueCollection of all Extensions.</value>
        public Dictionary<ExtensionInfo, Extension>.ValueCollection Values
        {
            get { return extensions.Values; }
        }

        /// <summary>
        /// Gets all ExtensionInfos for the Extensions in the ExtensionManager.
        /// </summary>
        /// <value>A ValueCollection of all ExtensionInfos.</value>
        public Dictionary<ExtensionInfo, Extension>.KeyCollection Keys
        {
            get { return extensions.Keys; }
        }

        /// <summary>
        /// Gets the count of loaded <see cref="Extension"/>s.
        /// </summary>
        /// <value>The number of loaded Extensions.</value>
        public int Count
        {
            get { return extensions.Count; }
        }
        
        /// <summary>
        /// Gets the Extension belonging to the given ExtensionInfo.
        /// </summary>
        /// <param name="key">The ExtensionInfo to lookup.</param>
        /// <value>The Extension for the given ExtensionInfo.</value>
        public Extension this[ExtensionInfo key] 
        {
            get { return extensions[key]; }
        }

        /// <summary>
        /// Checks if the given extension will be unloaded next time IrcShark starts.
        /// </summary>
        /// <param name="ext">The ExtensionInfo for the Extension to check.</param>
        /// <returns>True, if the extension will be unloaded, else false.</returns>
        public bool IsMarkedForUnload(ExtensionInfo ext)
        {
            if (!IsLoaded(ext)) 
                return true;
            foreach (ExtensionInfo enabledExt in application.Settings.LoadedExtensions)
            {
                if (enabledExt.CompareTo(ext)) 
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if the given extension is loaded or not.
        /// </summary>
        /// <param name="info">The ExtensionInfo for the Extension to check.</param>
        /// <returns>True, if the extension is loaded, else false.</returns>
        public bool IsLoaded(ExtensionInfo info)
        {
            return extensions.ContainsKey(info);
        }

        /// <summary>
        /// Loads the given extension.
        /// </summary>
        /// <param name="ext">The ExtensionInfo for the extension to load.</param>
        public void Load(ExtensionInfo ext)
        {
            if (IsLoaded(ext))
            {
                if (IsMarkedForUnload(ext))
                {
                    application.Settings.LoadedExtensions.Add(ext);
                    extensions[ext].Start();
                    if (StatusChanged != null) 
                        StatusChanged(this, new StatusChangedEventArgs(ext, ExtensionStates.Loaded));
                }
                return;
            }
            if (HiddenLoad(ext)) 
            {
                application.Settings.LoadedExtensions.Add(ext);
                if (StatusChanged != null) 
                    StatusChanged(this, new StatusChangedEventArgs(ext, ExtensionStates.Loaded));
            }
        }

        /// <summary>
        /// Marks the given extension to not be loaded anymore when IrcShark starts.
        /// </summary>
        /// <param name="ext">The ExtensionInfo for the extension to mark for unload.</param>
        public void Unload(ExtensionInfo ext)
        {
            if (!IsLoaded(ext)) return;
            List<ExtensionInfo> toRemove = new List<ExtensionInfo>();
            foreach (ExtensionInfo enabledExt in application.Settings.LoadedExtensions)
            {
                if (enabledExt.CompareTo(ext))
                {
                    toRemove.Add(enabledExt);
                }
            }
            foreach (ExtensionInfo toDel in toRemove)
            {
                application.Settings.LoadedExtensions.Remove(toDel);
            }
            if (StatusChanged != null) 
                StatusChanged(this, new StatusChangedEventArgs(ext, ExtensionStates.MarkedForUnload));
        }

        /// <summary>
        /// Loads all extensions what are configurated to be loaded.
        /// </summary>
        public void LoadEnabledExtensions()
        {
            List<ExtensionInfo> unloaded;
            List<ExtensionInfo> unavailable;
            unloaded = new List<ExtensionInfo>();
            unavailable = new List<ExtensionInfo>();
            unloaded.AddRange(AvailableExtensions);
            foreach (ExtensionInfo info in application.Settings.LoadedExtensions)
            {
                application.Log.Log(new LogMessage(Logger.CoreChannel, 1007, String.Format("Try to load {0}: {1} ({2})", info.Class, info.SourceFile, info.AssemblyGuid)));
                foreach (ExtensionInfo realInfo in unloaded)
                {
                    if (info.CompareTo(realInfo))
                    {
                        HiddenLoad(realInfo);
                        unloaded.Remove(realInfo);
                        break;
                    }
                    application.Log.Log(new LogMessage(Logger.CoreChannel, 3003, LogLevel.Error, Messages.Error3003_ExtensionLoadFail, info.Class));
                    unavailable.Add(info);
                }
                foreach (Extension ext in extensions.Values)
                {
                    ext.Start();
                }
            }
            foreach (ExtensionInfo info in unavailable)
            {
                application.Settings.LoadedExtensions.Remove(info);
            }
        }

        /// <summary>
        /// Trys to get the extension for the given ExtensionInfo.
        /// </summary>
        /// <param name="key">The ExtensionInfo to lookup.</param>
        /// <param name="value">The Extension out parameter to set to the Extension reference if found.</param>
        /// <returns>Returns true, if the <see cref="Extension"/> was found, false otherwise.</returns>
        public bool TryGetValue(ExtensionInfo key, out Extension value)
        {
            return extensions.TryGetValue(key, out value);
        }

        #region IDisposable Members
        /// <summary>
        /// Disposes the ExtensionManager.
        /// </summary>
        public void Dispose()
        {
            foreach (ExtensionInfo ext in this.extensions.Keys) 
            {
                Unload(ext);
            }
        }
        #endregion

        #region IEnumerable<KeyValuePair<TKey,TValue>> Members
        /// <summary>
        /// Gets a generic enumerator for this collection.
        /// </summary>
        /// <returns>The generic enumerator.</returns>
        public IEnumerator<KeyValuePair<ExtensionInfo, Extension>> GetEnumerator()
        {
            return extensions.GetEnumerator();
        }
        #endregion

        #region IEnumerable Members
        /// <summary>
        /// Gets an enumerator for this collection.
        /// </summary>
        /// <returns>The enumerator.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return extensions.GetEnumerator();
        }
        #endregion

        /// <summary>
        /// Loads an extension without raising an event.
        /// </summary>
        /// <param name="ext">The extension to load.</param>
        /// <returns>True if the extension was loaded, false otherwise.</returns>
        private bool HiddenLoad(ExtensionInfo ext)
        {
            Extension newExtension;
            if (IsLoaded(ext)) 
                return false;
            newExtension = (Extension)AppDomain.CurrentDomain.CreateInstanceFromAndUnwrap(ext.SourceFile, ext.Class, false, System.Reflection.BindingFlags.CreateInstance, null, new object[] { application, ext }, null, null, null);
            extensions.Add(ext, newExtension);
            return true;
        }

        /// <summary>
        /// Scans all extension directorys for available extensions.
        /// </summary>
        private void HashAvailableExtensions()
        {
            DirectoryInfo extDir;
            ExtensionAnalyzer extAnalyzer;
            availableExtensions.Clear();
            foreach (string dir in application.Settings.ExtensionDirectorys)
            {
                extDir = new DirectoryInfo(dir);
                if (!extDir.Exists)
                    application.Log.Log(new LogMessage(Logger.CoreChannel, 2002, LogLevel.Warning, Messages.Warning2002_ExtensionDirDoesntExist, dir));
                else 
                {
                    foreach (FileInfo dllFile in extDir.GetFiles("*.dll"))
                    {
                        extAnalyzer = new ExtensionAnalyzer(dllFile.FullName);
                        if (extAnalyzer.Extensions.Length > 0)
                        {
                            availableExtensions.AddRange(extAnalyzer.Extensions);
                        }
                    }
                }
            }
        }
    }
}
