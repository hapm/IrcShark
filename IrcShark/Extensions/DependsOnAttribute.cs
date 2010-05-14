// <copyright file="DependsOnAttribute.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the DependsOn attribute.</summary>

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
    /// The DependsOnAttribute is used to add dependencys to an extension.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DependsOnAttribute : Attribute
    {
        /// <summary>
        /// Saves the guids of the extensions to depend on.
        /// </summary>
        private Guid[] guids;
        
        /// <summary>
        /// Saves the full classname of the extensions to depend on.
        /// </summary>
        private string[] classNames;
        
        /// <summary>
        /// The minimum versions, the depended extensions need to have.
        /// </summary>
        private Version[] minVersions;
        
        /// <summary>
        /// The maximal versions of the depended extensions.
        /// </summary>
        private Version[] maxVersions;
        
        /// <summary>
        /// Initializes a new instance of the DependsOnAttribute class.
        /// </summary>
        /// <remarks>
        /// When applying this attribute to an extension, the extension will only be
        /// loaded if the given dependency is ok.
        /// </remarks>
        /// <param name="guids">The guid of the extension to depend on.</param>
        public DependsOnAttribute(string[] guids)
        {
            this.guids = new Guid[guids.Length];
            for (int i = 0; i < guids.Length; i++)
            {
                this.guids[i] = new Guid(guids[i]);
            }
        }
        
        /// <summary>
        /// Gets or sets the class name of the extensions depending on.
        /// </summary>
        /// <value>The full classname inclusiv namespace, or null if there was no name given.</value>
        public Guid[] Guids
        {
            get { return guids; }
            set { guids = value; }
        }
        
        /// <summary>
        /// Gets or sets the class name of the extensions depending on.
        /// </summary>
        /// <value>The full classname inclusiv namespace, or null if there was no name given.</value>
        public string[] ClassNames
        {
            get { return classNames; }
            set { classNames = value; }
        }
        
        /// <summary>
        /// Gets or sets the minimal need version of the given extension.
        /// </summary>
        /// <value>The minimal version or null if no minimal version limit was given.</value>
        public Version[] MinimalVersions
        {
            get { return minVersions; }
            set { minVersions = value; }
        }
        
        /// <summary>
        /// Gets or sets the maximal allowed version of the given extension.
        /// </summary>
        /// <value>The maximal version or null if no maximal version limit was given.</value>
        public Version[] MaximalVersions
        {
            get { return maxVersions; }
            set { maxVersions = value; }
        }
    }
}
