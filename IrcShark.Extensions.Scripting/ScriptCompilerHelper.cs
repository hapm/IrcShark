// <copyright file="ScriptCompilerHelper.cs" company="IrcShark Team">
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
namespace IrcShark.Extensions.Scripting
{
    using System;
    using System.CodeDom;
    using System.CodeDom.Compiler;
    using System.Reflection;
    using Microsoft.CSharp;

    /// <summary>
    /// Description of ScriptCompilerHelper.
    /// </summary>
    public class ScriptCompilerHelper : MarshalByRefObject
    {        
        private CodeCompileUnit scriptDom;
        
        private CodeDomProvider provider;
        
        private Assembly scriptAssembly;
        
        private Script script;
        
        private string mainType;

        public ScriptCompilerHelper()
        {
            provider = new CSharpCodeProvider();
        }
        
        public string MainType
        {
            get { return mainType; }
            set { mainType = value; }
        }
        
        public Script Instance
        {
            get { return script; }
        }
        
        public void Compile(IScriptEngine engine)
        {
            CompilerParameters comParams = new CompilerParameters();
            comParams.GenerateInMemory = true;
            comParams.CompilerOptions = "/optimize";
            comParams.ReferencedAssemblies.Add("System.dll");
            comParams.ReferencedAssemblies.Add("IrcShark.Core.dll");
            comParams.ReferencedAssemblies.Add("IrcShark.Extensions.Scripting.dll");
            comParams.ReferencedAssemblies.Add("IrcShark.Extensions.Scripting.Msl.dll");
            CompilerResults results = provider.CompileAssemblyFromDom(comParams, scriptDom);
            if (results.Errors.HasErrors)
            {
                throw new CompilationException(results.Errors);
            }
            scriptAssembly = results.CompiledAssembly;
            Type scriptType = scriptAssembly.GetType("IrcShark.Extensions.Scripting.Msl.Scripts." + mainType);
            if (scriptType != null)
            {
                script = scriptType.GetConstructor(new Type[] { typeof(IScriptEngine) } ).Invoke(new object[] { engine }) as Script;
            }
        }
        
        public CodeCompileUnit ScriptDom
        {
            get { return scriptDom; }
            set { scriptDom = value; }
        }
        
        public void Execute(string name, string[] parameters)
        {
            script.Execute(name, parameters);
        }
        
        public void Unload()
        {
            provider = null;
            scriptAssembly = null;
        }
    }
}
