/*
 * Erstellt mit SharpDevelop.
 * Benutzer: markus
 * Datum: 08.09.2009
 * Zeit: 21:51
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
using System;
using System.Xml;
using System.Xml.Serialization;

namespace IrcShark
{
	/// <summary>
	/// The ChannelFilter class is used by <see cref="LogHandlerSetting" /> to 
	/// define a channel specific filter
	/// </summary>
	public class ChannelFilter
	{
		/// <summary>
		/// saves the channel as a string
		/// </summary>
		private string channelName;
		
		/// <summary>
		/// saves, if debug messages are filtered or not
		/// </summary>
		private bool debug;
		
		/// <summary>
		/// saves, if information messages are filtered or not
		/// </summary>
		private bool information;
		
		/// <summary>
		/// saves, if warning messages are filtered or not
		/// </summary>
		private bool warning;
		
		/// <summary>
		/// saves, if error messages are filtered or not
		/// </summary>
		private bool error;
		
		/// <summary>
		/// holds the LogHandlerSetting, this channel belongs to
		/// </summary>
		private LogHandlerSetting defaultFilter;
		
		/// <summary>
		/// a boolean value saying if the default values are used or not
		/// </summary>
		private bool useDefaults;
		
		/// <summary>
		/// Constructor for creating a new ChannelFilter instance
		/// </summary>
		/// <param name="channel">the name of the channel, this filter applys to</param>
		/// <param name="defaults">the setting base, this filter belongs to</param>
		/// <remarks>
		/// The filter is copied from the defaults
		/// </remarks>
		internal ChannelFilter(string channel, LogHandlerSetting defaults)
		{
			channelName = channel;
			defaultFilter = defaults;
			CopyDefaults();
			useDefaults = true;
		}
		
		/// <summary>
		/// Constructor for creating a new ChannelFilter instance
		/// </summary>
		/// <param name="channel">the name of the channel, this filter applys to</param>
		/// <param name="defaults">the setting base, this filter belongs to</param>
		/// <param name="filter">the filter used for this channel</param>
		internal ChannelFilter(string channel, LogHandlerSetting defaults, string filter)
		{
			channelName = channel;
			defaultFilter = defaults;
			useDefaults = false;
			ParseFilter(filter);
		}
		
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
		/// Resets the filter to the defaults used by the associated <see cref="LogHandlerSetting" />
		/// </summary>
		public void ResetDefaults()
		{
			CopyDefaults();
			useDefaults = true;
		}
		
		/// <summary>
		/// Copys the default values from the associated settings
		/// </summary>
		private void CopyDefaults()
		{
			useDefaults = false;
			debug = defaultFilter.Debug;
			information = defaultFilter.Information;
			warning = defaultFilter.Warning;
			error = defaultFilter.Error;			
		}
		
		/// <summary>
		/// Gets the name of the channel, this filter belongs to.
		/// </summary>
		public string ChannelName
		{
			get { return channelName; }
		}
		
		/// <summary>
		/// Gets or sets if debug messages are logged or not.
		/// </summary>
		/// <remarks>
		/// By setting Debug to true all debug messages of the channel are filtered.
		/// </remarks>
		public bool Debug
		{
			get 
			{
				if (useDefaults)
					return defaultFilter.Debug;
				else
					return debug; 
			}
			set 
			{
				if (useDefaults)
					CopyDefaults();
				debug = value; 
			}
		}
		
		/// <summary>
		/// Gets or sets if information messages are logged or not.
		/// </summary>
		/// <remarks>
		/// By setting Information to true all information messages of the channel are filtered.
		/// </remarks>
		public bool Information
		{
			get 
			{ 
				if (useDefaults)
					return defaultFilter.Information;
				else
					return information; 
			}
			set 
			{ 
				if (useDefaults)
					CopyDefaults();
				information = value; 
			}
		}
		
		/// <summary>
		/// Gets or sets if warning messages are logged or not.
		/// </summary>
		/// <remarks>
		/// By setting Warning to true all warning messages of the channel are filtered.
		/// </remarks>
		public bool Warning
		{
			get 
			{ 
				if (useDefaults)
					return defaultFilter.Warning;
				else
					return warning; 
			}
			set
			{ 
				if (useDefaults)
					CopyDefaults();
				warning = value; 
			}
		}
		
		public bool UsingDefaults
		{
			get { return useDefaults; }
		}
		
		/// <summary>
		/// Gets or sets if error messages are logged or not.
		/// </summary>
		/// <remarks>
		/// By setting Error to true all error messages of the channel are filtered.
		/// </remarks>
		public bool Error
		{
			get 
			{ 
				if (useDefaults)
					return defaultFilter.Error;
				else
					return error; 
			}
			set 
			{ 
				if (useDefaults)
					CopyDefaults();
				error = value; 
			}
		}
		
		public bool ApplysTo(LogMessage msg)
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
				default:
					return false;
			}
		}
	}
}
