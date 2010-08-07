using Mono.Addins;
// <copyright file="ExtensionCommand.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the ExtensionCommand class.</summary>

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
namespace IrcShark.Extensions.Terminal.Commands
{
    using System;
    using System.Collections.Generic;

    using IrcShark;
    using IrcShark.Extensions;
    using IrcShark.Extensions.Terminal;

    /// <summary>
    /// The ExtensionCommand is used to manage extenions.
    /// </summary>
    [TerminalCommand("ext")]
    public class ExtensionCommand : TerminalCommand
    {
        /// <summary>
        /// Saves the instance of the ExtensionManager.
        /// </summary>
        private ExtensionManager extManager;
        
        /// <summary>
        /// Initializes the ExtensionCommand.
        /// </summary>
        /// <param name="extension">The instance of the TerminalExtension.</param>        
        public override void Init(TerminalExtension terminal)
        {
            base.Init(terminal);
            extManager = Terminal.Context.Application.Extensions;
        }
        
        /// <summary>
        /// Executes the ExtensionCommand.
        /// </summary>
        /// <param name="paramList">All parameters of the command.</param>
        public override void Execute(params string[] paramList)
        {
            if (paramList.Length < 1)
            {
                Terminal.WriteLine(Translation.Messages.SpecifyFlag);
                return;
            }
            
            switch (paramList[0])
            {
                case "-r":
                    ListRunningExtensions();
                    break;
                case "-a":
                    ListAvailableExtensions();
                    break;
                case "-l":
                    StartExtension(paramList);
                    break;
                case "-u":
                    UnloadExtension(paramList);
                    break;
                default:
                    Terminal.WriteLine(string.Format(Translation.Messages.UnknownFlag, paramList[0]));
                    break;
            }
        }
        
        public override string[] AutoComplete(CommandCall call, int paramIndex)
        {
            List<string> result = new List<string>();
            switch (paramIndex)
            {
                case 0:
                    if (string.IsNullOrEmpty(call.Parameters[0]) || call.Parameters[0].Length <= 1)
                    {
                        return new string[] { "-r", "-a", "-l", "-u" };
                    }
                    
                    break;
                case 1:
                    switch (call.Parameters[0])
                    {
                        case "-l":
                            foreach (ExtensionAttribute info in extManager.AvailableExtensions)
                            {
                                if (string.IsNullOrEmpty(call.Parameters[1]) || info.Id.StartsWith(call.Parameters[1]))
                                {
                                    result.Add(info.Id + "\n");
                                }
                            }
                            
                            if (result.Count > 0)
                            {
                                string lastComplete = result[result.Count-1];
                                lastComplete = lastComplete.Remove(lastComplete.Length-1);
                                result[result.Count-1] = lastComplete;
                                return result.ToArray();
                            }
                            
                            break;
                        case "-u":
                            foreach (string id in extManager.Keys)
                            {
                                if (string.IsNullOrEmpty(call.Parameters[1]) || id.StartsWith(call.Parameters[1]))
                                {
                                    result.Add(id + "\n");
                                }
                            }
                            
                            if (result.Count > 0)
                            {
                                return result.ToArray();
                            }
                            
                            break;
                    }
                    
                    break;
            }
            
            return base.AutoComplete(call, paramIndex);
        }

        /// <summary>
        /// Lists all available extensions on the terminal.
        /// </summary>
        private void ListAvailableExtensions()
        {
            int i = 1;
            Terminal.WriteLine(Translation.Messages.ListingAvailableExtensions);
            foreach (IrcShark.Extensions.ExtensionAttribute info in extManager.AvailableExtensions) 
            {
                Terminal.Write(i.ToString() + ". ");   
                Terminal.Write(info.Name);                
                Terminal.WriteLine();
                i++;
            }
        }

        /// <summary>
        /// Lists all loaded extensions on the terminal.
        /// </summary>
        private void ListRunningExtensions()
        {
            Terminal.WriteLine(Translation.Messages.ListingRunningExtensions);
            int i = 1;
            foreach (IExtension ext in extManager.Values) 
            {
                Terminal.Write(i.ToString() + ". ");
                Terminal.Write(ext.Name);
                Terminal.Write(" - ");;
                Terminal.Write(ext.Id);
                
                Terminal.WriteLine();
                i++;
            }
        }
        
        /// <summary>
        /// Trys to load a given extension.
        /// </summary>
        /// <param name="args">The parameters for this command.</param>
        private void StartExtension(string[] args)
        {
            if (args.Length < 2)
            {
                Terminal.WriteLine(Translation.Messages.SpecifyAnExtensionToLoad);
                return;
            }
            
            int nr;
            IrcShark.Extensions.ExtensionAttribute info;
            if (int.TryParse(args[1], out nr))
            {
                if (extManager.AvailableExtensions.Length < nr || nr < 1)
                {
                    Terminal.WriteLine(Translation.Messages.NoAvailableExtensionWithThisNumber);
                    return;
                }
                
                info = extManager.AvailableExtensions[nr - 1];
            }
            else
            {
                // TODO resolve the ExtensionInfo from the given name in the args here
                return;
            }
            
            if (extManager.IsStarted(info.Id))
            {
                Terminal.WriteLine(Translation.Messages.ExtensionAlreadyLoaded);
                return;
            }
            
            extManager.Start(info.Id);
        }
        
        /// <summary>
        /// Trys to unload a gicen extension.
        /// </summary>
        /// <param name="args">The parameters for this command.</param>
        private void UnloadExtension(string[] args)
        {
            if (args.Length < 2)
            {
                Terminal.WriteLine(Translation.Messages.SpecifyAnExtensionToUnload);
                return;
            }
            
            int nr;
            IExtension ext;
            if (int.TryParse(args[1], out nr))
            {
                //TODO get from the given number to the AddinId
                if (extManager.Count < nr || nr < 1)
                {
                    Terminal.WriteLine(Translation.Messages.NoLoadedExtensionWithThisNumber);
                    return;
                }
            }
            else
            {
                if (!extManager.IsStarted(args[1])) 
                {
                    Terminal.WriteLine(Translation.Messages.ExtensionNotLoaded);
                    return;
                }
            }
            
            extManager.Stop(args[1]);
        }
    }
}