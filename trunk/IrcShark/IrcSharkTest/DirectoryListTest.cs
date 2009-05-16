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
//

using System;
using System.Collections.Generic;
using IrcShark;
using NUnit.Framework;

namespace IrcSharkTest
{
	[TestFixture()]
	public class DirectoryListTest {
		private DirectoryList list;
		private List<string> dirs1;
		private List<string> dirs2;
		
		[SetUp()]
		public void SetUp() {
			dirs1 = new List<string>();
			dirs1.Add("test");
			dirs2 = new List<string>();
			dirs2.Add("test1");
			dirs2.Add("foo10");
			dirs2.Add("bar8");
			list = new DirectoryList(dirs1);
		}
		
		[Test()]
		public void Constructor() {
			Assert.IsNotNull(list);
			try {
				list = new DirectoryList(new List<string>());
				Assert.Fail("DirectoryList without an entry is not allowed");
			}
			catch (ArgumentException) {
			}
		}
		
		[Test()]
		public void Default() {
			Assert.AreEqual("test", list.Default);
			list = new DirectoryList(dirs2);
			Assert.AreEqual("test1", list.Default);
			dirs1.Add("blubb");
			Assert.AreEqual("test1", list.Default);
		}
		
		[Test()]
		public void ThisIndex() {
			Assert.AreEqual("test", list[0]);
			list = new DirectoryList(dirs2);
			Assert.AreEqual("test1", list[0]);
			Assert.AreEqual("foo10", list[1]);
			Assert.AreEqual("bar8", list[2]);
		}
		
		[Test()]
		public void Count() {
			Assert.AreEqual(1, list.Count);
			list = new DirectoryList(dirs2);
			Assert.AreEqual(3, list.Count);
			dirs2.Add("foobar");
			Assert.AreEqual(4, list.Count);			
		}
		
		[Test()]
		public void IsReadOnly() {
			Assert.IsTrue(list.IsReadOnly);
		}
	
		[Test()]
		public void Add()
		{
			try {
				list.Add("bla");
				Assert.Fail("Add method should not be supported");
			}
			catch (NotSupportedException) {}
		}
		
		[Test()]
		public void Contains()
		{
			Assert.IsTrue(list.Contains("test"));
			Assert.IsFalse(list.Contains("foo10"));
			list = new DirectoryList(dirs2);
			Assert.IsTrue(list.Contains("test1"));
			Assert.IsTrue(list.Contains("foo10"));
			Assert.IsFalse(list.Contains("ding"));
		}
		
		[Test()]	
		public void Clear()
		{
			try {
				list.Clear();
				Assert.Fail("Clear method should not be supported");
			}
			catch (NotSupportedException) {}
		}

		[Test()]
		public void Remove()
		{
			try {
				list.Remove("bla");
				Assert.Fail("Remove method should not be supported");
			}
			catch (NotSupportedException) {}
		}
			
		public void CopyTo ()
		{
			string[] result = new string[1];
			list.CopyTo(result, 0);
			Assert.AreEqual("test", result[0]);
			try {
				list.CopyTo(result, 1);
				Assert.Fail("Should not copy over the size of the array");
			}
			catch (IndexOutOfRangeException) {}
			result = new string[6];
			list.CopyTo(result, 2);
			Assert.IsNull(result[0]);
			Assert.IsNull(result[1]);
			Assert.AreEqual("test", result[2]);
			list = new DirectoryList(dirs2);
			list.CopyTo(result, 0);
			Assert.AreEqual("test1", result[0]);
			Assert.AreEqual("foo10", result[1]);
			Assert.AreEqual("bar8", result[2]);
			Assert.IsNull(result[3]);
			Assert.IsNull(result[4]);
			Assert.IsNull(result[5]);
			list.CopyTo(result, 2);
			Assert.AreEqual("test1", result[0]);
			Assert.AreEqual("foo10", result[1]);
			Assert.AreEqual("test1", result[2]);
			Assert.AreEqual("foo10", result[3]);
			Assert.AreEqual("bar8", result[4]);
			Assert.IsNull(result[5]);
		}
	}
}