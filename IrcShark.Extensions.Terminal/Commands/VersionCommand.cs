// <copyright file="VersionCommand.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the ExitCommand class.</summary>

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
    using System.Reflection;
    using System.Text;

    using IrcShark.Extensions.Terminal;

    /// <summary>
    /// The VersionCommandshow IrcShark version informations.
    /// </summary>
    public class VersionCommand : TerminalCommand
    {
        /// <summary>
        /// Initializes a new instance of the ExitCommand class.
        /// </summary>
        /// <param name="extension">The reference to the VersionCommand.</param>
        public VersionCommand(TerminalExtension extension)
            : base("version", extension)
        {
        }
        
        /// <summary>
        /// Executing this command will show all loaded .Net assebmlies and there version.
        /// </summary>
        /// <param name="paramList">
        /// A list of parameters the user typed.
        /// </param>
        public override void Execute(params string[] paramList)
        {
            ConsoleTable table = new ConsoleTable();
            table.SetHeaders(new string[] {"Assembly Name", "Version"});
			foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies()) 
			{
				AssemblyName name = asm.GetName();
				table.AppendRow(new string[] {name.Name, name.Version.ToString()});
            }
			
			Terminal.WriteLine(table.ToString());
        }
    }
}
