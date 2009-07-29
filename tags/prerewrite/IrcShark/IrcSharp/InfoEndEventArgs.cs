using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    public class InfoEndEventArgs : IrcEventArgs
    {
        private IrcLine[] InfoLinesValue;

        public InfoEndEventArgs(IrcLine baseLine, IrcLine[] infoLines) : base(baseLine)
        {
            InfoLinesValue = infoLines;
        }

        public IrcLine[] InfoLines
        {
            get { return InfoLinesValue; }
        }
    }
}
