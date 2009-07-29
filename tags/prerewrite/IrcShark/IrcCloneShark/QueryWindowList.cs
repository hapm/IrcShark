using System;
using System.Collections.Generic;
using System.Text;
using IrcSharp;

namespace IrcCloneShark
{
    public class QueryWindowList : List<QueryWindow>
    {
        public QueryWindow this[UserInfo index]
        {
            get
            {
                foreach (QueryWindow win in this)
                {
                    if (win.BoundedUser == index) return win;
                }
                throw new IndexOutOfRangeException("There is no query for the given UserInfo");
            }
        }

        public QueryWindow this[String index]
        {
            get
            {
                foreach (QueryWindow win in this)
                {
                    if (win.BoundedUser.NickName == index) return win;
                }
                throw new IndexOutOfRangeException("There is no query for the given Nickname");
            }
        }

        public bool HasOpenQuery(UserInfo info)
        {
            foreach (QueryWindow win in this)
            {
                if (win.BoundedUser.Equals(info)) return true;
            }
            return false;
        }

        public bool HasOpenQuery(String info)
        {
            foreach (QueryWindow win in this)
            {
                if (win.BoundedUser.NickName == info) return true;
            }
            return false;
        }
    }
}
