﻿// <copyright file="MslScriptEngine.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the MslScriptEngine class.</summary>

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
namespace IrcShark.Extensions.Scripting.Msl
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using IrcShark.Extensions.Scripting;

    /// <summary>
    /// The MslScriptEngine can be used to execute msl script files.
    /// </summary>
    [ScriptEngine]
    public class MslScriptEngine : MarshalByRefObject, IScriptEngine
    {
        /// <summary>
        /// Saves the language definition of msl.
        /// </summary>
        private LanguageDefinition language;
        
        /// <summary>
        /// Saves all methods, that can be used by scripts in this engine.
        /// </summary>
        private MethodCollection publishedMethods;
        
        /// <summary>
        /// Initializes a new instance of the MslScriptEngine class.
        /// </summary>
        public MslScriptEngine()
        {
            language = new LanguageDefinition("MSL", new string[] { "mrc", "ini" }, LanguageFeatures.Procedural | LanguageFeatures.TypeSave);
            publishedMethods = new MethodCollection();
            Type scriptTextMethods = typeof(MslStringMethods);
            MethodInfo info = scriptTextMethods.GetMethod("Left");
            publishedMethods.Add("+", new MultiParamIdentifierDelegate(MslStringMethods.Concat));
            publishedMethods.Add("left", new MslStringMethods.LeftDelegate(MslStringMethods.Left));
            publishedMethods.Add("me", new SimpleStringIdentifier(MslDummyMethods.Me));
            publishedMethods.Add("true", new SimpleStringIdentifier(MslDummyMethods.True));
            publishedMethods.Add("false", new SimpleStringIdentifier(MslDummyMethods.False));
        }
        
        /// <summary>
        /// Gets the language definition for the msl language.
        /// </summary>
        /// <value>
        /// The definition instance.
        /// </value>
        public LanguageDefinition Language 
        {
            get { return language; }
        }
        
        /// <summary>
        /// Gets a value indicating whether msl supports compilation.
        /// </summary>
        /// <value>
        /// This will be always true as msl supports compilation.
        /// </value>
        public bool SupportsCompilation 
        {
            get { return true; }
        }
        
        /// <summary>
        /// Gets a value indicating whether msl supports evaluation.
        /// </summary>
        /// <value>
        /// This will be always true as msl supports evaluation.
        /// </value>
        public bool SupportsEvaluation 
        {
            get { return true; }
        }
        
        /// <summary>
        /// Gets a collection of methods, that can be used from msl scripts running in this engine.
        /// </summary>
        /// <value>A collection of methods.</value>
        public MethodCollection PublishedMethods 
        {
            get { return publishedMethods; }
        }
        
        /// <summary>
        /// Gets nothing as msl doesn't support object oriented programming.
        /// </summary>
        /// <value>
        /// None value.
        /// </value>
        /// <exception cref="NotSupportedException">
        /// Object oriented programming isn't supported by msl yet.
        /// </exception>
        public ObjectCollection PublishedObjects 
        {
            get { throw new NotSupportedException(); }
        }
        
        public string GetGlobalVariableValue(string varname)
        {
            Dictionary<string, string> globalVars;
            if (!ScriptContext.CurrentContext.Globals.ContainsKey("msl.globalVariables"))
            {
                ScriptContext.CurrentContext.Globals.Add("msl.globalVariables", new Dictionary<string, string>());
            }
            
            globalVars = ScriptContext.CurrentContext.Globals["msl.globalVariables"] as Dictionary<string, string>;
            if (globalVars == null)
            {
                // TODO: echo error for bad type of global variables collection
                return string.Empty;
            }
            
            if (globalVars.ContainsKey(varname))
            {
                return globalVars[varname];
            }
            else
            {
                return string.Empty;
            }
        }
        
        public void SetGlobalVariableValue(string varname, string varvalue)
        {
            Dictionary<string, string> globalVars;
            if (!ScriptContext.CurrentContext.Globals.ContainsKey("msl.globalVariables"))
            {
                ScriptContext.CurrentContext.Globals.Add("msl.globalVariables", new Dictionary<string, string>());
            }
            
            globalVars = ScriptContext.CurrentContext.Globals["msl.globalVariables"] as Dictionary<string, string>;
            if (globalVars == null)
            {
                // TODO: echo error for bad type of global variables collection
                return;
            }
            
            if (globalVars.ContainsKey(varname))
            {
                globalVars[varname] = varvalue;
            }
            else
            {
                globalVars.Add(varname, varvalue);
            }
        }
        
        public string GetCurrentProperty()
        {
            return GetActivePropertyStack().Peek();
        }
        
        public void PushProperty(string property)
        {
            GetActivePropertyStack().Push(property);
        }
        
        public void PopProperty()
        {
            GetActivePropertyStack().Pop();
        }
        
        /// <summary>
        /// Evaluates the given string and returns the result, if any.
        /// </summary>
        /// <param name="script">The string, that should be interpreted as a msl script.</param>
        /// <returns>The result as a string, or null if there is no result.</returns>
        public object Evaluate(string script)
        {
            return null;
        }
        
        /// <summary>
        /// Compiles the given msl script file to a Script instance.
        /// </summary>
        /// <param name="file">The file to compile.</param>
        /// <returns>The compiled script.</returns>
        public ScriptContainer Compile(System.IO.FileInfo file, string binPathes)
        {
            ScriptContainer result = new ScriptContainer(binPathes, file.Name);
            Parser mslParser = new Parser();
            StreamReader reader = new StreamReader(file.OpenRead(), System.Text.Encoding.Default, false);
            CodeCompileUnit unit = mslParser.Parse(reader);
            reader.Close();
            result.ScriptDom = unit;
            result.Compile(file.Name, this);
            return result;
        }
        
        /// <summary>
        /// Compiles the given msl script string to a Script instance.
        /// </summary>
        /// <param name="script">The string to interpret as a msl script.</param>
        /// <returns></returns>
        public ScriptContainer Compile(string name, string source, string binPathes)
        {
            ScriptContainer result = new ScriptContainer(binPathes, name);
            Parser mslParser = new Parser();
            mslParser.ScriptName = name;
            StringReader reader = new StringReader(source);
            CodeCompileUnit unit = mslParser.Parse(reader);
            reader.Close(); 
            result.ScriptDom = unit;
            result.Compile(name, this);
            return result;
        }
        
        private Stack<string> GetActivePropertyStack()
        {
            Stack<string> propertyStack = null;
            if (ScriptContext.CurrentContext.Globals.ContainsKey("msl.propertys"))
            {
                propertyStack = ScriptContext.CurrentContext.Globals["msl.propertys"] as Stack<string>;
            }
            
            if (propertyStack == null)
            {
                propertyStack = new Stack<string>();
                ScriptContext.CurrentContext.Globals.Add("msl.propertys", propertyStack);
            }
            
            return propertyStack;            
        }
    }
}
