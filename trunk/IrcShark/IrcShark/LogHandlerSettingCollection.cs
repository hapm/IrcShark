/*
 * Erstellt mit SharpDevelop.
 * Benutzer: markus
 * Datum: 11.09.2009
 * Zeit: 20:48
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
using System;
using System.Collections;
using System.Collections.Generic;

namespace IrcShark
{
	/// <summary>
	/// Description of LogHandlerSettingCollection.
	/// </summary>
	public class LogHandlerSettingCollection : ICollection<LogHandlerSetting>
	{
		/// <summary>
		/// holds the list of settings
		/// </summary>
		private List<LogHandlerSetting> settings;
		
		/// <summary>
		/// Creates a new instance of the LogHandlerSettingCollection
		/// </summary>
		public LogHandlerSettingCollection()
		{
			settings = new List<LogHandlerSetting>();
		}
		
		/// <summary>
		/// Gets the <see cref="LogHandlerSetting" /> at the given index
		/// </summary>
		public LogHandlerSetting this[int index]
		{
			get { return settings[index]; }
		}
		
		/// <summary>
		/// Gets the count of <see cref="LogHandlerSetting" />'s in this collection
		/// </summary>
		public int Count 
		{
			get { return settings.Count; }
		}
		
		/// <summary>
		/// Gets false because LogHandlerSettingCollection is not read only
		/// </summary>
		public bool IsReadOnly 
		{
			get { return false; }
		}
		
		/// <summary>
		/// Adds a <see cref="LogHandlerSetting" /> to the collection
		/// </summary>
		/// <param name="item"></param>
		public void Add(LogHandlerSetting item)
		{
			settings.Add(item);
		}
		
		/// <summary>
		/// Removes all <see cref="LogHandlerSetting" />'s from this collection
		/// </summary>
		public void Clear()
		{
			settings.Clear();
		}
		
		/// <summary>
		/// Checks if the collection contains the given <see cref="LogHandlerSetting" />
		/// </summary>
		/// <param name="item">the item to check</param>
		/// <returns>true if the collection contains the item, false otherwise</returns>
		public bool Contains(LogHandlerSetting item)
		{
			return settings.Contains(item);
		}
		
		/// <summary>
		/// Copys all items from the collection to an item
		/// </summary>
		/// <param name="array">the array to copy to</param>
		/// <param name="arrayIndex">the index wher to copy to</param>
		public void CopyTo(LogHandlerSetting[] array, int arrayIndex)
		{
			settings.CopyTo(array, arrayIndex);
		}
		
		/// <summary>
		/// Returns an array of all <see cref="LogHandlerSettings" /> objects in this collection
		/// </summary>
		/// <returns>An array with all items of the collection</returns>
		public LogHandlerSetting[] ToArray()
		{
			return settings.ToArray();
		}
		
		/// <summary>
		/// Removes an item from the collection
		/// </summary>
		/// <param name="item">removes the item from the collection</param>
		/// <returns>returns true if the item was removed, false otherwise</returns>
		public bool Remove(LogHandlerSetting item)
		{
			return settings.Remove(item);
		}
		
		public IEnumerator<LogHandlerSetting> GetEnumerator()
		{
			return (settings as IEnumerable<LogHandlerSetting>).GetEnumerator();
		}
		
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return (settings as IEnumerable).GetEnumerator();
		}
	}
}
