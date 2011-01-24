/*
 * Erstellt mit SharpDevelop.
 * Benutzer: markus
 * Datum: 24.01.2011
 * Zeit: 17:33
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
using System;

namespace IrcShark.Extensions.Sessions
{
	/// <summary>
	/// The IAuthenticationProvider interface provides methods to implement
	/// an authentication provider for the SessionManagementExtension.
	/// </summary>
	public interface IAuthenticationProvider
	{
		/// <summary>
		/// Gets the name of the provided method of the authentication method.
		/// </summary>
		string ProvidedMethod
		{
			get;
		}
		
		/// <summary>
		/// Trys to apply the implemented authentication method to the provided
		/// IAuthenticationInformation.
		/// </summary>
		/// <param name="info">The information to out with.</param>
		/// <returns></returns>
		bool Authenticate(IAuthenticationInformation info);
	}
}
