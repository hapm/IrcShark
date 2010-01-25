namespace IrcCloneShark
{
    partial class BaseWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseWindow));
            this.InputBox = new System.Windows.Forms.TextBox();
            this.Splitter = new System.Windows.Forms.SplitContainer();
            this.OutputBox = new IrcCloneShark.mIRCCodeTextBox(this.components);
            this.SideListValue = new IrcCloneShark.CustomSortedListBox();
            this.Splitter.Panel1.SuspendLayout();
            this.Splitter.Panel2.SuspendLayout();
            this.Splitter.SuspendLayout();
            this.SuspendLayout();
            // 
            // InputBox
            // 
            this.InputBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.InputBox.Location = new System.Drawing.Point(0, 604);
            this.InputBox.Margin = new System.Windows.Forms.Padding(4);
            this.InputBox.Name = "InputBox";
            this.InputBox.Size = new System.Drawing.Size(1123, 22);
            this.InputBox.TabIndex = 1;
            this.InputBox.Visible = false;
            this.InputBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InputBox_KeyDown);
            this.InputBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.InputBox_KeyUp);
            // 
            // Splitter
            // 
            this.Splitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Splitter.Location = new System.Drawing.Point(0, 0);
            this.Splitter.Margin = new System.Windows.Forms.Padding(4);
            this.Splitter.Name = "Splitter";
            // 
            // Splitter.Panel1
            // 
            this.Splitter.Panel1.Controls.Add(this.OutputBox);
            // 
            // Splitter.Panel2
            // 
            this.Splitter.Panel2.Controls.Add(this.SideListValue);
            this.Splitter.Size = new System.Drawing.Size(1123, 604);
            this.Splitter.SplitterDistance = 915;
            this.Splitter.SplitterWidth = 5;
            this.Splitter.TabIndex = 3;
            // 
            // OutputBox
            // 
            this.OutputBox.BackColor = System.Drawing.SystemColors.Window;
            this.OutputBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.OutputBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OutputBox.Location = new System.Drawing.Point(0, 0);
            this.OutputBox.Margin = new System.Windows.Forms.Padding(4);
            this.OutputBox.Name = "OutputBox";
            this.OutputBox.ReadOnly = true;
            this.OutputBox.Size = new System.Drawing.Size(915, 604);
            this.OutputBox.TabIndex = 0;
            this.OutputBox.TabStop = false;
            this.OutputBox.Text = "";
            this.OutputBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OutputBox_MouseUp);
            // 
            // SideListValue
            // 
            this.SideListValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SideListValue.FormattingEnabled = true;
            this.SideListValue.IntegralHeight = false;
            this.SideListValue.ItemHeight = 16;
            this.SideListValue.Location = new System.Drawing.Point(0, 0);
            this.SideListValue.Name = "SideListValue";
            this.SideListValue.Size = new System.Drawing.Size(203, 604);
            this.SideListValue.SortComparer = ((System.Collections.Generic.Comparer<object>)(resources.GetObject("SideListValue.SortComparer")));
            this.SideListValue.TabIndex = 2;
            // 
            // BaseWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1123, 626);
            this.Controls.Add(this.Splitter);
            this.Controls.Add(this.InputBox);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "BaseWindow";
            this.Text = "BaseWindow";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Splitter.Panel1.ResumeLayout(false);
            this.Splitter.Panel2.ResumeLayout(false);
            this.Splitter.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox InputBox;
        private System.Windows.Forms.SplitContainer Splitter;
        private mIRCCodeTextBox OutputBox;
        private CustomSortedListBox SideListValue;
    }
}