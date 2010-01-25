using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IrcCloneShark
{
    public partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();
            richTextBox1.LinkClicked += new LinkClickedEventHandler(Link_Clicked);
        }

        private void AboutBox_Load(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        protected void Link_Clicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }
    }
}
