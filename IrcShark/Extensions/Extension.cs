// <copyright file="Extension.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the Extension class.</summary>

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
namespace IrcShark.Extensions
{
	using System;

	/// <summary>
	/// Classes deriving from this class can be loaded as an Extension in IrcShark.
	/// </summary>
	public abstract class Extension : MarshalByRefObject, IExtension
	{
		/// <summary>
		/// Saves the ExtensionContext belonging to the Extension instance.
		/// </summary>
		private ExtensionContext context;
		
		/// <summary>
		/// Saves the ExtensionAttribute for the current extension after first lookup.
		/// </summary>
		private ExtensionAttribute data;

		/// <summary>
		/// Initializes a new instance of the Extension class.
		/// </summary>
		/// <param name="info">
		/// The <see cref="ExtensionContext"/> for this extension.
		/// </param>
		protected Extension() {
		    
		}

		/// <summary>
		/// Initializes a new instance of the Extension class.
		/// </summary>
		/// <param name="info">
		/// The <see cref="ExtensionContext"/> for this extension.
		/// </param>
		protected Extension(ExtensionContext context)
		{
			if (context == null) {
				throw new ArgumentNullException("context", "You must specify a context");
			}

			if (!context.Info.Trusted) {
				throw new ExtensionException(context.Info, "You can't initialise an extension with an untrusted ExtensionInfo");
			}

			this.context = context;
		}

		/// <summary>
		/// Gets the context of this Extension.
		/// </summary>
		/// <value>The context.</value>
		public ExtensionContext Context {
			get { return context; }
			protected set { context = value; }
		}
		
		/// <summary>
		/// Gets the id of the extension from attributes.
		/// </summary>
		/// <value>The id of the extension.</value>
		public string Id {
		    get 
		    {
		        string result = null;
		        
		        if (data != null) 
		        {
		            result = data.Id;
		        }
		        else 
		        {
    		        foreach (object attrb in GetType().GetCustomAttributes(false))
    		        {
    		            if (attrb is ExtensionAttribute) 
    		            {
    		                data = attrb as ExtensionAttribute;
    		                result = data.Id;
    		                break;
    		            }
    		        }
		        }
		        
		        if (result == null)
		        {
		            result = GetType().Name;
		        }
		        
		        return result;
		    }
		}
		
		/// <summary>
		/// Gets the display name of the extension from attributes.
		/// </summary>
		/// <value>The name of the extension.</value>
		public string Name {
		    get 
		    {
		        if (data != null) 
		            return data.Name;
		        foreach (object attrb in GetType().GetCustomAttributes(false))
		        {
		            if (attrb is ExtensionAttribute) 
		            {
		                data = attrb as ExtensionAttribute;
		                return data.Name;
		            }
		        }
		        return "";
		    }
		}

		/// <summary>
		/// Gets the extension this IExtensionObject belongs to.
		/// </summary>
		/// <value>
		/// The extension instance.
		/// </value>
		public Extension BelongsTo {
			get { return this; }
		}

		/// <summary>
		/// Starts the extension after the initialisation of IrcShark.
		/// </summary>
		public abstract void Start(ExtensionContext context);

		/// <summary>
		/// Stops the extension before IrcShark quits or the extension is unloaded.
		/// </summary>
		public abstract void Stop();
	}
}
