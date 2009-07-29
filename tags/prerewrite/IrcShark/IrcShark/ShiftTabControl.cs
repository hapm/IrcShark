using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IrcShark
{
    public partial class ShiftTabControl : Control
    {
        private List<ShiftTabItem> ItemsValue;

        public ShiftTabControl()
        {
            InitializeComponent();
        }


        protected override void OnPaint(PaintEventArgs pe)
        {
            
            base.OnPaint(pe);
        }
    }
}
