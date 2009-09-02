// $Id$
//
// Note:
//
// Copyright (C) 2009 IrcShark Team
// 
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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace IrcShark
{
	/// <summary>
	/// Allows read only access to a list of directories.
	/// </summary>
	public class DirectoryCollection : ICollection<string>
	{
		private List<string> directorys;
		
		/// <summary>
		/// Creates a new directorylist based on the string collection
		/// </summary>
		/// <param name="list">
		/// The string <see cref="T:System.Collections.Generic.ICollection`1"/> of directories
		/// </param>
		public DirectoryCollection(IEnumerable<String> list)
		{
			directorys = new List<string>(list);
		}
		
		/// <summary>
		/// Creates a new empty directorylist
		/// </summary>
		public DirectoryCollection()
		{
			directorys = new List<string>();
		}
		
		/// <value>
		/// Because this is a readonly list, this will always be true
		/// </value>
		public bool IsReadOnly
		{
			get { return false; }
		}
		
		/// <value>
		/// The directory used as the default, it is always the first list entry
		/// </value>
		public string Default
		{
			get { return directorys[0]; }
		}
		
		/// <value>
		/// gives back the directory at the given index
		/// </value>
		public string this[int index]
		{
			get { return directorys[index]; }
		}
		
		/// <summary>
		/// Add a new directory to the collection
		/// </summary>
		/// <param name="item">
		/// the directory to add
		/// </param>
		public void Add(string item)
		{
			directorys.Add(item);
		}
		
		/// <summary>
		/// inserts a directory at the given index
		/// </summary>
		/// <param name="index">
		/// the index to place the new directory to
		/// </param>
		/// <param name="item">
		/// the directory to insert
		/// </param>
		public void Insert(int index, string item)
		{
			 directorys.Insert(index, item);
		}
		
		/// <summary>
		/// Checks if a given directory is in this list
		/// </summary>
		/// <param name="item">
		/// the directory to check as a <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="System.Boolean"/> what is
		/// true if the directory is in the list
		/// false otherwise
		/// </returns>
		public bool Contains(string item)
		{
			return directorys.Contains(item);
		}
		
		/// <summary>
		/// clears sthe list of directories
		/// </summary>
		public void Clear()
		{
			directorys.Clear();
		}

		/// <value>
		/// the number of directories in this list
		/// </value>
		public int Count
		{
			get { return directorys.Count; }
		}

		/// <summary>
		/// removes a directory form the list
		/// </summary>
		/// <param name="item">
		/// the directory to delete <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// true, if the directory was found and deleted, false otherwise
		/// </returns>
		public bool Remove(string item)
		{
			return directorys.Remove(item);
		}
		
		/// <summary>
		/// removes the directory at the given position
		/// </summary>
		/// <param name="index">
		/// the position of the directory to remove 
		/// </param>
		public void RemoveAt(int index)
		{
			directorys.RemoveAt(index);
		}
		
		/// <summary>
		/// Copies all directorys of this list into an array
		/// </summary>
		/// <param name="array">
		/// the <see cref="System.String"/> array to copy the directories to
		/// </param>
		/// <param name="arrayIndex">
		/// the index where to write the first directory entry as an <see cref="System.Int32"/>
		/// </param>
		public void CopyTo(string[] array, int arrayIndex)
		{
			directorys.CopyTo(array, arrayIndex);
		}
			
		IEnumerator<string> IEnumerable<string>.GetEnumerator()
		{
			return directorys.GetEnumerator();
		}
		
		IEnumerator IEnumerable.GetEnumerator()
		{
			return (directorys as IEnumerable).GetEnumerator();
		}
	}
}
