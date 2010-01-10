/*
 * Erstellt mit SharpDevelop.
 * Benutzer: markus
 * Datum: 16.10.2009
 * Zeit: 13:10
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
using System;
using System.Security;

namespace IrcShark.Policy
{
	/// <summary>
	/// Description of ExtensionInitialisationException.
	/// </summary>
	public class ExtensionInitialisationPermission : IPermission
	{
		public ExtensionInitialisationPermission()
		{
		}
		
		public IPermission Intersect(IPermission target)
		{
			throw new NotImplementedException();
		}
		
		public IPermission Union(IPermission target)
		{
			throw new NotImplementedException();
		}
		
		public bool IsSubsetOf(IPermission target)
		{
			throw new NotImplementedException();
		}
		
		public void Demand()
		{
			throw new NotImplementedException();
		}
		
		public IPermission Copy()
		{
			throw new NotImplementedException();
		}
		
		public SecurityElement ToXml()
		{
			throw new NotImplementedException();
		}
		
		public void FromXml(SecurityElement e)
		{
			throw new NotImplementedException();
		}
	}
}
