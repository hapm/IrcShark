using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    public class MotdBeginEventArgs : IrcEventArgs
    {
        public MotdBeginEventArgs(IrcLine baseLine) : base(baseLine)
        {
        }
    }
}