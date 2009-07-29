using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace IrcSharp
{

    /// <summary>
    /// Represents a raw irc line received over an irc connection.
    /// </summary>
    /// <remarks>Allows to analyze a raw irc line. The raw line is automatically broken down to it signle propertys as described in rfc 1459. They can be easily accessed by the given class propertys.</remarks>
    public class IrcLine : IIrcObject
    {
        private static Regex IrcLineRegEx = new Regex("(?::([^ ]*) )?([^ ]*)((?: [^: ][^ ]*)*)(?: :(.*))?", RegexOptions.Singleline & RegexOptions.Compiled);
        private String PrefixValue;
        private String CommandValue;
        private String[] ParametersValue;
        private IrcClient ClientValue;
        private int NumericValue;

        public IrcLine(IrcClient client, String line)
        {
            String[] normalParams;
            Match result;
            ClientValue = client;
            result = IrcLineRegEx.Match(line);
            if (result.Success)
            {
                PrefixValue = result.Groups[1].Value;
                CommandValue = result.Groups[2].Value;
                normalParams = result.Groups[3].Value.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (result.Groups[4].Success)
                {
                    ParametersValue = new String[normalParams.Length + 1];
                    normalParams.CopyTo(ParametersValue, 0);
                    ParametersValue[ParametersValue.Length - 1] = result.Groups[4].Value;
                }
                else
                {
                    ParametersValue = normalParams;
                }
                Int32.TryParse(CommandValue, out NumericValue);
            }
            else
            {
                throw new InvalidLineFormatException(line);
            }
        }

        public IrcLine(IrcClient client, String prefix, String command, String[] parameters)
        {
            ClientValue = client;
            PrefixValue = prefix;
            CommandValue = command;
            ParametersValue = (String[])parameters.Clone();
            Int32.TryParse(command, out NumericValue);
        }

        public IrcLine(IrcLine source)
        {
            PrefixValue = source.Prefix;
            CommandValue = source.Command;
            ParametersValue = source.Parameters;
            ClientValue = source.Client;
            int.TryParse(Command, out NumericValue);
        }

        /// <summary>
        /// The irc connection, the line was received from.
        /// </summary>
        /// <remarks>Any IrcLine object is associated with the irc connection it was received from. The reference can be used to reply to the command for example.</remarks>
        /// <value>the irc connection</value>
        public IrcClient Client
        {
            get
            {
                return ClientValue;
            }
        }

        /// <summary>
        /// The prefix of the irc line as described in rfc 1459.
        /// </summary>
        /// <value>the prefix</value>
        public String Prefix
        {
            get
            {
                return PrefixValue;
            }
        }

        /// <summary>
        /// The command of this line
        /// </summary>
        /// <remarks>Any irc line has a command or reply code, what is automatically provided by this property.</remarks>
        /// <value>the string representation of the command</value>
        public String Command
        {
            get
            {
                return CommandValue;
            }
        }

        /// <summary>
        /// The parameters given in the irc line.
        /// </summary>
        /// <remarks>The parameters are splitted as described in rfc 1459. The last parameter (trailing) can have spaces in it, if it is prefixed with a ':'.</remarks>
        /// <value>an array of all parameters</value>
        public string[] Parameters
        {
            get
            {
                return (string[])ParametersValue.Clone();
            }
        }

        /// <summary>
        /// Returns if the IrcLine is a numeric line or a normal command line.
        /// </summary>
        /// <value>
        /// true, if the represented line is a numeric line
        /// else false
        /// </value>
        public bool IsNumeric
        {
            get
            {
                return NumericValue > 0;
            }
        }

        /// <summary>
        /// The numeric, if the IrcLine is a numeric reply IrcLine
        /// </summary>
        /// <value>the int value of the numeric</value>
        public int Numeric
        {
            get
            {
                return NumericValue;
            }
        }

        /// <summary>
        /// Recreates the raw irc line as it was read from server.
        /// </summary>
        /// <returns>the string representation of the irc line</returns>
        public override String ToString()
        {
            StringBuilder result = new StringBuilder();
            if (!String.IsNullOrEmpty(Prefix))
            {
                result.Append(":");
                result.Append(Prefix);
                result.Append(" ");
            }
            result.Append(Command);
            if (Parameters.Length > 0) {
                for (int i = 0; i < Parameters.Length; i++)
                {
                    if (i == Parameters.Length - 1 && Parameters[i].IndexOf(' ') > 0)
                    {
                        result.Append(" :");
                        result.Append(Parameters[i]);
                    }
                    else
                    {
                        result.Append(" ");
                        result.Append(Parameters[i]);
                    }
                }
            }
            return result.ToString();
        }
    }
}
