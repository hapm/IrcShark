/*
 * Erstellt mit SharpDevelop.
 * Benutzer: markus
 * Datum: 24.01.2011
 * Zeit: 17:44
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
namespace IrcShark.Extensions.Sessions
{
	using System;

	/// <summary>
	/// The SecurePasswordAuthentication implements an authentication with an encrypted password.
	/// </summary>
	public class SecurePasswordAuthentication : IAuthenticationProvider
	{
		public SecurePasswordAuthentication()
		{
		}
		
		public string ProvidedMethod 
		{
			get 
			{
				return "SecurePassword";
			}
		}
		
		public bool Authenticate(IAuthenticationInformation info)
		{
			throw new NotImplementedException();
		}
	}
}
