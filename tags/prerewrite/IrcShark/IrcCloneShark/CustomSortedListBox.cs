using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace IrcCloneShark
{
    public class CustomSortedListBox : ListBox
    {
        private Comparer<Object> SortComparerValue;

        public CustomSortedListBox()
            : base()
        {
            SortComparerValue = Comparer<Object>.Default;
        }

        protected override void Sort()
        {
            int i, j, k, h;
            Object temp;
            // Using shellsort here, not sure if it works ...
            int[] cols = {1391376, 463792, 198768, 86961, 33936, 13776, 4592,
                          1968, 861, 336, 112, 48, 21, 7, 3, 1};
            for (k = 0; k < 16; k++)
            {
                h = cols[k];
                for (i = h; i < Items.Count; i++)
                {
                    temp = Items[i];
                    j = i;
                    while (j >= h && SortComparer.Compare(Items[j - h], temp) > 0)
                    {
                        Items[j] = Items[j - h];
                        j = j - h;
                    }
                    Items[j] = temp;
                }
            }
        }

        public new void RefreshItems()
        {
            base.RefreshItems();
            if (Sorted) Sort();
        }

        public Comparer<Object> SortComparer
        {
            get { return SortComparerValue; }
            set { SortComparerValue = value; }
        }
    }
}
