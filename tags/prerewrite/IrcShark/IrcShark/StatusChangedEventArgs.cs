using System;
using System.Collections.Generic;
using System.Text;
using IrcShark.Extensions;

namespace IrcShark
{
    public class StatusChangedEventArgs : EventArgs
    {
        private ExtensionInfo ExtensionValue;
        private ExtensionStates StatusValue;

        public StatusChangedEventArgs(ExtensionInfo ext, ExtensionStates status)
        {
            ExtensionValue = ext;
            StatusValue = status;
        }

        public ExtensionInfo Extension
        {
            get { return ExtensionValue; }
        }

        public ExtensionStates Status
        {
            get { return StatusValue; }
        }
    }
}
