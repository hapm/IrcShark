// <copyright file="Parser.Helper.cs" company="IrcShark Team">
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
        
        private int usedBufferCount = 0;
        
        private int bufferCount = 0;
        
        public void SetupScript()
        {
            //TODO make this member setable from outside
            scriptName = "Test";
            dom = new CodeCompileUnit();
            nm = new CodeNamespace("IrcShark.Extensions.Scripting.Msl.Scripts");
            nm.Imports.Add(new CodeNamespaceImport("System.Text"));
            nm.Imports.Add(new CodeNamespaceImport("System.Collections.Generic"));
            nm.Imports.Add(new CodeNamespaceImport("IrcShark.Extensions.Scripting.Msl"));
            dom.Namespaces.Add(nm);
            SetupScriptClass();
        }
        
        public void SetupScriptClass()
        {
            script = new CodeTypeDeclaration(scriptName);
            script.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            script.IsClass = true;
            nm.Types.Add(script);
        }
        
        public CodeConditionStatement SetupIfStatement()
        {
            CodeConditionStatement ifStmt = new CodeConditionStatement();
            return ifStmt;
        }
        
        public CodeMemberMethod SetupAlias()
        {
            ClearBuffers();
            CodeMemberMethod method = new CodeMemberMethod();
            CodeParameterDeclarationExpression context = new CodeParameterDeclarationExpression(typeof(ScriptContext), "context");
            CodeParameterDeclarationExpression param = new CodeParameterDeclarationExpression(typeof(string[]), "parameter");
            method.Attributes = MemberAttributes.Public & MemberAttributes.Final;
            method.Parameters.Add(context);
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
            //executor.
            return invoke;
        }
        
        public CodeStatement GetBuffer()
        {
            CodeObjectCreateExpression newStringBuilder = new CodeObjectCreateExpression(typeof(StringBuilder));
            usedBufferCount++;
            string bufferName = "buffer" + usedBufferCount;
            if (usedBufferCount <= bufferCount)
            {
                CodeVariableReferenceExpression buffer = new CodeVariableReferenceExpression(bufferName);
                CodeAssignStatement clearBuffer = new CodeAssignStatement(buffer, newStringBuilder);
                return clearBuffer;
            }
            else
            {
                bufferCount++;
                CodeVariableDeclarationStatement newBuffer = new CodeVariableDeclarationStatement(typeof(StringBuilder), bufferName, newStringBuilder);
                return newBuffer;
            }
        }
        
        public CodeStatement ReleaseBuffer()
        {
            string bufferName = "buffer" + usedBufferCount;
            CodeMethodInvokeExpression bufferToString = new CodeMethodInvokeExpression(
                new CodeMethodReferenceExpression(
                    new CodeVariableReferenceExpression(bufferName), "ToString"));
            CodeAssignStatement setValue = new CodeAssignStatement(new CodeVariableReferenceExpression("buffer"), bufferToString);
            usedBufferCount--;
            return setValue;
        }
        
        public CodeStatement AppendBuffer(CodeExpression expr)
        {
            string bufferName = "buffer" + usedBufferCount;
            CodeMethodInvokeExpression append = new CodeMethodInvokeExpression(new CodeVariableReferenceExpression(bufferName), "Append", expr);
            return new CodeExpressionStatement(append);
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
        
        public void ClearBuffers()
        {
            usedBufferCount = 0;
            bufferCount = 0;
        }
        
        public CodeStatement CallExecutor()
        {
            CodeExpressionStatement callExecutor = new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("context"), "Executor", new CodeVariableReferenceExpression("buffer")));
            return callExecutor;
        }
        
        public void CallExecutor(CodeStatementCollection stmts, string methodName, int paramCount, string property)
        {
            CodeMethodInvokeExpression executor;
            if (paramCount > 0)
            {
                CodeIterationStatement loop = new CodeIterationStatement();
                CodeVariableReferenceExpression pIndex = new CodeVariableReferenceExpression("pIndex");
                CodeVariableReferenceExpression textStack = new CodeVariableReferenceExpression("textStack");
                CodeVariableReferenceExpression paramArray = new CodeVariableReferenceExpression("paramArray");
                loop.InitStatement = new CodeVariableDeclarationStatement(typeof(int), "pIndex", new CodePrimitiveExpression(paramCount-1));
                loop.IncrementStatement = new CodeAssignStatement(pIndex, new CodeBinaryOperatorExpression(pIndex, CodeBinaryOperatorType.Subtract, new CodePrimitiveExpression(1)));
                loop.TestExpression = new CodeBinaryOperatorExpression(pIndex, CodeBinaryOperatorType.GreaterThanOrEqual, new CodePrimitiveExpression(0));
                loop.Statements.Add(new CodeAssignStatement(new CodeIndexerExpression(paramArray, pIndex), new CodeMethodInvokeExpression(textStack, "Pop")));
                stmts.Add(new CodeAssignStatement(paramArray, new CodeArrayCreateExpression(typeof(string), paramCount)));
                stmts.Add(loop);
                executor = new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("context"), "Executor", new CodePrimitiveExpression(methodName), new CodePrimitiveExpression(property), paramArray);
            }
            else
            {
                executor = new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("context"), "Executor", new CodePrimitiveExpression(methodName), new CodePrimitiveExpression(property), new CodePrimitiveExpression(null));
            }
            stmts.Add(AppendBuffer(executor));
        }
    }
}
