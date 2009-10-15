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
//
using IrcShark.Translation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using IrcShark.Extensions;

namespace IrcShark
{
    [Flags]
    public enum ExtensionStates
    {
        Available = 1,
        Loaded = 2,
        MarkedForUnload = 3
    }
    
    /// <summary>
    /// The delegate describing the event handler for the StatusChanged event
    /// </summary>
    public delegate void StatusChangedEventHandler(object sender, StatusChangedEventArgs args);
    
	/// <summary>
	/// This class represents the manager for all extensions loaded by an IrcShark instance.
	/// </summary>
	public class ExtensionManager : IEnumerable<KeyValuePair<ExtensionInfo, Extension>>
	{
		private IrcSharkApplication application;
		
        private ExtensionInfoCollection availableExtensions;
        private Dictionary<ExtensionInfo, Extension> extensions;
        //private ExtensionManagerSettings settings;

        /// <summary>
        /// This event is raised when an extension chages its auto load status.
        /// </summary>
        public event StatusChangedEventHandler StatusChanged;
		
		/// <summary>
		/// Creates a new ExtensionManager for the given IrcSharkApplication
		/// </summary>
		public ExtensionManager(IrcSharkApplication app)
		{
			if (app.Extensions != null)
				throw new ArgumentException("the given IrcSharkApplication already has an ExtensionManager", "app");
			application = app;
            extensions = new Dictionary<ExtensionInfo, Extension>();
            availableExtensions = new ExtensionInfoCollection();
            application.Log.Log(new LogMessage(Logger.CoreChannel, 1008, LogLevel.Information, "{0} extensions wait for loading", application.Settings.LoadedExtensions.Count));
            HashAvailableExtensions();
		}
		
		/// <summary>
		/// saves the instance of the application this ExtensionManager belongs to
		/// </summary>
		public IrcSharkApplication Application
		{
			get { return application; }
		}

        /// <summary>
        /// Checks if the given extension will be unloaded next time IrcShark starts.
        /// </summary>
        /// <returns>true, if the extension will be unloaded, else false</returns>
        public bool IsMarkedForUnload(ExtensionInfo ext)
        {
            if (!IsLoaded(ext)) 
            	return true;
            foreach (ExtensionInfo enabledExt in application.Settings.LoadedExtensions)
            {
                if (enabledExt.Equals(ext)) 
                	return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if the given extension is loaded or not.
        /// </summary>
        /// <returns>true, if the extension is loaded, else false</returns>
        public bool IsLoaded(ExtensionInfo info)
        {
            return extensions.ContainsKey(info);
        }

        /// <summary>
        /// Loads the given extension.
        /// </summary>
        public void Load(ExtensionInfo ext)
        {
            if (IsLoaded(ext))
            {
                if (IsMarkedForUnload(ext))
                {
                    application.Settings.LoadedExtensions.Add(ext);
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
        public void Unload(ExtensionInfo ext)
        {
            if (!IsLoaded(ext)) return;
            List<ExtensionInfo> toRemove = new List<ExtensionInfo>();
            foreach (ExtensionInfo enabledExt in application.Settings.LoadedExtensions)
            {
                if (enabledExt.Equals(ext))
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
        /// Loads an extension without raising an event.
        /// </summary>
        /// <param name="ext">The extension to load</param>
        /// <returns>true if the extension was loaded, false otherwise</returns>
        bool HiddenLoad(ExtensionInfo ext)
        {
            Extension newExtension;
            if (IsLoaded(ext)) 
            	return false;
            newExtension = (Extension)AppDomain.CurrentDomain.CreateInstanceFromAndUnwrap(ext.SourceFile, ext.Class, false, System.Reflection.BindingFlags.CreateInstance, null, new Object[] { application, ext }, null, null, null);
            extensions.Add(ext, newExtension);
            return true;
        }

        private void HashAvailableExtensions()
        {
            DirectoryInfo extDir;
            ExtensionAnalyzer extAnalyzer;
            //Dim PluginA As PluginAnalyzer
            availableExtensions.Clear();
            foreach (string dir in application.Settings.ExtensionDirectorys)
            {
            	extDir = new DirectoryInfo(dir);
            	if (!extDir.Exists)
            		application.Log.Log(new LogMessage(Logger.CoreChannel, 2002, LogLevel.Warning, "Extension directory {0} doesn't exist and is ignored", dir));
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
                    if (info.Equals(realInfo))
                    {
                        //AppValue.Logger.Log(String.Format("Loading {0}", info.TypeName));
                        HiddenLoad(realInfo);
                        unloaded.Remove(realInfo);
                        break;
                    }
                    application.Log.Log(new LogMessage(Logger.CoreChannel, 3003, LogLevel.Error, Messages.Error3003_ExtensionLoadFail, info.Class));
                    unavailable.Add(info);
                }
            }
            foreach (ExtensionInfo info in unavailable)
            {
                application.Settings.LoadedExtensions.Remove(info);
            }
        }

        /// <summary>
        /// An array of all extensions found in the extension directory.
        /// </summary>
        /// <value>a list of ExtensionInfo</value>
        public ExtensionInfo[] AvailableExtensions
        {
            get { return availableExtensions.ToArray(); }
        }
        
        public Extension this[ExtensionInfo key] 
        {
            get { return extensions[key]; }
        }

        public Dictionary<ExtensionInfo, Extension>.ValueCollection Values
        {
            get { return extensions.Values; }
        }

        public Dictionary<ExtensionInfo, Extension>.KeyCollection Keys
        {
            get { return extensions.Keys; }
        }

        public bool TryGetValue(ExtensionInfo key, out Extension value)
        {
            return extensions.TryGetValue(key, out value);
        }

        public int Count
        {
            get { return extensions.Count; }
        }

        #region IDisposable Members
        public void Dispose()
        {
        }
        #endregion

        #region IEnumerable<KeyValuePair<TKey,TValue>> Members
        public IEnumerator<KeyValuePair<ExtensionInfo, Extension>> GetEnumerator()
        {
            return extensions.GetEnumerator();
        }
        #endregion

        #region IEnumerable Members
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return extensions.GetEnumerator();
        }
        #endregion
    }
}
