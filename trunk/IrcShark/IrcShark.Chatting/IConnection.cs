/*
 * Erstellt mit SharpDevelop.
 * Benutzer: markus
 * Datum: 11.10.2009
 * Zeit: 22:11
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
using System;

namespace IrcShark.Extensions.Chatting
{
	/// <summary>
	/// Description of IConnection.
	/// </summary>
	public interface IConnection
	{
		IServer Server { get; }
	}
}
