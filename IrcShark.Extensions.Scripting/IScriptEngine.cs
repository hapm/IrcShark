// <copyright file="IScriptEngine.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the IScriptEngine interface.</summary>

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
    using System.IO;

    /// <summary>
    /// By implementing this interface, you can register the implementing class as a script engine, used to execute scriptfiles.
    /// </summary>
    public interface IScriptEngine
    {
        /// <summary>
        /// Gets the language supported by this engine.
        /// </summary>
        /// <value>The LanguageDefinition of the supported language.</value>
        LanguageDefinition Language { get; }
        
        /// <summary>
        /// Gets a value indicating whether the language supports to compile a script into an asssembly.
        /// </summary>
        /// <value>Its true if a script can be compiled, false otherwise.</value>
        bool SupportsCompilation { get; }
        
        /// <summary>
        /// Gets a value indicating whether a script can be evaluated by the engine.
        /// </summary>
        /// <value>Its true if a script can be directly evaluated and interpreted by the engine, false otherwise.</value>
        bool SupportsEvaluation { get; }
        
        /// <summary>
        /// Gets a collection of methods, that can be used by scripts running in the engine.
        /// </summary>
        MethodCollection PublishedMethods { get; }
        
        /// <summary>
        /// Gets a collection of objects, that can be used by scripts running in the engine.
        /// </summary>
        ObjectCollection PublishedObjects { get; }
        
        /// <summary>
        /// Evaluates the given script, if it was written in the supported language.
        /// </summary>
        /// <param name="script"></param>
        /// <returns></returns>
        object Evaluate(string script);
        
        /// <summary>
        /// Compiles the given script and returns the associated Script object.
        /// </summary>
        /// <param name="file">The file containing the script to compile.</param>
        /// <returns>The Script instance to interact with the compiled script.</returns>
        ScriptContainer Compile(FileInfo file);
        
        /// <summary>
        /// Compiles the given script and returns the associated Script object.
        /// </summary>
        /// <param name="script">The script as a string.</param>
        /// <returns>The Script instance to interact with the compiled script.</returns>
        ScriptContainer Compile(string name, string source, string binPathes);
    }
}
