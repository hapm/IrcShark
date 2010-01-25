using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IrcCloneShark
{
    public partial class DebugWindow : IrcCloneShark.BaseWindow
    {
        public DebugWindow()
        {
            InitializeComponent();
        }

        public DebugWindow(GUIIrcConnection baseCon) : base(baseCon)
        {
            InitializeComponent();
            AssociatedConnection.BaseConnection.LineReceived += new IrcSharp.LineReceivedEventHandler(Client_LineReceived);
            MdiParent = baseCon.MainForm;
            Disposed += new EventHandler(DebugWindow_Disposed);
        }

        void DebugWindow_Disposed(Object sender, EventArgs e)
        {
            AssociatedConnection.BaseConnection.LineReceived -= new IrcSharp.LineReceivedEventHandler(Client_LineReceived);
        }

        void Client_LineReceived(Object sender, IrcSharp.LineReceivedEventArgs e)
        {
            AddLine(e.BaseLine.ToString());
        }

        private void DebugWindow_Load(object sender, EventArgs e)
        {

        }
    }
}
