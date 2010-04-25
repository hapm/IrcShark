// <copyright file="ScriptContext.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the ScriptContext class.</summary>

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
    using System.Collections.Generic;

    /// <summary>
    /// The ScriptContext holds information of the current context, the system runs in.
    /// </summary>
    public class ScriptContext : IDisposable
    {
        /// <summary>
        /// Saves the currently active ScriptContext for each thread.
        /// </summary>
        [ThreadStatic]
        private static ScriptContext currentContext;
        
        /// <summary>
        /// Saves a list of all global available variables in the current context.
        /// </summary>
        private ObjectCollection globals;
        
        /// <summary>
        /// Hold the list of all currently active engines.
        /// </summary>
        private Stack<IScriptEngine> activeEngines;
        
        /// <summary>
        /// Saves if the ScriptContext was disposed.
        /// </summary>
        private bool disposed;
        
        /// <summary>
        /// Initializes a new instance of the ScriptContext class.
        /// </summary>
        public ScriptContext()
        {
            globals = new ObjectCollection();
            activeEngines = new Stack<IScriptEngine>();
            if (currentContext != null)
            {
                currentContext.Dispose();
            }
            
            currentContext = this;
        }
        
        /// <summary>
        /// Gets the currently active ScriptContext for the active thread.
        /// </summary>
        /// <value>
        /// The ScriptContext instance.
        /// </value>
        public static ScriptContext CurrentContext
        {
            get 
            {
                ScriptContext context = currentContext;
                if (context == null)
                {
                    context = new ScriptContext();
                }
                
                return context;
            }
        }
        
        /// <summary>
        /// Gets the currently active script engine.
        /// </summary>
        public IScriptEngine ActiveEngine
        {
            get { return activeEngines.Peek(); }
        }
        
        /// <summary>
        /// Gets a collection of globaly setted variables.
        /// </summary>
        /// <value>The collection of variable names and values.</value>
        public ObjectCollection Globals
        {
            get { return globals; }
        }
        
        /// <summary>
        /// Gets a value indicating whether this ScriptContext was disposed.
        /// </summary>
        /// <value>If the context was disposed, the value is true, false otherwise.</value>
        public bool Disposed
        {
            get { return disposed; }
        }
        
        /// <summary>
        /// Makes the given engine the active engine.
        /// </summary>
        /// <param name="engine">The engine to mark as the active one.</param>
        public void EngineStarts(IScriptEngine engine)
        {
            activeEngines.Push(engine);
        }
        
        /// <summary>
        /// Marks the currently active engine to be not active anymore.
        /// </summary>
        /// <remarks>
        /// If there was an engine active before the one deactiveted with 
        /// this command, that engine gets marked as the active one.
        /// </remarks>
        /// <returns>The engine, that was marked as disabled.</returns>
        public IScriptEngine EngineEnds()
        {
            return activeEngines.Pop();
        }
        
        /// <summary>
        /// Disposes the ScriptContext.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }
        
        protected void Dispose(bool dispose)
        {
            if (dispose)
            {
                if (currentContext == this)
                {
                    currentContext = null;
                }
            }
            
            disposed = true;
        }
    }
}
