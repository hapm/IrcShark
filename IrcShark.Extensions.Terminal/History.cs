// <copyright file="History.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the History class.</summary>

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
namespace IrcShark.Extensions.Terminal
{
    using System;

    /// <summary>
    /// The History saves a list of typed commands on the console and provides
    /// methods to switch through the saved commands.
    /// </summary>
    /// <remarks>
    /// This code bases on Miguel de Icaza's (miguel@novell.com) LineEditor from
    /// the mono project. It was changed to fit the needs of IrcShark and its 
    /// coding standards.
    /// </remarks>
    public class History
    {
        /// <summary>
        /// Saves the added commands.
        /// </summary>
		private string[] history;
		
		/// <summary>
		/// Saves current head and of the history.
		/// </summary>
		private int head;
		
		/// <summary>
		/// Saves current tail of the history.
		/// </summary>
		private int tail;
		
		/// <summary>
		/// Saves the cursor position of the currently shown command.
		/// </summary>
		private int cursor;
		
		/// <summary>
		/// Saves the number of all commands currently added to the history.
		/// </summary>
		private int count;
		
		/// <summary>
		/// Initializes a new instance of the History class.
		/// </summary>
		/// <param name="size">The number of commands, this history instance can save.</param>
		/// <exception cref="ArgumentException">Thrown if size is smaller than 1.</exception>
		public History(int size)
		{
			if (size < 1)
				throw new ArgumentException ("size");
			
			history = new string [size];
			head = tail = cursor = 0;
		}
		
		/// <summary>
		/// Initializes a new instance of the History class.
		/// </summary>
		/// <param name="size">The number of commands, this history instance can save.</param>
		/// <param name="savedHistory">An array of saved commands, that should be loaded to the History on initialisation.</param>
		/// <exception cref="ArgumentException">Thrown if size is smaller than 1 or than the length of the savedHistory array.</exception>
		public History(int size, string[] savedHistory) {
			if (size < savedHistory.Length || size < 1)
				throw new ArgumentException ("size");
			
			history = new string[size];
			savedHistory.CopyTo(history, 0);
			
			tail = 0;
			count = head = cursor = savedHistory.Length;
			cursor--;
		}

		/// <summary>
		/// Closes the history file.
		/// </summary>
		[Obsolete("Used to write the history file in old version. This is now handled outside of the History class.")]
		public void Close()
		{
		}
		
		/// <summary>
		/// Appends a value to the history.
		/// </summary>
		/// <param name="s">The value to append.</param>
		public void Append(string s)
		{
			history [head] = s;
			head = (head+1) % history.Length;
			if (head == tail)
				tail = (tail+1 % history.Length);
			if (count != history.Length)
				count++;
		}

		/// <summary>
		/// Updates the current cursor location with the string,
		/// to support editing of history items.
		/// </summary>
		/// <remarks>
		/// For the current line to participate, an Append must be done before.
		/// </remarks>
		/// <param name="s">The new value for the history item at the current location.</param>
		public void Update(string s)
		{
			history [cursor] = s;
		}

		/// <summary>
		/// Removes the last item that was added to the History.
		/// </summary>
		public void RemoveLast ()
		{
			head = head-1;
			if (head < 0)
				head = history.Length-1;
		}
		
		///TODO: Not sure what this is needed for, should get a better summary.
		/// <summary>
		/// Overrides the lastly added history item.
		/// </summary>
		/// <param name="s">The new value to add.</param>
		public void Accept(string s)
		{
			int t = head-1;
			if (t < 0)
				t = history.Length-1;
			
			history[t] = s;
		}
		
		/// <summary>
		/// Checks if there is a previous item before the current item under the cursor.
		/// </summary>
		/// <returns>Its true, if there is a previous item, false otherwise.</returns>
		public bool PreviousAvailable ()
		{
			if (count == 0 || cursor == tail)
				return false;

			return true;
		}

		/// <summary>
		/// Checks if there is a next item after the current item under the cursor.
		/// </summary>
		/// <returns>Its true, if there is a next item, false otherwise.</returns>
		public bool NextAvailable ()
		{
			int next = (cursor + 1) % history.Length;
			if (count == 0 || next >= head)
				return false;

			return true;
		}
		
		/// <summary>
		/// Moves the cursor to the previous item if possible, and returns it.
		/// </summary>
		/// <returns>The previous item or null, if there is no previous item.</returns>
		public string Previous ()
		{
			if (!PreviousAvailable ())
				return null;

			cursor--;
			if (cursor < 0)
				cursor = history.Length - 1;

			return history [cursor];
		}

		/// <summary>
		/// Moves the cursor to the next item if possible, and returns it.
		/// </summary>
		/// <returns>The next item or null, if there is no next item.</returns>
		public string Next ()
		{
			if (!NextAvailable ())
				return null;

			cursor = (cursor + 1) % history.Length;
			return history [cursor];
		}

		/// <summary>
		/// Moves the cursor to the end of the history.
		/// </summary>
		public void CursorToEnd ()
		{
			if (head == tail)
				return;

			cursor = head;
		}
		
		/// <summary>
		/// Gets an array containing all remembered items in the history from oldest to newest
		/// </summary>
		public string[] HistoryDump {
		    get {
		        string[] result = new string[count];
		        for (int i = 0; i < count; i++) {
		            result[i] = history[(tail + i) % history.Length];
		        }
		        return result;
		    }
		}

		/// <summary>
		/// Searches a given term in the history backwards, from newest to oldest command.
		/// </summary>
		/// <param name="term">The term to search for.</param>
		/// <returns>
		/// The first item containing the given term, or null if no item with the given
		/// term was found.
		/// </returns>
		public string SearchBackward (string term)
		{
			for (int i = 1; i < count; i++){
				int slot = cursor-i;
				if (slot < 0)
					slot = history.Length-1;
				if (history [slot] != null && history [slot].IndexOf (term) != -1){
					cursor = slot;
					return history [slot];
				}

				// Will the next hit tail?
				slot--;
				if (slot < 0)
					slot = history.Length-1;
				if (slot == tail)
					break;
			}

			return null;
		}
    }
}
