// <copyright file="TerminalExtension.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the TerminalExtension class.</summary>

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
namespace IrcShark.Extensions.Terminal
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;

    using IrcShark;
    using IrcShark.Extensions;
    using IrcShark.Extensions.Terminal.Commands;
    
    /// <summary>
    /// This extension allows the administration of IrcShark over the console.
    /// </summary>
    [Extension(Name="Terminal", Id="IrcShark.Extensions.Terminal.TerminalExtension")]
    public class TerminalExtension : Extension
    {
        /// <summary>
        /// Persistent GetLine instance for our hisory and autocomplet function
        /// </summary>
        private ITerminal currentTerminal;
        
        /// <summary>
        /// Saves a list of all commands added to the terminal.
        /// </summary>
        public List<string> AutoCompleteList = new List<string>();
        
        /// <summary>
        /// The log channel for the TerminalExtension.
        /// </summary>
        public const string LogChannel = "terminal";
        
        /// <summary>
        /// Saves the list of terminal implementations available.
        /// </summary>
        private Mono.Addins.ExtensionNodeList commandNodes;
        
        /// <summary>
        /// Saves a list of all commands added to the terminal.
        /// </summary>
        private Mono.Addins.ExtensionNodeList commandNodes;
        
        /// <summary>
        /// Saves the list of commands.
        /// </summary>
        private Dictionary<string, ITerminalCommand> commands;
        
        /// <summary>
        /// Saves the list of terminals currently running.
        /// </summary>
        private List<ITerminal> runningTerminals;
        
        /// <summary>
        /// Saves the state of the extension.
        /// </summary>
        private bool running;
        
        /// <summary>
        /// Saves the thread, what is used to read input from the terminal.
        /// </summary>
        private Thread readerThread;
        
        /// <summary>
        /// Saves a list of the last typed commands.
        /// </summary>
        private LinkedList<string> cmdHistory;
        
        /// <summary>
        /// Saves the currently selected foreground color.
        /// </summary>
        private ConsoleColor foregroundColor;
        
        private string inputPrefix;

        /// <summary>
        /// Initializes a new instance of the TerminalExtension class.
        /// </summary>
        /// <param name="context">The context, this extension is created for.</param>
        public TerminalExtension()
        {
            cmdHistory = new LinkedList<string>();
            commands = new Dictionary<string, ITerminalCommand>();
            inputPrefix = "shell> ";
        }
        
        /// <summary>
        /// Gets all commands currently added to the TerminalExtension.
        /// </summary>
        /// <value>
        /// An array of TerminalCommands.
        /// </value>
        public Mono.Addins.ExtensionNodeList CommandNodes
        {
            get { return commandNodes; }
        }
        
        /// <summary>
        /// Gets or sets the foregroundcolor of the drawn text.
        /// </summary>
        /// <value>A ConsoleColor value indicating the current foreground color.</value>
        public ConsoleColor ForegroundColor
        {
            get { return foregroundColor; }
            set 
            { 
                foregroundColor = value; 
                Console.ForegroundColor = foregroundColor;
            }
        }

        /// <summary>
        /// Executes the command with the given name.
        /// </summary>
        /// <param name="call">The CommandCall to execute.</param>
        public void ExecuteCommand(CommandCall call)
        {
            ITerminalCommand cmd;
            if (commands.ContainsKey(call.CommandName))
            {
                cmd = commands[call.CommandName];
            }
            else 
            {
                cmd = InitializeCommand(call.CommandName);
            }
            
            if (cmd != null)
            {
                cmd.Execute(call.Parameters);
            }
        }

        /// <summary>
        /// Starts the TerminalExtension.
        /// </summary>
        public override void Start(IrcShark.Extensions.ExtensionContext context)
        {
            Context = context;
            // Set  encoding to the system ANSI codepage for special characters
            Console.InputEncoding = Encoding.Default;
            AddCommands();
            
            // disable the default console logger and replace it with the TerminalLogger
            Context.Application.Log.LoggedMessage -= Context.Application.DefaultConsoleLogger;
            Context.Application.Log.LoggedMessage += TerminalLogger;
            
            currentTerminal = new ConsoleTerminal();
            
            // Register the AutoCompleteEvent
            currentTerminal.AutoCompleteEvent += new AutoCompleteHandler(AutoComplete);
                      	
            drawStartupLogo();    
            
            // unregister the default console logger as of incompatibility;
            readerThread = new Thread(new ThreadStart(this.Run));
            running = true;
            readerThread.Start();
        }
        
        private ITerminalCommand InitializeCommand(string cmdName)
        {
            foreach (Mono.Addins.TypeExtensionNode<TerminalCommandAttribute> cmdNode in CommandNodes)
            {
                if (cmdNode.Data.Name == cmdName)
                {
                    ITerminalCommand cmd = cmdNode.CreateInstance() as ITerminalCommand;
                    cmd.Init(this);
                    commands.Add(cmd.CommandName, cmd);
                    return cmd;
                }
            }
            
            return null;
        }
		
        private void drawStartupLogo()
        {
        	Version isVersion = Context.Application.Version;
            string info = string.Format("Version {0}.{1} Build {2}", isVersion.Major, isVersion.Minor, isVersion.Build);
            string[] logo = new string[]
            {
                @" _____           _____ _                _    ",
                @"|_   _|         / ____| |              | |   ",
                @"  | |  _ __ ___| (___ | |__   __ _ _ __| | __",
                @"  | | | '__/ __|\___ \| '_ \ / _` | '__| |/ /",
                @" _| |_| | | (__ ____) | | | | (_| | |  |   < ",
                @"|_____|_|  \___|_____/|_| |_|\__,_|_|  |_|\_\",
                @"...the new feeling of irc!                   ",
            };
            
            foreach (string line in logo)
            {
                Console.SetCursorPosition(Console.WindowWidth / 2 - line.Length / 2, Console.CursorTop);
                Console.WriteLine(line);
            }
            
            Console.WriteLine();
            Console.SetCursorPosition(Console.WindowWidth / 2 - info.Length / 2, Console.CursorTop);
            Console.WriteLine(info);
            Console.WriteLine();
        }

        /// <summary>
        /// This is a replacement logger for the <see cref="IrcSharkApplication.DefaultConsoleLogger" />.
        /// </summary>
        /// <remarks>
        /// The default loghandler is not compatible with the TerminalExtension and is replaced with this 
        /// loghandler.
        /// </remarks>
        /// <param name="sender">The Logger what send the message.</param>
        /// <param name="msg">The Message.</param>
        public void TerminalLogger(object sender, LogMessage msg)
        {
            LogHandlerSetting logSetting;
            try
            { 
                logSetting = Context.Application.Settings.LogSettings["IrcShark.Extensions.TerminalLogHandler"];
            }
            catch (IndexOutOfRangeException)
            {
                logSetting = new LogHandlerSetting("IrcShark.Extensions.TerminalLogHandler");
                logSetting.Debug = false;
                logSetting.Warning = true;
                logSetting.Error = true;
                Context.Application.Settings.LogSettings.Add(logSetting);
                return;
            }
            
            if (!logSetting.ApplysTo(msg))
            {
                return;
            }
            
            string format = "[{0}][{1}][{2}] {3}";
            switch (msg.Level)
            {
                case LogLevel.Debug:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case LogLevel.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogLevel.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
            }
            
            WriteLine(format, msg.Time, msg.Channel, msg.Level.ToString(), msg.Message);
            Console.ResetColor();
        }
        
        /// <summary>
        /// Reads a command from the Terminal.
        /// </summary>
        /// <returns>The command line that was read form the terminal.</returns>
        public CommandCall ReadCommand() 
        {
            while (running) 
            {
                Thread.Sleep(10);
                CommandCall call;
			    try
                {
                	call = currentTerminal.ReadCommand();
                	if (call != null) {
                    	return call;
                	}
                }
                catch (Exception ex)
                {
                    Context.Application.Log.Log(new LogMessage(LogChannel, 1337, LogLevel.Error, "Couldn't execute command: {0}", ex.ToString()));
                }
            }
            
            return null;
        }

        /// <summary>
        /// Writes text to the terminal.
        /// </summary>
        /// <param name="text">The text to write.</param>
        public void Write(string format, params object[] arg)
        {
            currentTerminal.Write(format, arg);
        }
        
        /// <summary>
        /// Writes text to the terminal.
        /// </summary>
        /// <param name="text">The text to write.</param>
        public void Write(string text) 
        {
            currentTerminal.Write(text);
        }
        
        /// <summary>
        /// Writes a complete line and appends a linebreak at the end.
        /// </summary>
        /// <param name="line">The line to write.</param>
        public void WriteLine(string line)
        {
            currentTerminal.WriteLine(line);
        }
        
        /// <summary>
        /// Resets the foreground and background color of the terminal.
        /// </summary>
        public void ResetColor()
        {
            currentTerminal.ResetColor();
        }
        
        /// <summary>
        /// Writes a complete formated line and appends a linebreak at the end.
        /// </summary>
        /// <param name="format">The format to write.</param>
        /// <param name="arg">The objects to use when formating the line.</param>
        public void WriteLine(string format, params object[] arg)
        {
            currentTerminal.WriteLine(format, arg);
        }
        
        /// <summary>
        /// Writes a linebreak to the terminal.
        /// </summary>
        public void WriteLine()
        {
            currentTerminal.WriteLine();
        }
        
        /// <summary>
        /// Stops the execution of the TerminalExtension.
        /// </summary>
        public override void Stop()
        {
            running = false;
            currentTerminal.StopReading();
            readerThread.Join();
            //CleanInputLine();
            Context.Application.Log.LoggedMessage += Context.Application.DefaultConsoleLogger;
            Context.Application.Log.LoggedMessage -= TerminalLogger;
        }
        
        /// <summary>
        /// Adds all commands.
        /// </summary>
        private void AddCommands()
        {
            commandNodes = Mono.Addins.AddinManager.GetExtensionNodes(typeof(ITerminalCommand));
        }
        
        /// <summary>
        /// This method is used by the internal reading thread for reading a
        /// command from the terminal.
        /// </summary>
        private void Run() 
        {
            CommandCall command;
            while (running) 
            {
                command = ReadCommand();
                if (command != null)
                {
                    try
                    {
                        ExecuteCommand(command);
                        //TODO bad solution to check the exit command hardcoded here to hold the terminal from showing a prompt.
                        if (command.CommandName == "exit")
                        {
                            running = false;
                        }
                    }
                    catch (System.Security.SecurityException ex)
                    {
                        ForegroundColor = ConsoleColor.Red;
                        WriteLine("Permission denied: " + ex.Message);
                        ResetColor();
                    }
                    catch (Exception ex)
                    {
                        Context.Application.Log.Error("Terminal", 0, "The command {0} throwed an exception: {1}", command.CommandName, ex.ToString());
                    }
                }
            }
        }
        
        /// <summary>
        /// Autocompletes the word, the cursor is currently on.
        /// </summary>
        private Completion AutoComplete(string text, int cursor)
        {
            if (text.Length == 0)
            {
                return new Completion(text, null);
            }
            
            CommandCall call = new CommandCall(text.Substring(0, cursor));
            string[] result = null;
            string prefix = "";
            if (call.Parameters.Length == 0)
            {
                List<string> list = new List<string>();
                prefix = call.CommandName;
                foreach (Mono.Addins.TypeExtensionNode<TerminalCommandAttribute> cmd in commandNodes)
                {
                    if (cmd.Data.Name.StartsWith(prefix))
                    {
                        list.Add(cmd.Data.Name);
                    }
                }
                
                if (list.Count > 0)
                {
                    result = list.ToArray();
                }
            }
            else
            {
                foreach (Mono.Addins.TypeExtensionNode<TerminalCommandAttribute> cmdNode in commandNodes)
                {
                    if (cmdNode.Data.Name.Equals(call.CommandName))
                    {
                        ITerminalCommand cmd = cmdNode.CreateInstance() as ITerminalCommand;
                        cmd.Init(this);
                        result = cmd.AutoComplete(call, call.Parameters.Length - 1);
                        prefix = call.Parameters[call.Parameters.Length - 1];
                        break;
                    }
                }
            }
            
            if (result != null) 
            {
                for (int i = 0; i < result.Length; i++) 
                {
                    result[i] = result[i].Substring(prefix.Length);
                }
            }
            
            return new Completion(prefix, result);
        }
    }
}
