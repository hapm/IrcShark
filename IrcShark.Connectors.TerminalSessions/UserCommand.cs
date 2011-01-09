// <copyright file="UserCommand.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the UserCommand class.</summary>

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
namespace IrcShark.Connectors.TerminalSessions
{
    using System;
    using System.Collections.Generic;
    using IrcShark.Extensions.Terminal;
    using IrcShark.Extensions.Sessions;
    using IrcShark.Extensions.Terminal.Translation;
    
    /// <summary>
    /// The UserCommand provides a TerminalCommand to administrate the users
    /// of the SessionManagementExtension.
    /// </summary>
    [TerminalCommand("user")]
    public class UserCommand : TerminalCommand
    {
        private SessionManagementExtension sessions;
        
        public override void Init(TerminalExtension terminal)
        {
            base.Init(terminal);
            this.sessions = Terminal.Context.Application.Extensions["IrcShark.Extensions.Sessions.SessionManagerExtension"] as SessionManagementExtension;
        }
        
        public override void Execute(params string[] paramList)
        {
            if (paramList.Length == 0)
            {
                Terminal.WriteLine(PublicMessages.PleaseSpecifyFlag);
                return;
            }
            
            switch (paramList[0])
            {
                case "-l":
                    ListUsers(paramList);
                    break;
                
                case "-a":
                    AddUser(paramList);
                    break;
                    
                case "-r":
                    RemoveUser(paramList);
                    break;
                    
                default:
                    Terminal.WriteLine(PublicMessages.UnknownFlag, paramList[0]);
                    break;
            }
        }
        
        public void ListUsers(string[] paramList)
        {
            foreach (User user in sessions.Users)
            {
                Terminal.WriteLine(user.Name);
            }
        }
        
        public void AddUser(string[] paramList)
        {
            
        }
        
        public void RemoveUser(string[] paramList)
        {
            
        }
    }
}
