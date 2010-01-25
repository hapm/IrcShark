using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using IrcSharp;
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
    /// Manages all extensions currently aviable on the system.
    /// </summary>
    public class ExtensionManager : MarshalByRefObject, IDisposable
    {
        private String ExtensionDirectoryValue;
        private List<ExtensionInfo> AviableExtensionsValue;
        private Dictionary<ExtensionInfo, Extension> ExtensionsValue;
        private ExtensionManagerSettings SettingsValue;
        private IrcSharkApplication AppValue;

        public delegate void StatusChangedEventHandler(ExtensionManager sender, StatusChangedEventArgs args);

        /// <summary>
        /// This event is raised when an extension chages its auto load status.
        /// </summary>
        public event StatusChangedEventHandler StatusChanged;

        public ExtensionManager(IrcSharkApplication app)
        {
            AppValue = app;
            ExtensionsValue = new Dictionary<ExtensionInfo, Extension>();
            AviableExtensionsValue = new List<ExtensionInfo>();
            ExtensionDirectoryValue = AppValue.ExtensionPath;
            try
            {
                AppValue.Logger.Log("Loading extension settings");
                FileStream settingFile = new FileStream(app.SettingPath + "Extensions.xml", FileMode.Open);
                XmlSerializer xmls = new XmlSerializer(typeof(ExtensionManagerSettings));
                SettingsValue = (ExtensionManagerSettings)xmls.Deserialize(settingFile);
                settingFile.Close();
                AppValue.Logger.Log(String.Format("extension settings loaded! {0} extensions wait for loading", SettingsValue.EnabledExtensions.Count));
            }
            catch (FileNotFoundException)
            {
                SettingsValue = new ExtensionManagerSettings();
            }
            HashAviableExtensions();
        }

        /// <summary>
        /// The settings for the extension manager.
        /// </summary>
        public ExtensionManagerSettings Settings
        {
            get { return SettingsValue; }
        }

        /// <summary>
        /// The directory were all extensions are located.
        /// </summary>
        public String ExtensionDirectory
        {
            get { return ExtensionDirectoryValue; }
        }

        /// <summary>
        /// Give information about if the given extension will be unloaded next time IrcShark starts.
        /// </summary>
        /// <returns>true, if the extension will be unloaded, else false</returns>
        public bool IsMarkedForUnload(ExtensionInfo ext)
        {
            if (!IsLoaded(ext)) return true;
            foreach (ExtensionManagerSettings.EditableExtensionInfo enabledExt in Settings.EnabledExtensions)
            {
                if (enabledExt.Equals(ext)) return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if the given extension is loaded or not.
        /// </summary>
        /// <returns>true, if the extension is loaded, else false</returns>
        public bool IsLoaded(ExtensionInfo info)
        {
            return Extensions.ContainsKey(info);
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
                    Settings.EnabledExtensions.Add(new ExtensionManagerSettings.EditableExtensionInfo(ext));
                    if (StatusChanged != null) StatusChanged(this, new StatusChangedEventArgs(ext, ExtensionStates.Loaded));
                }
                return;
            }
            if (HiddenLoad(ext))
                Settings.EnabledExtensions.Add(new ExtensionManagerSettings.EditableExtensionInfo(ext));
        }

        /// <summary>
        /// Marks the given extension to be unladed next time IrcShark starts.
        /// </summary>
        public void Unload(ExtensionInfo ext)
        {
            if (!IsLoaded(ext)) return;
            List<ExtensionManagerSettings.EditableExtensionInfo> toRemove = new List<ExtensionManagerSettings.EditableExtensionInfo>();
            foreach (ExtensionManagerSettings.EditableExtensionInfo enabledExt in Settings.EnabledExtensions)
            {
                if (enabledExt.Equals(ext))
                {
                    toRemove.Add(enabledExt);
                }
            }
            foreach (ExtensionManagerSettings.EditableExtensionInfo toDel in toRemove)
            {
                Settings.EnabledExtensions.Remove(toDel);
            }
            if (StatusChanged != null) StatusChanged(this, new StatusChangedEventArgs(ext, ExtensionStates.MarkedForUnload));
        }

        bool HiddenLoad(ExtensionInfo ext)
        {
            Extension newExtension;
            if (IsLoaded(ext)) return false;
            newExtension = (Extension)AppDomain.CurrentDomain.CreateInstanceFromAndUnwrap(ext.SourceFile, ext.TypeName, false, System.Reflection.BindingFlags.CreateInstance, null, new Object[] { AppValue, ext }, null, null, null);
            ExtensionsValue.Add(ext, newExtension);
            if (StatusChanged != null) StatusChanged(this, new StatusChangedEventArgs(ext, ExtensionStates.Loaded));
            return true;
        }

        /// <summary>
        /// A dictionary of all loaded extensions.
        /// </summary>
        public ConstantDictionary<ExtensionInfo, Extension> Extensions
        {
            get { return new ConstantDictionary<ExtensionInfo, Extension>(ExtensionsValue); }
        }

        private void HashAviableExtensions()
        {
            DirectoryInfo ExtDir;
            ExtensionAnalyzer ExtAnalyzer;
            //Dim PluginA As PluginAnalyzer
            AviableExtensionsValue.Clear();
            ExtDir = new DirectoryInfo(ExtensionDirectory);
            if (!ExtDir.Exists)
                throw new ArgumentOutOfRangeException("ExtensionDirectory", "Directory for Extensions doesn't exist");
            foreach (FileInfo dllFile in ExtDir.GetFiles("*.dll"))
            {
                ExtAnalyzer = new ExtensionAnalyzer(dllFile);
                if (ExtAnalyzer.Extensions.Length > 0)
                {
                    AviableExtensionsValue.AddRange(ExtAnalyzer.Extensions);
                }
            }
        }

        /// <summary>
        /// Loads all extensions what are configrated to be loaded.
        /// </summary>
        public void LoadEnabledExtensions()
        {
            List<ExtensionInfo> unloaded;
            List<ExtensionManagerSettings.EditableExtensionInfo> unaviable;
            unloaded = new List<ExtensionInfo>();
            unaviable = new List<ExtensionManagerSettings.EditableExtensionInfo>();
            unloaded.AddRange(AviableExtensions);
            foreach (ExtensionManagerSettings.EditableExtensionInfo info in Settings.EnabledExtensions)
            {
				AppValue.Logger.Log(String.Format("Try to load {0}: {1} ({2})", info.TypeName, info.SourceFile, info.AssemblyGuid));
                foreach (ExtensionInfo realInfo in unloaded)
                {
                    if (info.Equals(realInfo))
                    {
                        AppValue.Logger.Log(String.Format("Loading {0}", info.TypeName));
                        HiddenLoad(realInfo);
                        unloaded.Remove(realInfo);
                        break;
                    }
                    AppValue.Logger.Log(LogLevels.Error, String.Format("The extension \"{0}\" couldn't be loaded", info.TypeName), "Extensions");
                    unaviable.Add(info);
                }
            }
            foreach (ExtensionManagerSettings.EditableExtensionInfo info in unaviable)
            {
                Settings.EnabledExtensions.Remove(info);
            }
        }

        /// <summary>
        /// An array of all extensions found in the extension directory.
        /// </summary>
        /// <value>a list of ExtensionInfo</value>
        public ExtensionInfo[] AviableExtensions
        {
            get { return AviableExtensionsValue.ToArray(); }
        }

        #region IDisposable Members

        public void Dispose()
        {
            FileStream fs = new FileStream(AppValue.SettingPath + "Extensions.xml", FileMode.Create);
            XmlSerializer xmls = new XmlSerializer(typeof(ExtensionManagerSettings));
            xmls.Serialize(fs, Settings);
            fs.Close();
        }

        #endregion
    }
}