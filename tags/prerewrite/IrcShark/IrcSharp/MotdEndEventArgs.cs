using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    public class MotdEndEventArgs : IrcEventArgs
    {
        private IrcLine[] motdLines;

        public MotdEndEventArgs(IrcLine baseLine, IrcLine[] motdLines) : base(baseLine)
        {
            motdLines = motdLines;
        }

        public IrcLine[] MotdLines
        {
            get { return motdLines; }
        }
    }
}