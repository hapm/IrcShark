// <copyright file="Script.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the Script class.</summary>

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
    using System.Reflection;
    using System.Threading;

    /// <summary>
    /// Represents a loaded script.
    /// </summary>
    public abstract class Script : MarshalByRefObject
    {
        /// <summary>
        /// Saves the engine, this script was compiled with.
        /// </summary>
        IScriptEngine engine;
        
        /// <summary>
        /// Initializes a new instance of the Script class.
        /// </summary>
        /// <param name="engine">the engine, this script was compiled with.</param>
        public Script(IScriptEngine engine)
        {
            this.engine = engine;
        }
        
        /// <summary>
        /// Gets the engine, that compiled this script.
        /// </summary>
        /// <value>The engine instance.</value>
        public IScriptEngine Engine
        {
            get { return engine; }
        }
        
        /// <summary>
        /// Gets the assembly containing the compiled script type.
        /// </summary>
        /// <value>The Assembly instance.</value>
        public Assembly CompiledAssembly
        {
            get { return this.GetType().Assembly; }
        }
        
        
        /// <summary>
        /// Executes a method that is defined in the script.
        /// </summary>
        /// <param name="name">The name of the method.</param>
        /// <param name="parameters">The parameters for the method.</param>
        /// <returns>A resulting object of the execution.</returns>
        public object Execute(string name, object[] parameters)
        {
            object result;
            ScriptContext context = ScriptContext.CurrentContext;
            context.EngineStarts(engine);
            Type myType = this.GetType();
            MethodInfo method = myType.GetMethod(name);
            result = method.Invoke(this, parameters);
            return result;
        }
    }
}
