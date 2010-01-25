namespace IrcShark
{
    partial class StatusPanel
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
            this.ConnectionMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ConnectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DisconnectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NetworkMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewConnectionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RemoveConnectionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ConnectionListView = new System.Windows.Forms.ListView();
            this.ID = new System.Windows.Forms.ColumnHeader();
            this.Network = new System.Windows.Forms.ColumnHeader();
            this.Status = new System.Windows.Forms.ColumnHeader();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.logBox = new System.Windows.Forms.RichTextBox();
            this.channelList = new System.Windows.Forms.ListBox();
            this.ConnectionMenu.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ConnectionMenu
            // 
            this.ConnectionMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ConnectMenuItem,
            this.DisconnectMenuItem,
            this.NetworkMenuItem,
            this.NewConnectionMenuItem,
            this.RemoveConnectionMenuItem});
            this.ConnectionMenu.Name = "ConnectionMenu";
            this.ConnectionMenu.Size = new System.Drawing.Size(204, 124);
            this.ConnectionMenu.Text = "Connections";
            this.ConnectionMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ConnectionMenu_Opening);
            // 
            // ConnectMenuItem
            // 
            this.ConnectMenuItem.Name = "ConnectMenuItem";
            this.ConnectMenuItem.Size = new System.Drawing.Size(203, 24);
            this.ConnectMenuItem.Text = "Connect";
            this.ConnectMenuItem.Click += new System.EventHandler(this.ConnectMenuItem_Click);
            // 
            // DisconnectMenuItem
            // 
            this.DisconnectMenuItem.Name = "DisconnectMenuItem";
            this.DisconnectMenuItem.Size = new System.Drawing.Size(203, 24);
            this.DisconnectMenuItem.Text = "Disconnect";
            this.DisconnectMenuItem.Click += new System.EventHandler(this.DisconnectMenuItem_Click);
            // 
            // NetworkMenuItem
            // 
            this.NetworkMenuItem.Name = "NetworkMenuItem";
            this.NetworkMenuItem.Size = new System.Drawing.Size(203, 24);
            this.NetworkMenuItem.Text = "Network";
            // 
            // NewConnectionMenuItem
            // 
            this.NewConnectionMenuItem.Name = "NewConnectionMenuItem";
            this.NewConnectionMenuItem.Size = new System.Drawing.Size(203, 24);
            this.NewConnectionMenuItem.Text = "Add new connection";
            this.NewConnectionMenuItem.Click += new System.EventHandler(this.NewConnectionMenuItem_Click);
            // 
            // RemoveConnectionMenuItem
            // 
            this.RemoveConnectionMenuItem.Name = "RemoveConnectionMenuItem";
            this.RemoveConnectionMenuItem.Size = new System.Drawing.Size(203, 24);
            this.RemoveConnectionMenuItem.Text = "Remove connection";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ConnectionListView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1628, 765);
            this.splitContainer1.SplitterDistance = 225;
            this.splitContainer1.TabIndex = 1;
            // 
            // ConnectionListView
            // 
            this.ConnectionListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ID,
            this.Network,
            this.Status});
            this.ConnectionListView.ContextMenuStrip = this.ConnectionMenu;
            this.ConnectionListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConnectionListView.FullRowSelect = true;
            this.ConnectionListView.GridLines = true;
            this.ConnectionListView.HideSelection = false;
            this.ConnectionListView.Location = new System.Drawing.Point(0, 0);
            this.ConnectionListView.Margin = new System.Windows.Forms.Padding(2);
            this.ConnectionListView.MultiSelect = false;
            this.ConnectionListView.Name = "ConnectionListView";
            this.ConnectionListView.Size = new System.Drawing.Size(1628, 225);
            this.ConnectionListView.TabIndex = 1;
            this.ConnectionListView.UseCompatibleStateImageBehavior = false;
            this.ConnectionListView.View = System.Windows.Forms.View.Details;
            this.ConnectionListView.SelectedIndexChanged += new System.EventHandler(this.ConnectionListView_SelectedIndexChanged);
            // 
            // ID
            // 
            this.ID.Text = "ID";
            this.ID.Width = 40;
            // 
            // Network
            // 
            this.Network.Text = "Network";
            this.Network.Width = 229;
            // 
            // Status
            // 
            this.Status.Text = "Status";
            this.Status.Width = 263;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.logBox);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.channelList);
            this.splitContainer2.Size = new System.Drawing.Size(1628, 536);
            this.splitContainer2.SplitterDistance = 1449;
            this.splitContainer2.TabIndex = 0;
            // 
            // logBox
            // 
            this.logBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.logBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logBox.Location = new System.Drawing.Point(0, 0);
            this.logBox.Name = "logBox";
            this.logBox.ReadOnly = true;
            this.logBox.Size = new System.Drawing.Size(1449, 536);
            this.logBox.TabIndex = 0;
            this.logBox.Text = "";
            this.logBox.WordWrap = false;
            // 
            // channelList
            // 
            this.channelList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.channelList.FormattingEnabled = true;
            this.channelList.IntegralHeight = false;
            this.channelList.Location = new System.Drawing.Point(0, 0);
            this.channelList.Name = "channelList";
            this.channelList.Size = new System.Drawing.Size(175, 536);
            this.channelList.TabIndex = 0;
            // 
            // StatusPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.splitContainer1);
            this.Name = "StatusPanel";
            this.Size = new System.Drawing.Size(1628, 765);
            this.ConnectionMenu.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip ConnectionMenu;
        private System.Windows.Forms.ToolStripMenuItem ConnectMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NetworkMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DisconnectMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NewConnectionMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RemoveConnectionMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView ConnectionListView;
        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader Network;
        private System.Windows.Forms.ColumnHeader Status;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.RichTextBox logBox;
        private System.Windows.Forms.ListBox channelList;
    }
}
