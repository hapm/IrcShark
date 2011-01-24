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
            this.sessions = Terminal.Context.Application.Extensions["IrcShark.Extensions.Sessions.SessionManagementExtension"] as SessionManagementExtension;
        }
        
        public override void Execute(params string[] paramList)
        {
            if (paramList.Length == 0)
            {
                DisplayCurrentUserInfo();
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
                    
                case "-i":
                    ImpersonateUser(paramList);
                    break;
                    
                default:
                    Terminal.WriteLine(PublicMessages.UnknownFlag, paramList[0]);
                    break;
            }
        }
        
        public void DisplayCurrentUserInfo()
        {
            System.Security.Principal.IPrincipal principal = System.Threading.Thread.CurrentPrincipal;
            if (principal is IrcShark.Security.SystemPrincipal)
            {
                Terminal.WriteLine("Running as system user, having all roles!!!");
                return;
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
            if (paramList.Length < 2)
            {
                Terminal.WriteLine("You forgot to specify the username of the user you want to add.");
            }
            
            if (sessions.Users.Contains(paramList[1]))
            {
                Terminal.WriteLine(string.Format("The user {0} already exists.", paramList[1]));
            }
            
            User newUser = new User(paramList[1]);
            sessions.Users.Add(newUser);
            Terminal.WriteLine(string.Format("The user {0} has been successfully created.", paramList[1]));
        }
        
        public void RemoveUser(string[] paramList)
        {
            
        }
        
        public void ImpersonateUser(string[] paramList)
        {
            sessions.Impersonate(paramList[1]);
            Terminal.WriteLine(string.Format("Successfully impersonated user {0}.", paramList[1]));
        }
    }
}
