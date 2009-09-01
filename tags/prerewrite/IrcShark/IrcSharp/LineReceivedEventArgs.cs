using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    /// <summary>
    /// Arguments for the LineReceived event
    /// </summary>
    public class LineReceivedEventArgs : IrcEventArgs, IIrcObject
    {
        private IrcLine line;

        public LineReceivedEventArgs(IrcLine line) : base(line)
        {
            this.line = line;
        }

        /// <summary>
        /// The Line received from server.
        /// </summary>
        /// <value>the parsed IrcLine</value>
        public IrcLine Line
        {
            get { return line; }
        }
    }
}
