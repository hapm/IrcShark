// <copyright file="MslScript.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the MslScript class.</summary>

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
    using System.Collections.Generic;
    using System.Globalization;
    using System.Reflection;
    using IrcShark.Extensions.Scripting;
    
    /// <summary>
    /// The parent class of all compiled msl scripts.
    /// </summary>
    public abstract class MslScript : Script
    {
        public MslScript(IScriptEngine engine) : base(engine)
        {
        }
        
        public MslScriptEngine MslEngine
        {
            get { return (MslScriptEngine)Engine; }
        }
        
        public void CallAlias(string line)
        {
            string[] data = line.Split(" ".ToCharArray());
            if (data.Length < 1)
            {
                return;
            }
            
            string aliasName = data[0];
            string[] parameters = null;
            if (data.Length > 1)
            {
                parameters = new string[data.Length - 1];
                for (int i = 0; i < parameters.Length; i++)
                {
                    parameters[i] = data[i + 1];
                }
            }
            
            CallAlias(aliasName, parameters);
        }
        
        public void CallAlias(string name, string[] parameters)
        {
            MethodInfo method;
            if (!name.StartsWith("."))
            {
                string internalAliasName = "Alias" + name;
                method = GetType().GetMethod(internalAliasName);
                if (method != null)
                {
                    method.Invoke(this, new object[] { parameters });
                    return;
                }
            }
            else
            {
                name = name.Substring(1);
            }
            
            if (Engine.PublishedMethods.ContainsKey(name))
            {
                object[] realParameters;
                Delegate del = Engine.PublishedMethods[name];
                method = del.Method;
                if (method.ContainsGenericParameters)
                {
                    throw new ScriptingException(string.Format("Tried to call published method {0}, what contains unsupported generic parameters.", name));
                }
                
                realParameters = CreateRealParameters(method, parameters);
                del.DynamicInvoke(realParameters);
            }
        }
        
        /// <summary>
        /// Calls the given identifier.
        /// </summary>
        /// <param name="name">The name of the identifier.</param>
        /// <param name="parameters">The parameters for the identifier.</param>
        /// <param name="property">The property, assigned to the identifier.</param>
        /// <returns></returns>
        public string CallIdentifier(string name, string[] parameters, string property)
        {            
            name = name.Substring(1);
            string internalAliasName = "Alias" + name;
            MethodInfo method = GetType().GetMethod(internalAliasName);
            object result = string.Empty;
            if (property == null)
            {
                property = string.Empty;
            }
            
            if (Engine.PublishedMethods.ContainsKey(name))
            {
                object[] realParameters;
                Delegate del = Engine.PublishedMethods[name];
                method = del.Method;
                if (method.ContainsGenericParameters)
                {
                    throw new ScriptingException(string.Format("Tried to call published method {0}, what contains unsupported generic parameters.", name));
                }
                
                realParameters = CreateRealParameters(method, parameters);
                MslEngine.PushProperty(property);
                result = del.DynamicInvoke(realParameters);
                MslEngine.PopProperty();
            }
            else if (method != null)
            {
                MslEngine.PushProperty(property);
                result = method.Invoke(this, new object[] { parameters });
                MslEngine.PopProperty();
            }
            
            if (result == null)
            {
                return string.Empty;
            }
            
            return result.ToString();
        }
        
        /// <summary>
        /// Checks if the given string represents true or false.
        /// </summary>
        /// <param name="s">The string to check.</param>
        /// <returns>Its true if the string represents true, false otherwise.</returns>
        public bool Check(string s)
        {
            if (s == null)
            {
                return false;
            }
            
            s = s.Trim(' ');
            double d;
            if (double.TryParse(s, out d))
            {
                return d != 0;                   
            }
            
            switch (s)
            {
                case "":
                case "$false":
                    return false;
                    
                default:
                    return true;
            }
        }
        
        /// <summary>
        /// Checks if the given string matches the given operation.
        /// </summary>
        /// <param name="s">The string to check.</param>
        /// <param name="op">The operator to evaluate the string with.</param>
        /// <returns>Its true if the operator is true for the given string, false otherwise.</returns>
        public bool Check(string s, string op)
        {
            return false;
        }
        
        /// <summary>
        /// Compares two strings with the given operator.
        /// </summary>
        /// <param name="s1">The first string to check.</param>
        /// <param name="op">The operator to compare the strings with.</param>
        /// <returns>Its true if the operator is true for the two given strings, false otherwise.</returns>
        public bool Check(string s1, string op, string s2)
        {
            double d1 = 0;
            double d2 = 0;
            bool numeric = double.TryParse(s1, out d1) && double.TryParse(s2, out d2);
            switch (op)
            {
                case "&&":
                    return Check(s1) && Check(s2);
                    
                case "||":
                    return Check(s1) || Check(s2);
                    
                case "==":
                    if (numeric)
                    {
                        return d1 == d2;
                    }
                    
                    return s1.Equals(s2, StringComparison.CurrentCultureIgnoreCase);
                    
                case "===":
                    if (numeric)
                    {
                        return d1 == d2;
                    }
                    
                    return s1.Equals(s2);
                    
                case "!=":
                    if (numeric)
                    {
                        return d1 != d2;
                    }
                    
                    return s1.Equals(s2, StringComparison.CurrentCultureIgnoreCase);
                    
                case ">":
                    if (numeric)
                    {
                        return d1 > d2;
                    }
                    
                    for (int i = 0; i < s1.Length && i < s2.Length; i++)
                    {
                        if (s1[i] != s2[i])
                        {
                            return s1[i] > s2[i];
                        }
                    }
                    
                    return s1.Length > s2.Length;
                    
                case "<":
                    if (numeric)
                    {
                        return d1 < d2;
                    }
                    
                    for (int i = 0; i < s1.Length && i < s2.Length; i++)
                    {
                        if (s1[i] != s2[i])
                        {
                            return s1[i] < s2[i];
                        }
                    }
                    
                    return s1.Length < s2.Length;
            }
            
            return false;
        }
        
        /// <summary>
        /// Gets the value of a global variable.
        /// </summary>
        /// <param name="varname">The name of the global variable.</param>
        /// <returns>The value of the global variable or null if there is no value set.</returns>
        private string GetGlobalVariableValue(string varname)
        {
            return MslEngine.GetGlobalVariableValue(varname);
        }
        
        /// <summary>
        /// Sets the value of a global variable.
        /// </summary>
        /// <param name="varname">The name of the global variable.</param>
        /// <param name="varvalue">The new value to set.</param>
        private void SetGlobalVariableValue(string varname, string varvalue)
        {
            MslEngine.SetGlobalVariableValue(varname, varvalue);
        }
        
        /// <summary>
        /// Converts the string parameters to a list of objects matching the parameter types of a given method.
        /// </summary>
        /// <param name="method">The method, to get the parameter types from.</param>
        /// <param name="parameters">The parameters as strings.</param>
        /// <returns>A list of objects matching the parameter types of the given method.</returns>
        /// <remarks>
        /// This method uses the IConvertible interface to try to convert the strings in the proper types.
        /// </remarks>
        /// <exception cref="ScriptingException">
        /// If a method have a parameter that doesn't implement IConvertible, this method will fail with a
        /// ScriptingException.
        /// </exception>
        private object[] CreateRealParameters(MethodInfo method, string[] parameters)
        {
            ParameterInfo[] paramInfos = method.GetParameters();
            object[] realParameters = null;
            if (paramInfos.Length > 0)
            {
                if (paramInfos.Length == 1 && paramInfos[0].GetType().IsAssignableFrom(parameters.GetType()))
                {
                    return new object[] { parameters };
                }
                
                realParameters = new object[paramInfos.Length];
                if (parameters.Length > paramInfos.Length)
                {
                    throw new ScriptingException(string.Format("To much parameters for {0}", method.Name));
                }
                
                try
                {
                    CultureInfo culture = CultureInfo.InvariantCulture;
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        realParameters[i] = (parameters[i] as IConvertible).ToType(paramInfos[i].ParameterType, culture);
                    }
                }
                catch (Exception ex)
                {
                    throw new ScriptingException("Bad formating", ex);
                }
            }
            
            return realParameters;
        }
    }
}
