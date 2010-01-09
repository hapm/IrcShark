namespace IrcSharkTerminal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using IrcShark;
    using IrcShark.Extensions;
    using System.Threading;

    public class TerminalExtension : IrcShark.Extensions.Extension
    {
        private List<TerminalCommand> commands = new List<TerminalCommand>();
        private bool running;
        private Thread readerThread;

        public TerminalExtension(ExtensionContext context)
            : base(context)
        {
        }

        public void SearchCommand(string CommandName)
        {
            foreach (TerminalCommand cmd in commands)
            {
                if (cmd.CommandName == CommandName)
                {
                    cmd.Execute();
                    Console.Write("->");
                    string command = Console.ReadLine();
                    while (true)
                    {
                        Console.Write("->");
                        command = Console.ReadLine();
                        SearchCommand(command);
                    }
                }
            }
            while (true)
            {
                Console.Write("->");
                string command = Console.ReadLine();
                SearchCommand(command);
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
            readerThread = new Thread();
            readerThread.st
            running = true;
            Console.WriteLine("*******************************************************************************");
            Console.WriteLine("*                   IrcShark started sucsessfully, have fun!                  *");
            Console.WriteLine("*      Use the \"help\" command to get a list of all available commands         *");
            Console.WriteLine("*******************************************************************************");
            Console.WriteLine();
            Console.Write("->");
            string command = ReadCommand();
            SearchCommand(command);
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
                        return line.ToString();
                        Console.WriteLine();
                        break;
                    case ConsoleKey.End:
                        //TODO handle moving the cursor to the endline here
                        break;
                    case ConsoleKey.Home:
                        //TODO handle moving the cursor to the beginning here
                        break;
                    case ConsoleKey.LeftArrow:
                        //TODO handle moving the cursor one left
                        break;
                    case ConsoleKey.RightArrow:
                        //TODO handle moving the cursor one right
                        break;
                    case ConsoleKey.UpArrow:
                        //TODO get previous command in history
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
        }
    }
}
