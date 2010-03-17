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
                parameters = new string[data.Length-1];
                for (int i = 0; i < parameters.Length; i++)
                {
                    parameters[i] = data[i+1];
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
            object result = "";
            if (property == null)
            {
                property = "";
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
                return "";
            }
            
            return result.ToString();;
        }
        
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
