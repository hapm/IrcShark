using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    public class InfoEndEventArgs : IrcEventArgs
    {
        private IrcLine[] infoLines;

        public InfoEndEventArgs(IrcLine baseLine, IrcLine[] infoLines) : base(baseLine)
        {
            this.infoLines = infoLines;
        }

        public IrcLine[] InfoLines
        {
            get { return infoLines; }
        }
    }
}
