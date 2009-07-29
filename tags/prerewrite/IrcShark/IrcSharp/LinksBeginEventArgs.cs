using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    public class LinksBeginEventArgs : IrcEventArgs
    {
        public LinksBeginEventArgs(IrcLine baseLine) : base(baseLine)
        {
        }
    }
}
