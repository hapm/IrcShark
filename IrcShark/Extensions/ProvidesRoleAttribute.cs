// <copyright file="ProvidesRuleAttribute.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the ProvidesRoleAttribute class.</summary>

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
    using Mono.Addins;
    using System; 
    
    /// <summary>
    /// Allows an AddIn to provide additional roles.
    /// </summary>
    public class ProvidesRoleAttribute : CustomExtensionAttribute
    {
        /// <summary>
        /// Saves the internal name of the role.
        /// </summary>
        private string internalName;
        
        /// <summary>
        /// Saves the name of the resource that helps to display the role.
        /// </summary>
        private string nameResource;
        
        /// <summary>
        /// Saves the list of groups, this role is added to by default.
        /// </summary>
        private string[] groups;
        
        /// <summary>
        /// Saves the name of the resourcethat contains the description of the role
        /// as a string.
        /// </summary>
        private string descriptionResource;
        
        /// <summary>
        /// Initializes a new instance of the ProvidesRoleAttribute.
        /// </summary>
        public ProvidesRoleAttribute()
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the ProvidesRoleAttribute.
        /// </summary>
        public ProvidesRoleAttribute(string defaultGroups)
        {
        	groups = defaultGroups.Split(' ');
        }
        
        /// <summary>
        /// Gets or sets the internal name of the role.
        /// </summary>
        [NodeAttribute]
        public string InternalName
        {
            get { return internalName; }
            set { internalName = value; }
        }
        
        /// <summary>
        /// Gets the groups, this role should be added to by default.
        /// </summary>
        public string[] Groups
        {
        	get { return groups; }
        	set { groups = value; }
        }
        
        /// <summary>
        /// Gets the name of the resource that contains the localized name of the role.
        /// </summary>
        [NodeAttribute]
        public string NameResource
        {
            get { return nameResource; }
            set { nameResource = value; }
        }
        
        /// <summary>
        /// Gets the name of the resource that contains the localized name of the description.
        /// </summary>
        [NodeAttribute]
        public string DescriptionResource
        {
            get { return descriptionResource; }
            set { descriptionResource = value; }
        }
    }
}
