using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using IrcSharp;

namespace IrcCloneShark
{
    public partial class BaseWindow : Form
    {
        public delegate void InputEventHandler(BaseWindow sender, InputEventArgs args);

        public event InputEventHandler Input;

        private delegate void AddLineDelegate(String Line);
        private GUIIrcConnection AssociatedConnectionValue;
        private List<String> InputBuffer;
        private int CurrentBufferPos;

        public BaseWindow()
        {
            InitializeComponent();
        }

        public BaseWindow(GUIIrcConnection baseCon)
        {
            InitializeComponent();
            AssociatedConnectionValue = baseCon;
            MdiParent = AssociatedConnection.MainForm;
            InputBuffer = new List<String>();
        }

        [Category("Appearance"), Description("Shows an inputbox if enabled")]
        public bool HasInputBox
        {
            get { return InputBox.Visible; }
            set { InputBox.Visible = value; }
        }

        [Category("Appearance"), Description("Shows a side list if enabled")]
        public bool HasSideList
        {
            get { return !Splitter.Panel2Collapsed; }
            set { Splitter.Panel2Collapsed = !value; }
        }

        public GUIIrcConnection AssociatedConnection
        {
            get { return AssociatedConnectionValue; }
        }

        public new MainForm ParentForm
        {
            get
            {
                if (MdiParent is MainForm) return (MainForm)MdiParent;
                return null;
            }
        }

        public CustomSortedListBox SideList
        {
            get { return SideListValue; }
        }

        public void AddLine(String Line)
        {
            if (InvokeRequired) Invoke(new AddLineDelegate(AddLine), Line);
            else
            {
                OutputBox.Select(OutputBox.Text.Length, 0);
                OutputBox.SelectedText = Line + "\x0F" + Environment.NewLine;
                OutputBox.Select(OutputBox.Text.Length, 0);
                OutputBox.ScrollToCaret();
            }
        }

        private void InputBox_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    e.Handled = true;
                    break;
                case Keys.Up:
                    e.Handled = true;
                    break;
                case Keys.Down:
                    e.Handled = true;
                    break;
            }

        }

        private void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    {
                        if (InputBox.Text == "") return;
                        e.Handled = true;
                        InputBuffer.Add(InputBox.Text);
                        CurrentBufferPos = InputBuffer.Count;
                        if (Input != null) Input(this, new InputEventArgs(InputBox.Text));
                        InputBox.Text = "";
                    }
                    break;
                case Keys.Up:
                    {
                        e.Handled = true;
                        if (CurrentBufferPos == InputBuffer.Count && InputBox.Text != "") InputBuffer.Add(InputBox.Text);
                        if (CurrentBufferPos == 0) return;
                        CurrentBufferPos--;
                        InputBox.Text = InputBuffer[CurrentBufferPos];
                        InputBox.SelectionStart = InputBox.Text.Length;
                    }
                    break;
                case Keys.Down:
                    {
                        e.Handled = true;
                        if (CurrentBufferPos == InputBuffer.Count) return;
                        CurrentBufferPos++;
                        if (CurrentBufferPos == InputBuffer.Count) InputBox.Text = "";
                        else InputBox.Text = InputBuffer[CurrentBufferPos];
                        InputBox.SelectionStart = InputBox.Text.Length;
                    }
                    break;
            }
        }

        private void OutputBox_MouseUp(object sender, MouseEventArgs e)
        {
            Clipboard.SetText(OutputBox.SelectedRtf, TextDataFormat.Rtf);
            InputBox.Focus();
        }
    }
}
