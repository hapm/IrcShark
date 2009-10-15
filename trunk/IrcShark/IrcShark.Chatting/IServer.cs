/*
 * Erstellt mit SharpDevelop.
 * Benutzer: markus
 * Datum: 11.10.2009
 * Zeit: 21:50
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
using System;

namespace IrcShark.Extensions.Chatting
{
	/// <summary>
	/// Description of IServer.
	/// </summary>
	public interface IServer
	{
		string Name { get; }
		string Address { get; }
		INetwork Network { get; }
	}
}
