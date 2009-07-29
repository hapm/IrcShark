using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IrcCloneShark
{
    public partial class WindowSwitchToolStrip : ToolStrip
    {
        private GUIIrcConnection ConnectionValue;
        private WindowSwitchToolStripButton StatusButtonValue;

        public WindowSwitchToolStrip()
        {
            InitializeComponent();
        }

        public WindowSwitchToolStrip(GUIIrcConnection con)
        {
            InitializeComponent();
            ConnectionValue = con;
            StatusButtonValue = new WindowSwitchToolStripButton(con.Status);
            Items.Add(StatusButtonValue);
            Connection.WindowOpened += new GUIIrcConnection.WindowOpenedEventHandler(Connection_WindowOpened);
        }

        void Connection_WindowOpened(GUIIrcConnection sender, WindowOpenedEventArgs args)
        {
            WindowSwitchToolStripButton newBtn = new WindowSwitchToolStripButton(args.OpenedWindow);
            Items.Add(newBtn);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        public GUIIrcConnection Connection
        {
            get { return ConnectionValue; }
        }
    }
}
