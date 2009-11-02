// <copyright file="IrcSharkAdministrationPermissionAttribute.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the ChatManagerExtension class.</summary>

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
namespace IrcShark.Policy
{
    using System;
    using System.Security;
    using System.Security.Permissions;
        
    /// <summary>
    /// This attribute allows you to set an IrcShark related access permission to your code.
    /// </summary>
    /// Learn mote about the <see cref="IrcSharkPermission"/>
    [AttributeUsageAttribute(AttributeTargets.All, AllowMultiple = true)]
    public class IrcSharkAdministrationPermissionAttribute : CodeAccessSecurityAttribute
    {
        bool unrestricted = false;
        
        public IrcSharkAdministrationPermissionAttribute(SecurityAction action) : base(action)
        {
        }
        
           public new bool Unrestricted
           {
              get { return unrestricted; }
              set { unrestricted = value; }
           }
        
           public override IPermission CreatePermission()
           {
              if (Unrestricted)
              {
                 return new IrcSharkAdministrationPermission(PermissionState.Unrestricted);
              }
              else
              {
                 return new IrcSharkAdministrationPermission(PermissionState.None);
              }
        }
    }
}
