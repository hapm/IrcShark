// <copyright file="ExtensionCommand.cs" company="IrcShark Team">
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
namespace IrcShark.Extensions.Terminal
{
    using System;
    using IrcShark;
    using IrcShark.Extensions;

    /// <summary>
    /// The ExtensionCommand is used to manage extenions.
    /// </summary>
    public class ExtensionCommand : TerminalCommand
    {
        /// <summary>
        /// Saves the instance of the ExtensionManager.
        /// </summary>
        private ExtensionManager extManager;
        
        /// <summary>
        /// Initializes a new instance of the ExtensionCommand class.
        /// </summary>
        /// <param name="extension">The instance of the TerminalExtension.</param>
        public ExtensionCommand(TerminalExtension extension)
            : base("ext", extension)
        {
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
                    LoadExtension(paramList);
                    break;
                case "-u":
                    UnloadExtension(paramList);
                    break;
                default:
                    Terminal.WriteLine(string.Format(Translation.Messages.UnknownFlag, paramList[0]));
                    break;
            }
        }

        /// <summary>
        /// Lists all available extensions on the terminal.
        /// </summary>
        private void ListAvailableExtensions()
        {
            int i = 1;
            Terminal.WriteLine(Translation.Messages.ListingAvailableExtensions);
            foreach (ExtensionInfo info in extManager.AvailableExtensions) 
            {
                Terminal.Write(i.ToString() + ". ");                
                if (!string.IsNullOrEmpty(info.Name)) 
                {
                    Terminal.Write(info.Name);
                } 
                else
                {
                    Terminal.Write(info.Class);
                }
                
                if (!string.IsNullOrEmpty(info.Description)) 
                {
                    Terminal.Write(" " + info.Description);
                }
                
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
            foreach (ExtensionInfo info in extManager.Keys) 
            {
                Terminal.Write(i.ToString() + ". ");
                if (!string.IsNullOrEmpty(info.Name)) 
                {
                    Terminal.Write(info.Name);
                } 
                else 
                {
                    Terminal.Write(info.Class);
                }
                
                if (!string.IsNullOrEmpty(info.Description)) 
                {
                    Terminal.Write(" " + info.Description);
                }
                
                Terminal.WriteLine();
                i++;
            }
        }
        
        /// <summary>
        /// Trys to load a given extension.
        /// </summary>
        /// <param name="args">The parameters for this command.</param>
        private void LoadExtension(string[] args)
        {
            if (args.Length < 2)
            {
                Terminal.WriteLine(Translation.Messages.SpecifyAnExtensionToLoad);
                return;
            }
            
            int nr;
            ExtensionInfo info;
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
            
            if (extManager.IsLoaded(info))
            {
                Terminal.WriteLine(Translation.Messages.ExtensionAlreadyLoaded);
                return;
            }
            
            extManager.Load(info);
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
            ExtensionInfo info;
            if (int.TryParse(args[1], out nr))
            {
                if (extManager.Count < nr || nr < 1)
                {
                    Terminal.WriteLine(Translation.Messages.NoLoadedExtensionWithThisNumber);
                    return;
                }
                
                info = extManager[nr - 1];
            }
            else
            {
                // TODO resolve the ExtensionInfo from the given name in the args here
                return;
            }
            
            if (!extManager.IsLoaded(info))
            {
                Terminal.WriteLine(Translation.Messages.ExtensionNotLoaded);
                return;
            }
            
            extManager.Unload(info);            
        }
    }
}
