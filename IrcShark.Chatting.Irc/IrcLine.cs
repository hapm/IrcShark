// <copyright file="IrcLine.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the IrcLine class.</summary>

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
namespace IrcShark.Chatting.Irc
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Represents a raw irc line received over an irc connection.
    /// </summary>
    /// <remarks>
    /// Allows to analyze a raw irc line. The raw line is automatically broken down to it single propertys according to rfc 1459. 
    /// They can be easily accessed by the given class propertys.
    /// </remarks>
    public class IrcLine : IIrcObject
    {
        /// <summary>
        /// Saves the regular expression for parsing an irc line based on the irc rfc 1459.
        /// </summary>
        private static Regex ircLineRegEx = new Regex("(?::([^ ]*) )?([^ ]+)((?: [^: ][^ ]*)*)(?: :(.*))?", RegexOptions.Singleline & RegexOptions.Compiled);
        
        /// <summary>
        /// Saves the client instance, this line was received from.
        /// </summary>
        private IrcClient client;
        
        /// <summary>
        /// Saves the prefix of the line.
        /// </summary>
        private string prefix;
        
        /// <summary>
        /// Saves the command of the line.
        /// </summary>
        private string command;
        
        /// <summary>
        /// Saves the numeric if the line is a numeric line.
        /// </summary>
        private int numeric;
        
        /// <summary>
        /// Saves all parameters of the line.
        /// </summary>
        private string[] parameters;
        
        /// <summary>
        /// Initializes a new instance of the IrcLine class from the the given raw string.
        /// </summary>
        /// <param name="client">
        /// The <see cref="IrcClient"/>, this line was received by.
        /// </param>
        /// <param name="line">
        /// The raw line as a <see cref="System.String"/>.
        /// </param>
        /// <exception cref="InvalidLineFormatException">If the format of the raw string can't be parsed as an irc line, an InvalidLineFormatException is thrown.</exception>
        public IrcLine(IrcClient client, string line)
        {
            this.client = client;
            string[] normalParams;
            Match m = ircLineRegEx.Match(line);
            if (m.Success) 
            {
                if (m.Groups[1].Success)
                    prefix = m.Groups[1].Value;
                command = m.Groups[2].Value;
                if (!Int32.TryParse(command, out numeric))
                    numeric = 0;
                normalParams = m.Groups[3].Value.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (m.Groups[4].Success)
                {
                    parameters = new string[normalParams.Length + 1];
                    normalParams.CopyTo(parameters, 0);
                    parameters[parameters.Length - 1] = m.Groups[4].Value;
                }
                else if (normalParams.Length != 0)
                {
                    parameters = normalParams;
                }
            }
            else 
                throw new InvalidLineFormatException(line);
        }

        /// <summary>
        /// Initializes a new instance of the IrcLine class based on the parameters given.
        /// </summary>
        /// <remarks>
        /// The parameters array can only have one parameter with spaces in it, and this one need to be the last one!
        /// </remarks>
        /// <exception cref="InvalidLineFormatException">
        /// This exception is thrown if prefix, command or anyone of the parameters 
        /// excepting the last, have a space in them.
        /// </exception>
        /// <param name="client">
        /// The <see cref="IrcClient"/> this line belongs to.
        /// </param>
        /// <param name="prefix">
        /// The prefix of the line.
        /// </param>
        /// <param name="command">
        /// The command of the line.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters.
        /// </param>
        public IrcLine(IrcClient client, string prefix, string command, string[] parameters)
        {
            this.client = client;
            if (prefix != null && prefix.Contains(" "))
                throw new InvalidLineFormatException("prefix should not have spaces", prefix);
            this.prefix = prefix;
            if (command.Contains(" "))
                throw new InvalidLineFormatException("command should not have spaces", command);
            this.command = command;
            if (parameters != null)
            {
                for (int i = 0; i < parameters.Length - 1; i++)
                {
                    if (parameters[i].Contains(" "))
                        throw new InvalidLineFormatException("only the last parameter should have spaces", parameters[i]);
                }
                this.parameters = (string[])parameters.Clone();
            }
            if (!Int32.TryParse(command, out numeric))
                numeric = 0;
        }

        /// <summary>
        /// Initializes a new instance of the IrcLine class as a copy of another IrcLine object.
        /// </summary>
        /// <param name="source">
        /// The <see cref="IrcLine"/> to copy.
        /// </param>
        public IrcLine(IrcLine source)
        {
            prefix = source.Prefix;
            command = source.Command;
            parameters = source.Parameters;
            client = source.Client;
            numeric = source.Numeric;
        }
        
        /// <summary>
        /// Gets the prefix of the irc line as described in rfc 1459.
        /// </summary>
        /// <value>The prefix.</value>
        public string Prefix
        {
            get { return prefix; }
        }

        /// <summary>
        /// Gets the command of this line.
        /// </summary>
        /// <remarks>Any irc line has a command or reply code, what is automatically provided by this property.</remarks>
        /// <value>The string representation of the command.</value>
        public string Command
        {
            get { return command; }
        }

        /// <summary>
        /// Gets the numeric, if the IrcLine is a numeric reply IrcLine.
        /// </summary>
        /// <value>The int value of the numeric.</value>
        public int Numeric
        {
            get { return numeric; }
        }

        /// <summary>
        /// Gets a value indicating whether the IrcLine is a numeric line or a normal command line.
        /// </summary>
        /// <value>
        /// True, if the represented line is a numeric line, else false.
        /// </value>
        public bool IsNumeric
        {
            get { return numeric > 0; }
        }
        
        /// <summary>
        /// Gets the parameters given in the irc line.
        /// </summary>
        /// <remarks>The parameters are splitted as described in rfc 1459. The last parameter (trailing) can have spaces in it, if it is prefixed with a ':'.</remarks>
        /// <value>An array of all parameters.</value>
        public string[] Parameters
        {
            get
            {
                if (parameters == null)
                    return null;
                return (string[])parameters.Clone();
            }
        }

        #region IIrcObject implementation
        /// <summary>
        /// Gets the irc connection, the line was received from.
        /// </summary>
        /// <remarks>Any IrcLine object is associated with the irc connection it was received from. The reference can be used to reply to the command for example.</remarks>
        /// <value>The irc connection.</value>
        public IrcClient Client 
        {
            get { return client; }
        }
        #endregion

        /// <summary>
        /// Recreates the raw irc line as it was read from server.
        /// </summary>
        /// <returns>The string representation of the irc line.</returns>
        public override string ToString()
        {
            System.Text.StringBuilder result = new System.Text.StringBuilder();
            if (!String.IsNullOrEmpty(prefix))
            {
                result.Append(":");
                result.Append(prefix);
                result.Append(" ");
            }
            result.Append(command);
            if (parameters != null && parameters.Length > 0) 
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    if (i == parameters.Length - 1 && parameters[i].IndexOf(' ') > 0)
                    {
                        result.Append(" :");
                        result.Append(parameters[i]);
                    }
                    else
                    {
                        result.Append(" ");
                        result.Append(parameters[i]);
                    }
                }
            }
            return result.ToString();
        }
        
        /// <summary>
        /// Compares this object with another one.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="System.Object"/> to compare with.
        /// </param>
        /// <returns>
        /// True if they are equals, false otherwise.
        /// </returns>
        public override bool Equals(object obj)
        {
            IrcLine line = obj as IrcLine;
            if (line == null)
                return base.Equals(obj);
            else 
                return ToString().Equals(line.ToString());
        }
        
        /// <summary>
        /// Returns a hashcode of this object.
        /// </summary>
        /// <returns>
        /// The hashcode as a <see cref="System.Int32"/>.
        /// </returns>
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
