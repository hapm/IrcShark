// $Id$
// 
// Note:
// 
// Copyright (C) 2009 Full Name
//  
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

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
        private static Regex ircLineRegEx = new Regex("(?::([^ ]*) )?([^ ]*)((?: [^: ][^ ]*)*)(?: :(.*))?", RegexOptions.Singleline & RegexOptions.Compiled);
        private String prefix;
        private String command;
        private String[] parameters;
        private IrcClient client;
        private int numeric;

        public IrcLine(IrcClient client, String line)
        {
            String[] normalParams;
            Match result;
            this.client = client;
            result = ircLineRegEx.Match(line);
            
            if (result.Success)
            {
                prefix = result.Groups[1].Value;
                command = result.Groups[2].Value;
                normalParams = result.Groups[3].Value.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (result.Groups[4].Success)
                {
                    parameters = new String[normalParams.Length + 1];
                    normalParams.CopyTo(parameters, 0);
                    parameters[parameters.Length - 1] = result.Groups[4].Value;
                }
                else
                {
                    parameters = normalParams;
                }
                Int32.TryParse(command, out numeric);
            }
            else
            {
                throw new InvalidLineFormatException(line);
            }
        }

        public IrcLine(IrcClient client, String prefix, String command, String[] parameters)
        {
            this.client = client;
            this.prefix = prefix;
            this.command = command;
            parameters = (String[])parameters.Clone();
            Int32.TryParse(command, out numeric);
        }

        public IrcLine(IrcLine source)
        {
            prefix = source.Prefix;
            command = source.Command;
            parameters = source.Parameters;
            client = source.Client;
            int.TryParse(Command, out numeric);
        }

        /// <summary>
        /// The irc connection, the line was received from.
        /// </summary>
        /// <remarks>Any IrcLine object is associated with the irc connection it was received from. The reference can be used to reply to the command for example.</remarks>
        /// <value>the irc connection</value>
        public IrcClient Client
        {
            get { return client; }
        }

        /// <summary>
        /// The prefix of the irc line as described in rfc 1459.
        /// </summary>
        /// <value>the prefix</value>
        public String Prefix
        {
            get { return prefix; }
        }

        /// <summary>
        /// The command of this line
        /// </summary>
        /// <remarks>Any irc line has a command or reply code, what is automatically provided by this property.</remarks>
        /// <value>the string representation of the command</value>
        public String Command
        {
            get { return command; }
        }

        /// <summary>
        /// The parameters given in the irc line.
        /// </summary>
        /// <remarks>The parameters are splitted as described in rfc 1459. The last parameter (trailing) can have spaces in it, if it is prefixed with a ':'.</remarks>
        /// <value>an array of all parameters</value>
        public string[] Parameters
        {
            get { return (string[])parameters.Clone(); }
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
            get { return numeric > 0; }
        }

        /// <summary>
        /// The numeric, if the IrcLine is a numeric reply IrcLine
        /// </summary>
        /// <value>the int value of the numeric</value>
        public int Numeric
        {
            get { return numeric; }
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
