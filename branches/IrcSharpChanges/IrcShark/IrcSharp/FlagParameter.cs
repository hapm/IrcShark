// <copyright file="FlagParameter.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the FlagParameter enum.</summary>

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
namespace IrcSharp
{
    /// <summary>
    /// Describes if a flag has a parameter or not.
    /// </summary>
    public enum FlagParameter
    {
        /// <summary>
        /// None means the flag doesn't have a parameter.
        /// </summary>
        None,
        
        /// <summary>
        /// Optional means the flag didn't need a parameter, but can have one.
        /// </summary>
        Optional,
        
        /// <summary>
        /// Required means that a flag needs a parameter and don't work without one.
        /// </summary>
        Required
    }
}
