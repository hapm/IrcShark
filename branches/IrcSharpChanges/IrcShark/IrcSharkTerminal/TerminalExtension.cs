using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IrcShark;
using IrcShark.Extensions;

namespace IrcSharkTerminal
{
    public class TerminalExtension : IrcShark.Extensions.Extension
    {
        private List<TerminalCommand> commands = new List<TerminalCommand>();

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
            Console.WriteLine("*******************************************************************************");
            Console.WriteLine("*                   IrcShark started sucsessfully, have fun!                  *");
            Console.WriteLine("*      Use the \"help\" command to get a list of all available commands         *");
            Console.WriteLine("*******************************************************************************");
            Console.WriteLine();
            Console.Write("->");
            string command = Console.ReadLine();
            SearchCommand(command);
        }
        
        public override void Stop()
        {
            // TODO Add code to stop Console.ReadLine() here
        }
    }
}
