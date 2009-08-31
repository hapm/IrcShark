using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    public class NumericReceivedEventArgs : IrcEventArgs
    {
        public NumericReceivedEventArgs(IrcLine baseLine) : base(baseLine)
        {

        }

        public int Numeric
        {
            get { return BaseLine.Numeric; }
        }
    }
}
