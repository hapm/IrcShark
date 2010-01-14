namespace IrcSharkTerminal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using IrcShark;
    using IrcShark.Extensions;
    using System.Threading;
    
    /// <summary>
    /// This extension allows the administration of IrcShark over the console.
    /// </summary>
    public class TerminalExtension : IrcShark.Extensions.Extension
    {
        private List<TerminalCommand> commands;
        private bool running;
        private Thread readerThread;
        private LinkedList<string> cmdHistory;
        private LinkedListNode<string> currentHistoryCmd;

        public TerminalExtension(ExtensionContext context)
            : base(context)
        {
            commands = new List<TerminalCommand>();
            cmdHistory = new LinkedList<string>();
        }

        public void SearchCommand(string CommandName)
        {
            foreach (TerminalCommand cmd in commands)
            {
                if (cmd.CommandName == CommandName)
                {
                    cmd.Execute();
                }
            }
        }

        public void AddCommands()
        {
            commands.Add(new Help());
            commands.Add(new Exit());
        }

        public override void Start()
        {
            AddCommands();
            readerThread = new Thread(new ThreadStart(this.Run));
            running = true;
            Console.WriteLine("*******************************************************************************");
            Console.WriteLine("*                   IrcShark started sucsessfully, have fun!                  *");
            Console.WriteLine("*      Use the \"help\" command to get a list of all available commands         *");
            Console.WriteLine("*******************************************************************************");
            Console.WriteLine();
            readerThread.Start();
        }
        
        private void Run() {
            string command;
            while (running) {
                command = ReadCommand();
            }
        }
        
        public string ReadCommand() 
        {
            Console.Write("-> ");
            StringBuilder line = new StringBuilder();
            while (running) 
            {
                Thread.Sleep(300);
                if (!Console.KeyAvailable)
                    continue;
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.Enter:
                        Console.WriteLine();
                        return line.ToString();
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
                        Console.CursorLeft--;
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
        
        public override void Stop()
        {
            running = true;
            readerThread.Join();
        }
    }
}
