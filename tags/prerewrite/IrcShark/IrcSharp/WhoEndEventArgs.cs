using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    public class WhoEndEventArgs : IrcEventArgs
    {
        private IrcLine[] whoLines;

        public WhoEndEventArgs(IrcLine baseLine, IrcLine[] WhoLines) : base(baseLine)
        {
            whoLines = WhoLines;
        }

        public IrcLine[] WhoLines
        {
            get { return whoLines; }
        }
    }
}