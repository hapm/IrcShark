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
        public TerminalExtension(IrcSharkApplication app, ExtensionInfo info)
            : base(app, info)
        {
        }
        
        public override void StartTerminal()
        {
            Console.ReadKey();
        }
    }
}
