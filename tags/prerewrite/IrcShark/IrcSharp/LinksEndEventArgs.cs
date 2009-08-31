using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    public class LinksEndEventArgs : IrcEventArgs
    {
        private IrcLine[] linksLines;

        public LinksEndEventArgs(IrcLine baseLine, IrcLine[] linksLines)
            : base(baseLine)
        {
            linksLines = linksLines;
        }

        public IrcLine[] LinksLines
        {
            get { return linksLines; }
        }
    }
}
