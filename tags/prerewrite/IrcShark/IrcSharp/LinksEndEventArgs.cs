using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    public class LinksEndEventArgs : IrcEventArgs
    {
        private IrcLine[] LinksLinesValue;

        public LinksEndEventArgs(IrcLine baseLine, IrcLine[] linksLines)
            : base(baseLine)
        {
            LinksLinesValue = linksLines;
        }

        public IrcLine[] LinksLines
        {
            get { return LinksLinesValue; }
        }
    }
}
