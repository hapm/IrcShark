using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    public class InvalidLineFormatException : Exception
    {
        string line;

        public InvalidLineFormatException(String line) : base(String.Format("Couldn't parse the raw line \"{0}\"", line))
        {
            this.line = line;
        }

        public string Line
        {
            get { return line; }
        }
    }
}
