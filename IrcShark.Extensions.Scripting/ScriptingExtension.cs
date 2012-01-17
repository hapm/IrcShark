// <copyright file="ScriptingExtension.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the ScriptingExtension class.</summary>

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
    using System.Runtime.InteropServices;
    using IrcShark.Extensions;
    
    /// <summary>
    /// Description of MyClass.
    /// </summary>
    [Extension(Name="Script-Manager", Id="IrcShark.Extensions.Scripting.ScriptingExtension")]
    public class ScriptingExtension : Extension
    {
        /// <summary>
        /// Saves all supported languages.
        /// </summary>
        private List<IScriptEngine> languages;
        
        private List<ScriptContainer> scripts;
        
        private MethodCollection publishedMethods;
        
        private ObjectCollection publishedObjects;
        
        public ScriptingExtension()
        {
            languages = new List<IScriptEngine>();
            publishedMethods = new MethodCollection();
            publishedObjects = new ObjectCollection();
            PublishedMethods.Added += new TalkingCollectionEventHandler<string, Delegate>(HandleNewMethod);
            PublishedMethods.Removed += new TalkingCollectionEventHandler<string, Delegate>(HandleRemoveMethod);
        }
        
        public MethodCollection PublishedMethods
        {
            get { return publishedMethods; }
        }
        
        public ObjectCollection PublishedObjects
        {
            get { return publishedObjects; }
        }
        
        public IScriptEngine[] GetRegisteredLanguages()
        {
            return languages.ToArray();
        }
        
        public void RehashMethods()
        {
            publishedMethods.Clear();
            foreach (Mono.Addins.TypeExtensionNode<ScriptMethodAttribute> methodNode in Mono.Addins.AddinManager.GetExtensionNodes(typeof(IScriptMethod)))
            {
                IScriptMethod method = methodNode.CreateInstance() as IScriptMethod;
                publishedMethods.Add(methodNode.Data.Name, method.GetMethodDelegat(this));
            }
        }
        
        public void RehashLanguages()
        {
            languages.Clear();
            foreach (IScriptEngine engine in Mono.Addins.AddinManager.GetExtensionObjects(typeof(IScriptEngine)))
            {
                languages.Add(engine);
                LanguageDefinition lang = engine.Language;
                if (lang.IsObjectOriented)
                {
                    foreach (KeyValuePair<string, object> pair in publishedObjects)
                    {
                        engine.PublishedObjects.Add(pair.Key, pair.Value);
                    }
                }
                
                if (lang.IsProcedural)
                {
                    foreach (KeyValuePair<string, Delegate> pair in publishedMethods)
                    {
                        engine.PublishedMethods.Add(pair.Key, pair.Value);
                    }
                }
            }
        }
        
        public override void Start(ExtensionContext context)
        {
            Context = context;
            RehashLanguages();
            RehashMethods();
        }
        
        public override void Stop()
        {
        }
        
        private void HandleNewMethod(object sender, TalkingCollectionEventArgs<string, Delegate> args)
        {
            foreach (IScriptEngine engine in languages)
            {
                if (engine.Language.IsProcedural)
                {
                    engine.PublishedMethods.Add(args.ChangedKey, publishedMethods[args.ChangedKey]);
                }
            }
        }
        
        private void HandleRemoveMethod(object sender, TalkingCollectionEventArgs<string, Delegate> args)
        {
            foreach (IScriptEngine engine in languages)
            {
                if (engine.Language.IsProcedural)
                {
                    engine.PublishedMethods.Remove(args.ChangedKey);
                }
            }            
        }
    }
}