// <copyright file="ChannelFilter.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the ChannelFilter class.</summary>

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
namespace IrcShark
{
    using System;
    using System.Xml;
    using System.Xml.Serialization;
    
    /// <summary>
    /// The ChannelFilter class is used by <see cref="LogHandlerSetting" /> to 
    /// define a channel specific filter.
    /// </summary>
    public class ChannelFilter
    {
        /// <summary>
        /// Saves the channel as a string.
        /// </summary>
        private string channelName;
        
        /// <summary>
        /// Saves, if debug messages are filtered or not.
        /// </summary>
        private bool debug;
    
        /// <summary>
        /// Saves, if information messages are filtered or not.
        /// </summary>
        private bool information;
        
        /// <summary>
        /// Saves, if warning messages are filtered or not.
        /// </summary>
        private bool warning;
        
        /// <summary>
        /// Saves, if error messages are filtered or not.
        /// </summary>
        private bool error;
        
        /// <summary>
        /// Holds the LogHandlerSetting, this channel belongs to.
        /// </summary>
        private LogHandlerSetting defaultFilter;
        
        /// <summary>
        /// A boolean value saying if the default values are used or not.
        /// </summary>
        private bool useDefaults;
        
        /// <summary>
        /// Initializes a new instance of the ChannelFilter class.
        /// </summary>
        /// <param name="channel">The name of the channel, this filter applys to.</param>
        /// <param name="defaults">The setting base, this filter belongs to.</param>
        /// <remarks>
        /// The filter is copied from the defaults.
        /// </remarks>
        internal ChannelFilter(string channel, LogHandlerSetting defaults)
        {
            this.channelName = channel;
            this.defaultFilter = defaults;
            this.CopyDefaults();
            this.useDefaults = true;
        }
        
        /// <summary>
        /// Initializes a new instance of the ChannelFilter class.
        /// </summary>
        /// <param name="channel">The name of the channel, this filter applys to.</param>
        /// <param name="defaults">The setting base, this filter belongs to.</param>
        /// <param name="filter">The filter used for this channel.</param>
        internal ChannelFilter(string channel, LogHandlerSetting defaults, string filter)
        {
            channelName = channel;
            defaultFilter = defaults;
            ParseFilter(filter);
        }
        
        /// <summary>
        /// Gets the name of the channel, this filter belongs to.
        /// </summary>
        /// <value>
        /// The name of the channel.
        /// </value>
        public string ChannelName
        {
            get { return channelName; }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether debug messages are logged or not.
        /// </summary>
        /// <value>
        /// The value is true if debug messages are passing this filters, else false.
        /// </value>
        public bool Debug
        {
            get 
            {
                if (useDefaults)
                {
                    return defaultFilter.Debug;
                }
                else
                {
                    return debug; 
                }
            }
            
            set 
            {
                if (useDefaults)
                {
                    CopyDefaults();
                }
                
                debug = value; 
            }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether information messages are logged or not.
        /// </summary>
        /// <value>
        /// The value is true if information messages are passing this filters, else false.
        /// </value>
        public bool Information
        {
            get 
            { 
                if (useDefaults)
                {
                    return defaultFilter.Information;
                }
                else
                {
                    return information; 
                }
            }
            
            set 
            { 
                if (useDefaults)
                {
                    CopyDefaults();
                }
                
                information = value; 
            }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether warning messages are logged or not.
        /// </summary>
        /// <value>
        /// The value is true if warning messages are passing this filters, else false.
        /// </value>
        public bool Warning
        {
            get 
            { 
                if (useDefaults)
                {
                    return defaultFilter.Warning;
                }
                else
                {
                    return warning; 
                }
            }
            
            set
            { 
                if (useDefaults)
                {
                    CopyDefaults();
                }
                warning = value; 
            }
        }
        
        /// <summary>
        /// Gets a value indicating whether the filter uses the handler defaults or not.
        /// </summary>
        /// <value>
        /// The value is true if this channel filter uses the 
        /// default settings of the parent <see cref="LogHandlerSettings" />.
        /// </value>
        public bool UsingDefaults
        {
            get { return useDefaults; }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether error messages are logged or not.
        /// </summary>
        /// <value>
        /// The value is true if error messages are passing this filters, else false.
        /// </value>
        public bool Error
        {
            get 
            { 
                if (useDefaults)
                {
                    return defaultFilter.Error;
                }
                else
                {
                    return error; 
                }
            }
            
            set 
            { 
                if (useDefaults)
                {
                    CopyDefaults();
                }
                error = value; 
            }
        }
        
        /// <summary>
        /// Parses a filter string and set the new values.
        /// <para>
        /// The string can contain the following characters:
        /// 'd' - Debug messages
        /// 'i' - Information messages
        /// 'w' - Warning messages
        /// 'e' - Error messages
        /// </para>
        /// <para>
        /// If a character is in the string, the messages of the given type will pass 
        /// the filter, else it will be dismissed.
        /// </para>
        /// </summary>
        /// <param name="filter">The filter string.</param>
        public void ParseFilter(string filter)
        {
            if (filter == null)
            {
                CopyDefaults();
                useDefaults = true;
            }
            else
            {
                debug = filter.Contains("d");
                information = filter.Contains("i");
                warning = filter.Contains("w");
                error = filter.Contains("e");
            }
        }
        
        /// <summary>
        /// Resets the filter to the defaults used by the associated <see cref="LogHandlerSetting" />.
        /// </summary>
        public void ResetDefaults()
        {
            CopyDefaults();
            useDefaults = true;
        }
        
        /// <summary>
        /// Checks if the given <see cref="LogMessage" /> is filtered by this channel filter setting or not.
        /// </summary>
        /// <param name="msg">The message to check.</param>
        /// <returns>True if the message passes the filters, false otherwise.</returns>
        public bool ApplysTo(LogMessage msg)
        {
            if (msg.Channel != channelName) 
            {
                return false;
            }
            
            switch (msg.Level)
            {
                case LogLevel.Debug:
                    return Debug;
                case LogLevel.Information:
                    return Information;
                case LogLevel.Warning:
                    return Warning;
                case LogLevel.Error:
                    return Error;
                default:
                    return false;
            }
        }
        
        /// <summary>
        /// Copys the default values from the associated settings.
        /// </summary>
        private void CopyDefaults()
        {
            useDefaults = false;
            debug = defaultFilter.Debug;
            information = defaultFilter.Information;
            warning = defaultFilter.Warning;
            error = defaultFilter.Error;            
        }
    }
}
