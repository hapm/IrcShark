/*
 * Erstellt mit SharpDevelop.
 * Benutzer: markus
 * Datum: 11.09.2009
 * Zeit: 20:48
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
namespace IrcShark
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Description of LogHandlerSettingCollection.
    /// </summary>
    public class LogHandlerSettingCollection : ICollection<LogHandlerSetting>
    {
        /// <summary>
        /// Holds the list of settings.
        /// </summary>
        private Dictionary<string, LogHandlerSetting> settings;
        
        /// <summary>
        /// Initializes a new instance of the LogHandlerSettingCollection class.
        /// </summary>
        public LogHandlerSettingCollection()
        {
            settings = new Dictionary<string, LogHandlerSetting>();
        }
        
        /// <summary>
        /// Gets the count of <see cref="LogHandlerSetting" />'s in this collection.
        /// </summary>
        public int Count 
        {
            get { return settings.Count; }
        }
        
        /// <summary>
        /// Gets a value indicating whether the collection is readonly or not.
        /// </summary>
        /// <value>False because LogHandlerSettingCollection is not read only.</value>
        public bool IsReadOnly 
        {
            get { return false; }
        }
        
        /// <summary>
        /// Gets the <see cref="LogHandlerSetting" /> at the given index.
        /// </summary>
        public LogHandlerSetting this[int index]
        {
            get 
            {
                int i = 0;
                if (settings.Count <= index || index < 0)
                    throw new IndexOutOfRangeException("Index out of range");
                foreach (LogHandlerSetting current in settings.Values)
                {
                    if (i == index)
                        return current;
                    i++;
                }
                return null; // Should never happen as this was fetched by the IndexOutOfRangeException
            }
        }
        
        /// <summary>
        /// Gets the <see cref="LogHandlerSetting" /> with the given name.
        /// </summary>
        public LogHandlerSetting this[string name]
        {
            get 
            { 
            	try { return settings[name]; }
            	catch (KeyNotFoundException ex)
            	{
            		throw new IndexOutOfRangeException(name + " wasn't found in the LogHnadlerSettings", ex);
            	}
            }
        }
        
        /// <summary>
        /// Adds a <see cref="LogHandlerSetting" /> to the collection.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void Add(LogHandlerSetting item)
        {
            settings.Add(item.HandlerName, item);
        }
        
        /// <summary>
        /// Removes all <see cref="LogHandlerSetting" />'s from this collection.
        /// </summary>
        public void Clear()
        {
            settings.Clear();
        }
        
        /// <summary>
        /// Checks if the collection contains the given <see cref="LogHandlerSetting" />.
        /// </summary>
        /// <param name="item">The item to check.</param>
        /// <returns>True if the collection contains the item, false otherwise.</returns>
        public bool Contains(LogHandlerSetting item)
        {
            return settings.ContainsValue(item);
        }
        
        /// <summary>
        /// Copys all items from the collection to an item.
        /// </summary>
        /// <param name="array">The array to copy to.</param>
        /// <param name="arrayIndex">The index wher to copy to.</param>
        public void CopyTo(LogHandlerSetting[] array, int arrayIndex)
        {
            settings.Values.CopyTo(array, arrayIndex);
        }
        
        /// <summary>
        /// Returns an array of all <see cref="LogHandlerSettings" /> objects in this collection.
        /// </summary>
        /// <returns>An array with all items of the collection.</returns>
        public LogHandlerSetting[] ToArray()
        {
            LogHandlerSetting[] array = new LogHandlerSetting[settings.Count];
            int i = 0;
            foreach (LogHandlerSetting current in settings.Values)
            {
                array[i] = current;
                i++;
            }
            return array;
        }
        
        /// <summary>
        /// Removes an item from the collection.
        /// </summary>
        /// <param name="name">Removes the item from the collection.</param>
        /// <returns>Returns true if the item was removed, false otherwise.</returns>
        public bool Remove(string name)
        {
            return settings.Remove(name);
        }
        
        /// <summary>
        /// Removes an item from the collection.
        /// </summary>
        /// <param name="item">Removes the item from the collection.</param>
        /// <returns>Returns true if the item was removed, false otherwise.</returns>
        public bool Remove(LogHandlerSetting item)
        {
            if (settings.ContainsValue(item))
                return settings.Remove(item.HandlerName);
            return false;
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
