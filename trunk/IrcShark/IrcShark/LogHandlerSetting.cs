/*
 * Erstellt mit SharpDevelop.
 * Benutzer: markus
 * Datum: 07.09.2009
 * Zeit: 21:56
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
using System;
using System.Text;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;

namespace IrcShark
{
	/// <summary>
	/// Saves the settings for a given log handler
	/// </summary>
	[XmlRoot("loghandler")]
	public class LogHandlerSetting : IEnumerable<ChannelFilter>, IXmlSerializable
	{
		private string name;
		private string target;
		private bool debug;
		private bool information;
		private bool warning;
		private bool error;
		private List<ChannelFilter> filters;
		
		/// <summary>
		/// Initializes a new setting for the given log handler name
		/// </summary>
		/// <param name="name">the name of the log handler</param>
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
		/// Initializes a new setting for the given log handler name
		/// </summary>
		/// <param name="name">the name of the log handler</param>
		/// <param name="filter">the filter to apply</param>
		public LogHandlerSetting(string name, string filter)
		{
			this.name = name;
			filters = new List<ChannelFilter>();
			ParseFilter(filter);
		}
		
		private void ParseFilter(string filter)
		{
			debug = filter.Contains("d");
			information = filter.Contains("i");
			warning = filter.Contains("w");
			error = filter.Contains("e");
		}
		
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
		/// Gets the name of the handler, what uses this LogHandlerSetting
		/// </summary>
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
		/// <remarks>
		/// The meaning of the target is handler dependent and can include
		/// placeholder replaced when the handler is initialized
		/// </remarks>
		public string Target
		{
			get { return target; }
			set { target = value; }
		}
		
		/// <summary>
		/// Gets or sets if debug messages are logged or not.
		/// </summary>
		public bool Debug
		{
			get { return debug; }
			set { debug = value; }
		}
		
		/// <summary>
		/// Gets or sets if information messages are logged or not.
		/// </summary>
		public bool Information
		{
			get { return information; }
			set { information = value; }
		}
		
		/// <summary>
		/// Gets or sets if warning messages are logged or not.
		/// </summary>
		public bool Warning
		{
			get { return warning; }
			set { warning = value; }
		}
		
		/// <summary>
		/// Gets or sets if error messages are logged or not.
		/// </summary>
		public bool Error
		{
			get { return error; }
			set { error = value; }
		}
		
		/// <summary>
		/// Gets the number of <see cref="ChannelFilter" /> added to the log handler.
		/// </summary>
		public int Count
		{
			get { return filters.Count; }
		}
		
		public ChannelFilter Add(string channelName)
		{
			if (Contains(channelName))
				throw new ArgumentException("channelName", String.Format("The channel {0} was already added", channelName));
			ChannelFilter filter = new ChannelFilter(channelName, this);
			filters.Add(filter);
			return filter;
		}
		
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
		
		public bool Contains(string channelName)
		{
			foreach (ChannelFilter filter in filters)
			{
				if (filter.ChannelName == channelName)
					return true;
			}
			return false;
		}
		
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
		
		public IEnumerator<ChannelFilter> GetEnumerator()
		{
			return filters.GetEnumerator();
		}
		
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return filters.GetEnumerator();
		}
		
		public System.Xml.Schema.XmlSchema GetSchema()
		{
			throw new NotImplementedException();
		}
		
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
				foreach(ChannelFilter f in filters)
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
	}
}
