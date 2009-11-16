/*
 * Erstellt mit SharpDevelop.
 * Benutzer: markus
 * Datum: 11.10.2009
 * Zeit: 21:30
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
using System;

namespace IrcShark.Chatting
{
	/// <summary>
	/// Defines the minimum propertys and methods of a chat protocol
	/// </summary>
	public interface IProtocol
	{
		bool MultiNetwork { get; }
		bool MultiServer { get; }
		
		INetwork CreateNetwork(string name);
	}
}
