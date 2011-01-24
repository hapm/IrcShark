// <copyright file="IAuthenticationInformation.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the IAuthentication interface.</summary>

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

	/// <summary>
	/// The IAuthenticationInformation interface allows authenticators to transfer
	/// authentication method specific informations.
	/// </summary>
	public interface IAuthenticationInformation
	{
		/// <summary>
		/// Gets the authentication method that should be used together with this information.
		/// </summary>
		string AuthenticationMethod
		{
			get;
		}
		
		/// <summary>
		/// Gets the User, this information is provided for.
		/// </summary>
		User User
		{
			get;
		}
	}
}
