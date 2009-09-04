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

namespace IrcSharp
{
    /// <summary>
    /// By implementing the IIrcObject, all objects of the class need to be "bound" to an irc connection.
    /// </summary>
    /// <remarks>There are many objects belonging to an irc connection. All these objects should implement this interface, so u can always get the associated connection from them. You can get it over the IrcClient property.</remarks>
    public interface IIrcObject
    {
        /// <summary>
        /// Returns the associated irc connection.
        /// </summary>
        /// <value>the associated IrcClient</value>
        IrcClient Client
        {
            get;
        }
    }
}
