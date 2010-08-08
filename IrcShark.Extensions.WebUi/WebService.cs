/*
 * Erstellt mit SharpDevelop.
 * Benutzer: Hendrik
 * Datum: 01.08.2010
 * Zeit: 23:22
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
using System;
using System.Reflection;

using Kayak.Framework;

namespace IrcShark.Extensions.WebUi
{
	/// <summary>
	/// Description of WebService.
	/// </summary>
	public class WebService : KayakService
	{
		public string eintrag(string wert1, string wert2)
		{
			string result = "<tr><td>" + wert1 + "</td><td>" + wert2 + "</td></tr>";
			return result;
		}
		
		 [Path("/")]
		 public void Root()
		 {
		 	string anfang =
		 	"<table border=\"1\">\n" +
			"  <tr>\n" +
			"    <th>Name</th>\n" +
			"    <th>Version</th>\n" +
			"  </tr>";
		 	
		 	Response.WriteLine(anfang);
		 	
		 	foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies()) 
            {
                AssemblyName name = asm.GetName();
                Response.WriteLine(eintrag(name.Name, name.Version.ToString()));
            }
			
		 	Response.WriteLine("</table>");
		 }
	}
}
