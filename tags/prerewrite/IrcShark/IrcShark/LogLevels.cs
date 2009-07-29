using System;

namespace IrcShark
{
    /// <summary>
    /// Types of lines, what can be logged.
    /// </summary>
    [Flags]
    public enum LogLevels
    {
        /// <summary>
        /// Unimportant informations.
        /// </summary>
        Verbose = 1,
        /// <summary>
        /// Something unexpacted appeared. Needs a lookover if it is correct.
        /// </summary>
        Warning = 2,
        /// <summary>
        /// An error occured.
        /// </summary>
        Error = 4,
        /// <summary>
        /// A mix of Warning and Error
        /// </summary>
        WarningAndError = 6,
        All = 7
    }
}