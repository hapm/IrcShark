using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    public class WhoBeginEventArgs : IrcEventArgs
    {
        public WhoBeginEventArgs(IrcLine baseLine) : base(baseLine)
        {
        }
    }
}
