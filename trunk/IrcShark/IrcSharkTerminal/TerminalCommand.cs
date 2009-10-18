using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IrcSharkTerminal
{
    public abstract class TerminalCommand
    {
        public TerminalCommand(string Command)
        {
            commandName = Command;
        }
        private string commandName;
        public string CommandName
        {
            get { return commandName; }
        }

        public abstract void Execute();
    }

    public class Help : TerminalCommand
    {
        public Help()
            : base("help")
        {
        }

        public override void Execute()
        {
            Console.WriteLine("Test Help");
        }
    }

    public class Exit : TerminalCommand
    {
        public Exit()
            : base("exit")
        {

        }
        public override void Execute()
        {
            Console.WriteLine("Test Exit");
        }
    }
}
