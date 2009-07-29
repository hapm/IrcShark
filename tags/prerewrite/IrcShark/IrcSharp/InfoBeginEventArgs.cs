using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    public class InfoBeginEventArgs : IrcEventArgs
    {
        public InfoBeginEventArgs(IrcLine baseLine) : base(baseLine)
        {
        }
    }
}
