/*
 * Erstellt mit SharpDevelop.
 * Benutzer: markus
 * Datum: 11.10.2009
 * Zeit: 21:50
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
using System;

namespace IrcShark.Chatting
{
	/// <summary>
	/// Defines the structur of a server address for chat protocols
	/// </summary>
	public interface IServer
	{
		/// <summary>
		/// Gets or sets the display name of a server
		/// </summary>
		string Name { get; set; }
		string Address { get; set; }
		INetwork Network { get; }
	}
}
