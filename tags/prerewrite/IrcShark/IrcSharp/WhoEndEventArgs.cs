using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    public class WhoEndEventArgs : IrcEventArgs
    {
        private IrcLine[] WhoLinesValue;

        public WhoEndEventArgs(IrcLine baseLine, IrcLine[] WhoLines) : base(baseLine)
        {
            WhoLinesValue = WhoLines;
        }

        public IrcLine[] WhoLines
        {
            get { return WhoLinesValue; }
        }
    }
}