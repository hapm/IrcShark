﻿// <copyright file="Parser.Helper.cs" company="IrcShark Team">
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
namespace IrcShark.Extensions.Scripting.Msl
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Description of Parser_Helper.
    /// </summary>
    public partial class Parser
    {
        /// <summary>
        /// Saves the overall compiling unit for the parsed script.
        /// </summary>
        private CodeCompileUnit dom;
        
        /// <summary>
        /// Holds the type, resulting from the parsed script.
        /// </summary>
        private CodeTypeDeclaration script;
        
        private CodeNamespace nm;
        
        /// <summary>
        /// Holds the name of the type created from the parsed script.
        /// </summary>
        private string scriptName;
        
        /// <summary>
        /// Gets or sets the name of the script.
        /// </summary>
        /// <value>The name of the script.</value>
        public string ScriptName
        {
            get { return scriptName; }
            set { scriptName = value; }
        }
        
        /// <summary>
        /// Creates the outer structure of the script compilation file.
        /// </summary>
        public void SetupScript()
        {
            if (scriptName == null)
            {
                scriptName = "Unnamed";
            }
            
            dom = new CodeCompileUnit();
            nm = new CodeNamespace("IrcShark.Extensions.Scripting.Msl.Scripts");
            nm.Imports.Add(new CodeNamespaceImport("System.Text"));
            nm.Imports.Add(new CodeNamespaceImport("System.Collections.Generic"));
            nm.Imports.Add(new CodeNamespaceImport("IrcShark.Extensions.Scripting.Msl"));
            dom.Namespaces.Add(nm);
            SetupScriptClass();
        }
        
        /// <summary>
        /// Creates the script class.
        /// </summary>
        public void SetupScriptClass()
        {
            script = new CodeTypeDeclaration(scriptName);
            script.IsClass = true;
            script.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            script.BaseTypes.Add(new CodeTypeReference(typeof(MslScript)));
            CodeConstructor scriptConstructor = new CodeConstructor();
            scriptConstructor.Attributes = MemberAttributes.Public;
            scriptConstructor.ReturnType = new CodeTypeReference(scriptName);
            scriptConstructor.Parameters.Add(new CodeParameterDeclarationExpression("IScriptEngine", "engine"));
            scriptConstructor.BaseConstructorArgs.Add(new CodeVariableReferenceExpression("engine"));
            script.Members.Add(scriptConstructor);
            nm.Types.Add(script);
        }
        
        /// <summary>
        /// Creates an if statement.
        /// </summary>
        /// <returns>The created if statement.</returns>
        public CodeConditionStatement SetupIfStatement()
        {
            CodeConditionStatement ifStmt = new CodeConditionStatement();
            return ifStmt;
        }
        
        /// <summary>
        /// Creates a while statement.
        /// </summary>
        /// <returns>The created while statement.</returns>
        public CodeIterationStatement SetupWhileStatement()
        {
            CodeIterationStatement whileStmt = new CodeIterationStatement();
            return whileStmt;
        }
        
        public CodeMemberMethod SetupAlias()
        {
            CodeMemberMethod method = new CodeMemberMethod();
            CodeParameterDeclarationExpression param = new CodeParameterDeclarationExpression(typeof(string[]), "parameter");
            method.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            method.Parameters.Add(param);
            CodeVariableDeclarationStatement varStmt = new CodeVariableDeclarationStatement(typeof(string), "buffer");
            CodeVariableDeclarationStatement stackStmt = new CodeVariableDeclarationStatement(typeof(Stack<string>), "textStack", new CodeObjectCreateExpression(typeof(Stack<string>)));
            CodeVariableDeclarationStatement paramStmt = new CodeVariableDeclarationStatement(typeof(string[]), "paramArray");
            method.Statements.Add(varStmt);
            method.Statements.Add(stackStmt);
            method.Statements.Add(paramStmt);
            return method;
        }
        
        public CodeMethodInvokeExpression SetupCommandCall()
        {
            CodeMethodInvokeExpression invoke = new CodeMethodInvokeExpression();
            CodeMethodReferenceExpression executor = new CodeMethodReferenceExpression();
            executor.MethodName = "Executor";
            invoke.Method = executor;
            
            // executor.
            return invoke;
        }
        
        public CodeStatement PushText()
        {
            CodeMethodInvokeExpression push = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeVariableReferenceExpression("textStack"), "Push"), new CodeVariableReferenceExpression("buffer"));
            return new CodeExpressionStatement(push);
        }
        
        public CodeStatement PopText()
        {
            CodeAssignStatement pop = new CodeAssignStatement(new CodeVariableReferenceExpression("buffer"), new CodeMethodReferenceExpression(new CodeVariableReferenceExpression("textStack"), "Pop"));
            return pop;
        }
        
        public CodeExpression BooleanEvaluation(CodeExpression expr)
        {
            CodeMethodInvokeExpression result = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "Check", expr);
            return result;
        }
        
        public CodeExpression BooleanEvaluation(CodeExpression expr, string op)
        {
            CodeMethodInvokeExpression result = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "Check", expr, new CodePrimitiveExpression(op));
            return result;
        }
        
        public CodeExpression BooleanEvaluation(CodeExpression expr1, string op, CodeExpression expr2)
        {
            CodeMethodInvokeExpression result = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "Check", expr1, new CodePrimitiveExpression(op), expr2);
            return result;
        }
        
        public CodeStatement CallAlias(CodeExpression parameters)
        {
            CodeExpressionStatement callAlias = new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "CallAlias", parameters));
            return callAlias;            
        }
        
        public CodeStatement CallExecutor()
        {
            CodeExpressionStatement callExecutor = new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Engine"), "Executor", new CodeVariableReferenceExpression("buffer")));
            return callExecutor;
        }
        
        public CodeExpression CallIdentifier(string methodName, CodeExpression[] parameters, string property)
        {
            CodeMethodInvokeExpression executor;
            if (parameters.Length > 0)
            {
                CodeArrayCreateExpression paramArray = new CodeArrayCreateExpression(typeof(string), parameters);
                executor = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "CallIdentifier", new CodePrimitiveExpression(methodName), paramArray, new CodePrimitiveExpression(property));
            }
            else
            {
                executor = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "CallIdentifier", new CodePrimitiveExpression(methodName), new CodePrimitiveExpression(null), new CodePrimitiveExpression(property));
            }
            
            return executor;
        }
        
        public CodeStatement DeclareLocalVariable(string varname, CodeExpression varValue, ParserState state)
        {
            varname = "v_" + varname;
            
            // if the varname is already defined, only use an assignment, else a declaration
            if (state.localVarNames.Contains(varname))
            {
                if (varValue == null)
                {
                    return null;
                }
                
                CodeAssignStatement assign = new CodeAssignStatement(new CodeVariableReferenceExpression(varname), varValue);
                return assign;
            }
            else
            {
                CodeVariableDeclarationStatement declare;
                if (varValue != null)
                {
                    declare = new CodeVariableDeclarationStatement(typeof(string), varname, varValue);
                }
                else
                {
                    declare = new CodeVariableDeclarationStatement(typeof(string), varname, new CodePrimitiveExpression(string.Empty));
                }
                
                return declare;
            }
        }
        
        public CodeStatement SetGlobalVariable(string varname, CodeExpression varValue, ParserState state)
        {
            CodeExpressionStatement setVar;
            setVar = new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "SetGlobalVariableValue", new CodePrimitiveExpression(varname), varValue));
            return setVar;
        }
        
        public CodeExpression GetVariableValue(string varname, ParserState state)
        {
            if (state.localVarNames.Contains("v_" + varname))
            {
                return new CodeVariableReferenceExpression("v_" + varname);
            }
            else
            {
                return new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "GetGlobalVariableValue", new CodePrimitiveExpression(varname));
            }
        }
        
        public CodeStatement SetVariableValue(string varname, CodeExpression varvalue, ParserState state)
        {
            if (state.localVarNames.Contains("v_" + varname))
            {
                return new CodeAssignStatement(new CodeVariableReferenceExpression("v_" + varname), varvalue);
            }
            else
            {
                return new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "SetGlobalVariableValue", new CodePrimitiveExpression(varname), varvalue));
            }            
        }
        
        public bool IsElseIf()
        {
            Token t = la;
            if (t.kind == _EOL)
            {
                t = scanner.Peek();
            }
            
            if (t.kind == _sp)
            {
                t = scanner.Peek();
            }
            
            return t.kind == _elseif;
        }
        
        public bool IsElse()
        {
            Token t = la;
            if (t.kind == _EOL)
            {
                t = scanner.Peek();
            }
            
            if (t.kind == _sp)
            {
                t = scanner.Peek();
            }
            
            return t.kind == _else;
        }
        
        public bool IsAssignment()
        {
            Token t = la;
            if (t.kind != _varname)
            {
                return false;
            }
            
            t = scanner.Peek();
            if (t.kind != _sp)
            {
                return false;
            }
            
            t = scanner.Peek();
            if (t.kind != _assign)
            {
                return false;
            }
            
            t = scanner.Peek();
            if (t.kind != _sp)
            {
                return false;
            }
            
            return true;
        }
        
        public bool IsLocal(string varname, ParserState state)
        {
            varname = "v_" + varname;
            return state.localVarNames.Contains(varname);
        }
    }
}
