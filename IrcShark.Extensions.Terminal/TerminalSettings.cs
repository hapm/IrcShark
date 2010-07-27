// <copyright file="TerminalSettings.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the TerminalSettings class.</summary>

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
namespace IrcShark.Extensions.Terminal
{
    using System;
    using System.Xml.Serialization;
    
    /// <summary>
    /// The TerminalSettings class holds all settings for the terminal extension.
    /// </summary>
    [XmlRoot("terminal")]
    public class TerminalSettings
    {
        /// <summary>
        /// Initializes a new instance of the TerminalSettings class.
        /// </summary>
        public TerminalSettings()
        {
        }
        
        /// <summary>
        /// Saves if the TerminalExtension should save the history of typed commands.
        /// </summary>
        private bool saveHistory;
        
        /// <summary>
        /// Saves the command history for the current session.
        /// </summary>
        private string[] commandHistory;
        
        /// <summary>
        /// Gets or sets a value indicating whether the history of typed commands is 
        /// saved after the user session is closed.
        /// </summary>
        [XmlElement("saveHistory")]
        public bool SaveHistory {
            get { 
                return saveHistory; 
            }
            set { 
                saveHistory = value; 
                if (!saveHistory) {
                    commandHistory = null;
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the command history that will be saved if SaveHistory is true.
        /// </summary>
        [XmlElement("history")]
        public string[] CommandHistory {
            get { 
                return commandHistory; 
            }
            set { 
                if (saveHistory) {
                    commandHistory = value; 
                }
            }
        }
    }
}
