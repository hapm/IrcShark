// 
// $Id$
//
// Note:
//
// Copyright (C) 2009 IrcShark Team
// 
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

using System;
using System.Text;
using System.Security;
using System.Security.Permissions;

namespace IrcShark.Policy
{
	[SerializableAttribute()]
	public sealed class IrcSharkAdministrationPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		private bool unrestricted;
		
		
		public IrcSharkAdministrationPermission(PermissionState state)
		{
			this.unrestricted = state == PermissionState.Unrestricted;
		}
		
		public override SecurityElement ToXml() {
			SecurityElement element = new SecurityElement("IPermission");
  			Type type = this.GetType();
   			StringBuilder AssemblyName = new StringBuilder(type.Assembly.ToString());
   			AssemblyName.Replace('\"', '\'');
   			element.AddAttribute("class", type.FullName + ", " + AssemblyName);
   			element.AddAttribute("version", "1");
   			element.AddAttribute("Unrestricted", unrestricted.ToString());
   			return element;
		}
		
		public override IPermission Intersect (IPermission target) {   
			try
   			{
      			if(null == target)
      			{
         			return null;
      			}
      			IrcSharkAdministrationPermission PassedPermission = (IrcSharkAdministrationPermission)target;
    			if(!PassedPermission.IsUnrestricted())
      			{
         			return PassedPermission;
      			}
      			return this.Copy();
   			}
   			catch (InvalidCastException)
   			{
      			throw new ArgumentException("Argument_WrongType", this.GetType().FullName);
   			}       
		}
		
		public override Boolean IsSubsetOf(IPermission target) 
		{
		   	if(null == target)
      			return !this.unrestricted;
   			try
   			{        
      			IrcSharkAdministrationPermission passedpermission = (IrcSharkAdministrationPermission)target;
      			if(this.unrestricted == passedpermission.unrestricted)
      			{
         			return true;
      			}
      			else
      			{
         			return false;
      			}
   			}
   			catch (InvalidCastException)
   			{
      			throw new ArgumentException("Argument_WrongType", this.GetType().FullName);
   			}          
		}
		
		public override void FromXml(SecurityElement PassedElement) {   
			string element = PassedElement.Attribute("Unrestricted");
   			if(null != element)
   			{  
      			this.unrestricted = Convert.ToBoolean(element);
			}
		}
		
		public override IPermission Copy()
		{
			IrcSharkAdministrationPermission copy = new IrcSharkAdministrationPermission(PermissionState.None);

  			if(this.IsUnrestricted())
   			{
      			copy.unrestricted = true;
   			}
   			else
   			{
      			copy.unrestricted = false;
   			}
   			return copy;
		}
		
		public bool IsUnrestricted ()
		{
			return unrestricted;
		}

	}
}
