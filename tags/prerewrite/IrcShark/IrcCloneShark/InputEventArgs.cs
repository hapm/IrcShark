using System;
using System.Collections.Generic;
using System.Text;

namespace IrcCloneShark
{
    public class InputEventArgs : EventArgs
    {
        private String LineValue;

        public InputEventArgs(String line)
        {
            LineValue = line;
        }

        public String Line
        {
            get { return LineValue; }
        }
    }
}
