/*
 * Erstellt mit SharpDevelop.
 * Benutzer: markus
 * Datum: 11.10.2009
 * Zeit: 21:39
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
using System;

namespace IrcShark.Extensions.Chatting
{
	/// <summary>
	/// Description of INetwork.
	/// </summary>
	public interface INetwork
	{
		string Name { get; }
		
		IServer AddServer(string name, string address);
	}
}
