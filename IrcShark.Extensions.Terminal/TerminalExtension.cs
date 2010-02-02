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
        
        /// <summary>
        /// Saves a value indicating whether the last written console line has 
        /// a linebreak at the end or not.
        /// </summary>
        /// <remarks>
        /// This value is needed in <see cref="CleanInputLine" /> to know if
        /// the linebreak should be cleared or not.
        /// </remarks>
        private bool newLine;
        
        /// <summary>
        /// Saves the length of the lastly written line.
        /// </summary>
        /// <remarks>
        /// This value is needed for the Write method to be able to determine where
        /// new written text should start.
        /// </remarks>
        private int lastLineLength;
        
        /// <summary>
        /// Saves the currently selected foreground color.
        /// </summary>
        private ConsoleColor fgColor;
        
        /// <summary>
        /// Saves if the autoCompleteList is up to date or need to be updated.
        /// </summary>
        private bool autoCompleteUpToDate;

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
        /// Adds all the default commands, that are part of the TerminalExtension.
        /// </summary>
        private void AddDefaultCommands()
        {
            commands.Add(new ExitCommand(this));
            commands.Add(new ExtensionCommand(this));
            commands.Add(new LogCommand(this));
            commands.Add(new HelpCommand(this));
        }
        
        /// <summary>
        /// This method is used by the internal reading thread for reading a
        /// command from the terminal.
        /// </summary>
        private void Run() {
            CommandCall command;
            while (running) {
                command = ReadCommand();
                if (command != null)
	                ExecuteCommand(command);
            }
        }
        
        /// <summary>
        /// Autocompletes the word, the cursor is currently on.
        /// </summary>
        private void AutoComplete()
        {
            if (!autoCompleteUpToDate)
            {
                
            }
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
        /// Gets or sets the foregroundcolor of the drawn text.
        /// </summary>
        /// <value>A ConsoleColor value indicating the current foreground color.</value>
        public ConsoleColor ForegroundColor
        {
        	get { return fgColor; }
        	set { fgColor = value; }
        }

        /// <summary>
        /// Executes the command with the given name.
        /// </summary>
        /// <param name="call">The CommandCall to execute.</param>
        public void ExecuteCommand(CommandCall call)
        {
            foreach (TerminalCommand cmd in commands)
            {
                if (cmd.CommandName == call.CommandName)
                {
                    cmd.Execute(call.Parameters);
                }
            }
        }

        /// <summary>
        /// Starts the TerminalExtension.
        /// </summary>
        public override void Start()
        {
            AddDefaultCommands();
            // disable the default console logger and replace it with the TerminalLogger
            Context.Application.Log.LoggedMessage -= Context.Application.DefaultConsoleLogger;
            Context.Application.Log.LoggedMessage += TerminalLogger;
            Console.ResetColor();
            fgColor = Console.ForegroundColor;
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
        /// Reads a command from the Terminal.
        /// </summary>
        /// <returns>The command line that was read form the terminal.</returns>
        public CommandCall ReadCommand() 
        {
            line = new StringBuilder();
            while (running) 
            {
            	Thread.Sleep(10);
                if (!Console.KeyAvailable)
                    continue;
                ConsoleKeyInfo key = Console.ReadKey(true);
                autoCompleteUpToDate = autoCompleteUpToDate && key.Key == ConsoleKey.Tab;
                switch (key.Key)
                {
                    case ConsoleKey.Enter:
                		//Search and execute the entered command
                		try
                		{
                			CommandCall call = new CommandCall(line.ToString());
                        	Console.WriteLine();
                        	Console.Write("-> ");
                        	line = null;
                        	return call;
                		}
                		catch(Exception) {}
                		break;
                    case ConsoleKey.End:
                		//Move the cursor to the end of the entered command
                		if (line.Length > 0) {
                			Console.CursorLeft = line.Length + 3;
                		}
                        break;
                    case ConsoleKey.Home:
                        //Move the cursor to the begining of the entered command
                        Console.CursorLeft = 3;
                        break;
                    case ConsoleKey.LeftArrow:
                        //Move the cursor leftwards, but we have to be sure that the cursor is not going out of the console
                        if (Console.CursorLeft > 3)
                        	Console.CursorLeft--;
                        break;
                    case ConsoleKey.RightArrow:
                        //Move the cursor rightwards, but we have to be sure that the cursor is not going out of the console
                        if ( Console.CursorLeft < Console.WindowWidth - 1 && Console.CursorLeft < line.Length + 3)
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
                        AutoComplete();
                        //TODO autocomplete command here
                        break;
                    case ConsoleKey.Backspace:
                   		//Make it for the user impossible to press backspace if the command line is blank
                   		if (line.Length > 0) {
                   			CleanInputLine();
                   			line.Remove(line.Length - 1, 1);
                   			Console.Write("-> " + line);
                   		}
                   		break;
                    default:;
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
            if (lastLineLength > 0)
            {
            	if (text.Contains("\n"))
            	{
            		string[] lines;
            	    lines = text.Split(new char[] { '\n' }, StringSplitOptions.None);
            	    Console.Write(lines[0]);
            	    Console.MoveBufferArea(0, Console.CursorTop, lines[0].Length, 1, lastLineLength, Console.CursorTop - 1);
            	    Console.Write(new string('\b', lines[0].Length));
            		for (int i = 1; i < lines.Length; i++)
            		{
            			Console.WriteLine(lines[i]);
            		}
            		lastLineLength = lines[lines.Length - 1].Length;
            	}
            	else 
            	{
            		Console.Write(text);
            		Console.MoveBufferArea(0, Console.CursorTop, text.Length, 1, lastLineLength, Console.CursorTop - 1);
            		Console.Write(new string('\b', text.Length));
            		lastLineLength += text.Length;
            	}
            }
            else
            {
            	Console.WriteLine(text);
            	lastLineLength = text.Length;
            	if (text.Contains("\n"))
            		lastLineLength -= 1 + text.LastIndexOf('\n');
            }
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
            lastLineLength = 0;
        }
        
        /// <summary>
        /// Resets the foreground and background color of the terminal.
        /// </summary>
        public void ResetColor()
        {
        	Console.ResetColor();
        	fgColor = Console.ForegroundColor;
        }
        
        /// <summary>
        /// Cleans the current input line to draw something else instead.
        /// </summary>
        private void CleanInputLine()
        {
        	Console.ResetColor();
        	int charCount = 3;
        	if (line != null)
        		charCount += line.Length;
        	if (!newLine)
        		charCount += 10;
        	else
        		newLine = false;
        	Console.Write(new string('\b', charCount));
        	Console.Write(new string(' ', charCount));
        	Console.Write(new string('\b', charCount));
        	Console.ForegroundColor = fgColor;
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
            CleanInputLine();
            Context.Application.Log.LoggedMessage += Context.Application.DefaultConsoleLogger;
            Context.Application.Log.LoggedMessage -= TerminalLogger;
        }
    }
}
