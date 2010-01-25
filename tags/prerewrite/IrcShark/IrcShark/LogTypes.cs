using System;

namespace IrcShark
{
    /// <summary>
    /// Types of lines, what can be logged.
    /// </summary>
    [Flags]
    public enum LogLevels
    {
        Verbose = 1,
        Warning = 2,
        Error = 4,
        WarningAndError = 6,
        All = 7
    }
}