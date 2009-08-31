using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using IrcSharp;

namespace IrcShark
{
    /// <summary>
    /// The main application class for IrcShark. It manages the needed thinks like the ExtensionManager and initialise them.
    /// </summary>
    /// <remarks>IrcSharkApplication represents a complete instance of IrcShark. You can access all aviable settings over it. You can't instanciate an IrcSharkApplication object by yourself. Use IrcSharkApplicaton.Instance instead. There will be only one instance per AppDomain.</remarks>
    public class IrcSharkApplication
    {
        private static IrcSharkApplication myInstance;
        private ExtensionManager ExtensionsValue;
        private MainForm AppForm;
        private bool ShowGUIValue;
        private IrcConnectionList ConnectionsValue;
        private NetworkManager ServersValue;
        private Logger log;
        private bool RunningValue;
        private List<SettingPanel> SettingPanelsValue;
        private String SettingPathValue;
        private String ExtensionPathValue;
        private String LogPathValue;
        private StreamWriter logStream;
        private String logFile;
        private DateTime started;

        /// <summary>
        /// Creates an instance of IrcSharkApplication
        /// </summary>
        /// <remarks>Can't be used from outside, use IrcSharkApplication.Instance instead to get an instance.</remarks>
        private IrcSharkApplication()
        {
            SettingPathValue = Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Settings" + Path.DirectorySeparatorChar;
            ExtensionPathValue = Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Extensions" + Path.DirectorySeparatorChar;
            LogPathValue = Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Logs" + Path.DirectorySeparatorChar;
            logFile = LogPath + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            log = new Logger();
            log.Filter = LogLevels.All;
            log.LogLine += new LogLineDelegate(FileLogger);
            log.Log("Initialising IrcShark...");
            log.Log("Setting-Path: " + SettingPathValue);
            log.Log("Extension-Path: " + ExtensionPathValue);
            RunningValue = false;
            ShowGUIValue = true;
            ExtensionsValue = new ExtensionManager(this);
            ConnectionsValue = new IrcConnectionList(this);
            ConnectionsValue.Added += new IrcSharp.Extended.AddedEventHandler<IrcSharp.Extended.IrcConnection>(Connections_Added);
            SettingPanelsValue = new List<SettingPanel>();
            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);			
        }

        void Connections_Added(object sender, IrcSharp.Extended.AddedEventArgs<IrcSharp.Extended.IrcConnection> args)
        {
            args.Item.Error += new IrcSharp.ErrorEventHandler(Client_Error);
        }

        void FileLogger(object sender, LogMessage args)
        {
            if (logStream == null)
            {
                logStream = new StreamWriter(new FileStream(logFile, FileMode.Append, FileAccess.Write, FileShare.Read));
            }
            logStream.WriteLine(string.Format("[{0}][{1}: {2}] {3}", args.Created.ToShortTimeString(),args.Level, args.Subject, args.Message));
            logStream.Flush();
        }

        public static IrcSharkApplication Instance
        {
            get
            {
                if (myInstance == null) myInstance = new IrcSharkApplication();
                return myInstance;
            }
        }

        /// <summary>
        /// Starts the execution of IrcShark
        /// </summary>
        /// <remarks>This method should be called at the startup of the application. It'll load all activated extensions and plugins and connect to all autoconnect servers.</remarks>
        public void Start()
        {
            if (RunningValue) return;
            log.Log("Starting IrcShark...");
            RunningValue = true;
            ServersValue = NetworkManager.LoadServers(this);
            log.Log(String.Format("{0} networks loaded", ServersValue.Networks.Count));
            SettingPanelsValue.Add(new NetworkManagerPanel(this));
            SettingPanelsValue.Add(new StatusPanel(this));
            SettingPanelsValue.Add(new ExtensionManagerPanel(this));
            Extensions.LoadEnabledExtensions();
            log.Log(String.Format("{0} of {1} extensions loaded", Extensions.Extensions.Count, Extensions.AviableExtensions.Length));
            started = DateTime.Now;
            if (ShowGUIValue)
            {
                MainForm.Show();
            }
        }

        void Application_ApplicationExit(object sender, EventArgs e)
        {
            if (AppForm != null)
            {
                AppForm.Close();
                AppForm.Dispose();
            }
            log.Log("Shuting down IrcShark...\n");
            foreach (IrcSharp.Extended.IrcConnection con in Connections)
            {
                con.Close();
            }
            Extensions.Dispose();
            Servers.SaveServers();
            logStream.Close();
        }

        void Client_Error(object sender, IrcSharp.ErrorEventArgs args)
        {
            Logger.Log(LogLevels.Error, args.Message, "irc");
        }

        public ExtensionManager Extensions
        {
            get { return ExtensionsValue; }
        }

        public NetworkManager Servers
        {
            get { return ServersValue; }
        }

        public DateTime Started
        {
            get { return started; }
        }

        public IrcConnectionList Connections
        {
            get { return ConnectionsValue; }
        }

        public MainForm MainForm
        {
            get 
            {
                //If gui wasn't initialized yet, init it now
                if (AppForm == null)
                {
                    AppForm = new MainForm(this);
                }
                return AppForm; 
            }
        }

        public SettingPanel[] SettingPanels
        {
            get { return SettingPanelsValue.ToArray(); }
        }

        public String SettingPath
        {
            get { 
                if (!Directory.Exists(SettingPathValue)) Directory.CreateDirectory(SettingPathValue);
                return SettingPathValue;
            }
        }

        public String ExtensionPath
        {
            get {
                if (!Directory.Exists(ExtensionPathValue)) Directory.CreateDirectory(ExtensionPathValue);
                return ExtensionPathValue;
            }
        }

        public String LogPath
        {
            get
            {
                if (!Directory.Exists(LogPathValue)) Directory.CreateDirectory(LogPathValue);
                return LogPathValue;
            }
        }

        public Logger Logger
        {
            get { return log; }
        }

        public string CurrentLogFile
        {
            get 
            {
                return logFile;
            }
        }

        public bool ShowGUI
        {
            get { return ShowGUIValue; }
            set
            {
                if (value)
                    MainForm.ShowTrayIcon = value;
                else if (AppForm != null)
                {
                    MainForm.ShowTrayIcon = value;
                    MainForm.Visible = false;
                }
                ShowGUIValue = value;
            }
        }
    }
}
