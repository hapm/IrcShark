using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace IrcCloneShark
{
    public class WindowSwitchToolStripButton : ToolStripButton
    {
        private BaseWindow WindowValue;

        public WindowSwitchToolStripButton()
        {
            InitializeComponent();
        }

        public WindowSwitchToolStripButton(BaseWindow associatedWindow)
        {
            InitializeComponent();
            CheckOnClick = true;
            WindowValue = associatedWindow;
            Window.Activated += new EventHandler(Window_Activated);
            Window.Deactivate += new EventHandler(Window_Deactivate);
            Window.TextChanged += new EventHandler(Window_TextChanged);
            Window.Disposed += new EventHandler(Window_Disposed);
            Text = Window.Text.Split(" ".ToCharArray())[0];
        }

        void Window_TextChanged(object sender, EventArgs e)
        {
            Text = Window.Text.Split(" ".ToCharArray())[0];
        }

        void Window_Disposed(object sender, EventArgs e)
        {
            // the window watched by this button was disposed, doing the same for the button
            Dispose();
        }

        void Window_Deactivate(object sender, EventArgs e)
        {
            Checked = false;
        }

        void Window_Activated(object sender, EventArgs e)
        {
            Checked = true;
        }

        public BaseWindow Window
        {
            get { return WindowValue; }
        }

        private void InitializeComponent()
        {
            // 
            // WindowSwitchToolStripButton
            // 
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.WindowSwitchToolStripButton_MouseUp);

        }

        private void WindowSwitchToolStripButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (Window.MdiParent == null)
            {
                Checked = false;
                return;
            }
            if (!Checked) Checked = Window.MdiParent.ActiveMdiChild == Window;
            else if (Checked && Window.MdiParent.ActiveMdiChild != Window)
            {
                Window.MdiParent.SuspendLayout();
                Window.WindowState = FormWindowState.Maximized;
                Window.Activate();
                Window.MdiParent.ResumeLayout();
            }
        }
    }
}
