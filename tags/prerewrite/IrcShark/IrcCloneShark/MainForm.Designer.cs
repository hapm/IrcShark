namespace IrcCloneShark
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.FileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ConnectItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DisconnectItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.SelectServerItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewConnectionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DebugMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.HaltMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugRawToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.IRCSharkGUItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SwitchBarValue = new System.Windows.Forms.ToolStripPanel();
            this.hilfeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.überIRCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.AccessibleDescription = null;
            this.MainMenu.AccessibleName = null;
            resources.ApplyResources(this.MainMenu, "MainMenu");
            this.MainMenu.BackgroundImage = null;
            this.MainMenu.Font = null;
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenu,
            this.DebugMenu,
            this.hilfeToolStripMenuItem});
            this.MainMenu.Name = "MainMenu";
            // 
            // FileMenu
            // 
            this.FileMenu.AccessibleDescription = null;
            this.FileMenu.AccessibleName = null;
            resources.ApplyResources(this.FileMenu, "FileMenu");
            this.FileMenu.BackgroundImage = null;
            this.FileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ConnectItem,
            this.DisconnectItem,
            this.toolStripMenuItem1,
            this.SelectServerItem,
            this.NewConnectionMenuItem,
            this.toolStripMenuItem2,
            this.exitToolStripMenuItem});
            this.FileMenu.Name = "FileMenu";
            this.FileMenu.ShortcutKeyDisplayString = null;
            this.FileMenu.DropDownOpening += new System.EventHandler(this.FileMenu_DropDownOpening);
            // 
            // ConnectItem
            // 
            this.ConnectItem.AccessibleDescription = null;
            this.ConnectItem.AccessibleName = null;
            resources.ApplyResources(this.ConnectItem, "ConnectItem");
            this.ConnectItem.BackgroundImage = null;
            this.ConnectItem.Name = "ConnectItem";
            this.ConnectItem.ShortcutKeyDisplayString = null;
            this.ConnectItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
            // 
            // DisconnectItem
            // 
            this.DisconnectItem.AccessibleDescription = null;
            this.DisconnectItem.AccessibleName = null;
            resources.ApplyResources(this.DisconnectItem, "DisconnectItem");
            this.DisconnectItem.BackgroundImage = null;
            this.DisconnectItem.Name = "DisconnectItem";
            this.DisconnectItem.ShortcutKeyDisplayString = null;
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.AccessibleDescription = null;
            this.toolStripMenuItem1.AccessibleName = null;
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            // 
            // SelectServerItem
            // 
            this.SelectServerItem.AccessibleDescription = null;
            this.SelectServerItem.AccessibleName = null;
            resources.ApplyResources(this.SelectServerItem, "SelectServerItem");
            this.SelectServerItem.BackgroundImage = null;
            this.SelectServerItem.Name = "SelectServerItem";
            this.SelectServerItem.ShortcutKeyDisplayString = null;
            this.SelectServerItem.Click += new System.EventHandler(this.SelectServerItem_Click);
            // 
            // NewConnectionMenuItem
            // 
            this.NewConnectionMenuItem.AccessibleDescription = null;
            this.NewConnectionMenuItem.AccessibleName = null;
            resources.ApplyResources(this.NewConnectionMenuItem, "NewConnectionMenuItem");
            this.NewConnectionMenuItem.BackgroundImage = null;
            this.NewConnectionMenuItem.Name = "NewConnectionMenuItem";
            this.NewConnectionMenuItem.ShortcutKeyDisplayString = null;
            this.NewConnectionMenuItem.Click += new System.EventHandler(this.NewConnectionMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.AccessibleDescription = null;
            this.toolStripMenuItem2.AccessibleName = null;
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.AccessibleDescription = null;
            this.exitToolStripMenuItem.AccessibleName = null;
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.BackgroundImage = null;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeyDisplayString = null;
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // DebugMenu
            // 
            this.DebugMenu.AccessibleDescription = null;
            this.DebugMenu.AccessibleName = null;
            resources.ApplyResources(this.DebugMenu, "DebugMenu");
            this.DebugMenu.BackgroundImage = null;
            this.DebugMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HaltMenuItem,
            this.debugRawToolStripMenuItem,
            this.IRCSharkGUItem});
            this.DebugMenu.Name = "DebugMenu";
            this.DebugMenu.ShortcutKeyDisplayString = null;
            this.DebugMenu.DropDownOpening += new System.EventHandler(this.DebugMenu_DropDownOpening);
            // 
            // HaltMenuItem
            // 
            this.HaltMenuItem.AccessibleDescription = null;
            this.HaltMenuItem.AccessibleName = null;
            resources.ApplyResources(this.HaltMenuItem, "HaltMenuItem");
            this.HaltMenuItem.BackgroundImage = null;
            this.HaltMenuItem.Name = "HaltMenuItem";
            this.HaltMenuItem.ShortcutKeyDisplayString = null;
            this.HaltMenuItem.Click += new System.EventHandler(this.HaltMenuItem_Click);
            // 
            // debugRawToolStripMenuItem
            // 
            this.debugRawToolStripMenuItem.AccessibleDescription = null;
            this.debugRawToolStripMenuItem.AccessibleName = null;
            resources.ApplyResources(this.debugRawToolStripMenuItem, "debugRawToolStripMenuItem");
            this.debugRawToolStripMenuItem.BackgroundImage = null;
            this.debugRawToolStripMenuItem.CheckOnClick = true;
            this.debugRawToolStripMenuItem.Name = "debugRawToolStripMenuItem";
            this.debugRawToolStripMenuItem.ShortcutKeyDisplayString = null;
            this.debugRawToolStripMenuItem.Click += new System.EventHandler(this.debugRawToolStripMenuItem_Click);
            // 
            // IRCSharkGUItem
            // 
            this.IRCSharkGUItem.AccessibleDescription = null;
            this.IRCSharkGUItem.AccessibleName = null;
            resources.ApplyResources(this.IRCSharkGUItem, "IRCSharkGUItem");
            this.IRCSharkGUItem.BackgroundImage = null;
            this.IRCSharkGUItem.CheckOnClick = true;
            this.IRCSharkGUItem.Name = "IRCSharkGUItem";
            this.IRCSharkGUItem.ShortcutKeyDisplayString = null;
            this.IRCSharkGUItem.CheckedChanged += new System.EventHandler(this.IRCSharkGUItem_CheckedChanged);
            // 
            // SwitchBarValue
            // 
            this.SwitchBarValue.AccessibleDescription = null;
            this.SwitchBarValue.AccessibleName = null;
            resources.ApplyResources(this.SwitchBarValue, "SwitchBarValue");
            this.SwitchBarValue.BackgroundImage = null;
            this.SwitchBarValue.Font = null;
            this.SwitchBarValue.Name = "SwitchBarValue";
            this.SwitchBarValue.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.SwitchBarValue.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            // 
            // hilfeToolStripMenuItem
            // 
            this.hilfeToolStripMenuItem.AccessibleDescription = null;
            this.hilfeToolStripMenuItem.AccessibleName = null;
            resources.ApplyResources(this.hilfeToolStripMenuItem, "hilfeToolStripMenuItem");
            this.hilfeToolStripMenuItem.BackgroundImage = null;
            this.hilfeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.überIRCToolStripMenuItem});
            this.hilfeToolStripMenuItem.Name = "hilfeToolStripMenuItem";
            this.hilfeToolStripMenuItem.ShortcutKeyDisplayString = null;
            // 
            // überIRCToolStripMenuItem
            // 
            this.überIRCToolStripMenuItem.AccessibleDescription = null;
            this.überIRCToolStripMenuItem.AccessibleName = null;
            resources.ApplyResources(this.überIRCToolStripMenuItem, "überIRCToolStripMenuItem");
            this.überIRCToolStripMenuItem.BackgroundImage = null;
            this.überIRCToolStripMenuItem.Name = "überIRCToolStripMenuItem";
            this.überIRCToolStripMenuItem.ShortcutKeyDisplayString = null;
            this.überIRCToolStripMenuItem.Click += new System.EventHandler(this.überIRCToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.SwitchBarValue);
            this.Controls.Add(this.MainMenu);
            this.Font = null;
            this.Icon = null;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.MainMenu;
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem FileMenu;
        private System.Windows.Forms.ToolStripMenuItem DisconnectItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem SelectServerItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DebugMenu;
        private System.Windows.Forms.ToolStripMenuItem HaltMenuItem;
        private System.Windows.Forms.ToolStripPanel SwitchBarValue;
        private System.Windows.Forms.ToolStripMenuItem debugRawToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem IRCSharkGUItem;
        private System.Windows.Forms.ToolStripMenuItem ConnectItem;
        private System.Windows.Forms.ToolStripMenuItem NewConnectionMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hilfeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem überIRCToolStripMenuItem;
    }
}