// <copyright file="${FILENAME}" company="IrcShark Team">
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
namespace IrcShark.Extensions.Sessions
{
    using System;
    using System.Collections.Generic;
    using System.Security.Principal;
    using IrcShark.Extensions;
    using IrcShark.Security;
    
    /// <summary>
    /// The SessionManagementExtension manages configuration and access
    /// sessions to IrcShark. Sessions are bound to users and rights of
    /// this users.
    /// </summary>
    [Extension(Name="Session", Id="IrcShark.Extensions.Sessions.SessionManagementExtension")]
    public class SessionManagementExtension : Extension
    {
        /// <summary>
        /// Saves a list of all active sessions
        /// </summary>
        private List<Session> sessions;
        
        private List<Role> roles;
        
        /// <summary>
        /// Saves the list of all users registred to the SessionManagementExtension.
        /// </summary>
        private UserCollection users;
        
        /// <summary>
        /// Saves the list of all groups registred to the SessionManagementExtension.
        /// </summary>
        private GroupCollection groups;
        
        /// <summary>
        /// Saves the impersonation stack for each thread.
        /// </summary>
        [ThreadStatic]
        private static Stack<IPrincipal> principals;
        
        /// <summary>
        /// Initializes a new instance of the SessionManagementExtension class.
        /// </summary>
        public SessionManagementExtension()
        {
            sessions = new List<Session>();
            roles = new List<Role>();
            users = new UserCollection();
            groups = new GroupCollection();
        }
        
        /// <summary>
        /// Starts the SessionManagementExtension.
        /// </summary>
        /// <param name="context">The context, this extension runs in.</param>
        public override void Start(ExtensionContext context)
        {
            roles.Clear();
            foreach (Mono.Addins.ExtensionNode<ProvidesRoleAttribute> node in Mono.Addins.AddinManager.GetExtensionNodes("/IrcShark/Roles")) 
            {
                try
                {
                    string name = node.Data.NameResource;
                    string description = node.Data.DescriptionResource;
                    if (name != null)
                        name = node.Addin.GetResourceString(name);
                
                    if (description != null)
                        description = node.Addin.GetResourceString(description);
                
                    roles.Add(new Role(node.Data.InternalName, name, description));
                }
                catch (Exception ex)
                {
                    context.Log.Error("Sessions", 0, "Tried to register role {0}, but an exception occured: {1}", node.Data.InternalName, ex.ToString());
                }
            }
        }
        
        /// <summary>
        /// Gets a collection of all users registred to the SessionManagementExtension.
        /// </summary>
        public UserCollection Users
        {
            get { return users; }
        }
        
        /// <summary>
        /// Initializes a new session, that doesn't have any rights.
        /// </summary>
        /// <returns>The new Session instance.</returns>
        public Session RequestSession() 
        {
            Session session = new Session(this);
            return session;
        }
        
        /// <summary>
        /// Impersonates the current thread to the user with the given name.
        /// </summary>
        /// <param name="name">The name of the user to impersonate to.</param>
        /// <returns>the principal that was active before the impersonation takes place.</returns>
        [RolePermission(System.Security.Permissions.SecurityAction.Assert, Roles="IrcShark.UserManager")]
        public IPrincipal Impersonate(string name)
        {
            User user = users[name];
            UserPrincipal principal = new UserPrincipal(user);
            if (SessionManagementExtension.principals == null)
            {
                SessionManagementExtension.principals = new Stack<IPrincipal>();
            }
            
            IPrincipal oldP = System.Threading.Thread.CurrentPrincipal;
            SessionManagementExtension.principals.Push(oldP);
            System.Threading.Thread.CurrentPrincipal = principal;
            return oldP;
        }
        
        /// <summary>
        /// Releases all impersonations until the given principal is reached.
        /// </summary>
        /// <param name="toPrincipal">The principal to release to.</param>
        public void Release(IPrincipal toPrincipal)
        {
            if (principals == null || !principals.Contains(toPrincipal))
            {
                throw new InvalidOperationException("The given principal is not in the imeronation stack.");
            }
            
            IPrincipal currentPrincipal = principals.Pop();
            while (currentPrincipal != toPrincipal)
            {
                currentPrincipal = principals.Pop();
            }
            
            System.Threading.Thread.CurrentPrincipal = currentPrincipal;
        }
        
        internal void CleanupSession(Session session) 
        {
            sessions.Remove(session);
        }
        
        /// <summary>
        /// Stops the SessionManagementExtension.
        /// </summary>
        public override void Stop()
        {
        }
    }
}