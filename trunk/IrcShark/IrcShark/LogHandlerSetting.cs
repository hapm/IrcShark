/*
 * Erstellt mit SharpDevelop.
 * Benutzer: markus
 * Datum: 07.09.2009
 * Zeit: 21:56
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
namespace IrcShark
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// Saves the settings for a given log handler.
    /// </summary>
    [XmlRoot("loghandler")]
    public class LogHandlerSetting : IEnumerable<ChannelFilter>, IXmlSerializable
    {
        /// <summary>
        /// Saves the name of the log handler, this settings belong to.
        /// </summary>
        private string name;
        
        /// <summary>
        /// Saves the target for the log handler.
        /// </summary>
        private string target;
        
        /// <summary>
        /// Saves if debug messages are logged.
        /// </summary>
        private bool debug;
        
        /// <summary>
        /// Saves if information messages are logged.
        /// </summary>
        private bool information;
        
        /// <summary>
        /// Saves if warning messages are logged.
        /// </summary>
        private bool warning;
        
        /// <summary>
        /// Saves if error messages are logged.
        /// </summary>
        private bool error;
        
        /// <summary>
        /// Saves a list of channel specific filters.
        /// </summary>
        private List<ChannelFilter> filters;
        
        /// <summary>
        /// Initializes a new instance of the LogHandlerSetting class for the given log handler name.
        /// </summary>
        /// <param name="name">The name of the log handler.</param>
        public LogHandlerSetting(string name)
        {
            this.name = name;
            filters = new List<ChannelFilter>();
            debug = true;
            information = true;
            warning = true;
            error = true;
        }
        
        /// <summary>
        /// Initializes a new instance of the LogHandlerSetting class for the given log handler name.
        /// </summary>
        /// <param name="name">The name of the log handler.</param>
        /// <param name="filter">The filter to apply.</param>
        public LogHandlerSetting(string name, string filter)
        {
            this.name = name;
            filters = new List<ChannelFilter>();
            ParseFilter(filter);
        }
        
        /// <summary>
        /// Gets the name of the handler, what uses this LogHandlerSetting.
        /// </summary>
        /// <value>The name of the handler.</value>
        /// <remarks>
        /// The name of the handler is an independant free selectable string.
        /// </remarks>
        public string HandlerName
        {
            get { return name; }
        }
        
        /// <summary>
        /// Gets or sets the target configured for this handler setting.
        /// </summary>
        /// <value>The target of the log handler.</value>
        /// <remarks>
        /// The meaning of the target is handler dependent and can include
        /// placeholder replaced when the handler is initialized.
        /// </remarks>
        public string Target
        {
            get { return target; }
            set { target = value; }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether debug messages are logged or not.
        /// </summary>
        public bool Debug
        {
            get { return debug; }
            set { debug = value; }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether information messages are logged or not.
        /// </summary>
        public bool Information
        {
            get { return information; }
            set { information = value; }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether warning messages are logged or not.
        /// </summary>
        public bool Warning
        {
            get { return warning; }
            set { warning = value; }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether error messages are logged or not.
        /// </summary>
        public bool Error
        {
            get { return error; }
            set { error = value; }
        }
        
        /// <summary>
        /// Gets the number of <see cref="ChannelFilter" /> added to the log handler.
        /// </summary>
        /// <value>The number of ChannelFilters.</value>
        public int Count
        {
            get { return filters.Count; }
        }
        
        /// <summary>
        /// Gets a ChannelFilter for the given channel name, if the name was added to this setting.
        /// </summary>
        /// <param name="channel">The name of the channel to get the ChannelFilter for.</param>
        /// <value>The ChannelFilter for the given channel name.</value>
        public ChannelFilter this[string channel]
        {
            get 
            {
                foreach (ChannelFilter filter in filters)
                {
                    if (filter.ChannelName == channel)
                        return filter;
                }
                throw new ArgumentOutOfRangeException("channel", String.Format("{0} wasn't added to this setting", channel));
            }
        }
        
        /// <summary>
        /// Adds the given channel as a filter to the LogHandlerSetting.
        /// </summary>
        /// <param name="channelName">The name of the channel to add.</param>
        /// <returns>The <see cref="ChannelFilter" /> for the given channel name.</returns>
        public ChannelFilter Add(string channelName)
        {
            if (Contains(channelName))
                throw new ArgumentException("channelName", String.Format("The channel {0} was already added", channelName));
            ChannelFilter filter = new ChannelFilter(channelName, this);
            filters.Add(filter);
            return filter;
        }
        
        /// <summary>
        /// Removes the <see cref="ChannelFilter" /> for the channel with the given name.
        /// </summary>
        /// <param name="channelName">The name of the <see cref="ChannelFilter" /> to remove.</param>
        public void Remove(string channelName)
        {
            foreach (ChannelFilter filter in filters)
            {
                if (filter.ChannelName == channelName)
                {
                    filters.Remove(filter);
                    return;
                }
            }
            throw new ArgumentException("channelName", String.Format("The channel {0} wasn't added", channelName));
        }
        
        /// <summary>
        /// Checks if there is a <see cref="ChannelFilter" /> for the given channel name.
        /// </summary>
        /// <param name="channelName">The name of the channel to check.</param>
        /// <returns>True if there is a <see cref="ChannelFilter" /> for the given name, else false.</returns>
        public bool Contains(string channelName)
        {
            foreach (ChannelFilter filter in filters)
            {
                if (filter.ChannelName == channelName)
                    return true;
            }
            return false;
        }
        
        /// <summary>
        /// Checks if the given <see cref="LogMessage" /> is filtered by this log handler setting or not.
        /// </summary>
        /// <param name="msg">The message to check.</param>
        /// <returns>True if the message passes the filters, false otherwise.</returns>
        /// <remarks>
        /// When implementing a log handler, you can use this method to check if the given message
        /// should be logged or not. You are free to implement your own check, if you want to have
        /// some special behaviour.
        /// </remarks>
        public bool ApplysTo(LogMessage msg)
        {
            if (Count == 0)
            {
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
                }
            }
            else
            {
                foreach (ChannelFilter filter in filters)
                {
                    if (filter.ChannelName == msg.Channel)
                        return filter.ApplysTo(msg);
                }
            }
            return false;
        }
        
        /// <summary>
        /// Gets an enumerator throw all ChannelFilter instances in this LogHandlerSetting.
        /// </summary>
        /// <returns>The generic enumerator.</returns>
        public IEnumerator<ChannelFilter> GetEnumerator()
        {
            return filters.GetEnumerator();
        }
        
        /// <summary>
        /// Gets an enumerator of all ChannelFilter instances in this LogHandlerSetting.
        /// </summary>
        /// <returns>The enumerator.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return filters.GetEnumerator();
        }
        
        /// <summary>
        /// Returns the schema for the xml representation of a LogHandlerSetting.
        /// </summary>
        /// <returns>
        /// A <see cref="XmlSchema"/>.
        /// </returns>
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Reads the instance settings from a XmlReader.
        /// </summary>
        /// <param name="reader">The XmlReader to read from.</param>
        public void ReadXml(XmlReader reader)
        {
            string tempFilter;
            name = reader.GetAttribute("name");
            target = reader.GetAttribute("target");
            tempFilter = reader.GetAttribute("filter");
            if (tempFilter == null) 
                tempFilter = "diwe";
            ParseFilter(tempFilter);
            if (reader.IsEmptyElement)
            {
                reader.Read();
                return;
            }
            reader.Read();
            while (true)
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.Name == "channel")
                            ReadChannelFilter(reader);
                        else
                            reader.Skip();
                        break;
                    case XmlNodeType.EndElement:
                        reader.Read();
                        return;
                    default:
                        if (!reader.Read())
                            return;
                        break;
                }
            }
        }
        
        /// <summary>
        /// Writes the current instance settings to a XmlWriter.
        /// </summary>
        /// <param name="writer">The writer to write to.</param>
        public void WriteXml(XmlWriter writer)
        {
            StringBuilder filter = new StringBuilder();
            writer.WriteAttributeString("name", HandlerName);
            if (Debug)
                filter.Append('d');
            if (Information)
                filter.Append('i');
            if (Warning)
                filter.Append('w');
            if (Error)
                filter.Append('e');
            if (filter.Length < 4)
                writer.WriteAttributeString("filter", filter.ToString());
            if (Target != null)
                writer.WriteAttributeString("target", Target);
            if (Count != 0)
            {
                foreach (ChannelFilter f in filters)
                {
                    writer.WriteStartElement("channel");
                    writer.WriteAttributeString("name", f.ChannelName);
                    if (!f.UsingDefaults)
                    {
                        filter = new StringBuilder();
                        if (Debug)
                            filter.Append('d');
                        if (Information)
                            filter.Append('i');
                        if (Warning)
                            filter.Append('w');
                        if (Error)
                            filter.Append('e');
                        writer.WriteAttributeString("filter", filter.ToString());
                    }
                    writer.WriteEndElement();
                }
            }
        }
        
        /// <summary>
        /// Parses a string as a filter.
        /// </summary>
        /// <param name="filter">The string to parse.</param>
        /// <remarks>
        /// <para>
        /// the string is searched for the following characters. If the given char exists,
        /// the associated option is set to true, else to false:
        /// 'd' - Debug
        /// 'i' - Information
        /// 'w' - Warning
        /// 'e' - Error
        /// </para>
        /// <para>
        /// If a character is in the string, the messages of the given type will pass 
        /// the filter, else it will be dismissed.
        /// </para>
        /// </remarks>
        private void ParseFilter(string filter)
        {
            debug = filter.Contains("d");
            information = filter.Contains("i");
            warning = filter.Contains("w");
            error = filter.Contains("e");
        }
        
        /// <summary>
        /// Reads a <see cref="ChannelFilter" /> from a XmlReader.
        /// </summary>
        /// <param name="reader">The XmlReader to read from.</param>
        private void ReadChannelFilter(XmlReader reader)
        {
            string cname = reader.GetAttribute("name");
            string filter = reader.GetAttribute("filter");
            Add(cname).ParseFilter(filter);
            if (reader.IsEmptyElement)
            {
                reader.Read();
                return;
            }
            reader.Read();
            while (true)
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        reader.Skip();
                        break;
                    case XmlNodeType.EndElement:
                        reader.Read();
                        return;
                }
            }
        }
    }
}
