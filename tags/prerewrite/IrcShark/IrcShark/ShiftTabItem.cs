using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace IrcShark
{
    public partial class ShiftTabItem : Component
    {
        public ShiftTabItem()
        {
            InitializeComponent();
        }

        public ShiftTabItem(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
