// <copyright file="LanguageDefinition.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the LanguageDefinition class.</summary>

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
namespace IrcShark.Extensions.Scripting
{
    using System;
    
    [Flags]
    public enum LanguageFeatures
    {
        TypeSave,
        ObjectOriented,
        Procedural,
        UserDefinedTypes
    }

    /// <summary>
    /// Defines a script language and its supported features.
    /// </summary>
    public class LanguageDefinition
    {
        /// <summary>
        /// Saves the name of the language.
        /// </summary>
        private string languageName;
        
        /// <summary>
        /// Saves the supported features.
        /// </summary>
        private LanguageFeatures features;
        
        /// <summary>
        /// Saves the file extensions used by script files containing this language.
        /// </summary>
        private string[] supportedFileExtensions;
        
        /// <summary>
        /// Initializes a new instance of the LanguageDefinition class.
        /// </summary>
        /// <param name="name">The name of the language.</param>
        /// <param name="fileExtensions">The file extensions for script files of this language.</param>
        /// <param name="features">Supported features of the language.</param>
        public LanguageDefinition(string name, string[] fileExtensions, LanguageFeatures features)
        {
            this.languageName = name;
            this.supportedFileExtensions = fileExtensions.Clone() as string[];
            this.features = features;
        }
        
        /// <summary>
        /// Gets a full name of the supported scripting language.
        /// </summary>
        /// <value>
        /// The name of the scripting language executed by this engine.
        /// </value>
        public string LanguageName 
        { 
            get { return languageName; }
        }
        
        public string[] SupportedFileExtensions
        {
            get { return supportedFileExtensions.Clone() as string[]; }
        }
        
        /// <summary>
        /// Gets a value indicating whether the used script language is type save or not.
        /// </summary>
        /// <remarks>
        /// When not overwritten in a derived class, this property is false.
        /// </remarks>
        /// <value>If the script language is type save, true is returned, false otherwise.</value>
        public bool IsTypeSave
        { 
            get { return (features & LanguageFeatures.TypeSave) == LanguageFeatures.TypeSave; }
        }
        
        /// <summary>
        /// Gets a value indicating whether the script language supports object oriented programming.
        /// </summary>
        /// <value>If objects are supported, this is true, false otherwise.</value>
        public bool IsObjectOriented 
        { 
            get { return (features & LanguageFeatures.ObjectOriented) == LanguageFeatures.ObjectOriented; }
        }
        
        /// <summary>
        /// Gets a value indicating whether the script language allows prozedural programming.
        /// </summary>
        /// <value>If prozedures are supported, this is true, false otherwise.</value>
        public bool IsProcedural
        {
            get { return (features & LanguageFeatures.Procedural) == LanguageFeatures.Procedural; }
        }
        
        /// <summary>
        /// Gets a value indicating whether the script language allows user defined types.
        /// </summary>
        /// <value>If user defined types are supported, this is true, false otherwise.</value>
        public bool HasUserDefinedTypes
        {
            get { return (features & LanguageFeatures.UserDefinedTypes) == LanguageFeatures.UserDefinedTypes; }
        }
        
        /// <summary>
        /// Gets all features supported by this language.
        /// </summary>
        /// <value>A flag combined list of supported features.</value>
        public LanguageFeatures Features
        {
            get { return features; }
        }
    }
}
