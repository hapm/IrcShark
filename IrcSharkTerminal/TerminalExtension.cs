namespace IrcSharkTerminal
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using IrcShark;
    using IrcShark.Extensions;
    using System.Threading;
    
    /// <summary>
    /// This extension allows the administration of IrcShark over the console.
    /// </summary>
    public class TerminalExtension : IrcShark.Extensions.Extension
    {
        /// <summary>
        /// Saves a list of all commands added to the terminal.
        /// </summary>
        private List<TerminalCommand> commands;
        
        /// <summary>
        /// Saves the line buffer of the current input line.
        /// </summary>
        private StringBuilder line;
        
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
        /// A reference to the currently selected command in the history.
        /// </summary>
        private LinkedListNode<string> currentHistoryCmd;
        
        private bool newLine;

        /// <summary>
        /// Initializes a new instance of the TerminalExtension class.
        /// </summary>
        /// <param name="context">The context, this extension is created for.</param>
        public TerminalExtension(ExtensionContext context)
            : base(context)
        {
            commands = new List<TerminalCommand>();
            cmdHistory = new LinkedList<string>();
            newLine = false;
        }
        
        /// <summary>
        /// Gets all commands currently added to the TerminalExtension.
        /// </summary>
        /// <value>
        /// An array of TerminalCommands.
        /// </value>
        public TerminalCommand[] Commands
        {
            get { return commands.ToArray(); }
        }

        /// <summary>
        /// Executes the command with the given name.
        /// </summary>
        /// <param name="name">The name of the command to execute.</param>
        public void ExecuteCommand(string name)
        {
            foreach (TerminalCommand cmd in commands)
            {
                if (cmd.CommandName == name)
                {
                    cmd.Execute();
                }
            }
        }
        
        /// <summary>
        /// Adds all the default commands, that are part of the TerminalExtension.
        /// </summary>
        private void AddDefaultCommands()
        {
            commands.Add(new HelpCommand(this));
            commands.Add(new ExitCommand(this));
        }

        /// <summary>
        /// Starts the TerminalExtension.
        /// </summary>
        public override void Start()
        {
            AddDefaultCommands();
            // disable the default console logger and replace it with the TerminalLogger
            Context.Application.Log.LoggedMessage -= Context.Application.DefaultConsoleLogger;
            Context.Application.Log.LoggedMessage += new LoggedMessageEventHandler(TerminalLogger);
            WriteLine("*******************************************************************************");
            WriteLine("*                   IrcShark started successfully, have fun!                  *");
            WriteLine("*      Use the \"help\" command to get a list of all available commands         *");
            WriteLine("*******************************************************************************");
            WriteLine();
            // unregister the default console logger as of incompatibility;
            readerThread = new Thread(new ThreadStart(this.Run));
            running = true;
            readerThread.Start();
        }

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
                return;
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
        /// This method is used by the internal reading thread for reading a
        /// command from the terminal.
        /// </summary>
        private void Run() {
            string command;
            while (running) {
                command = ReadCommand();
                if (command != null)
	                ExecuteCommand(command);
            }
        }
        
        /// <summary>
        /// Reads a command from the Terminal.
        /// </summary>
        /// <returns>The command line that was read form the terminal.</returns>
        public string ReadCommand() 
        {
            line = new StringBuilder();
            while (running) 
            {
            	Thread.Sleep(10);
                if (!Console.KeyAvailable)
                    continue;
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.Enter:
                        Console.WriteLine();
                        Console.Write("-> ");
                        String cmd = line.ToString();
                        line = null;
                        return cmd;
                    case ConsoleKey.End:
                        //TODO handle moving the cursor to the endline here
                        break;
                    case ConsoleKey.Home:
                        Console.CursorLeft = 0;
                        break;
                    case ConsoleKey.LeftArrow:
                        Console.CursorLeft--;
                        break;
                    case ConsoleKey.RightArrow:
                        Console.CursorLeft++;
                        break;
                    case ConsoleKey.UpArrow:
                        if (currentHistoryCmd == null) {
                            if (cmdHistory.Last == null)
                                break;
                            currentHistoryCmd = cmdHistory.Last;
                            cmdHistory.AddLast(line.ToString());
                            line = new StringBuilder(currentHistoryCmd.Value);
                            Console.CursorLeft = 0;
                            Console.Write(line.ToString());
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        //TODO get next command in history
                        break;
                    case ConsoleKey.Tab:
                        //TODO autocomplete command here
                        break;
                    default:
                        line.Append(key.KeyChar);
                        Console.Write(key.KeyChar);
                        break;
                }
            }
            return null;
        }
        
        /// <summary>
        /// Writes text to the terminal.
        /// </summary>
        /// <param name="text">The text to write.</param>
        public void Write(string text) 
        {
        	int col = Console.CursorLeft;
        	if (col < 3)
        		col = 3;
        	CleanInputLine();
            Console.WriteLine(text);
            Console.Write("-> ");
            if (this.line != null) 
            {
    	        Console.Write(this.line.ToString());
            }
	        Console.CursorLeft = col;
        }
        
        /// <summary>
        /// Writes a complete line and appends a linebreak at the end.
        /// </summary>
        /// <param name="line">The line to write.</param>
        public void WriteLine(string line)
        {
            Write(line);
            newLine = true;
        }
        
        private void CleanInputLine()
        {
        	int charCount = 3;
        	if (line != null)
        		charCount += line.Length;
        	if (!newLine)
        		charCount++;
        	else
        		newLine = false;
        	Console.Write(new string('\b', charCount));
        	Console.Write(new string(' ', charCount));
        	Console.Write(new string('\b', charCount));
        }
        
        /// <summary>
        /// Writes a complete formated line and appends a linebreak at the end.
        /// </summary>
        /// <param name="format">The format to write.</param>
        /// <param name="arg">The objects to use when formating the line.</param>
        public void WriteLine(string format, params object[] arg)
        {
        	WriteLine(string.Format(format, arg));
        }
        
        /// <summary>
        /// Writes a linebreak to the terminal.
        /// </summary>
        public void WriteLine()
        {
            WriteLine("");
        }
        
        /// <summary>
        /// Stops the execution of the TerminalExtension.
        /// </summary>
        public override void Stop()
        {
            running = false;
            readerThread.Join();
        }
    }
}
