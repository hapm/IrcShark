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
namespace IrcSharkTerminal
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
        ExtensionManager extManager;
        
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
                case "-l":
                    ListLoadedExtensions();
                    break;
                case "-a":
                    ListAvailableExtensions();
                    break;
                default:
                    Terminal.WriteLine(string.Format(Translation.Messages.UnknownFlag, paramList[0]));
                    break;
            }
        }

        void ListAvailableExtensions()
        {
            Terminal.WriteLine(Translation.Messages.ListingAvailableExtensions);
            foreach (ExtensionInfo info in extManager.AvailableExtensions) {
                if (!string.IsNullOrEmpty(info.Name)) {
                    Terminal.Write(info.Name);
                } else {
                    Terminal.Write(info.Class);
                }
                if (!string.IsNullOrEmpty(info.Description)) {
                    Terminal.Write(" " + info.Description);
                }
                Terminal.WriteLine();
            }
        }

        void ListLoadedExtensions()
        {
            Terminal.WriteLine(Translation.Messages.ListingLoadedExtensions);
            foreach (ExtensionInfo info in extManager.Keys) {
                if (!string.IsNullOrEmpty(info.Name)) {
                    Terminal.Write(info.Name);
                } else {
                    Terminal.Write(info.Class);
                }
                if (!string.IsNullOrEmpty(info.Description)) {
                    Terminal.Write(" " + info.Description);
                }
                Terminal.WriteLine();
            }
        }
    }
}
