/*
 * Erstellt mit SharpDevelop.
 * Benutzer: markus
 * Datum: 11.10.2009
 * Zeit: 21:39
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
namespace IrcShark.Chatting
{
    using System;
    
	/// <summary>
	/// Defines the structure of a chat network.
	/// </summary>
	public interface INetwork : System.Collections.Generic.IEnumerable<IServer>
	{
		
		string Name { get; set; }
		
		IServer AddServer(string name, string address);
		bool RemoveServer(string name);
		void RemoveServer(int index);
		IServer this[int index] { get; }
		IServer this[string name] { get; }
	}
}
