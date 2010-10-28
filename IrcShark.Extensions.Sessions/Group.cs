// <copyright file="Class1.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Place a summary here.</summary>

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
namespace IrcShark.Extensions.Sessions
{
    using System;
    
    /// <summary>
    /// The GroupType specifies the type of a Group instance.
    /// </summary>
    public enum GroupType
    {
        /// <summary>
        /// There are some predefined groups. All of them have the Special type, and can't
        /// be deleted from the group list of the SessionManager.
        /// </summary>
        Special,
        
        /// <summary>
        /// Extension groups are groups registered by an extension, and can not be deleted
        /// from the group list of the SessionManager as long as it isn't unregistered from
        /// the extension.
        /// </summary>
        Extension,
        
        /// <summary>
        /// Custom groups are groups defined by the user.
        /// </summary>
        Custom
    }

    /// <summary>
    /// The Group class represents a group of users.
    /// </summary>
    public class Group
    {
        /// <summary>
        /// Saves the name of the group.
        /// </summary>
        private string name;
        
        /// <summary>
        /// Saves a description for the group.
        /// </summary>
        private string description;
        
        /// <summary>
        /// Saves a value indicating the type of the group.
        /// </summary>
        private GroupType groupType;
        
        /// <summary>
        /// Initializes a new instance of the Group class.
        /// </summary>
        /// <param name="nane">The name of the grou that is created.</param>
        public Group(string name)
        {
            if (name.StartsWith("ext", StringComparison.CurrentCultureIgnoreCase))
                throw new ArgumentOutOfRangeException("name", "A group name is not allowed to start with \"ext\" if the group is not registred as an extension group.");
            
            this.groupType = GroupType.Custom;
            this.name = name;
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether the Group is a special group or not.
        /// </summary>
        /// <value>
        /// If the value is true, the group represented by the instance is a special group.
        /// </value>
        public bool IsSpecialGroup 
        {
            get { return groupType == GroupType.Special; }
            internal set { groupType = GroupType.Special; }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether the Group is an extension group or not.
        /// </summary>
        /// <value>
        /// If the value is true, the group represented by the instance is an extension group.
        /// </value>
        public bool IsExtensionGroup 
        {
            get { return groupType == GroupType.Extension; }
            internal set { groupType = GroupType.Extension; }
        }
        
        /// <summary>
        /// Gets the name of the group. 
        /// </summary>
        /// <value>The name of the group.</value>
        /// <remarks>If the group is an extension group, its name is automatically prefixed with "ext".</remarks>
        public string Name
        {
            get
            {
                if (groupType == GroupType.Extension)
                    return "ext" + name;
                return name;
            }
        }
    }
}
