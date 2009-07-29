using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    public class NamesBeginEventArgs : IrcEventArgs
    {
        public NamesBeginEventArgs(IrcLine baseLine)
            : base(baseLine)
        {
        }
    }
}
