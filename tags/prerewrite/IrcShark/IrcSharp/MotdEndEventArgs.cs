using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    public class MotdEndEventArgs : IrcEventArgs
    {
        private IrcLine[] MotdLinesValue;

        public MotdEndEventArgs(IrcLine baseLine, IrcLine[] motdLines) : base(baseLine)
        {
            MotdLinesValue = motdLines;
        }

        public IrcLine[] MotdLines
        {
            get { return MotdLinesValue; }
        }
    }
}